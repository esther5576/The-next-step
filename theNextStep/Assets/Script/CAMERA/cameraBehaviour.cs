using UnityEngine;
using System.Collections;

public class cameraBehaviour : MonoBehaviour
{
	public AnimationCurve _camAnimationCurve;

	public float _zoomNumber;

	public Animator _animation;

	// Use this for initialization
	void Start ()
	{
		_animation = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Zoom
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && _zoomNumber < 1) { // forward
			_zoomNumber += 0.07f;
		}

		//Dezoom
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && _zoomNumber > 0) { // back
			_zoomNumber -= 0.07f;
		}

		//Adjust minimum
		if (_zoomNumber < 0) {
			_zoomNumber = 0;
		}

		//Curve
		float _newZoomNumber = _camAnimationCurve.Evaluate (_zoomNumber);

		_animation.SetFloat ("cameraType", _newZoomNumber);
	}
}
