using UnityEngine;
using System.Collections;

/**
 * script that handles various things for the intestine game
 */
public class IntestineGameManager : MonoBehaviour 
{
	public int MAX_HEALTH = 20;				//!< the max health value is the number of nuttrients that can escape before losing
	
	private SpawnManager spawnScript;		//!< to hold a reference to the spawnManager script

	public GameObject GameOverScript;		//!< to hold a reference to the gameOver script which will be instantiated when
											//!< the game is over

	public GUIStyle FontStyle;				//!< to hold a font style
	
	public int health;						//!< variable to keep track of the remaining health
	public int nutrients;					//!< variable to keep track of the nutrient currency

	public int NutrientHitScore;			//!< Points gained for hitting a nutrient

	private bool m_IsGameOver;				//!< a flag to mark whether or not the game is over

	// remember if the tower menu/sell menu is up to manage glow effects
	private bool isTowerMenuUp;				//!< a flag to indicate if a tower menu is currently up
	private bool isSellBoxUp;				//!< a flag to remember if a tower sell box is up
	// the following three variables help make the tower menu up check work properly
	private bool setTowerMenuIsUpFalse;		//!< a flag to indicate that the "isTowerMenuUp" variable needs to be set to false
	private float elapsedTime;				//!< a counter to keep track of elapsed time
	private float maxElapsedTime = .1f;		//!< the maximum elapsed time we will allow

	public GameObject nutrientsCounter;		//!< hold a reference to the "nutrients" ui element object
	private NutrientsText nutrientsText;	//!< to hold a reference to the script on the nutrientsCounter object

	public GameObject healthFace;			//!< to hold a reference to the "health face" ui element object
	private DrawHealthFace drawHealthFace;	//!< to hold a reference to the script on the healthFace object

	public GameObject healthBar;			//!< to hold a reference to the "health bar" ui element object
	private DrawHealthBar drawHealthBar;	//!< to hold a reference to the script on the healthBar object

	public GameObject createPlus;			//!< to hold a reference to the "createPlus" object
	private GameObject instantiatedPlus;	//!< to hold a referenece to an instance of an instantiated createPlus object

	public GameObject lostNutrientTutorial;
	private GameObject instantiatedLostNutrientTutorial;

	public BadgePopupSystem SIbps;

	/**
	 * called for initialization
	 * gets all references and resets stats
	 */
	void Start()
	{
		resetStats ();		// reset the vars in player prefs for later

		spawnScript = gameObject.GetComponent<SpawnManager> ();	// find the referrence to the spawn script

		nutrientsText = nutrientsCounter.GetComponent<NutrientsText> ();	// set the nutrients text script

		drawHealthFace = healthFace.GetComponent<DrawHealthFace> ();	// get the script on the healthFace object

		drawHealthBar = healthBar.GetComponent<DrawHealthBar> ();	// get the script on the healthBar object

		SIbps = FindObjectOfType (typeof(BadgePopupSystem)) as BadgePopupSystem;

		// draw the initial nutrients text
		nutrientsText.updateText (nutrients);
	}
		
	/**
	 * reset player prefs vars for the si game
	 */
	void resetStats()
	{
		// this needs to be called to make sure all the stats are reset on game start 
		// failure to reset the stats will result in unstable game behavior
		PlayerPrefs.DeleteKey("SIStats_nutrientsEarned");
		PlayerPrefs.DeleteKey("SIStats_nutrientsSpent");
		PlayerPrefs.DeleteKey("SIStats_foodLost");
		PlayerPrefs.DeleteKey("SIStats_towersPlaced");
		PlayerPrefs.DeleteKey("SIStats_towersSold");
		PlayerPrefs.DeleteKey("SIStats_towersUpgraded");
		PlayerPrefs.DeleteKey("SIStats_enzymesFired");
		PlayerPrefs.Save();		// need to call this to save the changes
	}

