using UnityEngine;
using System.Collections;

/**
 * script to handle spawning food
 */
public class SpawnFood : MonoBehaviour 
{
	private EsophagusDebugConfig debugConfig;	//!< to hold a reference to the mouth debugger script
	private LoadScriptMouth loadScript;			//!< to hold a reference to the mouth script loader
	private MouthWave[] waves;					//!< to keep track of mouthwaves

	private MouthLoadLevelCounter level;		//!< to hold a reference to the load level counter
		
	public GameObject food;						//!< to hold a reference to the gameobject we will spawn as foodstuff
	public Vector3 startingSpawn;				//!< to hold the spawn starting location

	private openFlap flap;						//!< to hold a reference to the scipt on the flaps					

	// list of variables that are in the debugger and in the script that we will populate
	public float SpawnInterval;
	private int currentWave;
	private float waveDelay;
	private float waveTime;
	private float speed;

	private float m_TimeSinceLastSpawn;			//!< to keep track of the time since the last foodstuff was spawned

	public bool end = false;					//!< flag to indicate whether we are at the end of the script

	/**
	 * Use this for initialization
	 * Verifies the level was loaded correctly and if not reloads it.
	 * Starts loading from script/debugger.
	 */
	void Start () 
	{
		level = null;		// set the MouthLoadLevelCounter to null to begin with

		// find the reference to the mouth debugger
		debugConfig = GameObject.Find("Debugger").GetComponent<EsophagusDebugConfig>();
		if (debugConfig == null)	// if we are starting the game directly we need to add debugger
		{
			debugConfig = gameObject.AddComponent<EsophagusDebugConfig>() as EsophagusDebugConfig;
		}

		// find the background chooser
		GameObject chooseBackground = GameObject.Find ("MouthChooseBackground");
		if (chooseBackground != null)	// if we did find the background chooser, get the level from it
		{
			level = chooseBackground.GetComponent<MouthLoadLevelCounter>();
		} else // in this case we started the game load game properly
		{
			// this will cause the mouth game to be properly loaded from level 1
			PlayerPrefs.SetInt("DesiredMouthLevel", 1);
			PlayerPrefs.Save();
			Application.LoadLevel("LoadLevelMouth");
		}

		// we also need to find the references to the flaps
		GameObject flaps = GameObject.Find("Flaps");		// find the reference to the flaps
		flap = flaps.GetComponent<openFlap>();				// get the openFlap script from the flaps

		if (level != null)	// guard for if level was null since this will still execute before the reloading from level 1
		{
			// finally load the script
			loadScript = new LoadScriptMouth();							// create a new script loader
			waves = loadScript.loadMouthLevel(level.getLevel());	// load the proper script to the level and store data

			// begin parsing the data
			currentWave = 0;								// reset the current wave counter to 0
			waveDelay = waves[0].startDelay;				// get the waveDelay for the current wave
			waveTime = waves[0].runTime;					// get the waveTime for the current wave
			SpawnInterval = waves[0].foodSpawnInterval;		// get the spawnInterval for the current wave
			speed = waves[0].foodSpeed;						// get the foodSpped for the current wave
			m_TimeSinceLastSpawn = 0f;						// reset the time since last spawn to 0
		}
	}
	
	/**
	 * Update is called once per frame
	 * Checks for updated wave values from the debugger and handles the spawning of food based on parsed wave data
	 */
	void Update () 
	{
		if(flap.isCough())		// if we are currently coughing, don't do any other updates for this script
		{
			return;
		}

		if(waveDelay < 0 && !end)			// if we aren't currently waiting for a delay and the script isn't over
		{
			waveTime -= Time.deltaTime;		// decrement the timer for the length of the current wave
		
			if(waveTime > 0)				// if there is still more time in the current wave continue processing it
			{
				if(debugConfig.debugActive)	// if we are using debugger values, get the spawn interval from the debugger
				{
					SpawnInterval = debugConfig.foodSpawnInterval;
				}
				m_TimeSinceLastSpawn += Time.deltaTime;	// increase the timer counting the time since the last spawn
				
				if (m_TimeSinceLastSpawn >= SpawnInterval)	// if the time since the last spawn exceeds the spawn interval
															// we need to spawn a new foodstuff
				{
					// create a new foodstuff instance
					GameObject foodInstance = (GameObject)Instantiate(food, startingSpawn, Quaternion.identity);
					// get the script on the foodstuff to change some values
					MoveFood moveFoodScript = foodInstance.GetComponent<MoveFood>();
					moveFoodScript.foodSpeed = speed;		// set the speed the foodstuff will move

					m_TimeSinceLastSpawn = 0;				// reset the time since last spawn timer
				}
			}
			else 							// otherwise, if the time for the current wave is over, move to the next one
			{
				if(debugConfig.debugActive)	// check if we are using values from the debugger, and if we are get the values
				{
					// get the values from the debugger
					waveDelay = debugConfig.waveDelay;
					waveTime = debugConfig.waveTime;
					SpawnInterval = debugConfig.foodSpawnInterval;
					speed = debugConfig.foodSpeed;

					m_TimeSinceLastSpawn = 0;	// reset the timeSinceLastSpawn counter
				}
				else 						// otherwise we are getting the values from the script
				{
					currentWave++;						// increase the counter for the current wave we are on


					if(currentWave == waves.Length)		// check if the script is done, if it is throw the flag
					{
						end = true;
					} 
					else 								// otherwise get the values for the next wave from the script
					{

						// get variable values from the script
						waveDelay = waves[currentWave].startDelay;
						waveTime = waves[currentWave].runTime;
						SpawnInterval = waves[currentWave].foodSpawnInterval;
						speed = waves[currentWave].foodSpeed;

						m_TimeSinceLastSpawn = 0;		// reset the timeSinceLastSpawn counter
					}
				}
			}
		}
		else
		{
			waveDelay -= Time.deltaTime;	// count down the wave delay timer if we have a delay between waves
		}
	}
}
