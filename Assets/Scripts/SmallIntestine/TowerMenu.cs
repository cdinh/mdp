using UnityEngine;
using System.Collections;

/**
 * script that handles basic tower menu creation and functionality for the small intestine game
 */
public class TowerMenu : MonoBehaviour 
{
	public bool IsEnabled;					//!< flag that holds whether the tower menu is currently up
	public Font font;						//!< for the font displaying the prices of towers on the tower menu

	// for sounds
	public GameObject sellSound;			//!< to store the sound to play when a tower is sold

	// for buttons
	public GUIStyle powerActive;			//!< to hold the style for the power uprade active button
	public GUIStyle powerInactive;			//!< to hold the style for the power upgrade inactive button
	public GUIStyle speedActive;			//!< to hold the style for the speed upgrade active button
	public GUIStyle speedInactive;			//!< to hold the style for the speed upgrade inactive button
	public GUIStyle sellActive1;			//!< sell button for the base model
	public GUIStyle sellActive2;			//!< sell button for level1 towers
	public GUIStyle sellActive3;			//!< sell button for level2 towers
	public GUIStyle sellInactive;			//!< inactive sell button for the tutorial

	// for sell popup
	private bool displaySellBox;		//!< mark whether we should draw sell box
	public Texture sellConfirmBox;		//!< mark whether we should be displaying the sell confirm box
	public GUIStyle confirmYes;			//!< to hold the confirm sell button textures
	public GUIStyle confirmNo;			//!< to hold the cancel sell button textures
	
	private Tower m_Tower;					//!< to hold a reference to a tower script
	
	private Vector3 m_ScreenPosition;		//!< for the position where the user clicks/taps
	
	private IntestineGameManager m_GameManager;	//!< to hold a reference to the intestine game manager
	
	// Dimension and position consts
	private float UPGRADE_BUTTON_WIDTH;		//!< for the width of the upgrade buttons
	private float UPGRADE_BUTTON_HEIGHT;	//!< for the height of the upgrade buttons
	private float SELL_BUTTON_WIDTH;		//!< for the width of the sell buttons
	private float SELL_BUTTON_HEIGHT;		//!< for the height of the sell buttons
	
	private bool m_MouseDownLastFrame = false;	//!< helper for detecting clicks/taps



	private bool m_OneClick = false;
	private float doubleClickTime;
	private float clickTimer;
	private bool m_doubleClick = false;

	/**
	 * Use this for initialization
	 * Gets the tower the menu is on and initializes values for drawing it when brought up
	 */
    void Start()
    {
		// set all the sizes relative to screen size
		UPGRADE_BUTTON_WIDTH = Screen.width * (76.5f / 1024f);  // Any size you want, multiply by 1.5 and divide by 1024 or 768
		UPGRADE_BUTTON_HEIGHT = Screen.height * (90f / 768f);
		SELL_BUTTON_WIDTH = Screen.width * (165f / 1024f);
		SELL_BUTTON_HEIGHT = Screen.height * (63f / 768f);

		// find the instestine game manager and store the reference
		m_GameManager = GameObject.Find ("Managers").GetComponent<IntestineGameManager>();

		// find the reference to the tower script attached to the same tower this script is attached to
        m_Tower = gameObject.GetComponent<Tower>();


		doubleClickTime = 0.5f;
		clickTimer = 0f;
	}

	/**
	 * function that helps tower menu initialization
	 * this is called by the tower spawner when a tower is spawned and helps the tower menu show up
	 * in the correct location when brought up for a tower
	 */
	public void Initialize()
	{
        m_ScreenPosition = MDPUtility.WorldToScreenPosition(transform.position);
		m_ScreenPosition.y -= Screen.height * (105f / 768f);
	}


	void Update(){
		if (m_OneClick) {
			if (Time.time - clickTimer > doubleClickTime) {
				m_OneClick = false;
			}

		}

	}


