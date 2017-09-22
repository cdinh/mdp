using UnityEngine;
using System.Collections;

/*
 * script that handles the 2x multiplier UI element but draws it on the lower left side of the screen in the si game
 */
public class SpeedMultiplierOdd : MonoBehaviour 
{
	private bool value;					//!< flag to indicate whether we are using 1x or 2x
	public GUIStyle speedButtonOff;		//!< to hold the texture to draw when 2x speed is disabled
	public GUIStyle speedButtonOn;		//!< to hold the texture to draw when 2x speed is enabled

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		value = false;					// initialize the value of "value" to false
	}

	/**
	 * Handles drawing the correct speed multiplier graphic
	 */
	void OnGUI()
	{
		// when the 2x speed button is on
		if(value && 
		   GUI.Button(new Rect(Screen.width * .01f, Screen.height * .745f, 
		                    Screen.width * .05f, Screen.width * .05f), "", speedButtonOn))
		{
			// when we click on it change the value of value to false
			value = false;
		}

		// when the 2x speed button is off
		if(!value && 
		   GUI.Button(new Rect(Screen.width * .01f, Screen.height * .745f, 
		                    Screen.width * .05f, Screen.width * .05f), "", speedButtonOff))
		{
			// when we click on it change the value of value to true
			value = true;
		}

		// this changes the speed by alteriing the time scale of the game
		if (value && Time.timeScale == 1)		// when value is true (so we should be using 2x speed)
		{
			Time.timeScale = 2;					// increase the speed to 2x
		}
		else if(!value && Time.timeScale == 2)	// when the value is false (so we should not be using 2x speed)
		{
			Time.timeScale = 1;					// decrease the speed to 1x
		}
	}
}
