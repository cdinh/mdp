using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIGameOver : MonoBehaviour {

	public Texture[] gameOverPopup;	//!< to store the texture for the gameOverPopup
	public GUIStyle restart;		//!< to store the textures for the restart button
	public GUIStyle mainMenu;		//!< to store the textures for the mainMenu button



	private poopmeter pm;
	private LI_WaterScript WS;
	private LargeIntestGameManager ligm;
	private BadgePopupSystem LIbps;

	private bool gameOver;			//!< flag to indicate if the game is over
	private int gameOverStatus;

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// get references
		pm = FindObjectOfType(typeof(poopmeter))as poopmeter;
		WS = FindObjectOfType(typeof(LI_WaterScript)) as LI_WaterScript;
		ligm = FindObjectOfType (typeof(LargeIntestGameManager)) as LargeIntestGameManager;
		LIbps = FindObjectOfType (typeof(BadgePopupSystem)) as BadgePopupSystem;
		gameOverStatus = 0;
	}

	/**
	 * Update is called once per frame
	 */
	void Update () 
	{
		if (ligm.getWaterValue() >= 50 && ligm.getWaterValue()<75)
		{
			gameOver = false;
			//Application.LoadLevel("LargeIntestineEndStoryboard");
			LIbps.end();
		}
			
		if(ligm.getWaterValue() < 50)
		{
			gameOver = true;
			gameOverStatus = 0;
			Time.timeScale = 0;
		}

		if (ligm.getWaterValue() >= 75) {
			gameOver = true;
			gameOverStatus = 1;
			Time.timeScale = 0;
		}

	}

	/**
	 * Game over popup is drawn with legacy gui
	 */
	void OnGUI()
	{


		float scale = 234f / 489f;
		float buttonWidth = Screen.width * 0.1591796875f;

		if (gameOver)
		{
			// draw the game over popup box in the middle of the screen
			GUI.DrawTexture(new Rect(Screen.width * 0.26953125f, 
				Screen.height * 0.18359375f, 
				Screen.width * 0.4609375f, 
				Screen.height * 0.6328125f), gameOverPopup[gameOverStatus]);

			// draw restart button
			if (GUI.Button (new Rect (Screen.width * 0.3251953125f, 
				Screen.height * 0.66666666666f,
				buttonWidth,
				buttonWidth * scale), "", restart))
			{
				// if restart is pressed
				Time.timeScale = 1;													// unpause the game
				Application.LoadLevel("LargeIntestine");
			}

			// draw main menu button
			if (GUI.Button (new Rect (Screen.width * 0.5166015625f, 
				Screen.height * 0.66666666666f,
				buttonWidth,
				buttonWidth * scale), "", mainMenu))
			{
				// if main menu is selected
				Time.timeScale = 1;													// unpause the game
				Application.LoadLevel("MainMenu");	// load the main menu
			}
		}
	}
}