	/**
	 * a function that is called after update
	 * things are here to help control order of execution
	 */
	void LateUpdate()
	{
		if(!gameObject.GetComponent<Tower>().enabled)	// first check if the tower itself is enabled
		{
			return;										// if it's not we don't need to do anything
		}

		bool mouseDown = Input.GetMouseButton(0);		// check if the mouse is currently pressed

		// check if the mouse was down last frame, but is not currently pressed
		if (m_MouseDownLastFrame && !mouseDown)
		{
			StartCoroutine(CheckMouseClick());		// need to use startcoroutine because the function is of type 
													// ienumerator so we can delay the thread
													// without this delay the menu DOES NOT function properly due 
													// to the execution order of functions
		}
		
		m_MouseDownLastFrame = mouseDown;	// assign mouse down to mousedownlastframe for the next time lateupdate is called
	}

	/**
	 * Handles drawing of the appropriate tower menu
	 * The look of the menu will changed based on what the current tower model is
	 */
	void OnGUI()
	{
		GUI.depth -= 2;				// change the gui depth so that the tower menu shows up on top of game elements

		// check if the sell box is supposed to be shown
		// if it is display it
		if(displaySellBox)
		{
			// draws the sell confirm box in the middle of the screne
			GUI.DrawTexture(new Rect(Screen.width * 0.3193359375f, 
			                         Screen.height * 0.28515625f, 
			                         Screen.width * 0.3603515625f, 
			                         Screen.height * 0.248697917f), sellConfirmBox);
			
			// draw yes button
			if (GUI.Button(new Rect(Screen.width * 0.41015625f, 
			                        Screen.height * 0.41927083f,
			                        Screen.width * 0.0654296875f,
			                        Screen.height * 0.06640625f), "", confirmYes))
			{
				// if the "yes" button was clicked
				Sell ();							// sell the tower
				displaySellBox = false;				// change the flag so that the confirm box is no longer shown
				m_GameManager.setSellBoxUp(false);	// change the variable in the game manager that keeps track of whether
													// the sell box is up
			}
			
			// draw no button
			if (GUI.Button(new Rect(Screen.width * 0.53125f, 
			                        Screen.height * 0.41927083f,
			                        Screen.width * 0.0654296875f,
			                        Screen.height * 0.06640625f), "", confirmNo))
			{
				// if the "no" button was pressed
				displaySellBox = false;				// change the flag so that the confirm box is no longer shown
				m_GameManager.setSellBoxUp(false);	// change the variable in the game manager that keeps track of whether
													// the sell box is up
			}

		}

		// check if we should be displaying a tower sell menu, if not exit this function
		// otherwise keep going to display the menu
		if (!IsEnabled)
			return;

		// choose the proper tower menu to display based on the tower model
		switch (m_Tower.ActiveModelName)		// switch to the correct function based on the model name
		{
		case "Base":							// for the tower "base" model
			ShowPowerUpgrade();					// we need to show the power upgrade button
			ShowSpeedUpgrade();					// we need to show the speed upgrade button
			ShowSell(sellActive1);				// we need to show the sell base model button
			break;
		case "Speed1":							// for the tower "speed1" model
			ShowSpeedUpgrade();					// we need to display the speed upgrade button
			ShowSell(sellActive2);				// we need to display the sell upgrade1 model button
			break;
		case "Power1":							// for the tower "power1" model
			ShowPowerUpgrade();					// we need to display the power upgrade button
			ShowSell(sellActive2);				// we need to display the sell upgrade 1 model button
			break;
		default:								// for other models (2x upgraded models)
			ShowSell(sellActive3);				// just show the sell upgrade2 model button
			break;
		}
	}

