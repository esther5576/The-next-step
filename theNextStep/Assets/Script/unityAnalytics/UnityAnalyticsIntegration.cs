using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{
		
		const string projectId = "0514174b-f010-4681-843a-623fd1a36555";
		UnityAnalytics.StartSDK (projectId);
		
	}
	
}