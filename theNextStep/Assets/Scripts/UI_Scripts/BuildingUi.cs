﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BuildingUi : MonoBehaviour
{
	[Range(0,3)]
	public int
		Needs, Production;
	public int PowerNeed, HumanNeed;
	public Button.ButtonClickedEvent PowerLinks;
	public Button.ButtonClickedEvent RepairLinks;
	public Button.ButtonClickedEvent UpgradLinks;
	public List<Sprite> NeedsSprites, ProductionSprites;
	
	private BuildingUiCanvas _canvas;
	private bool _displayed;
	private CanvasScaler _scale;
	
	private static bool _SwitchOff = false;
	
	public string _name;
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
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			if (!_displayed) {
				_SwitchOff = true;
				StartCoroutine (DisplayUi ());
			}
		}
	}
	IEnumerator DisplayUi ()
	{
		if (_SwitchOff) {
			yield return new WaitForFixedUpdate ();
			_SwitchOff = false;
		}
		//<<<<<<< HEAD
		_canvas.Display (Needs, Production, PowerLinks, NeedsSprites, ProductionSprites, PowerNeed, HumanNeed, RepairLinks, UpgradLinks, _name);
		//=======
		//	_canvas.Display (Needs, Production, PowerLinks, NeedsSprites, ProductionSprites, PowerNeed, HumanNeed, RepairLinks, UpgradLinks, Name);
		//>>>>>>> origin/master
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
			
			if (Input.GetMouseButton (1) || _SwitchOff) {
				_displayed = false;
				_SwitchOff = false;
				_canvas.SwitchOff ();
			}
			yield return null;
		}
	}
	
	
}
