using UnityEngine;
using System.Collections;

/**
 * script to store the variables for the small intestine debugger
 */
public class DebugConfig : MonoBehaviour 
{
	// debug variables for the small intestine game along with their default values
	public float NutrientSpeed = 2f;				//!< nutrient speed, the speed the food blobs move down the path, defaults to 2
	public float NutrientSpawnInterval = 3.5f;		//!< nutrient spawn interval, the time between food blob spawns, defaults to 3.5sec between spawns
	public float BaseCooldown = 2f;					//!< base cooldown, the time between bullets for a tower, defaults to 2sec
	public float Level1Cooldown = 1f;				//!< level1cooldown, the time between bullets for a speed1 tower, defaults to 1sec
	public float Level2Cooldown = .5f;				//!< level2cooldown, the time between bullets for a speed2 tower, defaults to .5sec 
	public int baseTargets = 1;						//!< base targets for a tower defaults to 1
	public int level1Targets = 3;					//!< level1Targets, # targets for a power1 tower, defaults to 3
	public int level2Targets = 6;					//!< level2Targets, # targets for a power2 tower, defaults to 6
	public int TOWER_BASE_COST = 20;				//!< cost of placing a tower, 20 nutrients default
	public int TOWER_UPGRADE_LEVEL_1_COST = 50;		//!< cost of upgrading a tower from base to 1 level, 50 nutrients default
	public int TOWER_UPGRADE_LEVEL_2_COST = 50;		//!< cost of upgrading a tower from level 1 to level 2, 50 nnutrients default
	public bool FPSActive = false;					//!< whether we should show the fps, defaults to false
	public bool debugActive = false;				//!< whether we should use the debugger values, defaults to false
	public float waveTimer = 30;					//!< wave timer, the length of a wave, defaults to 30 seconds
	public float waveDelay = 10;					//!< waveDelay, the break between waves, defaults to 10 seconds
	public int minBlobs = 1;						//!< min blobs, the min nutrients that will spawn on a food blob, defaults to 1
	public int maxBlobs = 5;						//!< max blobs, the max nutrients that will spawn on a food blob, defaults to 5

	public ArrayList colors = new ArrayList();			//!< create a new arraylist to store the colors
	private Color Fats1Color = new Color(37f/255f, 97f/255f, 139f/255f, 1); 	//!< create a new color for the Fats1 Particles
    private Color ProColor = new Color(235f / 255f, 0f, 139f / 255f);
    /**
	 * Use this for initialization
	 */
    void Start () 
	{
		// add the possible starting nutrient colors to the arraylist
		colors.Add(ProColor);
		colors.Add(Color.yellow);
		colors.Add(Fats1Color);
        Debug.Log(colors[0]);
	}
}
