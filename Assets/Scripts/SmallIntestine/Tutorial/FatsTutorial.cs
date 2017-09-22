using UnityEngine;
using System.Collections;

/**
 * Tutorial stuff for fats
 */
public class FatsTutorial : MonoBehaviour 
{
	// for zyme
	public GameObject zyme;
	private ZymePopupScript zymeScript;

	// store images that will pop up for the tutorial parts
	public Texture zymePopupImageFats1;
	public Texture zymePopupImageFats2;
	// arrow to assist with fats tutorial
	public Texture arrow;

	// bools to help with control flow
	private bool showTutorialPart1;
	private bool part1Done;
	private bool showTutorialPart2;
	private bool part2Done;

	// counter for timing of tutorial components
	public float maxTimeSinceStart;
	private float elapsedTimeSinceStart;

	// to hold a reference to the intestine game manager
	private IntestineGameManager gameManager;

	private Color Fats1Color = new Color(37f/255f, 97f/255f, 139f/255f, 1); 	//!< create a new color for the Fats1 Particles

	// Use this for initialization
	void Start () 
	{
		zymeScript = ((GameObject)Instantiate(zyme)).GetComponent<ZymePopupScript> ();
		// get a reference to the intestine game manager currently being used
		gameManager = GameObject.Find ("Managers").GetComponent<IntestineGameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// counting time
		elapsedTimeSinceStart += Time.deltaTime;

		// check if we should begin showing the tutorial
		if (!part1Done && PlayerPrefs.GetInt ("SIFatsTutorial") == 1 && elapsedTimeSinceStart > maxTimeSinceStart)
		{
			showTutorialPart1 = true;
		}

		// main control flow of tutorial.
		// checks if the desired tower was placed
		// gives nutrients to purchase the tower if they don't have enough
		if (PlayerPrefs.GetInt ("SIFatsTutorial") == 1 && !part2Done)
		{
			if (showTutorialPart1)
			{
				if (gameManager.nutrients < 20)
				{
					gameManager.nutrients += 20;
				}
				checkForTowerOfColor(Fats1Color);
			} else
			{
				if (gameManager.nutrients < 20)
				{
					gameManager.nutrients += 20;
				}
				checkForTowerOfColor(Color.white);
			}
		}
	}

	/*
	 * Function that checks current towers for an instantiated tower
	 * of the desired color. 
	 */
	private bool checkForTowerOfColor(Color color)
	{
		GameObject[] towers;

		// get all towers on the game board
		towers = GameObject.FindGameObjectsWithTag ("tower");

		// iterate through the towers, looking for ones that are actually instantiated and of
		// the desired color
		for (int i = 0; i < towers.Length; i++)
		{
			if (towers[i].GetComponent<Tower>().enabled == true && 
			    towers[i].GetComponent<Tower>().getColor() == color)
			{
				// if we find a fat1 tower
				if (color == Fats1Color)
				{
					// reset the variables for the fat1 tower placement 
					// and set the variables to indicate we are now
					// looking for a white tower
					zymeScript.setDraw(false);
					Time.timeScale = 1;
					showTutorialPart1 = false;
					showTutorialPart2 = true;
					part1Done = true;
					return true;
				}
				// if we find a white tower
				else if (color == Color.white)
				{
					// reset the variables for white tower placement
					// set the variables to move on to the next portion of
					// the tutorial (if any)
					zymeScript.setDraw(false);
					Time.timeScale = 1;
					PlayerPrefs.SetInt ("SIFatsTutorial", 0);
					PlayerPrefs.Save();
					showTutorialPart2 = false;
					part2Done = true;
					return true;
				}
			}
		}

		return false;
	}

	/*
	 * Handles drawing the correct zyme popup and pausing the game
	 * while the popup is active.
	 */
	void OnGUI()
	{
		if (showTutorialPart1)
		{
			zymeScript.setDraw(true);
			zymeScript.setImage(zymePopupImageFats1);
			Time.timeScale = .01f;
		}

		if (showTutorialPart2 && part1Done)
		{
			zymeScript.setDraw(true);
			zymeScript.setImage(zymePopupImageFats2);
			GUI.DrawTexture(new Rect(.3f*Screen.width, .62f*Screen.height, .45f*Screen.width, .3f*Screen.height), arrow);
			Time.timeScale = .01f;
		}
	}
}
