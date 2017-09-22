using UnityEngine;
using System.Collections;

/**
 * just draws the return button on the upper right corner
 */
public class ReturnButtonSIOdd : MonoBehaviour 
{
	public GUIStyle mainMenuStyle;	//!< for main menu button

	public Texture confirmPopup;	//!< for pop up confirm box
	public GUIStyle confirmYes;		//!< button for yes
	public GUIStyle confirmNo;		//!< button for no

	private bool confirmUp;			//!< flag whether or not the confirm box should be shown

	/**
	 * Draws the return button plus the associated pop up box
	 */
	void OnGUI()
	{
		GUI.depth = GUI.depth - 100;

		// this just handles the menu button in the corner
		if(Time.timeScale != 0)
		{
			if (GUI.Button(new Rect(Screen.width * .89f, 
			                        Screen.height * 0.01822916f,
			                        Screen.width * .09f,
			                        Screen.height * .06f), "", mainMenuStyle))
	        {
				Time.timeScale = 0;		// pause the game
				confirmUp = true;		// throw flag
	        }
		}

		// if the menu button has been pressed
		if (confirmUp)
		{
			GUI.depth--;

			// draw gui texture that holds box with buttons
			GUI.DrawTexture(new Rect(Screen.width * 0.3193359375f, 
			                         Screen.height * 0.28515625f, 
		                         	 Screen.width * 0.3603515625f, 
			                         Screen.height * 0.248697917f), confirmPopup);

			// draw yes button
			if (GUI.Button(new Rect(Screen.width * 0.41015625f, 
			                        Screen.height * 0.41927083f,
			                        Screen.width * 0.0654296875f,
			                        Screen.height * 0.06640625f), "", confirmYes))
			{
				// if the "yes" button was pressed
				Time.timeScale = 1;				// unpause the game
				GameObject chooseBackground = GameObject.Find("ChooseBackground");

				// reset the level for the si game for next time it is loaded
				if (chooseBackground != null)
				{
					SmallIntestineLoadLevelCounter  level = chooseBackground.GetComponent<SmallIntestineLoadLevelCounter>();
					level.resetLevel();
				}

				// load the main menu
				Application.LoadLevel("MainMenu");
			}

			// draw no button
			if (GUI.Button(new Rect(Screen.width * 0.53125f, 
			                        Screen.height * 0.41927083f,
			                        Screen.width * 0.0654296875f,
			                        Screen.height * 0.06640625f), "", confirmNo))
			{
				// if the "no" button was pressed
				Time.timeScale = 1;			// unpause the game
				confirmUp = false;			// throw the flag to indicate not to show the confirm box anymore
			}

		}
    }
}
