using UnityEngine;
using System.Collections;

/**
 * script to create the debug config gui for the mouth game
 */
public class EsophagusDebugConfigGUI : MonoBehaviour 
{
	EsophagusDebugConfig debugConfig;	//!< to hold a reference to the esophagus debug config

	private string foodSpawnInterval;	//!< to hold string represenation of foodSpawnInterval variable
	private string waveDelay;			//!< to hold string representation of waveDelay variable
	private string waveTime;			//!< to hold string representation of waveTime variable
	private string foodSpeed;			//!< to hold string representation of foodSpeed variable
	private string oxygenDeplete;		//!< to hold string representation of oxygenDeplete variable
	private string oxygenGain;			//!< to hold string representation of the oxygenGain variable

	bool debugActive;					//!< to hold flag over whether we are using values from the debug settings or script
	bool showGUI = false;				//!< flag to hold whether or not to show the gui above the game

	/**
	 * Use this for initialization.
	 * For this script the string representations of variable values are populated.
	 */
	void Start () 
	{
		debugConfig = gameObject.GetComponent<EsophagusDebugConfig>();	// find the reference to the debug config

		foodSpawnInterval = "" + debugConfig.foodSpawnInterval;		// create the string for the foodSpawnInterval variable
		waveDelay = "" + debugConfig.waveDelay;						// create the string for the waveDelay variable
		waveTime = "" + debugConfig.waveTime;						// create the string for the waveTime variable
		foodSpeed = "" + debugConfig.foodSpeed;						// create the string for the foodSpeed variable
		oxygenDeplete = "" + debugConfig.oxygenDeplete;				// create the string for the oxygenDeplete variable
		oxygenGain = "" + debugConfig.oxygenGain;					// create the string for the oxygenGain variable
		debugActive = debugConfig.debugActive;						// create the string for the debugActive variable
	}

	/**
	 * Handles drawing the debug config gui on top of the game if it's enabled.
	 */
	void OnGUI()
	{
		GUIStyle style = GUI.skin.label;			// set the guistyle we will use to be of default type label
		style.normal.textColor = Color.black;		// change the color of the text to black
		GUI.depth -= 5;								// set the gui depth so the debug config stuff will show on top of game

		if(showGUI)									// check if we should show the gui
		{
			/******FOOD SPAWN INTERVAL*******/
			/* FOOD SPAWN INTERVAL is the amount of time between different "foodstuff"s spawning during a wave */
			// create label for foodSpawnInterval
			GUI.Label(new Rect(100, 10, 100, 50), "Food Spawn Interval", style);
			// create a textfield to enter desired foodSpawnInterval
			foodSpawnInterval = GUI.TextField(new Rect(200, 10, 100, 50),
			                                  foodSpawnInterval);
			// get the value entered into the debug menu and assign it to the debug config
			float foodSpawnIntervalOut;
			if(float.TryParse(foodSpawnInterval, out foodSpawnIntervalOut))
			{
				debugConfig.foodSpawnInterval = foodSpawnIntervalOut;
			}

			/*******OXYGEN DEPLETE RATE********/
			/* OXYGEN DEPLETE RATE controls the speed at which the oxygen bar runs out when not breathing */
			// create label for oxygenDeplete
			GUI.Label(new Rect(100, 60, 100, 50), "Oxygen Depletion Rate",style);
			// create textfield to enter desired oxygenDeplete
			oxygenDeplete = GUI.TextField(new Rect(200, 60, 100, 50),
			                              oxygenDeplete);
			// get the value entered into the debug menu and assign it to the debug config
			float oxygenDepleteOut;
			if(float.TryParse(oxygenDeplete, out oxygenDepleteOut))
			{
				debugConfig.oxygenDeplete = oxygenDepleteOut;
			}

			/*********OXYGEN GAIN RATE*******/
			/* OXYGEN GAIN RATE controls the speed at which the oxygen bar fills when breathing */
			// create lablel for oxygen Gain
			GUI.Label(new Rect(100, 110, 100, 50), "Oxygen Gain Rate",style);
			// create textfield to enter desired oxygen gain
			oxygenGain = GUI.TextField(new Rect(200, 110, 100, 50),
			                           oxygenGain);
			// get the value entered into the debug menu and assign it to the debug config
			float oxygenGainOut;
			if(float.TryParse(oxygenGain, out oxygenGainOut))
			{
				debugConfig.oxygenGain = oxygenGainOut;
			}

			/**********WAVE DELAY******/
			/* WAVE DELAY is the time between different waves in a script */
			// create label for wave delay
			GUI.Label(new Rect(100, 160, 100, 50), "Wave Delay",style);
			// create textfield to enter desired wave delay
			waveDelay = GUI.TextField(new Rect(200, 160, 100, 50),
			                          waveDelay);
			// get the value entered into the debug menu and assign it to the debug config
			float waveDelayOut;
			if(float.TryParse(waveDelay, out waveDelayOut))
			{
				debugConfig.waveDelay = waveDelayOut;
			}

			/********WAVE TIME*******/
			/* WAVE TIME is the time that a wave lasts */
			// create label for wave time
			GUI.Label(new Rect(350, 0, 100, 50), "Wave Time",style);
			// create text field to enter desired wave time
			waveTime = GUI.TextField(new Rect(450, 0, 100, 50),
			                         waveTime);
			// get the value entered into the debug menu and assign it to the debug config
			float waveTimeOut;
			if(float.TryParse(waveTime, out waveTimeOut))
			{
				debugConfig.waveTime = waveTimeOut;
			}

			/*******FOOD SPEED********/
			/* FOOD SPEED controls the rate at which each "foodstuff" moves along the track */
			// create label for food speed
			GUI.Label(new Rect(350, 50, 100, 50), "Food Speed",style);
			// create textfield to enter desired food speed
			foodSpeed = GUI.TextField(new Rect(450, 50, 100, 50),
			                          foodSpeed);
			// get the value entered into the debug menu and assign it to the debug config
			float foodSpeedOut;
			if(float.TryParse(foodSpeed, out foodSpeedOut))
			{
				debugConfig.foodSpeed = foodSpeedOut;
			}

			/*******DEBUG ACTIVE****/
			/* DEBUG ACTIVE marks whether we should use debug entered values or read from a script */
			// create a toggle (checkbox) for debug active
			debugActive = GUI.Toggle(new Rect(100, 310, 100, 20), debugActive, "Debug Active");
			// get the value from the checkbox (checked, unchecked) and assign it to the debug config
			if (debugActive)
			{
				debugConfig.debugActive = true;
			} else
			{
				debugConfig.debugActive = false;
			}
		}
	}
}