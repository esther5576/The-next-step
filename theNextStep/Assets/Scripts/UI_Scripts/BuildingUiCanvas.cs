using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildingUiCanvas : MonoBehaviour
{
	private GameObject[] _needs, _products;
	private GameObject _power, _information;
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

		//Display (4, 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Display (int NeedsNumber, int ProductsNumber)
	{
		for (int i = 0; i < NeedsNumber; i++) {
			_needs [i].SetActive (true);
		}
		for (int i = 0; i < ProductsNumber; i++) {
			_products [i].SetActive (true);
		}

		_power.SetActive (true);
		_information.SetActive (true);
	}

	public void SwitchOff ()
	{
		_power.SetActive (false);
		_information.SetActive (false);
		foreach (var Ui in _needs) {
			Ui.SetActive (false);
		}
		foreach (var UI in _products) {
			UI.SetActive (false);
		}
	}
}
