using UnityEngine;
using System.Collections;

public class JumpingCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space)) {
			RedMetricsManager.get ().sendEvent(TrackingEvent.JUMP);
			giveImpulsion();
		}

		if(this.gameObject.transform.localPosition.y < -2.0f) {
			RedMetricsManager.get ().sendEvent(TrackingEvent.BOUNCE);
			giveImpulsion();
		}
	}

	void giveImpulsion() {
			this.GetComponent<Rigidbody>().velocity = Vector3.zero;
			this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500.0f, 0));
	}
}
