using UnityEngine;
using System.Collections;

/**
 * script that handles the behavior of the oxygen bar
 */
public class OxygenBar : MonoBehaviour 
{
	private Vector2 position;					//!< variable to hold the position of the oxygenBar
	private Vector2 originalSize;				//!< variable to hold the size of the oxygenBar

	public Texture[] oxygenBar;					//!< to hold the texture of the oxygen bar
	public float percent;						//!< the current percentage the oxygen bar is filled (between 0 and 1)
	private float previousPercent;

	private openFlap flap;								//!< to hold a reference to the script on the flaps
	private EsophagusDebugConfig config;				//!< to hold a reference to the debug config in the mouth game

	private float depletionRate = .05f;					//!< to hold the oxygen depletion rate
	private float gainRate = .05f;						//!< to hold the oxygen gain rate

	public Texture AHAHAPopupTexture;
	public Texture YAYPopupTexture;
	private bool AhahaPopup;
	private bool YayPopup;
	private float timer;

	/**
	 * Use this for initialization
	 * Find references, initialize variables
	 */
	void Start () 
	{
		percent = 1f;												// the oxygen bar should start off at 100%
		previousPercent = percent;

		GameObject flaps = GameObject.Find("Flaps");				// find the flaps reference
		flap = flaps.GetComponent<openFlap>();						// get the script from the flaps

		GameObject debugger = GameObject.Find("Debugger");			// find a reference to the debugger
		config = debugger.GetComponent<EsophagusDebugConfig>();		// get the script from the debug config

	}

	/**
	 * Update is called once per frame
	 * Finds out if flap is open or not and updates oxygen percentage accordingly
	 */
	void Update () 
	{
		previousPercent = percent;
		if(config.debugActive)						// check if we are using debugger values or reading from the script
		{
			depletionRate = config.oxygenDeplete;	// if necessary get the depletion rate set in the debugger
			gainRate = config.oxygenGain;			// if necessary get the gain rate set in the debugger
		}

		if(flap.isEpiglotisOpen() || flap.isCough())	// check if the flap is open or a cough is happening
		{
			percent -= depletionRate * Time.deltaTime;	// if either is the case the oxygen should decrease
		}
		else
		{
			percent += gainRate * Time.deltaTime;		// otherwise the oxygen should gain
		}

		percent = Mathf.Clamp(percent, 0, 1f);		// clamp the value between 0 and 1 so the bar doesn't over or under flow

		if (percent > 0.5 && previousPercent < 0.5) {
			YayPopup = true;
			timer = Time.time;
			AhahaPopup = false;
		}

		if (percent < 0.5 && previousPercent > 0.5) {
			AhahaPopup = true;
			timer = Time.time;
			YayPopup = false;
		}

		if (YayPopup && Time.time - timer > 2.0f) {
			YayPopup = false;
		}

		if (AhahaPopup && Time.time - timer > 2.0f) {
			AhahaPopup = false;
		}
	}

	/**
	 * Handles drawing of the oxygen bar properly
	 */
	void OnGUI()

	{
		// set the x and y position of the oxygen bar relative to screen size
		position.x = (2834f / 3072f) * Screen.width;
		position.y = (392f / 2304f) * Screen.height + (1f-percent) * (673f / 2304f) * Screen.height;

		// set the size of the oxygen bar relative to screen size
		originalSize.x = (196f / 3072f) * Screen.width;
		originalSize.y = (702f / 2304f) * Screen.height * percent; // *percent will grow or shrink the vertical size of 
		// the bar based on oxygen level

		// draw the oxygen bar at the specified position at the specified size
		if (percent >= 0.5f) {
			GUI.DrawTexture (new Rect (position.x, position.y, originalSize.x, originalSize.y), oxygenBar[0]);
		}
		else {
			GUI.DrawTexture (new Rect (position.x, position.y, originalSize.x, originalSize.y), oxygenBar[1]);
		}

		if (AhahaPopup == true)
			GUI.DrawTexture (new Rect(Screen.width * 0.7493359375f, 
				Screen.height * 0.73515625f, 
				Screen.width * 0.2093359375f, 
				Screen.height * 0.300697917f), AHAHAPopupTexture);

		if (YayPopup == true)
			GUI.DrawTexture (new Rect(Screen.width * 0.7493359375f, 
				Screen.height * 0.73515625f, 
				Screen.width * 0.2093359375f, 
				Screen.height * 0.300697917f), YAYPopupTexture);
	}

	/**
	 * function that will allow checking what percent the oxygen bar is at
	 */
	public float getPercent()
	{
		return percent;
	}
}
