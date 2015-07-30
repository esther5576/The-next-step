using UnityEngine;
using System.Collections;

public class Visualisation : MonoBehaviour
{
	public bool Collide { get { return _collide; } }
	public GameObject ColliderObject { get { return _colliderObject; } }
	public bool BuildAble { get; set; }


	private GameObject _colliderObject;
	private bool _collide;
	private Color _initialColor;
	// Use this for initialization
	void Start ()
	{
		BuildAble = true;
		//_initialColor = GetComponent<Renderer> ().material.color;
		GetComponent<Renderer> ().material.color = _initialColor * Color.green;
	}
	
	// Update is called once per frame
	void Update ()
	{

		//GetComponent<Renderer> ().material.color = _initialColor * ((!_collide && BuildAble) ? Color.green : Color.red);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.GetComponent<Terrain> ()) {
			return;
		}
		Debug.Log (col.gameObject);
		_collide = true;
		_colliderObject = col.gameObject;
	}

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.GetComponent<Terrain> ())
			return;
		_collide = false;
		_colliderObject = null;
	}


}
