//By Esther Berges 
//This script goes on the com tower canvas
//This takes the player to the forum general board of the next step
//By Esther Berges

using UnityEngine;
using System.Collections;

public class goToForum : MonoBehaviour
{
	public void goToForumYes ()
	{
		Application.OpenURL ("http://thenextstep.freeforums.net/board/1/general-board");
	}
}
