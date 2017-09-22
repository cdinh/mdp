using UnityEngine;
using System.Collections;

// PLACEHOLDER!!!
public class StomachStoryboard : MonoBehaviour 
{

	DetectStraightSwipe swipeDetection;	//!< to hold a reference to the script that controls swipe detection

	// Use this for initialization
	void Start () 
	{
		// find the script for detecting touch
		swipeDetection = gameObject.GetComponent<DetectStraightSwipe> ();
	}

	// Update is called once per frame
	void Update () {
		if (swipeDetection.getSwipeLeft())		// attempt to detect a swipe to the right
		{
			Application.LoadLevel ("LoadLevelStomach");
		}
	}
}
