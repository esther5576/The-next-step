using UnityEngine;
using System.Collections;

public class slowAndFastMotion : MonoBehaviour
{
	public float _speed = 1;
	
	// Update is called once per frame
	void Update ()
	{
		Time.timeScale = _speed;
	}

	public void AdjustSpeed (float _newSpeed)
	{
		_speed = _newSpeed;
	}
}