	/**
	 * asynchronous function that checks mouse clicks to see if it brings up the tower menu or closes a tower menu
	 */
	private IEnumerator CheckMouseClick()
	{	
		Vector3 mousePos = MDPUtility.MouseToWorldPosition();	// get the mouse click position
		mousePos.y = 5;											// change the y position to 5 for proper hit detection
		RaycastHit hitInfo;										// storing raycast hits
			
		// check if we click on a tower by doing a raycast down to see if a tower was below it
		if (Physics.Raycast(mousePos, Vector3.down, out hitInfo, mousePos.y)) 
		{
			// if we click on tower, toggle whether menu is showed
			if (hitInfo.transform.position == transform.position)
			{
				if (!m_OneClick) {
					m_OneClick = true;
					clickTimer = Time.time;
				} else {
					m_OneClick = false;
					//double click is true;
					Debug.Log ("Double Clicked");
				
					IsEnabled = !IsEnabled;						// change the value in this class
					m_GameManager.setTowerMenuUp (IsEnabled);	// also set the flag in the game manager

				}
			}
			else 		// if we didn't click directly on a tower disable a menu if it's up
			{
				if (IsEnabled)
				{
					IsEnabled = false;						// change the value in this class
					m_GameManager.setTowerMenuUp(false);	// also set the flag in the game manager
				}
			}
		} else
		{
			// otherwise if we clicked in a random place cancel the menu
			yield return new WaitForSeconds(.1f);		// wait for .1 seconds
			if (IsEnabled)
			{
				IsEnabled = false;						//change the value in this class
				m_GameManager.setTowerMenuUp(false);	// change the value in game manager
			}
		}
		yield return new WaitForSeconds(.0f);
	}
	
    /**
     * Returns a rectangle with an adjusted position for
     * the next button
     */
	private Rect GetPowerButtonRect()
	{
		Vector2 spawnPosition;		// for storing the power button location

		// set the correct x location for the button
		spawnPosition.x = Mathf.Clamp(m_ScreenPosition.x, 
		                              (UPGRADE_BUTTON_WIDTH + Screen.width * (22.5f / 1024f)),
		                              Screen.width - (Screen.width * (22.5f / 1024f)) - UPGRADE_BUTTON_WIDTH);

		// set the correct y location for the button
		spawnPosition.y = Mathf.Clamp(m_ScreenPosition.y, 
		                              0, 
		                              Screen.height - UPGRADE_BUTTON_HEIGHT 
		                              - Screen.height * (30f / 768f) 
		                              - SELL_BUTTON_HEIGHT);

		// create the rectangle where the power button will be drawn in
		Rect rect = new Rect(
			spawnPosition.x + Screen.width * (22.5f / 1024f), 
			spawnPosition.y, 
            UPGRADE_BUTTON_WIDTH,
			UPGRADE_BUTTON_HEIGHT);
	
		return rect;	// return the location for the power button
	}

	/**
	 * Returns a rectangle with an adjusted position for
	 * the next button
	 */
	private Rect GetSpeedButtonRect()
	{
		Vector2 spawnPosition;		// for storing the speed button location

		// set the correct x location for the button
		spawnPosition.x = Mathf.Clamp(m_ScreenPosition.x, 
		                              (UPGRADE_BUTTON_WIDTH + Screen.width * (22.5f / 1024f)),
		                              Screen.width - (Screen.width * (22.5f / 1024f)) - UPGRADE_BUTTON_WIDTH);

		// set the correct y location for the button
		spawnPosition.y = Mathf.Clamp(m_ScreenPosition.y, 
		                              0, 
		                              Screen.height - UPGRADE_BUTTON_HEIGHT 
		                              - Screen.height * (30f / 768f) 
		                              - SELL_BUTTON_HEIGHT);

		// create the rectangle where the speed button will be drawn in
		Rect rect = new Rect(
			spawnPosition.x - UPGRADE_BUTTON_WIDTH - Screen.width * (22.5f / 1024f), 
			spawnPosition.y, 
			UPGRADE_BUTTON_WIDTH,
			UPGRADE_BUTTON_HEIGHT);
		
		return rect;		// return the location for the speed button
	}

	/**
	 * Returns a rectangle with an adjusted position for
	 * the next button
	 */
	private Rect GetSellButtonRect()
	{
		Vector2 spawnPosition;			// for storing the speed button location

		// set the correct x location for the button
		spawnPosition.x = Mathf.Clamp(m_ScreenPosition.x, 
		                              (UPGRADE_BUTTON_WIDTH + Screen.width * (22.5f / 1024f)),
		                              Screen.width - (Screen.width * (22.5f / 1024f)) - UPGRADE_BUTTON_WIDTH);

		// set the correct y location for the button
		spawnPosition.y = Mathf.Clamp(m_ScreenPosition.y, 
		                              0, 
		                              Screen.height - UPGRADE_BUTTON_HEIGHT 
		                              - Screen.height * (30f / 768f) 
		                              - SELL_BUTTON_HEIGHT);

		// create the rectangle where the sell button will be drawn in
		Rect rect = new Rect(
			spawnPosition.x - SELL_BUTTON_WIDTH / 2, 
			spawnPosition.y + UPGRADE_BUTTON_HEIGHT + Screen.height * (30f / 768f), 
			SELL_BUTTON_WIDTH,
			SELL_BUTTON_HEIGHT);
		
		return rect;			// return the location for the sell button
	}

