//By Esther Berges 
//This script goes on the warehouse
//It gives more space to stock stuff
//By Esther Berges


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WareHouseScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		Camera.main.GetComponent<gameStats> ()._numberOfWarehouses.Add (this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
