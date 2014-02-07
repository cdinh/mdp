using UnityEngine;
using System.Collections;

public class ReturnButton : MonoBehaviour {
	public GUIStyle mainMenuStyle;	// for main menu button

	public Texture confirmPopup;	// for pop up confirm box
	public GUIStyle confirmYes;		// button for yes
	public GUIStyle confirmNo;		// button for no

	private bool confirmUp;

	void OnGUI()
    {
		// this just handles the menu button in the corner
		if (GUI.Button(new Rect(Screen.width * .89f, 
		                        Screen.height * 0.01822916f,
		                        Screen.width * .09f,
		                        Screen.height * .06f), "", mainMenuStyle))
        {
			Time.timeScale = 0;		// pause the game
			confirmUp = true;		// throw flag
            //Application.LoadLevel("MainMenu");
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
				Time.timeScale = 1;
				Application.LoadLevel("MainMenu");
			}

			// draw no button
			if (GUI.Button(new Rect(Screen.width * 0.53125f, 
			                        Screen.height * 0.41927083f,
			                        Screen.width * 0.0654296875f,
			                        Screen.height * 0.06640625f), "", confirmNo))
			{
				Time.timeScale = 1;
				confirmUp = false;
			}

		}
    }
}
