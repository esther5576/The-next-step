using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedMetricsManager : MonoBehaviour
{    
	
	//////////////////////////////// singleton fields & methods ////////////////////////////////
	public static string gameObjectName = "RedMetricsManager";
	private static RedMetricsManager _instance;
	public static RedMetricsManager get ()
	{
		if (_instance == null) {
			_instance = GameObject.Find (gameObjectName).GetComponent<RedMetricsManager> ();
			if (null != _instance) {
				//RedMetricsManager object is not destroyed when game restarts
				DontDestroyOnLoad (_instance.gameObject);
				_instance.initializeIfNecessary ();
			} else {
				logMessage ("RedMetricsManager::get couldn't find game object", MessageLevel.ERROR);
			}
		}
		return _instance;
	}
	void Awake ()
	{
		//logMessage("RedMetricsManager::Awake");
		antiDuplicateInitialization ();
	}
	
	void antiDuplicateInitialization ()
	{
		RedMetricsManager.get ();
		//logMessage("RedMetricsManager::antiDuplicateInitialization with hashcode="+this.GetHashCode()+" and _instance.hashcode="+_instance.GetHashCode(), MessageLevel.ERROR);
		if (this != _instance) {
			//logMessage("RedMetricsManager::antiDuplicateInitialization self-destruction");
			Destroy (this.gameObject);
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////
	
	private void initializeIfNecessary ()
	{
	}
	
	private string redMetricsURL = "https://api.redmetrics.io/v1/";
	private string redMetricsPlayer = "player";
	private string redMetricsEvent = "event";

	//Redmetrics-Unity's test game version
	private static string defaultGameVersion = "\"ff2e8f35-38ad-4d1d-9728-74a7f09bf704\"";
	private string gameVersion = defaultGameVersion;
	private static string defaultPlayerID = "\"b5ab445a-56c9-4c5b-a6d0-86e8a286cd81\"";
	private string playerID = defaultPlayerID;	    
	
	public void setPlayerID (string pID)
	{
		logMessage ("setPlayerID(" + pID + ")");
		playerID = pID;
	}
	
	public void setGameVersion (string gVersion)
	{
		logMessage ("setGameVersion(" + gVersion + ")");
		gameVersion = gVersion;
	}

	public void Start ()
	{
		sendStartEvent (false);
	}

	private static void logMessage (string message, MessageLevel level = MessageLevel.DEFAULT)
	{

		//if the game is played using a web player
		if (Application.isWebPlayer) {
			Application.ExternalCall ("DebugFromWebPlayerToBrowser", message);

			//if the game is played inside the editor or as a standalone
		} else {
			switch (level) {
			case MessageLevel.DEFAULT:
				Debug.Log (message);
				break;
			case MessageLevel.WARNING:
				Debug.LogWarning (message);
				break;
			case MessageLevel.ERROR:
				Debug.LogError (message);
				break;
			default:
				Debug.Log (message);
				break;
			}
		}
	}
	
	//////////////////////////////////////////////////
	/// standalone methods
	
	public static IEnumerator GET (string url, System.Action<WWW> callback)
	{
		logMessage ("GET");
		WWW www = new WWW (url);
		return waitForWWW (www, callback);
	}
	
	public static IEnumerator POST (string url, Dictionary<string,string> post, System.Action<WWW> callback)
	{
		logMessage ("POST");
		WWWForm form = new WWWForm ();
		foreach (KeyValuePair<string,string> post_arg in post) {
			form.AddField (post_arg.Key, post_arg.Value);
		}
		
		WWW www = new WWW (url, form);
		return waitForWWW (www, callback);
	}
	
	public static IEnumerator POST (string url, byte[] post, Dictionary<string, string> headers, System.Action<WWW> callback)
	{
		logMessage ("POST url:" + url);
		WWW www = new WWW (url, post, headers);
		return waitForWWW (www, callback);
	}
	
	private static IEnumerator waitForWWW (WWW www, System.Action<WWW> callback)
	{
		logMessage ("waitForWWW");
		float elapsedTime = 0.0f;
		
		if (null == www) {
			logMessage ("waitForWWW: null www", MessageLevel.ERROR);
			yield return null;
		}
		
		while (!www.isDone) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= 30.0f) {
				logMessage ("waitForWWW: TimeOut!", MessageLevel.ERROR);
				break;
			}
			yield return null;
		}
		
		if (!www.isDone || !string.IsNullOrEmpty (www.error)) {
			string errmsg = string.IsNullOrEmpty (www.error) ? "timeout" : www.error;
			logMessage ("RedMetricsManager::waitForWWW Error: Load Failed: " + errmsg, MessageLevel.ERROR);
			callback (null);    // Pass null result.
			yield break;
		}
		
		logMessage ("waitForWWW: www good to ship!", MessageLevel.ERROR);
		callback (www); // Pass retrieved result.
	}
	
	////////////////////////////////////////
	/// helpers for standalone
	/// 
	
	private void sendData (string urlSuffix, string pDataString, System.Action<WWW> callback)
	{
		//logMessage("RedMetricsManager::sendData");
		string url = redMetricsURL + urlSuffix;
		Dictionary<string, string> headers = new Dictionary<string, string> ();
		headers.Add ("Content-Type", "application/json");
		byte[] pData = System.Text.Encoding.ASCII.GetBytes (pDataString.ToCharArray ());
		//logMessage("RedMetricsManager::sendData StartCoroutine POST with data="+pDataString+" ...");
		StartCoroutine (RedMetricsManager.POST (url, pData, headers, callback));
	}
	
	private void createPlayer (System.Action<WWW> callback)
	{
		//logMessage("RedMetricsManager::createPlayer");
		string ourPostData = "{\"type\":" + prepareEvent (TrackingEvent.CREATEPLAYER) + "}";
		sendData (redMetricsPlayer, ourPostData, callback);
	}
	
	private void testGet (System.Action<WWW> callback)
	{
		//logMessage("RedMetricsManager::testGet");
		string url = redMetricsURL + redMetricsPlayer;
		StartCoroutine (RedMetricsManager.GET (url, callback));
	}
	
	private void wwwLogger (WWW www, string origin = "default")
	{
		if (null == www) {
			logMessage ("RedMetricsManager::wwwLogger null == www from " + origin, MessageLevel.ERROR);
		} else {
			if (www.error == null) {
				//logMessage("RedMetricsManager::wwwLogger Success: " + www.text + " from "+origin, MessageLevel.ERROR);
			} else {
				logMessage ("RedMetricsManager::wwwLogger Error: " + www.error + " from " + origin, MessageLevel.ERROR);
			} 
		}
	}
	
	private string extractPID (WWW www)
	{
		//logMessage("RedMetricsManager::extractPID");
		string result = null;
		wwwLogger (www, "extractPID");
		string trimmed = www.text.Trim ();
		string[] split1 = trimmed.Split ('\n');
		foreach (string s1 in split1) {
			//logMessage(s1);
			if (s1.Length > 5) {
				string[] split2 = s1.Trim ().Split (':');
				foreach (string s2 in split2) {
					if (!s2.Equals ("id")) {
						//logMessage("id =? "+s2);
						result = s2;
					}
				}
			}
		}
		return result;
	}
	
	private void trackStart (WWW www)
	{
		//logMessage("RedMetricsManager::trackStart: www =? null:"+(null == www));
		string pID = extractPID (www);
		setPlayerID (pID);
		sendEvent (TrackingEvent.START);
	}
	//////////////////////////////////////////////////




	// filtering is done on Application.isWebPlayer
	// but could be done on Application.platform for better accuracy
	public void sendStartEvent (bool restart)
	{
		//logMessage("RedMetricsManager::sendStartEvent");


		if (Application.isWebPlayer) {
			// all web players
			// management of game start for webplayer
			if (!restart) {
				//restart event is sent from somewhere else
				connect ();
				StartCoroutine (waitAndSendStart ());
			}
				
		} else {
			// other players + editor
			if (defaultPlayerID == playerID) {
				//playerID hasn't been initialized
				//logMessage("RedMetricsManager::sendStartEvent other players/editor: createPlayer");
				createPlayer (www => trackStart (www));
				//testGet (www => trackStart(www));
			} else {
				sendEvent (TrackingEvent.RESTART);
			}
		}		
	}
	
	private IEnumerator waitAndSendStart ()
	{
		yield return new WaitForSeconds (5.0f);
		sendEvent (TrackingEvent.START);
	}
	
	public void connect ()
	{
		string json = "{\"gameVersionId\": " + gameVersion + "}";
		//logMessage("RedMetricsManager::connect will rmConnect json="+json);
		Application.ExternalCall ("rmConnect", json);
	}
	
	
	private string innerCreateJsonForRedMetrics (string eventCode, string customData, string section, string coordinates)
	{
		string eventCodePart = "", customDataPart = "", sectionPart = "", coordinatesPart = "";
		
		eventCodePart = "\"type\":\"";
		if (string.IsNullOrEmpty (eventCode)) {
			eventCodePart += "unknown";
		} else {
			eventCodePart += eventCode;
		}
		eventCodePart += "\"";
		
		if (!string.IsNullOrEmpty (customData)) {
			customDataPart = ",\"customData\":\"" + customData + "\"";
		}
		
		if (!string.IsNullOrEmpty (section)) {
			sectionPart = ",\"section\":\"" + section + "\"";
		}
		
		if (!string.IsNullOrEmpty (coordinates)) {
			coordinatesPart = ",\"coordinates\":\"" + coordinates + "\"";
		}
		
		return eventCodePart + customDataPart + sectionPart + coordinatesPart + "}";
	}
	
	private string createJsonForRedMetrics (string eventCode, string customData, string section, string coordinates)
	{
		string jsonPrefix = "{\"gameVersion\":" + gameVersion + "," +
			"\"player\":";
		string jsonSuffix = innerCreateJsonForRedMetrics (eventCode, customData, section, coordinates);

		string pID = playerID;
		if (!string.IsNullOrEmpty (pID)) {
			//logMessage("RedMetricsManager::sendEvent player already identified - pID="+pID);            
		} else {
			logMessage ("RedMetricsManager::sendEvent no registered player!", MessageLevel.WARNING);
			pID = defaultPlayerID;
		}
		return jsonPrefix + pID + "," + jsonSuffix;
		//sendData(redMetricsEvent, ourPostData, value => wwwLogger(value, "sendEvent("+eventCode+")"));
	}
	
	private string createJsonForRedMetricsJS (string eventCode, string customData, string section, string coordinates)
	{
		return "{" + innerCreateJsonForRedMetrics (eventCode, customData, section, coordinates);
	}

	private string prepareEvent (TrackingEvent tEvent)
	{
		return tEvent.ToString ().ToLower ();
	}

	// see github.com/CyberCri/RedMetrics.js
	// with type -> eventCode
	public void sendEvent (TrackingEvent trackingEvent, string customData = null, string section = null, string coordinates = null)
	{
		//logMessage("RedMetricsManager::sendEvent");
		if (Application.isWebPlayer) {
			string json = createJsonForRedMetricsJS (prepareEvent (trackingEvent), customData, section, coordinates);
			//logMessage("RedMetricsManager::sendEvent isWebPlayer will rmPostEvent json="+json);
			Application.ExternalCall ("rmPostEvent", json);
		} else {
			//logMessage("RedMetricsManager::sendEvent non web player");
			//TODO wait on playerID using an IEnumerator
			if (!string.IsNullOrEmpty (playerID)) {
				//logMessage("RedMetricsManager::sendEvent player already identified - pID="+playerID);
				string ourPostData = "{\"gameVersion\":" + gameVersion + "," +
					"\"player\":" + playerID + "," +
					"\"type\":\"" + prepareEvent (trackingEvent) + "\"}";
				sendData (redMetricsEvent, ourPostData, value => wwwLogger (value, "sendEvent(" + prepareEvent (trackingEvent) + ")"));
			} else {
				logMessage ("RedMetricsManager::sendEvent no registered player!", MessageLevel.ERROR);
			}
		}
	}
	
	public override string ToString ()
	{
		return string.Format ("[RedMetricsManager playerID:{0}, gameVersion:{1}, redMetricsURL:{2}]",
		                      playerID, gameVersion, redMetricsURL);
	}
	
}
