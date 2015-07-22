using UnityEngine;
using System.Collections;

public class cameraFocus : MonoBehaviour
{
	public float _speed;
	public Vector3 _target = new Vector3 (); 
	
	private bool _activeFocus;

	// Use this for initialization
	void Start ()
	{
		_target = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.rigidbody != null) {
					_activeFocus = true;
					_target = new Vector3 (hit.rigidbody.position.x - 2, this.transform.position.y, hit.rigidbody.position.z - 2);
				}
			}
		}

		if (_activeFocus == true) {
			float _step = _speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, _target, _step);

			if (Camera.main.GetComponent<cameraBehaviour> ()._zoomNumber < 0.5f) {
				Camera.main.GetComponent<cameraBehaviour> ()._zoomNumber += 1 * Time.deltaTime;
			}
			if (Camera.main.GetComponent<cameraBehaviour> ()._zoomNumber >= 0.5f) {
				Camera.main.GetComponent<cameraBehaviour> ()._zoomNumber = 0.5f;
			}
			

			//Camera.main.GetComponent<cameraBehaviour> ()._animation.SetFloat ("cameraType", _cameraStep);
			if (transform.position == _target) {
				_activeFocus = false;
			}
		}

	}
}
