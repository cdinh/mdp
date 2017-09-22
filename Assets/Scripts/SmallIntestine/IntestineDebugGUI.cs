using UnityEngine;
using System.Collections;

/**
 * script that creates the debug gui for the small intestine game
 */
public class IntestineDebugGUI : MonoBehaviour 
{	
	private DebugConfig debugConfig;				//!< to hold a reference to the debugconfig script

	private bool showGUI = false;					//!< flag that holds whether the gui should be displayed or hidden

	private string speed;							//!< to store the string representation of the speed variable
	private string spawnTime;						//!< to store the string representation of the spawnTime variable
	private string towerBaseCost;					//!< to store the string representation of the towerBaseCost variable
	private string towerLvl1Cost;					//!< to store the string representation of the towerLvl1Cost variable
	private string towerLvl2Cost;					//!< to store the string representation of the towerLvl2Cost variable
	private string towerLvl1Targets;				//!< to store the string representation of the towerLvl1Targets variable
	private string towerLvl2Targets;				//!< to store the string representation of the towerLvl2Targets variable
	private string towerBaseCD;						//!< to store the string representation of the towerBaseCD variable
	private string towerLvl1CD;						//!< to store the string representation of the towerLvl1CD variable
	private string towerLvl2CD;						//!< to store the string representation of the towerLvl2CD variable
	private string waveTimer;						//!< to store the string representation of the waveTimer variable
	private string waveDelay;						//!< to store the string representation of the waveDelay variable
	private string minBlobs;						//!< to store the string representation of the minBlobs variable
	private string maxBlobs;						//!< to store the string representation of the maxBlobs variable
	private string colors;							//!< to store the string representation of the colors code
	private bool FPSActive;							//!< to store the flag of whether we should show the fps or not

	private bool debugActive = false;				//!< to store the flag of whether we should use debugger values

	public GUIStyle fontStyle;						//!< for the font

	/**
	 * Use this for initialization
	 * Finds the debugger and populates all the text representations of values
	 */
	void Start () 
	{
		debugConfig = gameObject.GetComponent<DebugConfig>();			// find a reference to the script on the debugger
	
		// get values from the debugger for the variables
		speed = "" + debugConfig.NutrientSpeed;							// populate the string representation of speed
		spawnTime = "" + debugConfig.NutrientSpawnInterval;				// populate the string representation of spawnTime
		towerBaseCost = "" + debugConfig.TOWER_BASE_COST;				// populate the string representation of towerBaseCost
		towerLvl1Cost = "" + debugConfig.TOWER_UPGRADE_LEVEL_1_COST;	// populate the string representation of towerLvl1Cost
		towerLvl2Cost = "" + debugConfig.TOWER_UPGRADE_LEVEL_2_COST;	// populate the string representation of towerLvl2Cost
		towerLvl1Targets = "" + debugConfig.level1Targets;				// populate the string representation of towerLvl1Targets
		towerLvl2Targets = "" + debugConfig.level2Targets;				// populate the string representation of towerLvl2Targets
		towerBaseCD = "" + debugConfig.BaseCooldown;					// populate the string representation of towerBaseCD
		towerLvl1CD = "" + debugConfig.Level1Cooldown;					// populate the string representation of towerLvl1CD
		towerLvl2CD = "" + debugConfig.Level2Cooldown;					// populate the string representation of towerLvl2CD
		waveTimer = "" + debugConfig.waveTimer;							// populate the string representation of waveTimer
		waveDelay = "" + debugConfig.waveDelay;							// populate the string representation of waveDelay
		minBlobs = "" + debugConfig.minBlobs;							// populate the string representation of minBlobs
		maxBlobs = "" + debugConfig.maxBlobs;							// populate the string representation of maxBlobs
		colors = "";
		// for populating the colors string represenation
		if(debugConfig.colors.Contains(Color.red))
			colors += "R";
		if(debugConfig.colors.Contains(Color.yellow))
			colors += "Y";
		if(debugConfig.colors.Contains(Color.green))
			colors += "G";
	}

