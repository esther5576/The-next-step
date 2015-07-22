﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingUi : MonoBehaviour
{
	[Range(0,4)]
	public int
		Needs, Production;
	
	private BuildingUiCanvas _canvas;
	private bool _displayed;
	private CanvasScaler _scale;

	// Use this for initialization
	void Start ()
	{
		_canvas = GameObject.Find ("BuildingUi").GetComponent<BuildingUiCanvas> ();
		_scale = GameObject.Find ("BuildingNeedsAndProduction").GetComponent<CanvasScaler> ();
	}
	
	// Update is called once per frame
	void Update ()
	{


	}

	void OnMouseDown ()
	{	
		if (!_displayed) {
			StartCoroutine (DisplayUi ());
		}
	}
	
	IEnumerator DisplayUi ()
	{
		_canvas.Display (Needs, Production);
		_displayed = true;
		while (_displayed) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
			screenPos.x /= Screen.width;
			screenPos.y /= Screen.height;
			screenPos.x -= 0.5f;
			screenPos.y -= 0.5f;
			screenPos.x *= _scale.referenceResolution.x;
			screenPos.y *= _scale.referenceResolution.y;
			_canvas.GetComponent<RectTransform> ().localPosition = screenPos;
			
			if (Input.GetMouseButton (1)) {
				_displayed = false;
				_canvas.SwitchOff ();
			}
			yield return null;
		}
	}


}
