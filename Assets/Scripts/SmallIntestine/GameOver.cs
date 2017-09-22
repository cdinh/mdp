using UnityEngine;
using System.Collections;

/**
 * script to handle game over stuff for the si game
 */
public class GameOver : MonoBehaviour 
{
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
		// draw the game over popup box in the middle of the screen
		GUI.DrawTexture(new Rect(Screen.width * 0.3193359375f, 
		                         Screen.height * 0.28515625f, 
		                         Screen.width * 0.3603515625f, 
		                         Screen.height * 0.248697917f), gameOverPopup);
		
		// draw restart button
		if (GUI.Button(new Rect(Screen.width * 0.41015625f, 
		                        Screen.height * 0.41927083f,
		                        Screen.width * 0.0654296875f,
		                        Screen.height * 0.06640625f), "", restart))
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
		if (GUI.Button(new Rect(Screen.width * 0.53125f, 
		                        Screen.height * 0.41927083f,
		                        Screen.width * 0.0654296875f,
		                        Screen.height * 0.06640625f), "", mainMenu))
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
