using UnityEngine;
using System.Collections;

/**
 * script for the "game over" behavior of the mouth game
 */
public class EsophagusGameOver : MonoBehaviour 
{
	private bool isGameOver = false;	//!< flag to hold whether or not the game is over

	public Texture gameOverPopup;		//!< to hold the texture of the game over popup box
	public GUIStyle restart;			//!< to hold the textures for the restart button
	public GUIStyle mainMenu;			//!< to hold the textures for the mainMenu button

	OxygenBar oxygenBar;				//!< to hold a reference to the oxygen bar
	
	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		oxygenBar = gameObject.GetComponent<OxygenBar> ();	// find the reference to the oxygen bar
	}
	
	/**
	 * Update is called once per frame
	 * Checks whether the game is over and if it is handles the case.
	 */
	void Update () 
	{
		// check if we should throw the game over flag because the oxygen bar runs out
		if (oxygenBar.getPercent() <= 0 && !isGameOver)
		{
			isGameOver = true;		// if the oxygen bar has run out throw the game over flag
			Time.timeScale = 0;		// pause the game to stop movement of food
			Debug.Log("2.Time scale: "+Time.timeScale);
		}
		//Debug.Log("1.Time scale: "+Time.timeScale);

	}

	/**
	 * Draws the GUI overlay for Game Over if the game is over.
	 */
	void OnGUI()
	{


		float scale = 234f / 489f;
		float buttonWidth = Screen.width * 0.1591796875f;

		
		if (isGameOver)		// determine if we should draw the game over popup box
		{
			// this draws the popup box in the middle of the screen


			GUI.DrawTexture(new Rect(Screen.width * 0.26953125f, 
				Screen.height * 0.18359375f, 
				Screen.width * 0.4609375f, 
				Screen.height * 0.6328125f), gameOverPopup);





			// draw restart button in proper condition
			if (GUI.Button (new Rect (Screen.width * 0.3251953125f, 
				Screen.height * 0.66666666666f,
				buttonWidth,
				buttonWidth * scale), "", restart))
			{
				// if the restart button is pressed
				Time.timeScale = 1;					// unpause the game
				Application.LoadLevel("Mouth");		// reload the mouth game from the current level
			}
			
			// draw the main menu button
			if (GUI.Button (new Rect (Screen.width * 0.5166015625f, 
				Screen.height * 0.66666666666f,
				buttonWidth,
				buttonWidth * scale), "", mainMenu))
			{
				// if the main menu button is pressed
				Time.timeScale = 1;					// unpause the game
				Application.LoadLevel("MainMenu");	// load up the main menu
			}
		}
	}

	public bool getGameOver(){
		return isGameOver;
	}
	public void setGameOver(){
		isGameOver = true;
	}
}	