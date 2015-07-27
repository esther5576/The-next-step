using UnityEngine;
using System.Collections;

public class goToForum : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void goToForumYes ()
	{
		Application.OpenURL ("http://thenextstep.freeforums.net/board/1/general-board");
	}
}
