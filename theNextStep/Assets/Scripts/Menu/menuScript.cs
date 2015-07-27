using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void PlayGame ()
	{
		Application.LoadLevel ("Game");
	}

	public void Credits ()
	{
		Application.LoadLevel ("Credits");
	}

	public void MoreInfo ()
	{
		Application.OpenURL ("http://thenextstep.freeforums.net/board/1/general-board");
	}
}
