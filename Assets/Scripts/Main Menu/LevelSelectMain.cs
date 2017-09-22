using UnityEngine;
using System.Collections;

/**
 * just a random script where i tested a way that could potentially be used in the 
 * future for marking levels as locked/unlocked and creating a selection screen
 * where unlocked levels can be freely moved between.
 * It would probably need quite a few changes, especially a more thought out structural 
 * setup. I originally created this to just mock up something quick to see how hard it would
 * be to do.
 */
public class LevelSelectMain : MonoBehaviour 
{
	private bool showSI, showMouth;			//!< Marks whether we should show the Si level select or mouth level select

	// for drawing stars
	public Texture filledStar;				//!< Holds texture for filled star graphic
	public Texture emptyStar;				//!< Holds texture for the empty star graphic

	// for high scores
	private int mouthLevel1HS;				//!< will hold the high score for the mouth game level 1
	private int mouthLevel2HS;				//!< will hold the high score for the mouth game level 2
	private int smallIntestineLevel1HS;		//!< will hold the high score for the si game level	1
	private int smallIntestineLevel2HS;		//!< will hold the high score for the si game level 2
	private int smallIntestineLevel3HS;		//!< will hold the high score for the si game level 3
	private int smallIntestineLevel4HS;		//!< will hold the high score for the si game level 4
	private int smallIntestineLevel5HS;		//!< will hold the high score for the si game level 5
	private int smallIntestineLevel6HS;		//!< will hold the high score for the si game level 6

