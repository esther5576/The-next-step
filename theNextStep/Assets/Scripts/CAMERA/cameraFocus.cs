//this scripts makes the camera focus on a building when it is clicked on

using UnityEngine;
using System.Collections;

public class cameraFocus : MonoBehaviour
{
	public float _speed;
	public Vector3 Offset;
	private Vector3 _target = new Vector3 ();
	
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
					StopAllCoroutines ();
					StartCoroutine (FocusOn (hit.transform));
				}
			}
		}

	}
	
	IEnumerator FocusOn (Transform target)
	{
		Vector3 targetVect = target.position;
		targetVect.y = transform.position.y;
		while (targetVect != transform.position) {
			transform.position = Vector3.MoveTowards (transform.position, targetVect, _speed * Time.deltaTime);
			yield return null;
		}
		Debug.Log ("End!");
		yield return null;
	}
}
