using UnityEngine;
using System.Collections;

public class MapRover : MonoBehaviour
{
	[HideInInspector]
	public GameObject
		_roverCanvas;
	[HideInInspector]
	public GameObject
		_zonesCanvas;
	[HideInInspector]
	public GameObject
		_nextPartCanvas;
	[HideInInspector]
	public int
		_zone;
	// Use this for initialization
	void Start ()
	{
		_roverCanvas = GameObject.Find ("RoverStationCanvas");
		_zonesCanvas = GameObject.Find ("Zones");
		_nextPartCanvas = GameObject.Find ("NextPart");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void openRoverMap (bool _openMap)
	{
		_roverCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_roverCanvas.GetComponent<CanvasGroup> ().interactable = false;

		this.GetComponent<CanvasGroup> ().alpha = 1;
		this.GetComponent<CanvasGroup> ().interactable = true;

		_zonesCanvas.GetComponent<CanvasGroup> ().alpha = 1;
		_zonesCanvas.GetComponent<CanvasGroup> ().interactable = true;
		
		_nextPartCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_nextPartCanvas.GetComponent<CanvasGroup> ().interactable = false;
	}

	public void topLeftPartOfTheMap (bool _openZone1)
	{
		canvasGroupChange ();
		_zone = 1;
	}

	public void topRightPartOfTheMap (bool _openZone2)
	{
		canvasGroupChange ();
		_zone = 2;
	}

	public void bottomLeftPartOfTheMap (bool _openZone3)
	{
		canvasGroupChange ();
		_zone = 3;
	}

	public void bottomRightPartOfTheMap (bool _openZone4)
	{
		canvasGroupChange ();
		_zone = 4;
	}

	void canvasGroupChange ()
	{
		_zonesCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_zonesCanvas.GetComponent<CanvasGroup> ().interactable = false;
		
		_nextPartCanvas.GetComponent<CanvasGroup> ().alpha = 1;
		_nextPartCanvas.GetComponent<CanvasGroup> ().interactable = true;
	}
}
