using UnityEngine;
using System.Collections;

/**
 * script that is used to display the breathing animation in the mouth game
 */
public class Breathing : MonoBehaviour 
{
	// hold the images
	public Texture[] breathing;				//!< array to hold the textures for "breathing phase" of animation
	public Texture[] notBreathing;			//!< array to hold the textures for the "not breathing phase" of animation

	// animation delay
	public float animationDelay1 = .5f;		//!< time to show first animation frame
	public float animationDelay2 = 1.0f;	//!< time to show second animation frame
	public float animationDelay3 = 1.5f;	//!< time to show the third animation frame
	public float animationDelay4 = 2.0f;	//!< time to show the fourth animation frame
	private float timePassed = 0f;			//!< float to hold time passed to use to decide which frame to show

	public GameObject flaps;				//!< to hold reference to the flaps
	private openFlap flapScript;			//!< to hold a reference to the script on the flaps

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		flapScript = (openFlap)flaps.GetComponent(typeof(openFlap));	// find the script on the flaps
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () 
	{
		timePassed += Time.deltaTime;		// increment the time passed since the last update call
	}

	/**
	 * Draws the proper animated image to match whether we are breathing or not.
	 */
	void OnGUI()
	{
		if (flapScript.isEpiglotisOpen() == true)	// if the epiglottis is open, we're not breathing
		{
			if (timePassed < animationDelay1)		// check to see which frame to draw
			{
				// draw the first frame if the time is animationDelay1 or less
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), notBreathing[0]);
			} else if (timePassed > animationDelay1 && timePassed < animationDelay2)
			{
				// draw the second frame if the tie is animationDelay2 or less but more than animationDelay1
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), notBreathing[1]);
			} else if (timePassed > animationDelay2 && timePassed < animationDelay3)
			{
				// draw the second frame if the tie is animationDelay2 or less but more than animationDelay1
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), notBreathing[2]);
			}else if (timePassed > animationDelay3 && timePassed < animationDelay4)
			{
				// draw the second frame if the tie is animationDelay2 or less but more than animationDelay1
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), notBreathing[3]);
			}  else if (timePassed > animationDelay4)
			{
				// if the time is greather than animationDelay2, start back counting from 0
				timePassed = 0f;
			}
		} else 										// otherwise the epiglottis is closed, so we're breathing
		{
			if (timePassed < animationDelay1)		// check to see which frame to draw
			{
				// draw the first frame if the time is animationDelay1 or less
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), breathing[0]);
			} else if (timePassed > animationDelay1 && timePassed < animationDelay2)
			{
				// draw the second frame if the tie is animationDelay2 or less but more than animationDelay1
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), breathing[1]);
			} else if (timePassed > animationDelay2 && timePassed < animationDelay3)
			{
				// draw the second frame if the tie is animationDelay2 or less but more than animationDelay1
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), breathing[2]);
			} else if (timePassed > animationDelay3 && timePassed < animationDelay4)
			{
				// draw the second frame if the tie is animationDelay2 or less but more than animationDelay1
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), breathing[3]);
			} else if (timePassed > animationDelay4)
			{
				// if the time is greather than animationDelay2, start back counting from 0
				timePassed = 0f;
			}
		}
	}
}
