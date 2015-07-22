using UnityEngine;

public class TimeStop : MonoBehaviour {

    // A time we use to make sure our input is framerate independent.
	float timer;

    // A reference to the DayNightController script.
	DayNightController controller;
	
	void Awake() {
        // Find the DayNightController game object by its name and get the DayNightController script on it.
		controller = GameObject.Find("DayNightController").GetComponent<DayNightController>();
	}

	void Update() {
        // Get the raw vertical axis input (W, S, Arrow key up and down by default).
		float input = Input.GetAxisRaw("Vertical");

        // Increase our timer.
		timer += Time.deltaTime;

        // If we pressed the W or Up-arrow key and our timer is higher than our
        // treshold increase the speed of our day night cycle.
        if (input > 0 && timer > 0.01f) {
			controller.timeMultiplier += 0.1f;
            // Cap it to a sane value.
			if (controller.timeMultiplier >= 50) {
				controller.timeMultiplier = 50;
			}
			timer = 0;
		}

        // If we pressed the S or Down-arrow key and our timer is higher than our
        // treshold decrease the speed of our day night cycle.
        if (input < 0 && timer > 0.01f) {
			controller.timeMultiplier -= 1f;
            // Cap it to a sane value. I think the code handles negative values
            // as well allowing you to rewind the day night cycle, but then it 
            // would be harder to pause the day night cycle at will.
			if (controller.timeMultiplier <= 0) {
				controller.timeMultiplier = 0;
			}
			timer = 0;
		}
	}
}
