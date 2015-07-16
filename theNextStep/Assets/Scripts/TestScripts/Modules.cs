using UnityEngine;
using System.Collections;

public class Modules : MonoBehaviour
{


	public GameObject Model;
	private ModuleCreator Creator;
	// Use this for initialization
	void Start ()
	{
		Creator = ModuleCreator.Instance;
		Creator.Initiate (1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			Creator.Deploy (Model);
		}

	}
}
