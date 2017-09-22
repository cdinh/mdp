using UnityEngine;
using System.Collections;

/**
 * manager class to handle spawning waves in the si game
 */
public class SpawnManager : MonoBehaviour 
{
	public GameObject SpawnType;				//!< to hold a reference to the thing that will be spawning (foodblobparent)
	public Vector3 SpawnPoint;                  //!< a vector to hold the initial spawn location

    public int ColorAllowed;


	private DebugConfig debugConfig;			//!< to hold a reference to the debug config script (for values from debugger)
	private LoadScript loadScript;				//!< to hold a reference to the script loader (for values from script)
	private SIWave[] waves;						//!< an array to hold parsed wave data

	private SmallIntestineLoadLevelCounter level; //!< for holding the level counter to determine level info

	private FoodBlob blobScript;				//!< holds a newly spawned food blob
	private FollowITweenPath path;				//!< holds a reference to the path in the SI game so the food can follow it

	public float SpawnInterval; 				//!< to hold the value set for the spawn interval
	private int currentWave;					//!< remember what wave we are on
	private float waveDelay;					//!< to hold the value set for the wave delay
	private float waveTime;						//!< to hold the value set for the wave time
	private float speed;						//!< to hold the value set for the speed
	private int minNutrients;					//!< to hold the value set for the minNutrients (minBlobs)
	private int maxNutrients;					//!< to hold the value set for the maxNutrients (maxBlobs)
	Color[] availableColors;					//!< to hold the colors we can choose to spawn from

	private float m_TimeSinceLastSpawn;			//!< to remember the time elapsed since the last spawn (for use with spawnInterval)

	public bool end = false;					//!< to remember if we are done reading new waves from a script

	/**
	 * Use this for initialization
	 * Check if game was loaded properly. If not reload.
	 * Start spawning waves.
	 */
	void Start () 
	{
		level = null;				// initially set the reference to null

		GameObject counter = GameObject.Find ("ChooseBackground");	// try to find an instance of the background chooser
	
		if (counter != null)					// if the counter is not null we can continue as normal with initialization
		{
			// if the counter was there we can just get the level
			level = counter.GetComponent<SmallIntestineLoadLevelCounter> ();
		} else // if we start the level from the game itself just start at level 0, 1, or 2 appropriately
		{ 
			// this part should only happen if we start the game from the wrong scene in the unity editor
			// if the counter wasn't there reload properly
			// this will reload the entire game from the level it was supposed to be loaded from
			if (Application.loadedLevelName == "SmallIntestineTutorial")
			{	
				// if we load directly from the tutorial scene, then reload the si game from level 0
				PlayerPrefs.SetInt("DesiredSILevel", 0);
				PlayerPrefs.Save();
				Application.LoadLevel("LoadLevelSmallIntestine");
			} else if (Application.loadedLevelName == "SmallIntestineOdd")
			{
				// if we load directly from the si odd scene, then reload the si game from level 1
				PlayerPrefs.SetInt("DesiredSILevel", 1);
				PlayerPrefs.Save();
				Application.LoadLevel("LoadLevelSmallIntestine");
			} else if (Application.loadedLevelName == "SmallIntestineEven")
			{
				// if we load directly from the si even scene, then reload the game from level 2
				PlayerPrefs.SetInt("DesiredSILevel", 2);
				PlayerPrefs.Save();
				Application.LoadLevel("LoadLevelSmallIntestine");;
			}
		}

		// load in the script info
		if (counter != null)	// guard check to prevent this code from executing before the game is reloaded
		{
			// start loading in the script 
			loadScript = new LoadScript();								// create a new script loader

			//if (!level.getTutorial ()) {									//if not tutorial
				
			waves = loadScript.loadIntestineLevel (level.getLevel (), level.isTutorial(), level.getTutorialNum());	// get the waves for the correct script
			
			currentWave = 0;											// set the current wave index to 0

			waveDelay = waves[0].startDelay;							// get the start delay from the first parsed wave
			waveTime = waves[0].runTime;								// get the run time from the first parsed wave
			SpawnInterval = waves[0].nutrientSpawnInterval;				// get the nutrient spawn interval from the first parsed wave
			speed = waves[0].nutrientSpeed;								// get the nutrient speed from the first parsed wave
			availableColors = waves[0].colors;							// get the colors for the first parsed wave
			minNutrients = waves[0].minBlobs;							// get the min blobs for the first parsed wave
			maxNutrients = waves[0].maxBlobs;							// get the max blobs for the first parsed wave

	        m_TimeSinceLastSpawn = 0f;									// start the timesincelastspawn variable to 0						
		}

		// we don't use debug config in the tutorial level so check that we aren't in the tutorial level before
		// we look for a reference to the debugger to get the debug script
		if (Application.loadedLevelName != "SmallIntestineTutorial")
		{
			debugConfig = ((GameObject)GameObject.Find("Debug Config")).GetComponent<DebugConfig>();
		}
	}
	
