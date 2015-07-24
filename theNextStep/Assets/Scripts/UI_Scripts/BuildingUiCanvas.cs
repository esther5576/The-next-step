using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingUiCanvas : MonoBehaviour
{
	private GameObject[] _needs, _products;
	private GameObject _power, _information, _upgrade, _repair;
	// Use this for initialization
	void Start ()
	{
		//Array assignation
		GameObject[] Needs = GameObject.FindGameObjectsWithTag ("NeedUi");
		GameObject[] Products = GameObject.FindGameObjectsWithTag ("ProductUi");
		_needs = new GameObject[Needs.Length];
		_products = new GameObject[Products.Length];

		//Member assignation
		_power = GameObject.Find ("Power");
		_information = GameObject.Find ("Informations");
		_repair = GameObject.Find ("Repair");
		_upgrade = GameObject.Find ("Upgrade");

		//initialisation and order
		for (int i = 0; i < _needs.Length; i++) {
			_needs [Convert.ToInt32 (Needs [i].transform.name.Remove (0, 5)) - 1] = Needs [i];
			Needs [i].SetActive (false);
		}
		for (int i = 0; i < _products.Length; i++) {
			_products [Convert.ToInt32 (Products [i].transform.name.Remove (0, 8)) - 1] = Products [i];
			Products [i].SetActive (false);
		}
		_power.SetActive (false);
		_information.SetActive (false);
		_repair.SetActive (false);
		_upgrade.SetActive (false);

		//Display (4, 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Display (int NeedsNumber, int ProductsNumber, List<Button.ButtonClickedEvent> NeedsEvents, List<Button.ButtonClickedEvent> ProductsEvents, Button.ButtonClickedEvent PowerEvents)
	{
		for (int i = 0; i < NeedsNumber; i++) {
			_needs [i].SetActive (true);
			_needs [i].GetComponent<Button> ().onClick = NeedsEvents [i];
		}
		for (int i = 0; i < ProductsNumber; i++) {
			_products [i].SetActive (true);
			_products [i].GetComponent<Button> ().onClick = ProductsEvents [i];
		}

		_power.SetActive (true);
		_power.GetComponent<Button> ().onClick = PowerEvents;	
		_information.SetActive (true);
		_repair.SetActive (true);
		_upgrade.SetActive (true);
	}

	public void SwitchOff ()
	{
		_power.SetActive (false);
		_information.SetActive (false);
		_repair.SetActive (false);
		_upgrade.SetActive (false);
		foreach (var Ui in _needs) {
			Ui.SetActive (false);
		}
		foreach (var UI in _products) {
			UI.SetActive (false);
		}
	}
}
