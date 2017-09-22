using UnityEngine;
using System.Collections;

/**
 * script to handle general mouth game things
 */
public class MouthGameManager : MonoBehaviour 
{
	/*
	 * Use this for initialization.
	 * Initializes the stats tracker.
	 */
	void Start () 
	{
		resetStats ();		// reset the stats for each time a game level is loaded
	}

	/*
	 * Function that resets the mouth game stats in player prefs
	 */
	void resetStats()
	{
		// resets the stats relevant to the mouth game and saves changes
		// if any other specific mouth stats are tracked in the future they should be added here
		PlayerPrefs.DeleteKey("MouthStats_longestStreak");
		PlayerPrefs.DeleteKey("MouthStats_timesCoughed");
		PlayerPrefs.DeleteKey("MouthStats_foodLost");
		PlayerPrefs.DeleteKey("MouthStats_foodSwallowed");
		PlayerPrefs.DeleteKey("MouthStats_highestMultiplier");
		PlayerPrefs.DeleteKey("MouthStats_score");
		PlayerPrefs.Save();
	}
}
