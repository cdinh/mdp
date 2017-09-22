using UnityEngine;
using System.Collections;

/**
 * script to handle cough collisions in the mouth game
 */
public class CoughCollision : MonoBehaviour 
{
	private OxygenBar oxygen;		//!< to hold a reference to the oxygen bar so we can modify values
	private openFlap flap;			//!< to hold a reference to the flap script on the flaps

	/**
	 * Use this for initialization
	 */ 
	void Start () 
	{
		// find the oxygen bar
		oxygen = GameObject.Find("MouthGUI").GetComponent<OxygenBar>();

		// get the script "openFlap" on the flaps
		flap = transform.parent.gameObject.GetComponent<openFlap>();
	}

	/**
	 * On trigger enter is called when the object enters a collision.
	 * In this script this function causes select statistics for the game to update, and performs
	 * updates to the oxygen bar and flaps based on a cough happening.
	 */
	void OnTriggerEnter(UnityEngine.Collider other)
	{
		if (!flap.isCough())
		{
			if(other.gameObject.name.Contains ("MouthFoodContainer"))	// check to see if the flaps collided with something
																// with the tag "foodstuff"
			{
				// track stats
				PlayerPrefs.SetInt("MouthStats_timesCoughed", PlayerPrefs.GetInt("MouthStats_timesCoughed") + 1);
				PlayerPrefs.Save ();

				oxygen.percent -= .07f;			// on cough we decrease oxygen by a chunk amount as a penalty

				flap.setCough();				// set the cough indicate variable on the script
			}
		}
	}
}