	/**
	 * controls showing the speed upgrade button
	 */
	private void ShowSpeedUpgrade()
	{
		if (m_Tower.ActiveModelName == "Base")									// if the current model is the base model
		{
			speedActive.fontSize = (int)Mathf.Ceil(Screen.width * .0106f);		// set the font size for the speed upgrade text

			// if we are in the tutorial, the speed will be greyed out for the 2nd upgrade
			if (Application.loadedLevelName == "SmallIntestineTutorial" && 
			    PlayerPrefs.GetInt("SIStats_towersUpgraded") == 1)
			{
				GUI.Button(GetSpeedButtonRect(), "", speedInactive);
				return;
			}

			// otherwise we draw the button as normal as long as the user has enough nutrients to upgrade
			if(m_GameManager.nutrients - m_Tower.TOWER_UPGRADE_LEVEL_1_COST < 0)
			{
				// if there weren't enough nutrients we draw the inactive button
				GUI.Button(GetSpeedButtonRect(), "", speedInactive);
			}
			else if (GUI.Button(GetSpeedButtonRect(), "Speed (" + -m_Tower.TOWER_UPGRADE_LEVEL_1_COST + ")", speedActive))
			{
				// if there were enough we draw the active upgrade button
				// if the user clicks on the upgrade button:
				m_Tower.UpgradeSpeed();					// call the upgrade speed function
				IsEnabled = false;						// disable showing the tower menu
				m_GameManager.setTowerMenuUp(false);	// also change the value in the game manager
			}
		} else 			// if we're not in the base model
		{
			speedActive.fontSize = (int)Mathf.Ceil(Screen.width * .0106f);		// set the relative font size 

			// block upgrading speed a second time in the first part of the tutorial level
			if (Application.loadedLevelName == "SmallIntestineTutorial" && 
			 PlayerPrefs.GetInt("SIStats_towersUpgraded") == 1)
			{
				GUI.Button(GetSpeedButtonRect(), "", speedInactive);
				return;
			}

			// if we aren't in the tutorial we can do upgrades as normal
			if(m_GameManager.nutrients - m_Tower.TOWER_UPGRADE_LEVEL_2_COST < 0)
			{
				// if we don't have enough nutrients draw the inactive button
				GUI.Button(GetSpeedButtonRect(), "", speedInactive);
			}
			else if (GUI.Button(GetSpeedButtonRect(), "Speed (" + -m_Tower.TOWER_UPGRADE_LEVEL_2_COST + ")", speedActive))
			{
				// if we had enough draw the active upgrade button
				// if the button is clicked on
				m_Tower.UpgradeSpeed();					// call the upgrade speed function
				IsEnabled = false;						// disable showing the tower menu
				m_GameManager.setTowerMenuUp(false);	// also change the value in the game manager
			}
		}
	}

