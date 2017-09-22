using UnityEngine;
using System.Collections;

/**
 * script to control the mouth stats tracker
 */
public class MouthStats : MonoBehaviour 
{
	// for drawing stars
	public Texture filledStar;				//!< texture of the filled star for mouth stats
	public Texture emptyStar;				//!< texture of the empty star for mouth stats
	
	// for drawing next level button
	public GUIStyle nextLevelButton;		//!< hold the style for the next level button
	
	private int numStars = 1;				//!< the default number of stars is 1

	// for holding level
	private GameObject counter;				//!< to hold a refernece to the counter
	private MouthLoadLevelCounter level;	//!< to hold a reference to the mouthloadlevelcounter script

	// variables to hold stats
	// list desired stats for tracking here
	private int longestStreak;				//!< will hold the value of longestStreak loaded from PlayerPrefs
	private int timesCoughed;				//!< will hold the value of timesCoughed loaded from PlayerPrefs
	private int foodLost;					//!< will hold the value of foodLost loaded from PlayerPrefs
	private int foodSwallowed;				//!< will hold the value of foodSwallowed loaded from PlayerPrefs
	private int highestMultiplier;			//!< will hold the value of highestMultiplier loaded from PlayerPrefs

	private int score;						//!< will hold the score calculated based off of stats

	private int prevHighScore;						//!< for high scores
	
	/**
	 * Use this for initialization
	 * Load level information and start calculating stats data
	 */
	void Start () 
	{
		// pull up the level counter
		counter = GameObject.Find ("MouthChooseBackground");		// find the reference to the MouthChooseBackground
		level = counter.GetComponent<MouthLoadLevelCounter> ();		// get the MouthLoadLevelScript on the background chooser

		populateStats();		// look up the stats we are tracking from the data saved on disk
		calculateStars();		// calculate the # of stars earned based on the stats pulled up
	}

	/**
	 * Load up stats data from PlayerPrefs
	 */
	void populateStats()
	{
		// populate the stats by pulling them from the saved data on disk with playerprefs and storing them in
		// temporary variables
		longestStreak = PlayerPrefs.GetInt("MouthStats_longestStreak");
		timesCoughed = PlayerPrefs.GetInt("MouthStats_timesCoughed");
		foodLost = PlayerPrefs.GetInt("MouthStats_foodLost");
		foodSwallowed = PlayerPrefs.GetInt("MouthStats_foodSwallowed");
		highestMultiplier = PlayerPrefs.GetInt("MouthStats_highestMultiplier");
		score = PlayerPrefs.GetInt("MouthStats_score");
	}
	
	/**
	 * placeholder algorithm
	 * currently stars are calculated only from the number of times coughed
	 */
	void calculateStars()
	{
		if (timesCoughed == 0)			// if there were 0 coughs recorded the score is 5 stars
		{
			numStars = 5;
		} else if (timesCoughed == 1)	// if there was 1 cough recorded the score is 4 stars
		{
			numStars = 4;
		} else if (timesCoughed == 2)	// if there were 2 coughs recorded the score is 3 stars
		{
			numStars = 3;
		} else if (timesCoughed == 3)	// if there were 3 coughs recorded the score is 2 stars
		{
			numStars = 2;
		} else 							// any other number of coughs is 1 star
		{
			numStars = 1;
		}

		saveHighScore();				// save this score if it's the highest to have an overall high score data
	}

	/**
	 * this function checks if the score earned on this level this time is the highest score EVER
	 */
	void saveHighScore()
	{
		prevHighScore = PlayerPrefs.GetInt ("Mouth" + (level.getLevel() - 1));	// get the old saved high score

		// check if high score
		if (prevHighScore < numStars)
		{
			// if it is the high score save it
			prevHighScore = numStars;
			PlayerPrefs.SetInt("Mouth" + (level.getLevel() - 1), numStars);		// save the score
			PlayerPrefs.Save();				// needs to be called to write the data to disk
		}
	}

	/**
	 * Handles drawing of stats information and all related graphics
	 */
	void OnGUI()
	{
		// Draw the number of stars text
		GUIStyle starStyle = new GUIStyle ();									// create new style
		starStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");		// set font style
		starStyle.normal.textColor = Color.yellow;								// set font color
		starStyle.fontSize = (int)(34f / 597f * Screen.height);					// set a relative font size
		// draw the text indicating the number of stars in the specified area
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
		GUIStyle statsStyle = new GUIStyle ();									// create a new style
		statsStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");		// set the font
		statsStyle.normal.textColor = Color.yellow;								// set the font color
		statsStyle.fontSize = (int)(20f / 597f * Screen.height);				// set the font relative size

		// create 2 labels to display the stats in text
		GUI.Label(new Rect((600f/1024f)*Screen.width, (90f/768f)*Screen.height, (((961f-27f)-600f)/1024f)*Screen.width,
		                   ((520f-90f)/768f)*Screen.height), 
		          "Longest Streak:\n" +
		          "Coughs:\n" +
		          "Food Lost:\n" +
		          "Food Swallowed:\n" +
		          "Highest Multiplier:\n" +
		          "Score:\n" +
		          "\n" +						//TODO: move this somewhere?
		          "\n" +						//TODO: move this somehwere?
		          " High Score:",				//TODO: move this somewhere?
		          statsStyle);
		// this second label is needed to line up everything since we aren't using a fixed size font
		GUI.Label(new Rect((820f/1024f)*Screen.width, (90f/768f)*Screen.height, (((961f-27f)-600f)/1024f)*Screen.width,
		                   ((520f-90f)/768f)*Screen.height), 
		          "" + longestStreak + "\n" +
		          "" + timesCoughed + "\n" +
		          "" + foodLost + "\n" +
		          "" + foodSwallowed + "\n" +
		          "" + highestMultiplier + "x\n" +
		          "" + score + "\n" +
		          "\n" +						//TODO: move this somewhere?
		          "\n" + 						//TODO: move this somewhere?
		          "" + prevHighScore,			//TODO: move this somewhere?
		          statsStyle);

		// draw the button for next level in the specified area of the screen
		if (GUI.Button(new Rect((635f/1024f)*Screen.width, (535f/768f)*Screen.height,
		                        ((905f-635f)/1024f)*Screen.width, ((665f-535f)/768f)*Screen.height), "", nextLevelButton))
		{
			// make sure to not show the load screen after we're done
			if (level.getLevel() > level.getMaxLevels())
			{
				Application.LoadLevel("MouthEndStoryboard");	// if the mouth game is done then we load the small
																	// intestine story
			} else
			{
				Application.LoadLevel("LoadLevelMouth");			// if there are more mouth levels than load the next one
			}
		}
	}
}