using UnityEngine;
using System.Collections;

public enum TrackingEvent
{
	//standard events
	DEFAULT,
	CREATEPLAYER,
	START,
	END,
	WIN,
	FAIL,
	RESTART,
	GAIN,
	LOSE,

	//other examples of events
	CHANGEPLAYER,
	JUMP,
	BOUNCE,
	STATS
}