	/**
	 * Handles updating all aspects of the game managed here every frame.
	 * Sends updated information to the various UI element scripts.
	 */
    void Update()
    {
		// check if the game is over
		// if it is just exit because we don't need to go through the rest of the stuff in update
        if (m_IsGameOver)
		{
            return;
		}

		// next check if the user has any health yet because if they don't, the game is over
        if (health <= 0)
        {
            Instantiate(GameOverScript);	// if there is no health start the game over script
            m_IsGameOver = true;			// set the flag in this script to indicate the game is over
        }

		// determine if the game is over because the user won the game
		// we do so by looking for any remaining food blobs alive on the screen and also checking to make sure the
		// script we are reading from is over
		if((Application.loadedLevelName != "SmallIntestineTutorial" && 
		    GameObject.FindWithTag("foodBlobParent") == null && spawnScript.end) ||
		   (Application.loadedLevelName == "SmallIntestineTutorial" && 
		 	GameObject.FindWithTag("foodBlobParentTutorial") == null && spawnScript.end))
		{
			// if the game was over because we won, we need to go to the next level
			// find the background chooser
			GameObject chooseBackground = GameObject.Find("ChooseBackground");
			// get the load level script from the background chooser
			SmallIntestineLoadLevelCounter  level = chooseBackground.GetComponent<SmallIntestineLoadLevelCounter>();
	
			level.nextLevel();		// increase the level count on the load level script

			if (Application.loadedLevelName != "SmallIntestineTutorial")
			{
				Application.LoadLevel("SmallIntestineStats");	// load the si stats screen
				//SIbps.end();
			} else
			{
				Application.LoadLevel("LoadLevelSmallIntestine");	// otherwise load the next level
			}
		}

		// delay for setting sell to false to allow for race conditions due to order of execution issues
		if (setTowerMenuIsUpFalse)
		{
			elapsedTime += Time.deltaTime;		// count the time elapsed since last update
			if (elapsedTime > maxElapsedTime)	// if the elapsed time is greater than the max time we change the isTowerMenuUp variable
			{
				isTowerMenuUp = false;			// set isTowerMenuUp to false
				setTowerMenuIsUpFalse = false;	// change the value of the flag to change isTowerMenuUp to false since it is now changed
				elapsedTime = 0f;				// reset the timer for next time
			}
		}
			
		// draw nutrients text
		nutrientsText.updateText (nutrients);

		// choose face to draw	
		if (health > .8 * MAX_HEALTH)
		{
			drawHealthFace.setFace(0);
		} else if (health > .6 * MAX_HEALTH)
		{
			drawHealthFace.setFace(1);
		} else if (health > .4 * MAX_HEALTH)
		{
			drawHealthFace.setFace(2);
		} else if (health > .2 * MAX_HEALTH)
		{
			drawHealthFace.setFace(3);
		} else 
		{
			drawHealthFace.setFace(4);
		}

		// for drawing the health bar
		drawHealthBar.setPercent(((float)health / (float)MAX_HEALTH));
    }

	/**
	 * function that handles tracking score when a nutrient is absorbed
	 */
    public void OnNutrientHit()
    {
		// track nutrients earned
		PlayerPrefs.SetInt ("SIStats_nutrientsEarned", PlayerPrefs.GetInt("SIStats_nutrientsEarned") + NutrientHitScore);
		PlayerPrefs.Save();

		// add the score to the total and update the score being displayed on the screen
		nutrients += NutrientHitScore;				// add the difference to update the score

		// if there is no plus sign currently showing, display one to help bring attention to the nutrients score
		if (instantiatedPlus == null)
		{
			// if no current plus sign, make a new one
			instantiatedPlus = (GameObject)Instantiate (createPlus);
		} else
		{
			// if there already is one, destroy the old one and make a new one
			Destroy(instantiatedPlus.gameObject);
			instantiatedPlus = (GameObject)Instantiate(createPlus);
		}
	}

	/**
	 * function that handles when a food blob reaches the end of the small intestine path
	 */
    public void OnFoodBlobFinish(int numNutrientsAlive)
    {
		if (numNutrientsAlive > 0) 		// check if any nutrients were on the food blob
		{
			if (PlayerPrefs.GetInt("SIStats_foodLost") == 0 && Application.loadedLevelName == "SmallIntestineTutorial")
			{
				instantiatedLostNutrientTutorial = (GameObject)Instantiate(lostNutrientTutorial);
			}

			// track the food particles left at the end if any
			PlayerPrefs.SetInt("SIStats_foodLost", PlayerPrefs.GetInt("SIStats_foodLost") + numNutrientsAlive);
			PlayerPrefs.Save();

			// alter the health left based on how many nutrients were left
			health = Mathf.Clamp(health - numNutrientsAlive, 0, MAX_HEALTH);
		} 
	}

	/**
	 * function that is called to indicate that a tower menu is either being brought up or closed
	 * this will mostly be called by the TowerMenu.cs script
	 */
	public void setTowerMenuUp(bool isUp)
	{
		if(isUp == false)
		{
			// if we are closing a tower menu we have to have a delay before taking action in this class
			// to do so we have a secondary marker that is used to indicate we should start the delay before 
			// setting the "isTowerMenuUp" variable directly to false (which is done in update())
			setTowerMenuIsUpFalse = true;
		} else
		{
			// if we are opening a tower menu, we can change the isTowerMenuUp variable directly
			isTowerMenuUp = true;
		}
	}

	/**
	 * function that can be called to get the current status of the isTowerMenuUp variable
	 * this will mostly be called by the TowerMenu.cs and Glow related scripts
	 */
	public bool getTowerMenuUp()
	{
		return isTowerMenuUp;
	}

	/**
	 * function that can be called to tell the manager that a sell box is up
	 * this will mostly be called by the TowerMenu.cs script
	 */
	public void setSellBoxUp(bool isUp)
	{
		isSellBoxUp = isUp;
	}

	/**
	 * function that can be called to get the current status of the variable isSellBoxUp
	 * this will mostly be called by the TowerMenu.cs and Glow related scripts
	 */
	public bool getSellBoxUp()
	{
		return isSellBoxUp;
	}
}