	/**
	 * Update is called once per frame
	 * Go through parsed wave data and perform the actions dictated
	 */
	void Update () 
	{
		if(waveDelay < 0 && !end)			// check if we're not waiting for a delay or the script is not over
		{
			waveTime -= Time.deltaTime;		// decrement the timer timing how long a wave lasts

			if(waveTime > 0)				// check if there is still time in the wave
			{
				// if there is still time in the wave 
				if (Application.loadedLevelName != "SmallIntestineTutorial")	// make sure we aren't in the tutorial
				{
					if(debugConfig.debugActive)	// if we aren't in the tutorial and we are using the debugger
					{
						SpawnInterval = debugConfig.NutrientSpawnInterval;	// get the value of spawninterval from the debugger
					}
				}

				m_TimeSinceLastSpawn += Time.deltaTime;		// increment the timer timing the time since the last food blob spawn

		        if (m_TimeSinceLastSpawn >= SpawnInterval)	// check if the timesincelast spawn is greater than the spawn interval
		        {
					// if it is
		            SpawnBlob();				// spawn a new blob
		            m_TimeSinceLastSpawn = 0;	// reset the time since last spawn timer
		        }
			}
			else 			// otherwise the current wave is over because waveTime <= 0
			{
				// check if we aren't in the tutorial and that we are using debugger values
				// if we are load the values from the debugger
				if(Application.loadedLevelName != "SmallIntestineTutorial" && debugConfig.debugActive)
				{
					// loading values from the debugger
					waveDelay = debugConfig.waveDelay;
					waveTime = debugConfig.waveTimer;
					SpawnInterval = debugConfig.NutrientSpawnInterval;
					speed = debugConfig.NutrientSpeed;
                    if (level.getLevel() != 1)
                    {
                        availableColors = new Color[debugConfig.colors.Count];
                        for (int i = 0; i < debugConfig.colors.Count; i++)
                            availableColors[i] = (Color)debugConfig.colors[i];
                    }
                    else
                    {
                        availableColors = new Color[ColorAllowed];
                        for (int i = 0; i < ColorAllowed; i++)
                            availableColors[i] = (Color)debugConfig.colors[i];

                    }
                    
                        minNutrients = debugConfig.minBlobs;
					maxNutrients = debugConfig.maxBlobs;

					m_TimeSinceLastSpawn = 0;		// reset the timesincelastspawn timer
				}
				else 	// otherwise we are either in the tutorial or not using the debugger so we 
						// are loading values from a script
				{
					currentWave++;						// increase the current wave counter
					if(currentWave == waves.Length)		// check if the script is over, and if it is set the flag to indicate
					{
						end = true;
					}
					else 								// if the script is not over its safe to load the values
					{
						// load the values from the script
						waveDelay = waves[currentWave].startDelay;
						waveTime = waves[currentWave].runTime;
						SpawnInterval = waves[currentWave].nutrientSpawnInterval;
						speed = waves[currentWave].nutrientSpeed;
                        if (level.getLevel() != 1)
                            availableColors = waves[currentWave].colors;
                        else
                        {
                            availableColors = new Color[ColorAllowed];
                            for (int i = 0; i < ColorAllowed; i++)
                                availableColors[i] = (Color)debugConfig.colors[i];

                        }

                        minNutrients = waves[currentWave].minBlobs;
						maxNutrients = waves[currentWave].maxBlobs;

						m_TimeSinceLastSpawn = 0;	// reset the timesincelastspawn timer
					}
				}
			}
		}
		else // otherwise we might be waiting on the wave delay so decrement the wave delay timer
		{
			waveDelay -= Time.deltaTime;
		}
	}

	/**
	 * function that handles spawning the food blob
	 */
    private void SpawnBlob()
    {
		// instantiate a blob
        GameObject blob = (GameObject)Instantiate(SpawnType, SpawnPoint, Quaternion.identity);
		// add an itween path to the blob

		path = blob.GetComponent<FollowITweenPath>();
		path.nutrientSpeed = speed;									// set the food blob's speed on the path

		blobScript = blob.GetComponent<FoodBlob>();		// get the script on the foodblob
		// call the function to generateenzymes on the blob
		blobScript.GenerateEnzymes(minNutrients, maxNutrients, availableColors);	
    }
}
