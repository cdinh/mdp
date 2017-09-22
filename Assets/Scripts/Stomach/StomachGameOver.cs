using UnityEngine;
using System.Collections;

/**
 * Handle game over conditions for the stomach game
 */
public class StomachGameOver : MonoBehaviour 
{
	public Texture[] gameOverPopup;	//!< to store the texture for the gameOverPopup
	public GUIStyle restart;		//!< to store the textures for the restart button
	public GUIStyle mainMenu;		//!< to store the textures for the mainMenu button
	
	public int maxFood;				//!< the max food allowed to pile up before a gameover
	
	private StomachFoodManager fm;	//!< to hold a reference to the stomach food manager
	private StomachGameManager gm;	//!< hold a reference to the stomach game manager
    private CellManager cm;

	private StomachEnzyme stEz;
	
	private bool gameOver;			//!< flag to indicate if the game is over
	private int gameOverStatus;
	
	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// get references
		fm = FindObjectOfType (typeof(StomachFoodManager)) as StomachFoodManager;
		gm = FindObjectOfType (typeof(StomachGameManager)) as StomachGameManager;
        cm = FindObjectOfType(typeof(CellManager)) as CellManager;
		stEz = FindObjectOfType (typeof(StomachEnzyme)) as StomachEnzyme;
		gameOverStatus = 0;
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () 
	{
		// lose condition: food stacked too high
		if (fm.getNumFoodBlobs() == maxFood)
		{
			gameOver = true;
			gameOverStatus = 1;
			Time.timeScale = 0;
		}

		// lose condition: the same cell dies 3 times in a row

		/*
		int[] deathCounts = gm.getCellDeathCounts ();
		for (int i = 0; i < 6; i++)
		{
			if (deathCounts[i] == gm.MAX_CELL_DEATHS)
			{
				gameOver = true;
				Time.timeScale = 0;
			}
		}
		*/

		//if all cells died, game over
		if(gm.getDeadCellNum() == cm.getCellNumber())
		{
			gameOver = true;
			gameOverStatus = 0;
			Time.timeScale = 0;
		}

		if (stEz.hasEaten ()) {
			gameOver = true;
			gameOverStatus = 2;
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
				Application.LoadLevel("Stomach");
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