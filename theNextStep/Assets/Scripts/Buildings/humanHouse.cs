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
