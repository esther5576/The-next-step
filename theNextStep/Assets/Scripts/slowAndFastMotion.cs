using UnityEngine;
using System.Collections;

public class slowAndFastMotion : MonoBehaviour
{
	public float _speed = 1;
	public GameObject _cameraController;
	// Update is called once per frame
	void Update ()
	{
		Time.timeScale = _speed;

		if (_speed != 1) {
			_cameraController.GetComponent<cameraController> ().MoveEnabled = false;
			_cameraController.GetComponent<cameraController> ().CombinedMovement = false;
		} else {
			_cameraController.GetComponent<cameraController> ().MoveEnabled = true;
			_cameraController.GetComponent<cameraController> ().CombinedMovement = true;
		}

	}

	public void AdjustSpeed (float _newSpeed)
	{
		_speed = _newSpeed;
	}
}
