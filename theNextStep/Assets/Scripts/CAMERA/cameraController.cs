﻿//this script lets you move around with your camera
//by using the arrows keys or by putting the mouse on the corner of the screen
//it has also a zoom but it is commented because we use the animated zoom from another camera script
//the camera also turns around a poitn using Q or E

using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour
{
	public float verticalScrollArea = 10f;
	public float horizontalScrollArea = 10f;
	public float verticalScrollSpeed = 10f;
	public float horizontalScrollSpeed = 10f;
	public float RotateSpeed = 35f;
	
	public bool ZoomEnabled = true;
	public bool MoveEnabled = true;
	public bool CombinedMovement = true;
	
	private Vector2 _mousePos;
	private Vector3 _moveVector;
	private float _xMove;
	private float _yMove;
	private float _zMove;
	
	void Update ()
	{
		//Avec espace on arrete le mouvement de la camera
		if (Input.GetKey (KeyCode.Space)) {
			MoveEnabled = false;
			CombinedMovement = false;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			MoveEnabled = true;
			CombinedMovement = true;
		}

		_mousePos = Input.mousePosition;
		
		//Move camera if mouse is at the edge of the screen
		if (MoveEnabled) {
			
			//Move camera if mouse is at the edge of the screen
			if (_mousePos.x < horizontalScrollArea) {
				_xMove = -1;
			} else if (_mousePos.x >= Screen.width - horizontalScrollArea) {
				_xMove = 1;
			} else {
				_xMove = 0;
			}
			
			if (_mousePos.y < verticalScrollArea) {
				_zMove = -1;
			} else if (_mousePos.y >= Screen.height - verticalScrollArea) {
				_zMove = 1;
			} else {
				_zMove = 0;
			}
			
			//Move camera if wasd or arrow keys are pressed
			float xAxisValue = Input.GetAxis ("Horizontal");
			float zAxisValue = Input.GetAxis ("Vertical");
			
			if (xAxisValue != 0) {
				if (CombinedMovement) {
					_xMove += xAxisValue;
				} else {
					_xMove = xAxisValue;
				}
			}
			
			if (zAxisValue != 0) {
				if (CombinedMovement) {
					_zMove += zAxisValue;
				} else {
					_zMove = zAxisValue;
				}
			}
			
		} else {
			_xMove = 0;
			_yMove = 0;
		}
		
		// Zoom Camera in or out
		/*if (ZoomEnabled) {
			if (Input.GetAxis("Mouse ScrollWheel") < 0) 
			{
				_yMove = 1;
			}
			else if (Input.GetAxis("Mouse ScrollWheel") > 0) 
			{
				_yMove = -1;
			}
			else 
			{
				_yMove = 0;
			}
		} else {
			_zMove = 0;
		}*/
		
		//move the object
		MoveMe (_xMove, _yMove, _zMove);

		//Move the camera around a point
		if (Input.GetKey (KeyCode.Q) && !Input.GetKey (KeyCode.E)) {
			
			Ray r = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (r, out hit)) {
				transform.RotateAround (hit.point, Vector3.up, RotateSpeed * Time.deltaTime);
			}
		}
		//Move the camera around a point
		if (Input.GetKey (KeyCode.E) && !Input.GetKey (KeyCode.Q)) {
			
			Ray r = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (r, out hit)) {
				transform.RotateAround (hit.point, Vector3.up, -RotateSpeed * Time.deltaTime);
			}
		}
	}

	//move the object
	private void MoveMe (float x, float y, float z)
	{
		_moveVector = (new Vector3 (x * horizontalScrollSpeed + z * horizontalScrollSpeed,
		                            y * horizontalScrollSpeed, z * horizontalScrollSpeed - x * horizontalScrollSpeed) * Time.deltaTime);

		transform.Translate (_moveVector, Space.Self);
	}
}
