using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Controls the behavior of the textbox display
 */
public class StomachTextBoxes : MonoBehaviour 
{
	public Image textbox;							//!< reference to the image that displays the textbox
	public Sprite[] textboxes;						//!< reference to a script with textbox specific functions
	
	private float timeToHoldTextBox = 5.0f;			//!< Time the textbox should stay up on the screen before fading
	private float elapsedTime;						//!< count the time elapsed since an event
	private bool refreshImageTimer = false;			//!< boolean to mark if we should rfresh the elapsed Time timer
	
	private float timeSinceGameStart;				//!< to hold the time passed since the game started
	private float timeToShowHint = 5f;				//!< the time that has passed when we display the first hint
	private bool hintShown;							//!< flag to mark the hint has been shown
	
	/**
	 * Update is called once each frame
	 */
	void Update()
	{
		// increment elapsed time
		elapsedTime += Time.deltaTime;
		timeSinceGameStart += Time.deltaTime;
		
		// check if we should refresh the image hold timer
		if (refreshImageTimer)
		{
			elapsedTime = 0f;
			refreshImageTimer = false;
		}
		
		// if the text box is up and has been up fro the max time, remove it
		if (elapsedTime >= timeToHoldTextBox)
		{
			setTextbox (0);
		} else
		{
			return;
		}
		
		// for showing the initial hint in the game
		if (!hintShown && timeSinceGameStart > timeToShowHint)
		{
			setTextbox(7);
			hintShown = true;
		}
	}
	
	/**
	 * Function to call to set a textbox to the right message and 
	 * hold it up for 5 seconds
	 */
	public void setTextbox(int index)
	{
		refreshImageTimer = true;
		textbox.sprite = textboxes [index];
	}
}