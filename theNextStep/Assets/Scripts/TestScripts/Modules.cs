using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Modules : MonoBehaviour
{

	public GameObject Model;
	private ModuleCreator Creator;

	public List<GameObject> _buildings = new List<GameObject> ();

	public GameObject _buildingsCanvas;
	public GameObject _sliderTime;
	public GameObject _constructionCanvas;

	public bool _landingZoneCreated;

	public GameObject _buttonLandingZone;
	// Use this for initialization
	void Awake ()
	{

		Creator = ModuleCreator.Instance;
		Creator.Initiate (1f);

		_buildingsCanvas = GameObject.Find ("Buildings");
		_sliderTime = GameObject.Find ("SlidderForSpeed");
		_constructionCanvas = GameObject.Find ("Construction");
		_buttonLandingZone = GameObject.Find ("LandingZoneButton");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.F1)) {
			Creator.Deploy (Model);
		}

		if (_landingZoneCreated == true) {
			_buttonLandingZone.GetComponent<CanvasGroup> ().alpha = 0;
			_buttonLandingZone.GetComponent<CanvasGroup> ().interactable = false;
			_buttonLandingZone.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}

	public void MedicalCenter ()
	{
		Creator._price = 50f;
		Model = _buildings [0];

		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	public void FoodProduction ()
	{
		Creator._price = 200f;

		Model = _buildings [1];

		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	public void SolarPanels ()
	{
		Creator._price = 200f;

		Model = _buildings [2];

		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	public void HumanHabitats ()
	{
		Creator._price = 1000f;

		Model = _buildings [3];

		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	public void WaterExtractionCenter ()
	{
		Creator._price = 200f;

		Model = _buildings [4];

		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	public void LandingZone ()
	{
		_landingZoneCreated = true;

		Creator._price = 50f;

		Model = _buildings [5];

		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	public void RoverStation ()
	{
		Creator._price = 500f;

		Model = _buildings [6];
		
		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	public void WareHouse ()
	{
		Creator._price = 500f;

		Model = _buildings [7];
		
		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	public void ComTower ()
	{
		Creator._price = 50f;

		Model = _buildings [8];
		
		if (Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials >= Creator._price) {
			Creator.Deploy (Model);
			canvasOptions ();
		}
	}

	void OnGUI ()
	{
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = 25;
		myStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width - 400, 100, 500, 30), "buildings materials: " + Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials, myStyle);
	}

	void canvasOptions ()
	{
		_buildingsCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_buildingsCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		_buildingsCanvas.GetComponent<CanvasGroup> ().interactable = false;
		
		_sliderTime.GetComponent<CanvasGroup> ().alpha = 1;
		_sliderTime.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		_sliderTime.GetComponent<CanvasGroup> ().interactable = true;
		
		_constructionCanvas.GetComponent<CanvasGroup> ().alpha = 1;
		_constructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		_constructionCanvas.GetComponent<CanvasGroup> ().interactable = true;
	}
}
