using UnityEngine;
using System.Collections;

public class slowAndFastMotion : MonoBehaviour
{
	public float _speed = 1;
	public GameObject _cameraController;
	// Update is called once per frame
	void Update ()
	{

		Camera.main.GetComponent<DayNightController> ().timeMultiplier = _speed;
		
	}
	
	public void AdjustSpeed (float _newSpeed)
	{
		_speed = _newSpeed;
	}
}