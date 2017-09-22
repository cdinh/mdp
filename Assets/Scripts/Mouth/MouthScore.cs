using UnityEngine;
using System.Collections;

/**
 * script that manages calculating and tracking the score in the mouth game
 */
public class MouthScore : MonoBehaviour 
{
	public int score;								//!< variable to hold the score
	public int foodChain;							//!< variable to hold the current "chain" length, or "streak" length
	public int FourMultCount, SixteenMultCount;		//!< variables to count how many food pieces to enter each multiplier

	public Texture FourMultTexture, SixteenMulTexture;	//!< textures for the 4x and 16x icons that show above the score in the gui

	private openFlap flapScript;					//!< variable to hold a reference to the script on the flaps

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// find the flaps and get the script reference from them
		flapScript = GameObject.Find("Flaps").GetComponent<openFlap>();
	}
	
	/**
	 * Update is called once per frame
	 * Checks if a cough is happening.
	 */
	void Update () 
	{
		if(flapScript.isCough())	// check if there is currently a cough happening
		{
			foodChain = 0;			// if there is a cough happening, we reset the "chain length" variable to 0
		}
	}

	/**
	 * called when food is swallowed to determine the score
	 */
	public void collectFood()
	{
		int scoreToAdd = 1;										// default score is 1
		scoreToAdd *= foodChain >= FourMultCount ? 4 : 1;		// determine whether score should have a x4 multiplier applied
		scoreToAdd *= foodChain >= SixteenMultCount ? 4 : 1;	// determine whether score should have a x16 multiplier applied
		score += scoreToAdd;									// add the new score amount to the total score
		foodChain++;											// increase the counter of the current streak

		// track stats
		// track score
		PlayerPrefs.SetInt ("MouthStats_score", score);
		// track food streak
		if (PlayerPrefs.GetInt ("MouthStats_longestStreak") < foodChain)
		{
			PlayerPrefs.SetInt ("MouthStats_longestStreak", foodChain);
		}
		// track food swallowed
		PlayerPrefs.SetInt ("MouthStats_foodSwallowed", PlayerPrefs.GetInt("MouthStats_foodSwallowed") + 1);

		// track the highest multiplier
		if (foodChain >= SixteenMultCount) 
		{
			// if the multiplier is 16x, then this will be automatically the highest multiplier so we can save it
			// without checking what is saved
			PlayerPrefs.SetInt ("MouthStats_highestMultiplier", 16);
		} else if (foodChain >= FourMultCount)
		{
			// however, if we aren't at 16x we need to check the old "highest multiplier" to see if the current 
			// multiplier, for example, 4x, is actually the highest multiplier reached this game.
			if (PlayerPrefs.GetInt("MouthStats_highestMultiplier") < 4)
			{
				PlayerPrefs.SetInt("MouthStats_highestMultiplier", 4);
			}
		} else  // by default the highest multiplier must be at least 1x
		{
			if (PlayerPrefs.GetInt("MouthStats_highestMultiplier") < 1)
			{
				PlayerPrefs.SetInt("MouthStats_highestMultiplier", 1);
			}
		}

		PlayerPrefs.Save();				// needs to be called to save the changes to disk
	}

	/**
	 * Handles drawing the mouth game score and the appropriate multiplier image
	 */
	void OnGUI()
	{
		GUI.depth--;						// decrease the gui depth so that the multiplier textures actually show up
											// on top of the main gui components

		// display the proper multiplier texture on top of the score in the bottom right corner based on 
		// what the food chain indicates the current multiplier count is
		// note that no texture if drawn for the default 1x multiplier
		if(foodChain >= SixteenMultCount)
		{
			GUI.DrawTexture(new Rect(Screen.width * .91f, Screen.height * .85f, Screen.height * .08f,
			                         Screen.height * .08f), SixteenMulTexture);
		} else if(foodChain >= FourMultCount)
		{
			GUI.DrawTexture(new Rect(Screen.width * .91f, Screen.height * .85f, Screen.height * .08f,
			                         Screen.height * .08f), FourMultTexture);
		}

		// this is for drawing the actual score below the multiplier
		GUIStyle scoreStyle = GUI.skin.label;											// set the style to be of a label
		scoreStyle.alignment = TextAnchor.MiddleCenter;									// change alignment of text in label
		scoreStyle.fontSize = (int)(34f / 597f * Screen.height);						// set a relative font size
		scoreStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");				// set the font type
		scoreStyle.normal.textColor = new Color (206f / 255f, 39f / 255f, 115f / 255f);	// set the font color

		// draw the actual label containing the score in the specified location
		GUI.Label(new Rect(Screen.width * .89f,Screen.height * .86f, Screen.width * .1f, Screen.height * .07f), "" + score);
	}
}
