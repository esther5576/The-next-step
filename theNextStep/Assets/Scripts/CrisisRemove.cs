using UnityEngine;
using System.Collections;

public class CrisisRemove : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (Input.GetMouseButtonDown (1)) {
			this.GetComponent<CanvasGroup> ().alpha = 0;
			this.GetComponent<CanvasGroup> ().interactable = false;
			this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}

	}
}
