using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIGameOver : MonoBehaviour {

	public Texture gameOverPopup;	//!< to store the texture for the gameOverPopup
	public GUIStyle restart;		//!< to store the textures for the restart button
	public GUIStyle mainMenu;		//!< to store the textures for the mainMenu button

	/**
	 * Initialization
	 * Pauses game.
	 */
	void Start()
	{
		Time.timeScale = 0;			// when the game is over we pause the game
	}

	/**
	 * Handles drawing of game over popup menu
	 */
	void OnGUI()
	{

		float scale = 234f / 489f;
		float buttonWidth = Screen.width * 0.1591796875f;


		// draw the game over popup box in the middle of the screen
		GUI.DrawTexture(new Rect(Screen.width * 0.26953125f, 
			Screen.height * 0.18359375f, 
			Screen.width * 0.4609375f, 
			Screen.height * 0.6328125f), gameOverPopup);

		// draw restart button
		if (GUI.Button (new Rect (Screen.width * 0.3251953125f, 
			Screen.height * 0.66666666666f,
			buttonWidth,
			buttonWidth * scale), "", restart))
		{
			// if restart is pressed
			Time.timeScale = 1;													// unpause the game
			GameObject chooseBackground = GameObject.Find("ChooseBackground");	// find the background chooser
			SmallIntestineLoadLevelCounter  level = chooseBackground.GetComponent<SmallIntestineLoadLevelCounter>();

			// restart from the correct level
			if (level.getLevel() % 2 == 0)
			{
				Application.LoadLevel("SmallIntestineEven");
			} else 
			{
				Application.LoadLevel("SmallIntestineOdd");
			}
		}

		// draw main menu button
		if (GUI.Button (new Rect (Screen.width * 0.5166015625f, 
			Screen.height * 0.66666666666f,
			buttonWidth,
			buttonWidth * scale), "", mainMenu))
		{
			// if main menu is selected
			Time.timeScale = 1;													// unpause the game
			GameObject chooseBackground = GameObject.Find("ChooseBackground");	// find the background chooser
			SmallIntestineLoadLevelCounter  level = chooseBackground.GetComponent<SmallIntestineLoadLevelCounter>();
			level.resetLevel();			// reset the small intestine current level to 1 for next time
			Application.LoadLevel("MainMenu");	// load the main menu
		}
	}
}
