using UnityEngine;
using System.Collections;

/**
 * script class that helps with loading and keeping track of the proper mouth level
 */
public class MouthLoadLevelCounter : MonoBehaviour 
{
	private int level;			//!< variable to hold the level
	public int MAX_LEVEL = 2; 	//!< for bounds checking

	/**
	 * Use this for initialization.
	 * Checks how the game is starting and makes sure the correct level is loaded.
	 */
	void Start () 
	{
		level = PlayerPrefs.GetInt ("DesiredMouthLevel");		// pull the desired mouth level from playerprefs
		
		if (level != -1)										// check if the level is -1
																// -1 means that we are starting the game fresh
		{
			// reset the variable for later use
			PlayerPrefs.SetInt("DesiredMouthLevel", -1);		// after we read rewrite the default value to stats
			PlayerPrefs.Save ();
		} else
		{
			// if it was -1 then we are starting as normal which means the current level is 1
			level = 1;
		}

		// find if there is more than one background chooser alive, if there is destroy this one
		if (GameObject.FindGameObjectsWithTag ("mouthBackgroundChooser").Length > 1)
		{
			Destroy (gameObject);
		}
	}
	
	/**
	 * Update is called once per frame
	 * Makes sure we are in the correct scene to be using this script, otherwise destroys it since it's not needed.
	 */
	void Update () 
	{
		string scene = Application.loadedLevelName;	// get the name of the current scene to verify we should use this

		// check if we are in a scene where it is appropriate to be using this script
		// since this script does not destroy when we change scenes, we have to check to make sure we are still
		// in a valid mouth game related scene to determine if we should manually destroy this instance of the script
		if (scene != "LoadLevelMouth" && scene != "Mouth" && scene != "MouthStats")
		{
			Destroy (gameObject);
		}
	}

	/**
	 * Called when scenes are switched
	 */
	void Awake() 
	{
		// this prevents this gameobject from being destroyed when we switch scenes
		DontDestroyOnLoad(transform.gameObject);
	}
	
	/**
	 * Allow manual level changing forced by a menu rather than automatic transitions
	 */
	public void manualSetLevel(int newLevel)
	{
		// check if the desired level is valid, and if it is allow the change
		if (newLevel > 0 && newLevel <= MAX_LEVEL)
		{
			level = newLevel;
		}
	}

	/**
	 * For moving on to the next level
	 */
	public void nextLevel()
	{
		level++;	// just increase the level count by 1
	}
	
	/**
	 * For reseting game
	 */
	public void resetLevel()
	{
		level = 1;	// resetting means we should be back on level 1
	}

	/**
	 * For returning the current value stored in level
	 */
	public int getLevel()
	{
		return level;	
	}

	/**
	 * For returning the current value stored in max_level
	 */
	public int getMaxLevels()
	{
		return MAX_LEVEL;	// just return the value of the MAX_LEVEL bounds checker
	}
}
