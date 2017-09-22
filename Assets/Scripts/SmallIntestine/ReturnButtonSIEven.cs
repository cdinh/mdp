using UnityEngine;
using System.Collections;

/**
 * this just draws the return button on the upper left corner
 */
public class ReturnButtonSIEven : MonoBehaviour 
{
	public GUIStyle mainMenuStyle;	//!< for main menu button
	
	public Texture confirmPopup;	//!< for pop up confirm box
	public GUIStyle confirmYes;		//!< button for yes
	public GUIStyle confirmNo;		//!< button for no
	
	private bool confirmUp;			//!< flag whether or not the confirm box should be shown

	/**
	 * handles drawing the return button plus associated pop up box
	 */
	void OnGUI()
	{
		GUI.depth = GUI.depth - 100;

		// this just handles the menu button in the corner
		if(Time.timeScale != 0)
		{
			if (GUI.Button(new Rect(Screen.width * .01f, 
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
				// if the "yes" button is pressed
				Time.timeScale = 1;				// unpause the game
				GameObject chooseBackground = GameObject.Find("ChooseBackground");

				// reset the level we are on for next time we start the si game
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
				// if "no" was pressed
				Time.timeScale = 1;		// unpause the game
				confirmUp = false;		// indicate we no longer want to show the confirm box
			}
			
		}
	}
}