	// for unlocked games
	private bool mouthUnlocked;				//!< will be set to indicate whether the mouth game has been reached yet
	private bool smallIntestineUnlocked;	//!< will be set to indicate whether the si game has been reached yet

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		getUnlockedGames ();
		populateHighScores ();
	}

	/**
	 * determine which games are unlocked by looking at high scores saved and other flags in player prefs
	 */
	void getUnlockedGames()
	{
		// these were variables that were saved as "1" when a story was played through, 0 otherwise
		// this is just an example of how a marker can be saved using PlayerPrefs to remember a gameplay event
		mouthUnlocked = (PlayerPrefs.GetInt ("PlayedMouthStory") == 1) ? true : false;
		smallIntestineUnlocked = (PlayerPrefs.GetInt ("PlayedSIStory") == 1) ? true : false;
	}

	/**
	 * get all the high scores from player prefs and store them accordingly
	 */
	void populateHighScores()
	{
		mouthLevel1HS = PlayerPrefs.GetInt("Mouth1");
		mouthLevel2HS = PlayerPrefs.GetInt("Mouth2");
		smallIntestineLevel1HS = PlayerPrefs.GetInt("SI1");
		smallIntestineLevel2HS = PlayerPrefs.GetInt("SI2");
		smallIntestineLevel3HS = PlayerPrefs.GetInt("SI3");
		smallIntestineLevel4HS = PlayerPrefs.GetInt("SI4");
		smallIntestineLevel5HS = PlayerPrefs.GetInt("SI5");
		smallIntestineLevel6HS = PlayerPrefs.GetInt("SI6");
	}

	/**
	 * created this method to draw the "high score" beside each unlocked level
	 * motivation to play to improve score maybe?
	 */
	void drawStars(float y, int highScore)
	{
		// draw the actual stars
		if (highScore == 0)
		{
			GUI.DrawTexture(new Rect(((450f + 0*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 1*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 2*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 3*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 4*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
		} else if (highScore == 1)
		{
			GUI.DrawTexture(new Rect(((450f + 0*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 1*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 2*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 3*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 4*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
		} else if (highScore == 2)
		{
			GUI.DrawTexture(new Rect(((450f + 0*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 1*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 2*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 3*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 4*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
		} else if (highScore == 3)
		{
			GUI.DrawTexture(new Rect(((450f + 0*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 1*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 2*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 3*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect(((450f + 4*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
		} else if (highScore == 4)
		{
			GUI.DrawTexture(new Rect(((450f + 0*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 1*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 2*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 3*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 4*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), emptyStar);
		} else if (highScore == 5)
		{
			GUI.DrawTexture(new Rect(((450f + 0*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 1*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 2*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 3*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect(((450f + 4*82f)/1024f)*Screen.width, y, (64f/1024f)*Screen.width,
			                         (64f/768f)*Screen.height), filledStar);
		}
	}

	/**
	 * Controls drawing what is seen on the level select screen based on user selection and 
	 * what is unlocked
	 */
	void OnGUI()
	{
		// text style for regular text
		GUIStyle textStyle = new GUIStyle ();
		textStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");
		textStyle.normal.textColor = Color.yellow;
		textStyle.fontSize = (int)(30f / 597f * Screen.height);
		textStyle.alignment = TextAnchor.MiddleCenter;

		// text style for active buttons
		GUIStyle activeStyle = new GUIStyle (GUI.skin.button);
		activeStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");
		activeStyle.normal.textColor = Color.yellow;
		activeStyle.fontSize = (int)(22f / 597f * Screen.height);
		activeStyle.alignment = TextAnchor.MiddleLeft;

		// text style for inactive buttons
		GUIStyle inactiveStyle = new GUIStyle (GUI.skin.button);
		inactiveStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");
		inactiveStyle.normal.textColor = Color.gray;
		inactiveStyle.fontSize = (int)(22f / 597f * Screen.height);
		inactiveStyle.alignment = TextAnchor.MiddleLeft;

		// display the main menu only
		if (!showMouth && !showSI)
		{
			GUI.Label(new Rect(0, 0, Screen.width,
			                   (100f/768f)*Screen.height), 
			          "Select a Game", textStyle);

			// show active or inactive mouth level selection
			if (mouthUnlocked)
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.2f, 
				                        Screen.width * 0.35f, Screen.height * 0.1f), "Mouth Game", activeStyle))
				{
					showMouth = true;
				}
			} else
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.2f, 
				                        Screen.width * 0.35f, Screen.height * 0.1f), "Mouth Game", inactiveStyle))
				{
				}
			}

			// show active or inactive SI level selection
			if(smallIntestineUnlocked)
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.3f, 
				                        Screen.width * 0.35f, Screen.height * 0.1f), "Small Intestine Game", activeStyle))
				{
					showSI = true;
				}
			} else
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.3f, 
				                        Screen.width * 0.35f, Screen.height * 0.1f), "Small Intestine Game", inactiveStyle))
				{
				}
			}
		}

		// allow selection between mouth game levels
		// right now i just marked a level as unlocked if it has a high score or the level immediately before it has
		// a high score
		// users can only select these levels. "unlocked" levels are greyed out
		if (showMouth)
		{
			GUI.Label(new Rect(0, 0, Screen.width,
			                   (100f/768f)*Screen.height), "Select a Level to Play", textStyle);

			// draw stars by buttons
			drawStars (Screen.height*.2f, mouthLevel1HS);
			drawStars (Screen.height*.3f, mouthLevel2HS);

			// if the game is unlocked the first level will be available so we don't need to check here
			if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.2f, 
			                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 1", activeStyle))
			{
				PlayerPrefs.SetInt("DesiredMouthLevel", 1);
				PlayerPrefs.Save();
				Application.LoadLevel("LoadLevelMouth");
			}

			// check if next level is unlocked by checking if previous was beaten
			if (mouthLevel1HS > 0)
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.3f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 2", activeStyle))
				{
					PlayerPrefs.SetInt("DesiredMouthLevel", 2);
					PlayerPrefs.Save();
					Application.LoadLevel("LoadLevelMouth");
				}
			} else
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.3f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 2", inactiveStyle))
				{

				}
			}
		} else if (showSI)
		{
			GUI.Label(new Rect(0, 0, Screen.width,
			                   (100f/768f)*Screen.height), "Select a Level to Play", textStyle);

			// draw the stars by each level
			drawStars (Screen.height*.2f, smallIntestineLevel1HS);
			drawStars (Screen.height*.3f, smallIntestineLevel2HS);
			drawStars (Screen.height*.4f, smallIntestineLevel3HS);
			drawStars (Screen.height*.5f, smallIntestineLevel4HS);
			drawStars (Screen.height*.6f, smallIntestineLevel5HS);
			drawStars (Screen.height*.7f, smallIntestineLevel6HS);

			// if the game is unlocked the first level will be available so we don't need to check here
			if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.2f, 
			                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 1", activeStyle))
			{
				PlayerPrefs.SetInt("DesiredSILevel", 1);
				PlayerPrefs.Save();
				Application.LoadLevel("LoadLevelSmallIntestine");
			}

			// check if next level is unlocked by checking if previous was beaten
			if (smallIntestineLevel1HS > 0)
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.3f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 2", activeStyle))
				{
					PlayerPrefs.SetInt("DesiredSILevel", 2);
					PlayerPrefs.Save();
					Application.LoadLevel("LoadLevelSmallIntestine");
				}
			} else
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.3f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 2", inactiveStyle))
				{
					
				}
			}

			if (smallIntestineLevel2HS > 0)
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.4f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 3", activeStyle))
				{
					PlayerPrefs.SetInt("DesiredSILevel", 3);
					PlayerPrefs.Save();
					Application.LoadLevel("LoadLevelSmallIntestine");
				}
			} else
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.4f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 3", inactiveStyle))
				{
					
				}
			}

			if (smallIntestineLevel3HS > 0)
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.5f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 4", activeStyle))
				{
					PlayerPrefs.SetInt("DesiredSILevel", 4);
					PlayerPrefs.Save();
					Application.LoadLevel("LoadLevelSmallIntestine");
				}
			} else
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.5f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 4", inactiveStyle))
				{
					
				}
			}

			if (smallIntestineLevel4HS > 0)
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.6f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 5", activeStyle))
				{
					PlayerPrefs.SetInt("DesiredSILevel", 5);
					PlayerPrefs.Save();
					Application.LoadLevel("LoadLevelSmallIntestine");
				}
			} else
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.6f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 5", inactiveStyle))
				{
					
				}
			}

			if (smallIntestineLevel5HS > 0)
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.7f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 6", activeStyle))
				{
					PlayerPrefs.SetInt("DesiredSILevel", 6);
					PlayerPrefs.Save();
					Application.LoadLevel("LoadLevelSmallIntestine");
				}
			} else
			{
				if (GUI.Button(new Rect(Screen.width * .2f, Screen.height*.7f, 
				                        Screen.width * 0.2f, Screen.height * 0.1f), "Level 6", inactiveStyle))
				{
					
				}
			}
		}

		// add a back button to go back to main level selection
		if (showSI || showMouth)
		{
			if (GUI.Button(new Rect(0, Screen.height*.9f, 
			                        Screen.width * 0.15f, Screen.height * 0.1f), "Go Back", activeStyle))
			{
				showSI = false;
				showMouth = false;
			}
		}
	}
}
