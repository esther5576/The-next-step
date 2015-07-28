﻿using UnityEngine;
using System.Collections;

public class BarRepresentation : MonoBehaviour
{
	#region Oxygen
	//For oxygen ======================================================================================================================================================================================================
	//current progress
	public float _barDisplayOxygen; 
	//position of the bar
	public Vector2 _posOxygen = new Vector2 (100, 20);
	//size of the bar
	public Vector2 _sizeOxygen = new Vector2 (100, 20);
	//texture of the bar
	public Texture2D _emptyTexOxygen;
	//texture of the bar that is being filled
	public Texture2D _fullTexOxygen;
	//For oxygen ======================================================================================================================================================================================================
	#endregion

	#region Water
	//For water ======================================================================================================================================================================================================
	//current progress
	public float _barDisplayWater; 
	//position of the bar
	public Vector2 _posWater = new Vector2 (250, 20);
	//size of the bar
	public Vector2 _sizeWater = new Vector2 (100, 20);
	//texture of the bar
	public Texture2D _emptyTexWater;
	//texture of the bar that is being filled
	public Texture2D _fullTexWater;
	//For water ======================================================================================================================================================================================================
	#endregion

	#region Food
	//For food ======================================================================================================================================================================================================
	//current progress
	public float _barDisplayFood; 
	//position of the bar
	public Vector2 _posFood = new Vector2 (400, 20);
	//size of the bar
	public Vector2 _sizeFood = new Vector2 (100, 20);
	//texture of the bar
	public Texture2D _emptyTexFood;
	//texture of the bar that is being filled
	public Texture2D _fullTexFood;
	//For food ======================================================================================================================================================================================================
	#endregion

	void OnGUI ()
	{
		#region Oxygen
		//For oxygen ==================================================================================================================================================================================================
		//draw the background ==================================================
		GUI.BeginGroup (new Rect (((Screen.width / 2) - 300) - _sizeOxygen.x / 2, _posOxygen.y, _sizeOxygen.x, _sizeOxygen.y));
		GUI.Box (new Rect (0, 0, _sizeOxygen.x, _sizeOxygen.y), _emptyTexOxygen);
		//draw the background ==================================================

		
		//draw the filled-in part ==============================================
		GUI.BeginGroup (new Rect (0, 0, _sizeOxygen.x * _barDisplayOxygen, _sizeOxygen.y));
		GUI.Box (new Rect (0, 0, _sizeOxygen.x, _sizeOxygen.y), _fullTexOxygen);
		GUI.EndGroup ();
		GUI.EndGroup ();
		//draw the filled-in part ==============================================
		//For oxygen ==================================================================================================================================================================================================
		#endregion

		#region Water
		//For water ==================================================================================================================================================================================================
		//draw the background ==================================================
		GUI.BeginGroup (new Rect ((Screen.width / 2) - _sizeWater.x / 2, _posWater.y, _sizeWater.x, _sizeWater.y));
		GUI.Box (new Rect (0, 0, _sizeWater.x, _sizeWater.y), _emptyTexWater);
		//draw the background ==================================================
		
		
		//draw the filled-in part ==============================================
		GUI.BeginGroup (new Rect (0, 0, _sizeWater.x * _barDisplayWater, _sizeWater.y));
		GUI.Box (new Rect (0, 0, _sizeWater.x, _sizeWater.y), _fullTexWater);
		GUI.EndGroup ();
		GUI.EndGroup ();
		//draw the filled-in part ==============================================
		//For water ==================================================================================================================================================================================================
		#endregion

		#region Food
		//For food ==================================================================================================================================================================================================
		//draw the background ==================================================
		GUI.BeginGroup (new Rect (((Screen.width / 2) + 300) - _sizeFood.x / 2, _posFood.y, _sizeFood.x, _sizeFood.y));
		GUI.Box (new Rect (0, 0, _sizeFood.x, _sizeFood.y), _emptyTexFood);
		//draw the background ==================================================
		
		
		//draw the filled-in part ==============================================
		GUI.BeginGroup (new Rect (0, 0, _sizeFood.x * _barDisplayFood, _sizeFood.y));
		GUI.Box (new Rect (0, 0, _sizeFood.x, _sizeFood.y), _fullTexFood);
		GUI.EndGroup ();
		GUI.EndGroup ();
		//draw the filled-in part ==============================================
		//For food ==================================================================================================================================================================================================
		#endregion

		//INFO TEXT OPTIONNEL ===================================================
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = 30;

		myStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (((Screen.width / 2) - 300) + (_sizeOxygen.x / 4) - _sizeOxygen.x / 2, 30, 100, 30), "OXYGEN", myStyle);

		GUI.Label (new Rect ((Screen.width / 2) + (_sizeWater.x / 4) - _sizeWater.x / 2, 30, 100, 30), "WATER", myStyle);

		GUI.Label (new Rect (((Screen.width / 2) + 320) + (_sizeFood.x / 4) - _sizeFood.x / 2, 30, 100, 30), "FOOD", myStyle);
		//INFO TEXT OPTIONNEL ===================================================
	}
	
	void Update ()
	{
		//For oxygen ==================================================================================================================================================================================================
		_barDisplayOxygen = (GetComponent<gameStats> ()._actualOxygen / GetComponent<gameStats> ()._maxOxygen);
		//For oxygen ==================================================================================================================================================================================================


		//For water ==================================================================================================================================================================================================
		_barDisplayWater = (GetComponent<gameStats> ()._actualWater / GetComponent<gameStats> ()._maxWater);
		//For water ==================================================================================================================================================================================================


		//For water ==================================================================================================================================================================================================
		_barDisplayFood = (GetComponent<gameStats> ()._actualFood / GetComponent<gameStats> ()._maxFood);
		//For water ==================================================================================================================================================================================================
	}

}
