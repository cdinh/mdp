using UnityEngine;
using System.Collections;

/**
 * script to handle the SI stats
 */
public class SIStats : MonoBehaviour 
{
	// for drawing stars
	public Texture filledStar;						//!< to store the texture for the filled star
	public Texture emptyStar;						//!< to store the texture for the empty star

	// for drawing next level button
	public GUIStyle nextLevelButton;				//!< to show the textures for the next level button

	private int numStars = 1;						//!< the default number of stars is 1

	// for holding level 
	private GameObject counter;						//!< to hold a reference to the background chooser
	private SmallIntestineLoadLevelCounter level;	//!< to hold a reference to the script on the background choose
	
	// list desired stats for tracking here
	private int nutrientsEarned;					//!< will hold the value of nutrientsEarned loaded from PlayerPrefs
	private int nutrientsSpent;						//!< will hold the value of nutrientsSpent loaded from PlayerPrefs
	private int foodLost;							//!< will hold the value of foodLost loaded from PlayerPrefs
	private int towersPlaced;						//!< will hold the value of towersPlaced loaded from PlayerPrefs
	private int towersSold;							//!< will hold the value of towersSold loaded from PlayerPrefs
	private int towersUpgraded;						//!< will hold the value of towersUpgraded loaded from PlayerPrefs
	private int enzymesFired;						//!< will hold the value of enzymesFired loaded from PlayerPrefs

	int prevHighScore;								//!< for high scores
		
	/**
	 * Use this for initialization
	 * Find the correct level and start calculating stats
	 */
	void Start () 
	{
		// pull up the level
		counter = GameObject.Find ("ChooseBackground");						// get the reference to the background chooser
		level = counter.GetComponent<SmallIntestineLoadLevelCounter> ();	// get the script on the background chooser

		populateStats ();	// get the stats from player prefs and store them
		calculateStars();	// calculate the stars based on the stats
	}

	/**
	 * Pulls up all saved stat values from player prefs
	 */
	void populateStats()
	{
		// load each saved stat from PlayerPrefs into their corresponding variables
		nutrientsEarned = PlayerPrefs.GetInt("SIStats_nutrientsEarned");
		nutrientsSpent = PlayerPrefs.GetInt("SIStats_nutrientsSpent");
		foodLost = PlayerPrefs.GetInt("SIStats_foodLost");
		towersPlaced = PlayerPrefs.GetInt("SIStats_towersPlaced");
		towersSold = PlayerPrefs.GetInt("SIStats_towersSold");
		towersUpgraded = PlayerPrefs.GetInt("SIStats_towersUpgraded");
		enzymesFired = PlayerPrefs.GetInt("SIStats_enzymesFired");
	}

	/**
	 * placeholder algorithm
	 * right now the stats are only calculated based on nutrients lost
	 */
	void calculateStars()
	{
		if (foodLost == 0)			// if no nutrients are lost, then you get 5 stars
		{
			numStars = 5;
		} else if (foodLost <= 5)	// if 1-5 nutrients are lost, then you get 4 stars
		{
			numStars = 4;
		} else if (foodLost <= 10)	// if 6-10 nutrients are lost, then you get 3 stars
		{
			numStars = 3;
		} else if (foodLost <= 15)	// if 11-15 nutrients are lost, then you get 2 stars
		{
			numStars = 2;
		} else 						// any more than 15 nutrients lost means 1 star
		{
			numStars = 1;
		}
	
		// in order to keep a record of highest scores for each game we call this function
		// that will check if the new score is the highest score ever and if it is save it
		// to the high score area in playerPrefs
		saveHighScore();
	}	

	/**
	 * for checking if the new score earned is the highest ever and if it is save it
	 */
	void saveHighScore()
	{
		// load the previously saved highest score
		prevHighScore = PlayerPrefs.GetInt ("SI" + (level.getLevel() - 1));
		
		// check if the new score is higher than the previous high score
		if (prevHighScore < numStars)
		{
			// if it is the high score save it
			prevHighScore = numStars;
			PlayerPrefs.SetInt("SI" + (level.getLevel() - 1), numStars);
		}
	}

