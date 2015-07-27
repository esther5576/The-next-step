using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingUiCanvas : MonoBehaviour
{
	private GameObject[] _needs, _products;
	private GameObject _power, _information, _upgrade, _repair, _energy;
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
		_energy = GameObject.Find ("Power and Humans");

		//initialisation and order
		for (int i = 0; i < _needs.Length; i++) {
			_needs [Convert.ToInt32 (Needs [i].transform.name.Remove (0, 5)) - 1] = Needs [i];
			Needs [i].SetActive (false);
		}
		for (int i = 0; i < _products.Length; i++) {
			_products [Convert.ToInt32 (Products [i].transform.name.Remove (0, 11)) - 1] = Products [i];
			Products [i].SetActive (false);
		}
		_power.SetActive (false);
		_information.SetActive (false);
		_repair.SetActive (false);
		_upgrade.SetActive (false);
		_energy.SetActive (false);

		//Display (4, 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Display (int NeedsNumber, int ProductsNumber, Button.ButtonClickedEvent PowerEvents, List<Sprite> NeedsSprites, List<Sprite> ProductionSprites, int PowerNeeds, int HumanNeed)
	{
		for (int i = 0; i < NeedsNumber; i++) {
			_needs [i].SetActive (true);
			_needs [i].GetComponent<Image> ().sprite = NeedsSprites [i];
		}
		for (int i = 0; i < ProductsNumber; i++) {
			_products [i].SetActive (true);
			_products [i].GetComponent<Image> ().sprite = ProductionSprites [i];
		}

		_power.SetActive (true);
		_power.GetComponent<Button> ().onClick = PowerEvents;	
		_information.SetActive (true);
		_repair.SetActive (true);
		_upgrade.SetActive (true);
		_energy.SetActive (true);
		_energy.transform.GetChild (0).gameObject.GetComponent<Text> ().text = PowerNeeds.ToString ();
		_energy.transform.GetChild (1).gameObject.GetComponent<Text> ().text = HumanNeed.ToString ();
	}

	public void SwitchOff ()
	{
		_power.SetActive (false);
		_information.SetActive (false);
		_repair.SetActive (false);
		_upgrade.SetActive (false);
		_energy.SetActive (false);
		foreach (var Ui in _needs) {
			Ui.SetActive (false);
		}
		foreach (var UI in _products) {
			UI.SetActive (false);
		}
	}
}
