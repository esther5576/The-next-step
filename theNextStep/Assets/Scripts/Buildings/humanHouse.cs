//By Esther Berges 
//This script goes on the hu,qn house
//Each house increases the number of humans it has every time one is created
//By Esther Berges

using UnityEngine;
using System.Collections;

public class humanHouse : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Camera.main.GetComponent<gameStats> ()._maxHumans += 5;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
