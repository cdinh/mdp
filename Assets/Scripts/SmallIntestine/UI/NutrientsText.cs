using UnityEngine;
using System.Collections;

/**
 * script that handles drawing the nutrients text and score in the si game
 */
public class NutrientsText : MonoBehaviour 
{
	private int nutrients;				//!< to hold the score
	private int oldNutrientsVal = 0;	//!< to hold the old score value each time it changes
	private bool setColorGreen;			//!< flag that says whether we should change the nutrients text to green

	// for green fading
	public Color nutrientGainColor;			//!< color of the nutrient text when a nutrient is gained
	public Color originalColor;				//!< color of the nutrient text when no nutrients are being gained

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// set the font size relative to the screen size
		GetComponent<GUIText>().fontSize = (int)(Screen.width * .02f) + 1;	
		// set the pixel offset relative to the screen size
		GetComponent<GUIText>().pixelOffset = new Vector2 (.487f * Screen.height, .128f * Screen.width);
	}
	
	/**
	 * Update is called once per frame
	 * Updates the font color and what is printed to screen
	 */
	void Update () 
	{
		if (setColorGreen)		// check if we gained nutrients and should change the text color green. 
		{
			GetComponent<GUIText>().color = Color.Lerp(nutrientGainColor, originalColor, Time.deltaTime);	// fade the color from this color to the original color
		} else if (!setColorGreen && GetComponent<GUIText>().color.Equals(nutrientGainColor))					// otherwise we didn't gain nutrients and should change the text color white
		{
			GetComponent<GUIText>().color = Color.Lerp(originalColor, nutrientGainColor, Time.deltaTime);	// fade the color from green to white
		}

		// check if the color was green so we can mark to change it back to white
		if (GetComponent<GUIText>().color.Equals(Color.green))
		{
			setColorGreen = false;
		}

		GetComponent<GUIText>().text = "NUTRIENTS: " + nutrients;	// what we want printed to the screen
	}

	/**
	 * function that can be called to sett the desired score text and and desired font color
	 */
	public void updateText(int nutrients)
	{
		this.nutrients = nutrients;			// store the new nutrients value

		if (oldNutrientsVal < nutrients)	// check if the old value was less than the new one (thus nutrients were GAINED)
		{
			setColorGreen = true;			// if it was we should change the text color to green
		}

		oldNutrientsVal = nutrients;		// store the value of nutrients this call to save for comparison later
	}
}
