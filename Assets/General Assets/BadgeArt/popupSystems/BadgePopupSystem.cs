using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgePopupSystem : MonoBehaviour {

	public bool isPopup;
	public Texture BadgePopupWindow;
	public Texture[] filledStar;
	public string badgeTriger;
	public string PassText;

	public GUIStyle restart;			//!< to hold the textures for the restart button
	public GUIStyle mainMenu;			//!< to hold the textures for the mainMenu button
	public GUIStyle continueGame;





	private GUIStyle statsStyle;
	private GUIStyle statsStyle2;
	private int trigerNum;
	private string[] badgeText;
	private string scence;
	private string loadLevel;
	private string[] sentence;




	// Use this for initialization
	void Start () {
		isPopup = false;
		//Time.timeScale = 0;		// pause the game

		statsStyle = new GUIStyle ();									// create a new style
		statsStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");		// set the font
		statsStyle.normal.textColor = Color.white;								// set the font color
		statsStyle.fontSize = (int)(16f / 768f * Screen.height);				// set the font relative size

		statsStyle2 = new GUIStyle ();									// create a new style
		statsStyle2.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");		// set the font
		statsStyle2.normal.textColor = Color.white;								// set the font color
		statsStyle2.fontSize = (int)(22f / 768f * Screen.height);				// set the font relative size

		trigerNum = PlayerPrefs.GetInt(badgeTriger);
		scence = Application.loadedLevelName;

		badgeText = new string[3];
		sentence = new string[3];

		if (scence == "Stomach") {
			badgeText [0] = "  Cells\n" +
							"> 3 died\n" ;
			badgeText [1] = "  Cells\n" +
							"< 3 died\n" ;
			badgeText [2] = "No cells died!" ;

			loadLevel = "StomachStats";

			sentence[0] = "You turned the stomach enzyme on!";
			sentence[1] = "You burnt less than 3 cells!";
			sentence[2] = "You didn’t burn a hole in the stomach wall! ";

		}
		else if (scence == "SmallIntestineOdd" || scence == "SmallIntestineEven"){
			badgeText [0] = " Level One\n" + 
							" Passed!";
			badgeText [1] = " Level Two\n" + 
							" Passed!";
			badgeText [2] = " Level Three\n" + 
							" Passed!";
			loadLevel = "SmallIntestineStats";

			sentence[0] = "You completed the first part of the small intestine! ";
			sentence[1] = "You completed the second part of the small intestine! ";
			sentence[2] = "You completed the third part of the small intestine!";

		}
		else if (scence == "LargeIntestine"){
			badgeText [0] = "   Bacterias\n" + 
							">3 touched";
			badgeText [1] = "   Bacterias\n" + 
							"<3 touched";
			badgeText [2] = "No bacteria touched";
			loadLevel = "LargeIntestineEndStoryboard";

			sentence[0] = "You absorbed water from the food to make good poop! ";
			sentence[1] = "You hit less than 3 bacteria!";
			sentence[2] = "You didn’t hit any bacteria! ";

		}


	}

	// Update is called once per frame
	void Update () {


		trigerNum = PlayerPrefs.GetInt(badgeTriger);
		Debug.Log (badgeTriger + ": " + trigerNum);

	}


	void OnGUI()
	{


		float scale = 70f/138f;
		float buttonWidth = Screen.width * 0.134765625f;


		if (isPopup)		// determine if we should draw the game over popup box
		{
			// this draws the popup box in the middle of the screen
			Time.timeScale = 0;


			GUI.DrawTexture(new Rect(Screen.width * 0.26953125f, 
				Screen.height * 0.18359375f, 
				Screen.width * 0.4609375f, 
				Screen.height * 0.6328125f), BadgePopupWindow);



			GUI.DrawTexture(new Rect((546f/1024f)*Screen.width, (233f/768f)*Screen.height, (31f/1024f)*Screen.width,
				(31f/768f)*Screen.height), filledStar[0]);
			
			GUI.Label(new Rect((592f/1024f)*Screen.width, (230f/768f)*Screen.height, ((80f)/1024f)*Screen.width,
				((41f)/768f)*Screen.height), 
				badgeText [0],
				statsStyle);
			GUI.Label (new Rect ((592f / 1024f) * Screen.width, (309f / 768f) * Screen.height, ((80f) / 1024f) * Screen.width,
				((41f) / 768f) * Screen.height), 
				badgeText [1],
				statsStyle);
			GUI.Label (new Rect ((592f / 1024f) * Screen.width, (391f / 768f) * Screen.height, ((80f) / 1024f) * Screen.width,
				((103f) / 768f) * Screen.height), 
				badgeText [2],
				statsStyle);
			


			if (trigerNum >= 3) {
				GUI.Label(new Rect(((276f + 65f)/1024f)*Screen.width, ((141f + 322f)/768f)*Screen.height, ((340f)/1024f)*Screen.width,
					((29f)/768f)*Screen.height), 
					sentence[0], 										//"You helped the chef swalllow!" ,
					statsStyle2);
				
			}

			if (trigerNum < 3) {
				GUI.DrawTexture (new Rect ((546f / 1024f) * Screen.width, (312f / 768f) * Screen.height, (31f / 1024f) * Screen.width,
					(31f / 768f) * Screen.height), filledStar [1]);
				GUI.Label(new Rect(((276f + 65f)/1024f)*Screen.width, ((141f + 322f)/768f)*Screen.height, ((340f)/1024f)*Screen.width,
					((29f)/768f)*Screen.height), 
					sentence[1], 										//"You helped the chef swalllow!" ,
					statsStyle2);
				
			}


			if (trigerNum == 0) {
				GUI.DrawTexture (new Rect ((546f / 1024f) * Screen.width, (391f / 768f) * Screen.height, (31f / 1024f) * Screen.width,
					(31f / 768f) * Screen.height), filledStar [2]);
				GUI.Label(new Rect(((276f + 65f)/1024f)*Screen.width, ((141f + 322f)/768f)*Screen.height, ((340f)/1024f)*Screen.width,
					((29f)/768f)*Screen.height), 
					sentence[2], 										//"You helped the chef swalllow!" ,
					statsStyle2);
				
			}





			// draw restart button in proper condition
			if (GUI.Button (new Rect (Screen.width * 0.2822265625f, 
				Screen.height * 0.67578125f,
				buttonWidth,
				buttonWidth * scale), "", restart))
			{
				// if the restart button is pressed
				Time.timeScale = 1;					// unpause the game
				Application.LoadLevel(scence);		// reload the mouth game from the current level
			}

			// draw the main menu button
			if (GUI.Button (new Rect (Screen.width * 0.4345703125f, 
				Screen.height * 0.67578125f,
				buttonWidth,
				buttonWidth * scale), "", mainMenu))
			{
				// if the main menu button is pressed
				Time.timeScale = 1;					// unpause the game
				Application.LoadLevel("MainMenu");	// load up the main menu
			}

			if (GUI.Button (new Rect (Screen.width * 0.5830078125f, 
				Screen.height * 0.67578125f,
				buttonWidth,
				buttonWidth * scale), "", continueGame))
			{
				// if the main menu button is pressed
				Time.timeScale = 1;					// unpause the game
				Application.LoadLevel(loadLevel);	// load up the main menu
			}
		}
	}

	public void end(){
		isPopup = true;
	}
}

