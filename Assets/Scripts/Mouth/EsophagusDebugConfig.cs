using UnityEngine;
using System.Collections;

/**
 * script to hold debug config variables for the mouth game
 */
public class EsophagusDebugConfig : MonoBehaviour 
{
	// list of all debug config values with default values
	public float oxygenDeplete = .05f;			//!< oxygen deplete rate defaults to .05% per time unit
	public float oxygenGain = .05f;				//!< oxygen gain rate defaults to .05% per time unit
	public bool debugActive = false;			//!< by default, we don't use the debugger, so it's set to false
	public float foodSpawnInterval = .5f;		//!< food spawn interval, the interval between food blobs, is defaulted to one blob every .5sec
	public float waveDelay = 1f;				//!< wave delay, the time delay between different waves, is defaulted to 1sec
	public float waveTime = 5f;					//!< wave time, or the time a wave continues, is defaults to 5sec
	public float foodSpeed = 3f;				//!< food speed, the speed at which food moves down the path, is defaulted to 3
}
