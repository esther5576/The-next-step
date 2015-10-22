//By Esther Berges
//This script goes on the prefab comunication Tower
//It activates a canvas that leads you to the next step forum
//By Esther Berges

using UnityEngine;
using System.Collections;

public class comTowerScript : MonoBehaviour
{

	public GameObject _TowerComCanvas;

	// Use this for initialization
	void Start ()
	{
		_TowerComCanvas = GameObject.Find ("TowerComCanvas");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {

				if (hit.rigidbody != null && (hit.transform.tag == "ComTower")) {
					_TowerComCanvas.GetComponent<CanvasGroup> ().alpha = 1;
					_TowerComCanvas.GetComponent<CanvasGroup> ().interactable = true;
					_TowerComCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
				}
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			_TowerComCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_TowerComCanvas.GetComponent<CanvasGroup> ().interactable = false;
			_TowerComCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}
}