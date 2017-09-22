using UnityEngine;
using System.Collections;

/**
 * script to handle loading the correct levels in the small intestine game
 */
public class LoadSmallIntestine : MonoBehaviour 
{
	private GameObject counter;							//!< to hold a reference to the background chooser for the small intestine
	public Texture[] backgrounds;						//!< to hold the different loading screen images
	private SmallIntestineLoadLevelCounter level;		//!< to hold a reference to the script for the si load level counter
	private const float timer = 3.0f;					//!< how long to hold background image on the loading screen
	private float timePassed = 0.0f;					//!< to keep track of how long has passed

	/**
	 * Initialization
	 */
	void Start()
	{
		timePassed = timer;								// set the time passed to the timer value, we will decrement the
														// value of time passed until it reaches 0
	}

	/**
	 * Chooses and draws the correct loading screen image
	 */
	void OnGUI()
	{
		counter = GameObject.Find ("ChooseBackground");						// find a reference to the background chooser
		level = counter.GetComponent<SmallIntestineLoadLevelCounter> ();	// to get a reference to the script on the background chooser

		// draw the loading screen texture across the entire screen
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), backgrounds [Mathf.Clamp(level.getLevel(), 0, level.getMaxLevels())]);
	}

	/**
	 * Holds a background image up for the specified time.
	 * Loads the correct level
	 */
	void Update()
	{
		timePassed -= Time.deltaTime;		// decrement the time passed variable by how much time since the last update call
	
		if (timePassed < 0) 				// check if the maximum time has passed 
		{
			// if it has load the correct si level


			if (level.getLevel() == 0)
			{
				Application.LoadLevel("SmallIntestineTutorial");
			} else if (level.getLevel() % 2 == 0)
			{
				Application.LoadLevel("SmallIntestineEven");
			} else
			{
				Application.LoadLevel("SmallIntestineOdd");
			}

			/*
			if (level.getLevel () == 0) {
				Application.LoadLevel ("SmallIntestineTutorial");
			} else if (level.getLevel () == 1) {
				Application.LoadLevel("SmallIntestineOdd");
			} else if (level.getLevel () == 4) {
				Application.LoadLevel("SmallIntestineOdd");
			}
			*/

		}
	}
}
