using UnityEngine;
using System.Collections;

public class creditsScripts : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Menu ();
		}
	}

	public void Menu ()
	{
		Application.LoadLevel ("Menu");
	}
	
	public void MoreInfo ()
	{
		Application.OpenURL ("http://thenextstep.freeforums.net/board/1/general-board");
	}
}
