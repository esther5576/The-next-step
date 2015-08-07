using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buildingmaterialsHUD : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.GetComponent<Text> ().text = "Building\nMaterials\n" + Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials;
	}
}
