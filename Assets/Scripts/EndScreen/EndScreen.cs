using UnityEngine;
using System.Collections;

/**
 * placeholder for the ending of the game
 */
public class EndScreen : MonoBehaviour 
{
	public Texture background;	//!< background image
	public GUIStyle mainMenu;	//!< main menu button
	public GUIStyle restart;	//!< restart button

	void OnGUI()
	{
		// draw the background texture to fill the entire screen.
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

		// draw the main menu button
		if(GUI.Button(new Rect(Screen.width * .25f, .45f * Screen.height, Screen.width * .2f, Screen.height * .1f),
		                "", mainMenu))
		{
			// when yoou click on main menu then return to the main menu
			Application.LoadLevel("MainMenu");
		}

		// draw the restart button
		if(GUI.Button(new Rect(Screen.width * .55f, .45f * Screen.height, Screen.width * .2f, Screen.height * .1f),
		           "", restart))
		{
			// when you click on restart we currently restart the small intestine game
			// this will need to be changed as the game changes
			Application.LoadLevel("LoadLevelSmallIntestine");
		}
	}
}