	/**
	 * Handles drawing of the stats onto the stats screen plus all associated images
	 */
	void OnGUI()
	{
		// Draw the number of stars text
		GUIStyle starStyle = new GUIStyle ();								// create style for the text to draw # stars
		starStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");	// set font type
		starStyle.normal.textColor = Color.yellow;							// set font color
		starStyle.fontSize = (int)(34f / 597f * Screen.height);				// set the size relative to screen size

		// draw the text representation of the number of stars earned in the specified location
		GUI.Label(new Rect((290f/1024f)*Screen.width, (138f/768f)*Screen.height, (100f/1024f)*Screen.width,
		                   (100f/768f)*Screen.height), "" + numStars, starStyle);

		// draw the actual stars
		if (numStars == 1)
		{
			GUI.DrawTexture(new Rect((48f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((142f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect((236f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect((330f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect((424f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
		} else if (numStars == 2)
		{
			GUI.DrawTexture(new Rect((48f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((142f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((236f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect((330f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect((424f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
		} else if (numStars == 3)
		{
			GUI.DrawTexture(new Rect((48f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((142f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((236f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((330f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
			GUI.DrawTexture(new Rect((424f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
		} else if (numStars == 4)
		{
			GUI.DrawTexture(new Rect((48f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((142f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((236f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((330f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((424f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), emptyStar);
		} else if (numStars == 5)
		{
			GUI.DrawTexture(new Rect((48f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((142f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((236f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((330f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
			GUI.DrawTexture(new Rect((424f/1024f)*Screen.width, (236f/768f)*Screen.height, (74f/1024f)*Screen.width,
			                         (74f/768f)*Screen.height), filledStar);
		}


		// Draw the stats text
		GUIStyle statsStyle = new GUIStyle ();								// create a style for the text
		statsStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");	// set the font face
		statsStyle.normal.textColor = Color.yellow;							// set the font color
		statsStyle.fontSize = (int)(20f / 597f * Screen.height);			// set the font size relative to screen size

		// create a label to display the row headers for each stat type
		GUI.Label(new Rect((600f/1024f)*Screen.width, (90f/768f)*Screen.height, (((961f-27f)-600f)/1024f)*Screen.width,
		                   ((520f-90f)/768f)*Screen.height), 
		          "Nutrients Earned:\n" +
		          "Nutrients Spent:\n" +
		          "Food Lost:\n" +
		          "Towers Placed:\n" +
		          "Towers Sold:\n" +
		          "Towers Upgraded:\n" +
		          "Enzymes Released:\n" +
		          "\n" +						//TODO: move this somewhere?
		          "\n" +						//TODO: move this somehwere?
		          " High Score:",				//TODO: move this somewhere?
		          statsStyle);
		// create another label beside it to print out the actual stat numbers
		// because we are not using a fixed width font we use two labels so the columns will be perfectly lined up
		GUI.Label(new Rect((820f/1024f)*Screen.width, (90f/768f)*Screen.height, (((961f-27f)-600f)/1024f)*Screen.width,
		                   ((520f-90f)/768f)*Screen.height), 
		          ""   + nutrientsEarned + "\n" +
		          ""  + nutrientsSpent  + "\n" +
		          ""  + foodLost 	    + "\n" +
		          ""  + towersPlaced    + "\n" +
		          ""  + towersSold      + "\n" +
		          ""	  + towersUpgraded  + "\n" +
		          ""  + enzymesFired    + "\n"+
		          "\n" +						//TODO: move this somewhere?
		          "\n" + 						//TODO: move this somewhere?
		          "" + prevHighScore,			//TODO: move this somewhere?
		          statsStyle);

		// draw the button for next level
		if (GUI.Button(new Rect((635f/1024f)*Screen.width, (535f/768f)*Screen.height,
		                        ((905f-635f)/1024f)*Screen.width, ((665f-535f)/768f)*Screen.height), "", nextLevelButton))
		{
			// make sure we don't show the load screen after it's over
			if (level.getLevel() > level.getMaxLevels())			// check if we've played all the levels
			{
				//Application.LoadLevel("LargeIntestineStoryBoard");					// if we have load the end screen
				Application.LoadLevel("SmallIntestineEndStoryboard");	
			} else
			{
				if(level.isTutorial()){
					Application.LoadLevel ("SmallIntestineTutorial");
				}
				else{
					Application.LoadLevel("LoadLevelSmallIntestine");	// otherwise load the next level
				}
			} 
		}
	}
}