	/**
	 * Handles drawing of the debug GUI on top of the SI game when it is enabled
	 */
	void OnGUI()
	{
		// check if we're NOT showing the gui
		if (!showGUI)			// if we aren't showing the gui then make it draw below everything so it isn't seen
		{
			GUI.depth ++;
		}

	//	GUI.color = new Color() { a = 0.0f };	// this makes the button invisible by changing the alpha value

		// to handle showing the button for accessing the debugger in the bottom right corner of the small intestine game
		//Change width-100/+100 to enable/disable debugger
	//	if(GUI.Button(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Debug"))
	//	{
	//		showGUI = !showGUI;					// when the button is clicked show or unshow the gui
	//		Time.timeScale = showGUI ? 0 : 1;	// pause the game when the gui is up, unpause it if it's not shown
	//	}
	//	GUI.color = new Color() { a = 1.0f };	// this makes sure the invisibility isn't applied to anything we don't want

		// check if we ARE showing the gui
		if(showGUI)
		{
			/***********SPEED********/
			// SPEED refers to the speed at which nutrients move down the track in the si game (aka "nutrient speed")
			// create label for speed
			GUI.Label(new Rect(100, 10, 100, 50), "Nutrient Speed", fontStyle);
			// create a textfield for the user to enter the desired nutrient speed
			speed = GUI.TextField(new Rect(200, 10, 100, 50),
			                      speed);
			// attempt to get the value entered for nutrient speed into the debug menu and assign it to the debug config
			float nutrientSpeedOut;
			if(float.TryParse(speed, out nutrientSpeedOut))
			{
				debugConfig.NutrientSpeed = nutrientSpeedOut;
			}

			/***********SPAWN TIME************/
			// SPAWN TIME refers to the time between each food blob spawn (aka "Nutrient Interval")
			// create label for spawnTime
			GUI.Label(new Rect(100, 60, 100, 50), "Nutrient Interval", fontStyle);
			// create a textfield for the user to enter the desired spawn interval
			spawnTime = GUI.TextField(new Rect(200, 60, 100, 50),
			                          spawnTime);
			// attempt to get the value entered for the spawnTime into the debug menu and assign it to the debug config
			float nutrientSpawnIntervalOut;
			if(float.TryParse(spawnTime, out nutrientSpawnIntervalOut))
			{
				debugConfig.NutrientSpawnInterval = nutrientSpawnIntervalOut;
			}

			/************TOWER BASE COST********/
			// TOWER BASE COST refers to the base cost of a tower. This is how much a tower costs without any upgrades
			// create a label for towerBaseCost
			GUI.Label(new Rect(100, 110, 100, 50), "Tower Base Cost", fontStyle);
			// create a textfield for the user to enter the desired towerBaseCost
			towerBaseCost = GUI.TextField(new Rect(200, 110, 100, 50),
			                              towerBaseCost);
			// attempt to get the value entered for the towerBaseCost into the debug menu and assign it to the debug config
			int towerBaseCostOut;
			if(int.TryParse(towerBaseCost, out towerBaseCostOut))
			{
				debugConfig.TOWER_BASE_COST = towerBaseCostOut;
			}

			/*******TOWER LEVEL 1 COST********/
			// TOWER LEVEL 1 COST refers to the cost to upgrade a tower from base to the first upgrade level
			// create a label for towerLvl1Cost
			GUI.Label(new Rect(100, 160, 100, 50), "Tower Lvl 1 Cost", fontStyle);
			// create a textfield for the user to enter the desired towerLvl1Cost
			towerLvl1Cost = GUI.TextField(new Rect(200, 160, 100, 50),
			                              towerLvl1Cost);
			// attempt to get the value entered for the towerLvl1Cost into the debug menu and assign it to the debug config
			int towerLvl1CostOut;
			if(int.TryParse(towerLvl1Cost, out towerLvl1CostOut))
			{
				debugConfig.TOWER_UPGRADE_LEVEL_1_COST = towerLvl1CostOut;
			}

			/*********TOWER LEVEL 2 COST********/
			// TOWER LEVEL 2 COST refers to the cost to upgrade a tower from a first level upgrade to a 2nd level upgrade
			// create a label for towerLvl2Cost
			GUI.Label(new Rect(100, 210, 100, 50), "Tower Lvl 2 Cost", fontStyle);
			// create a textfield for the user to enter the desired towerLvl2Cost
			towerLvl2Cost = GUI.TextField(new Rect(200, 210, 100, 50),
			                              towerLvl2Cost);
			// attempt to get the value entered for the towerLvl2Cost into the debug menu and assign it to the debug config
			int towerLvl2CostOut;
			if(int.TryParse(towerLvl2Cost, out towerLvl2CostOut))
			{
				debugConfig.TOWER_UPGRADE_LEVEL_2_COST = towerLvl2CostOut;
			}

			/*******TOWER BASE COOLDOWN********/
			// TOWER BASE COOLDOWN refers to the cooldown between shots on a non-upgraded tower (or power towers)
			// create a label for towerBaseCD
			GUI.Label(new Rect(350, 10, 100, 50), "Tower Base CD", fontStyle);
			// create a textfield for the user to enter the desired towerBaseCD
			towerBaseCD = GUI.TextField(new Rect(450, 10, 100, 50),
			                            towerBaseCD);
			// attempt to get the value entered for the towerBaseCD into the debug menu and assign it to the debug config
			float towerBaseCDOut;
			if(float.TryParse(towerBaseCD, out towerBaseCDOut))
			{
				debugConfig.BaseCooldown = towerBaseCDOut;
			}

			/******TOWER LEVEL 1 COOLDOWN****/
			// TOWER LEVEL 1 COOLDOWN refers to the cooldown between shots for 1st level upgraded speed towers
			// create a label for towerLvl1CD
			GUI.Label(new Rect(350, 60, 100, 50), "Tower Lvl 1 CD", fontStyle);
			// create a textfield for the user to enter the desired towerLvl1CD
			towerLvl1CD = GUI.TextField(new Rect(450, 60, 100, 50),
			                            towerLvl1CD);
			// attempt to get the value entered for the towerLvl1CD in the debug menu and assign it to the debug config
			float towerLvl1CDOut;
			if(float.TryParse(towerLvl1CD, out towerLvl1CDOut))
			{
				debugConfig.Level1Cooldown = towerLvl1CDOut;
			}

			/******TOWER LEVEL 2 COOLDOWN****/
			// TOWER LEVEL 2 COOLDOWN refers to the cooldown between shots for a 2nd level upgraded speed tower
			// create a label for towerLvl2CD
			GUI.Label(new Rect(350, 110, 100, 50), "Tower Lvl 2 CD", fontStyle);
			// create a textfield for the user to enter the desired towerLvl2CD
			towerLvl2CD = GUI.TextField(new Rect(450, 110, 100, 50),
			                            towerLvl2CD);
			// attempt to get the value entered for the towerLvl2CD in the debug menu and assign it to the debug config
			float towerLvl2CDOut;
			if(float.TryParse(towerLvl2CD, out towerLvl2CDOut))
			{
				debugConfig.Level2Cooldown = towerLvl2CDOut;
			}

			/*********FPS ACTIVE*******/
			// FPS ACTIVE is a flag to set whether or not to show the fps on the top left corner of the screen in the si game
			// create a gui toggle for the FPSActive
			FPSActive = GUI.Toggle(new Rect(350, 160, 100, 20), FPSActive, "Toggle FPS");
			if (FPSActive)
			{
				debugConfig.FPSActive = true;	// if the box is checked, this means show it
			} else
			{
				debugConfig.FPSActive = false;	// if the box is not checked, don't show it
			}

			/*******DEBUG ACTIVE*****/
			// DEBUG ACTIVE is a flag to set whether we should read values from the debugger or from a script
			// create a gui toggle for the DebugActive
			debugActive = GUI.Toggle(new Rect(350, 180, 100, 20), debugActive, "Debug Active");
			if (debugActive)
			{
				debugConfig.debugActive = true;	// if the box is checked, we do use values from the debugger
			} else
			{
				debugConfig.debugActive = false;	// if the box is not checked we use the values from the script
			}

			/******WAVE TIMER*******/
			// WAVE TIMER refers to the length of time a wave should run with the set values
			// create a label for waveTimer
			GUI.Label(new Rect(350, 210, 100, 50), "Wave Timer", fontStyle);
			// create a textfield for the user to enter the desired value for waveTimer
			waveTimer = GUI.TextField(new Rect(450, 210, 100, 50),
			                          waveTimer);
			// attempt to get the value entered for the waveTimer in the debug menu and assign it to the debug config
			float waveTimerOut;
			if(float.TryParse(waveTimer, out waveTimerOut))
			{
				debugConfig.waveTimer = waveTimerOut;
			}

			/*******WAVE DELAY****/
			// WAVE DELAY refers to the time waited between different waves
			// create a label for waveDelay
			GUI.Label(new Rect(550, 60, 100, 50), "Wave Delay", fontStyle);
			// crate a textfield for the user to enter the desired value for waveDelay
			waveDelay = GUI.TextField(new Rect(650, 60, 100, 50),
			                          waveDelay);
			// attempt to get the value entered for the waveDelay in the debug menu and assign it to the debug config
			float waveDelayOut;
			if(float.TryParse(waveDelay, out waveDelayOut))
			{
				debugConfig.waveDelay = waveDelayOut;
			}

			/*******MIN BLOBS******/
			// MIN BLOBS refers to the minimum number of nutrients on a food blob
			// create a lable for minBlobs
			GUI.Label(new Rect(550, 110, 100, 50), "Min Blobs", fontStyle);
			// create a textfield for the user to enter the desired value for minBlobs
			minBlobs = GUI.TextField(new Rect(650, 110, 100, 50),
			                         minBlobs);
			// atempt to get the value entered for the minBlobs in the debug menu and assign it to the debug config
			int minBlobsOut;
			if(int.TryParse(minBlobs, out minBlobsOut))
			{
				debugConfig.minBlobs = minBlobsOut;
			}

			/*******MAX BLOBS*******/
			// MAX BLOBS refers to the maximum number of nutrients on a food blob
			// create a label for maxBlobs
			GUI.Label(new Rect(550, 160, 100, 50), "Max Blobs", fontStyle);
			// create a textfield for the user to enter the desired value for maxBlobs
			maxBlobs = GUI.TextField(new Rect(650, 160, 100, 50),
			                         maxBlobs);
			// attempt to get the value for the maxBlobs in the debug menu and assign it to the debug config
			int maxBlobsOut;
			if(int.TryParse(maxBlobs, out maxBlobsOut))
			{
				debugConfig.maxBlobs = maxBlobsOut;
			}

			/*****COLORS*****/
			// COLORS is a list of the colors of nutrients that can be on the food blob. valid entries are only R, Y, G
			// create the label for colors
			GUI.Label(new Rect(550, 210, 100, 50), "Colors (RYG)", fontStyle);
			// create a textfield for the user to enter the desired colors
			colors = GUI.TextField(new Rect(650, 210, 100, 50),
			                       colors);
			ArrayList colorsOut = new ArrayList();		// an arraylist to store the entered colors
			colors = colors.ToLower();					// force the user entry to lower case to make it easier to parse
			// check if the user entered an "r" and if they did add the color "red" to the arraylist
			if(colors.Contains("r"))
			{
				colorsOut.Add(Color.red);
			}
			// check if the user entered a "y" and if they did add the color "yellow" to the arrayList
			if(colors.Contains("y"))
			{
				colorsOut.Add(Color.yellow);
			}
			// check if the user entered a "g" and if they did add the color "green" to the arrayList
			if(colors.Contains("g"))
			{
				colorsOut.Add(Color.green);
			}
			debugConfig.colors = colorsOut;	// assign the color choices to the debug config

			/******TOWER LEVEL 1 TARGETS******/
			// TOWER LEVEL 1 TARGETS refers to the number of targets a once upgraded power tower can hit
			// create a label for towerLvl1Targets
			GUI.Label(new Rect(550, 260, 100, 50), "Level 1 Targets", fontStyle);
			// create a textfield for the user to enter the desired value for towerLvl1Targets
			towerLvl1Targets = GUI.TextField(new Rect(650, 260, 100, 50),
			                                 towerLvl1Targets);
			// attempt to get the value for the towerLvl1Targets in the debug menu and assign it to the debug config
			int towerLvl1TargetsOut;
			if(int.TryParse(towerLvl1Targets, out towerLvl1TargetsOut))
			{
				debugConfig.level1Targets = towerLvl1TargetsOut;
			}

			/*******TOWER LEVEL 2 TARGETS********/
			// TOWER LEVEL 2 TARGETS refers to the number of targets a twice upgraded power tower can hit
			// create a label for towerLvl2Targets
			GUI.Label(new Rect(550, 310, 100, 50), "Level 2 Targets", fontStyle);
			// create a textfield for the user to enter the desired value for towerLvl2Targets
			towerLvl2Targets = GUI.TextField(new Rect(650, 310, 100, 50),
			                                 towerLvl2Targets);
			// attempt to get the value for the towerLvl2Targets in the debug menu and assign it to the debug config
			int towerLvl2TargetsOut;
			if(int.TryParse(towerLvl2Targets, out towerLvl2TargetsOut))
			{
				debugConfig.level2Targets = towerLvl2TargetsOut;
			}
		}
	}
}
