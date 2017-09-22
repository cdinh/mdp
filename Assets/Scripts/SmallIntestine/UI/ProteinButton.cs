using UnityEngine;
using System.Collections;

/**
 * Controls behavior for the protein button
 */
public class ProteinButton : MonoBehaviour 
{
	public Texture activeTexture;	//!< hold the testure for the button's "active" mode
	public Texture pressedTexture;	//!< hold the texture for the button's "pressed" mode
	public Texture inactiveTexture;	//!< hold the texture for the button's "inactive" mode
	
	private float buttonTop;		//!< button top y coordinate
	private float buttonLeft;		//!< button left x coordinate
	private float buttonWidth;		//!< width of a button
	private float buttonHeight;		//!< height of a button
	private float buttonSpacing;	//!< the spacing between buttons
	
	private const int buttonColorCode = 2;	//!< this is from old legacy code to maintain the proper tower color
	
	private TowerSpawner towerSpawner;	//!< hold a reference to the TowerSpawner script
	
	/**
	 * Use this for initialization
	 * Sets the size and location for drawing the protein button
	 */
	void Start () 
	{
		// set the values for the protein button
		// the protein button is the third button from the left
		buttonWidth = Screen.width * 0.197f;									// set the button width to a value relative to screen size
		buttonHeight = Screen.height * 0.091f;									// set the button height relative to screen size
		buttonTop =  (Screen.height * 0.11f) - buttonHeight;					// set the top coordinate of the button relative to screen 
		buttonSpacing = Screen.width * 0.0123f;									// set the button spacing relative to screen size
		buttonLeft = Screen.width * 0.0148f + 2*(buttonWidth + buttonSpacing);	// set the button left coordinate relative to screen size

		// pass the calculated button location into the pixelinset, which is where it is drawn
		GetComponent<GUITexture>().pixelInset = new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight);

		// find the reference to the towerSpawner
		towerSpawner = GameObject.Find ("GUI").GetComponent<TowerSpawner> ();
	}
	
	/**
	 * Update is called once per frame
	 * Handles drawing the proper button texture and spawning a tower when button is pressed
	 */
	void Update () 
	{
		// first we check which texture to draw
		// if there are not enough nutrients we draw the inactive texture
		// if there are enough nutrients we draw the normal texture
		// however if we are in the tutorial we have to make sure two protein towers are placed before we allow
		// any other towers to be placed.
		if (towerSpawner.getGameManager().nutrients - towerSpawner.TOWER_BASE_COST < 0 || 
		    Application.loadedLevelName == "SmallIntestineTutorial" && 
			(PlayerPrefs.GetInt("SITowerPlaceTutorial") == 0 ||(PlayerPrefs.GetInt("SIStats_towersPlaced") == 2 && //PlayerPrefs.GetInt("SIStats_towersUpgraded") < 2)))
				PlayerPrefs.GetInt("SIGlowTutorial") == 1 )))
		{
			GetComponent<GUITexture>().texture = inactiveTexture;	// when the button is inactive show the "inactive" texture
			return;
		} else if (GetComponent<GUITexture>().HitTest(Input.mousePosition) == true || 
		           Input.touches.Length > 0 && GetComponent<GUITexture>().HitTest(Input.touches[0].position) == true)	// checks if we clicked on button
		{	
			foreach (Touch touch in Input.touches) 
			{
				if (touch.phase == TouchPhase.Began) 
				{
					GetComponent<GUITexture>().texture = pressedTexture;				// if the button is being pressed draw the pressed texture

					
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
			} else if (Input.GetMouseButtonUp(0)) 
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
