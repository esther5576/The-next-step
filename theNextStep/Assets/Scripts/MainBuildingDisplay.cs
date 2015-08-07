using UnityEngine;
using System.Collections;

public class MainBuildingDisplay : MonoBehaviour
{

	public GameObject _HUDstats;
	public GameObject _constructionButton;
	public GameObject _sliderTime;

	// Use this for initialization
	void Start ()
	{
		_HUDstats = GameObject.Find ("HUDstats");
		_constructionButton = GameObject.Find ("Construction");
		_sliderTime = GameObject.Find ("SlidderForSpeed");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.rigidbody != null && (hit.transform.name == "MainBuilding")) {

					_HUDstats.GetComponent<CanvasGroup> ().alpha = 0;
					
					_constructionButton.GetComponent<CanvasGroup> ().alpha = 0;
					_constructionButton.GetComponent<CanvasGroup> ().blocksRaycasts = false;
					_constructionButton.GetComponent<CanvasGroup> ().interactable = false;

					
					this.GetComponent<CanvasGroup> ().alpha = 1;
					this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
					this.GetComponent<CanvasGroup> ().interactable = true;

					_sliderTime.GetComponent<slowAndFastMotion> ()._speed = 0;
					_sliderTime.GetComponent<CanvasGroup> ().alpha = 0;
					_sliderTime.GetComponent<CanvasGroup> ().interactable = false;
					_sliderTime.GetComponent<CanvasGroup> ().blocksRaycasts = false;

				}
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			_HUDstats.GetComponent<CanvasGroup> ().alpha = 1;
			
			_constructionButton.GetComponent<CanvasGroup> ().alpha = 1;
			_constructionButton.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_constructionButton.GetComponent<CanvasGroup> ().interactable = true;
			
			
			this.GetComponent<CanvasGroup> ().alpha = 0;
			this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			this.GetComponent<CanvasGroup> ().interactable = false;

			_sliderTime.GetComponent<slowAndFastMotion> ()._speed = 1;
			_sliderTime.GetComponent<CanvasGroup> ().alpha = 1;
			_sliderTime.GetComponent<CanvasGroup> ().interactable = true;
			_sliderTime.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}

	}
}
