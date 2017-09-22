using UnityEngine;
using System.Collections;
/* script to handle loading the Stomach game
	*/
		
public class LoadStomach : MonoBehaviour 
{
	private GameObject counter;				//!< to keep a reference to the level counter
	
	public Texture[] backgrounds;			//!< an array of the backgrounds for the mouth game load screens
	private StomachLoadLevelCounter level;	//!< to hold a reference to the mouthloadlevelcounter
	
	private const float timer = 3.0f;		//!< how long to hold background image (force a longer load time)
	private float timePassed = 0.0f;		//!< to keep track of how long has actually passed
	
	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		timePassed = timer;					// assign the timer value to timepassed (we will count time down rather than
		// counting it up)
	}
	
	/**
	 * Update is called once per frame
	 * Used to count time between frames to see if it's time to switch to the game
	 */
	void Update () 
	{
		timePassed -= Time.deltaTime;		// decrement the timepassed variable by how much time since the last update call
		
		if (timePassed < 0)					// if the timepassed has been longer than that in timer then load the game 
		{
			Application.LoadLevel("Stomach");	// load the mouth game
		}
	}
	
	/**
	 * Draws the background image for the mouth loading screen
	 */
	void OnGUI()
	{
		counter = GameObject.Find ("StomachChooseBackground");			// find the reference to the mouth background choser
		level = counter.GetComponent<StomachLoadLevelCounter> ();			// get the current level from the counter
		
		// draw the proper level load screen to take up the entiere screen
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), 
		                 backgrounds [Mathf.Clamp(level.getLevel() - 1, 0, level.getMaxLevels())]);
	}
}
