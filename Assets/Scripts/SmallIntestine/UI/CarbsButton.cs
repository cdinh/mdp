using UnityEngine;
using System.Collections;

/**
 * Scripts for all buttons except protein are similar but have slight differences
 */
public class CarbsButton : MonoBehaviour 
{
	public Texture activeTexture;	//!< hold the testure for the button's "active" mode
	public Texture pressedTexture;	//!< hold the texture for the button's "pressed" mode
	public Texture inactiveTexture;	//!< hold the texture for the button's "inactive" mode

	private float buttonTop;		//!< button top y coordinate
	private float buttonLeft;		//!< button left x coordinate
	private float buttonWidth;		//!< width of a button
	private float buttonHeight;		//!< height of a button
	private float buttonSpacing;	//!< the spacing between buttons
	
	private const int buttonColorCode = 3;	//!< this is from old legacy code to maintain the proper tower color
	
	private TowerSpawner towerSpawner;	//!< hold a reference to the TowerSpawner script
	
	/**
	 * Use this for initialization
	 * Sets up conditions to draw the carbs button properly
	 */
	void Start () 
	{
		// set the values for the carbs button
		// the carbs button is the fourth button from the left
		buttonWidth = Screen.width * 0.197f;					// set the button width to a value relative to screen size
		buttonHeight = Screen.height * 0.091f;					// set the button height relative to screen size
		buttonTop =  (Screen.height * 0.11f) - buttonHeight;	// set the top coordinate of the button relative to screen size
		buttonSpacing = Screen.width * 0.0123f;					// set the button spacing relative to screen size
		buttonLeft = Screen.width * 0.0148f + 3*(buttonWidth + buttonSpacing);	// set the button left coordinate relative to screen size

		// pass the calculated button location into the pixelinset, which is where it is drawn
		GetComponent<GUITexture>().pixelInset = new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight);

		// find the reference to the towerSpawner
		towerSpawner = GameObject.Find ("GUI").GetComponent<TowerSpawner> ();
	}
	
	/**
	 * Update is called once per frame
	 * Checks for which version of the carbs button to draw and draws it.
	 * Spawns a tower when the button is clicked
	 */
	void Update () 
	{
		// first we check which texture to draw
		// if there are not enough nutrients we draw the inactive texture
		// if there are enough nutrients we draw the normal texture
		// however if we are in the tutorial we have to make sure two protein towers are placed before we allow
		// any carb towers to be placed
		if (towerSpawner.getGameManager().nutrients - towerSpawner.TOWER_BASE_COST < 0
		    || Application.loadedLevelName == "SmallIntestineTutorial" && PlayerPrefs.GetInt("SIStats_towersUpgraded") < 2)
		{
			GetComponent<GUITexture>().texture = inactiveTexture;	// when the button is inactive show the "inactive" texture
			return;
		} else if (GetComponent<GUITexture>().HitTest(Input.mousePosition) == true || 
		           Input.touches.Length > 0 && GetComponent<GUITexture>().HitTest(Input.touches[0].position) == true) // checks if we clicked on button
		{	
			// if the button is being pressed draw the pressed texture
			foreach (Touch touch in Input.touches) 
			{
				if (touch.phase == TouchPhase.Began) 
				{
					GetComponent<GUITexture>().texture = pressedTexture;
					
					// code to spawn towers
					if (!towerSpawner.getIsSpawnActive())
					{
						towerSpawner.SpawnTower(towerSpawner.AvailableColors[buttonColorCode]);	// spawn a tower of the specified color
						towerSpawner.getSpawnedTower().GetComponent<Tower> ().enabled = false;	// by default disable the tower until 
																								// it's sucessfully placed
						return;
					}
				}
				if (touch.phase == TouchPhase.Ended) 
				{
					GetComponent<GUITexture>().texture = activeTexture;	// when we aren't pressing a button then draw the "active" texture
				}
			}

			// for pc/mac
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
			if(Input.GetMouseButtonDown(0))
			{
				GetComponent<GUITexture>().texture = pressedTexture;
				
				// code to spawn towers
				if (!towerSpawner.getIsSpawnActive())
				{
					towerSpawner.SpawnTower(towerSpawner.AvailableColors[buttonColorCode]);
					towerSpawner.getSpawnedTower().GetComponent<Tower> ().enabled = false;
					return;
				}
			}else if (Input.GetMouseButtonUp(0)) 
			{
				GetComponent<GUITexture>().texture = activeTexture;
			} else
			{
				GetComponent<GUITexture>().texture = activeTexture;
			}
#endif
		} else
		{
			GetComponent<GUITexture>().texture = activeTexture;
		}
	}
}