	/**
	 * controls showing the power upgrade button
	 */
	private void ShowPowerUpgrade()
	{
		if (m_Tower.ActiveModelName == "Base")								// if the current model is the base model
		{
			powerActive.fontSize = (int)Mathf.Ceil(Screen.width * .0106f);	// set the font size for the speed upgrade text

			// if we are in the tutorial the power will always be greyed out initially
			if (Application.loadedLevelName == "SmallIntestineTutorial" && 
			    PlayerPrefs.GetInt("SIStats_towersUpgraded") == 0)
			{
				GUI.Button(GetPowerButtonRect(), "", powerInactive);
				return;
			}

			// otherwise we draw the button as normal as long as the user has enough nutrients to upgrade
			if(m_GameManager.nutrients - m_Tower.TOWER_UPGRADE_LEVEL_1_COST < 0)
			{
				// if there weren't enough nutrients we draw the inactive button
				GUI.Button(GetPowerButtonRect(), "", powerInactive);
			}
			else if (GUI.Button(GetPowerButtonRect(), "Power (" + -m_Tower.TOWER_UPGRADE_LEVEL_1_COST + ")", powerActive))
			{
				// if we had enough draw the active upgrade button
				// if the button is clicked on
				m_Tower.UpgradePower();					// call the upgrade function
				IsEnabled = false;						// disable the tower menu from showing
				m_GameManager.setTowerMenuUp(false);	// also change the variable value in the game manager
			}
		} else
		{
			powerActive.fontSize = (int)Mathf.Ceil(Screen.width * .0106f);	// set the font size for the speed upgrade text

			if(m_GameManager.nutrients - m_Tower.TOWER_UPGRADE_LEVEL_2_COST < 0)
			{
				// if there weren't enough nutrients we draw the inactive button
				GUI.Button(GetPowerButtonRect(), "", powerInactive);
			}
			else if (GUI.Button(GetPowerButtonRect(), "Power (" + -m_Tower.TOWER_UPGRADE_LEVEL_2_COST + ")", powerActive))
			{
				// if we had enough draw the active upgrade button
				// if the button is clicked on
				m_Tower.UpgradePower();					// call the upgrade function
				IsEnabled = false;						// disable the tower menu from showing
				m_GameManager.setTowerMenuUp(false);	// also chang ethe variable value in the game manager
			}
		}
	}

	/**
	 * handles showing the sell button
	 */
	private void ShowSell(GUIStyle style)
	{
		if (Application.loadedLevelName.Equals("SmallIntestineTutorial"))
		{
			GUI.Button(GetSellButtonRect(), "", sellInactive);
			return;
		} else if (GUI.Button(GetSellButtonRect(), "", style))
		{
			// if the sell button is clicked on	
			IsEnabled = false;						// disable showing the menu
			m_GameManager.setTowerMenuUp(false);	// also change this variable value in the game manager
			displaySellBox = true;					// set the flag to display the sell confirm box
			m_GameManager.setSellBoxUp(true);		// also change the flag for this in the game manager
		}
	}

	/**
	 * function that handles selling
	 * if the sell button is clicked on it is sold and 60% of the nutrients spent to get the current
	 * active tower menu is reimbursed
	 */
	private void Sell()
	{
		// refund nutrients
		switch (m_Tower.ActiveModelName)
		{
		case "Base":
			m_GameManager.nutrients = m_GameManager.nutrients + (int)(.6*(m_Tower.TOWER_BASE_COST));
			break;
		case "Speed1":
			m_GameManager.nutrients = m_GameManager.nutrients + (int)(.6*(m_Tower.TOWER_BASE_COST +
				m_Tower.TOWER_UPGRADE_LEVEL_1_COST));
			break;
		case "Speed2":
			m_GameManager.nutrients = m_GameManager.nutrients + (int)(.6*(m_Tower.TOWER_BASE_COST +
				m_Tower.TOWER_UPGRADE_LEVEL_1_COST + m_Tower.TOWER_UPGRADE_LEVEL_2_COST));
			break;
		case "Power1":
			m_GameManager.nutrients = m_GameManager.nutrients + (int)(.6*(m_Tower.TOWER_BASE_COST +
				m_Tower.TOWER_UPGRADE_LEVEL_1_COST));
			break;
		case "Power2":
			m_GameManager.nutrients = m_GameManager.nutrients + (int)(.6*(m_Tower.TOWER_BASE_COST +
				m_Tower.TOWER_UPGRADE_LEVEL_1_COST + m_Tower.TOWER_UPGRADE_LEVEL_2_COST));
			break;
		default:
			break;
		} 

		// play sound
		Instantiate (sellSound);

		// track stats
		PlayerPrefs.SetInt ("SIStats_towersSold", PlayerPrefs.GetInt("SIStats_towersSold") + 1);
		PlayerPrefs.Save();
		
		Destroy(gameObject);
	}
}
