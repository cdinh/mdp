using UnityEngine;
using System.Collections;

/**
 * script to determine what happens when the "foodstuff" reaches the end point of the track
 */
public class FoodStuffFinish : MonoBehaviour 
{
	private SpawnFood foodSpawner;	//!< to hold a reference to the foodSpawner
	private MouthScore score;		//!< to hold a reference to the mouthScore to modify score
	private MouthLoadLevelCounter  level; //!< to hold a reference to the load level counter for switching levels
	public GameObject endGameScript;

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		GameObject mouthGUI = GameObject.Find("MouthGUI");						// find the mouth GUI
		score = mouthGUI.GetComponent<MouthScore>();							// get the score off the gui
		foodSpawner = GameObject.Find("FoodSpawner").GetComponent<SpawnFood>();	// find the foodSpawner
	}

	/**
	 * on trigger enter is called when the object enters into a collision with another object with a collider.
	 * This checks if the food stuff hits the end of the track and if so adds a score and destroys it.
	 */
	void OnTriggerEnter(UnityEngine.Collider obj)
	{
		// check if we got the food through the mouth by seeing if we collided with the end point
		if (obj.gameObject.tag == "MouthEnd")
		{
			score.collectFood();		// increase the score if we successfully swallowed the food
			OnEndPointCollision();		// handle cleaning up unused assets
		}
	}

	/**
	 * Function that cleans up destroying the food stuff and checks if the game is over.
	 */
	void OnEndPointCollision()
	{
		GameObject[] foodstuff = GameObject.FindGameObjectsWithTag("MouthFood"); // find all foodstuff on the game
		// check if there are any foodstuff left
		if(foodstuff.Length == 1 && foodSpawner.end)	// if this is the last foodstuff, then we won
		{
			// handle win condition
			GameObject chooseBackground = GameObject.Find("MouthChooseBackground");	// find the background chooser for
														// the level selection texture

			if (chooseBackground != null)				// if we found the background chooser get the level
			{
				level = chooseBackground.GetComponent<MouthLoadLevelCounter>();
				level.nextLevel();						// increase the level counter to correctly load the next 
														// level (if there is one)
			}
			//Application.LoadLevel("MouthStats");		// load the stats screen for the completed level
			Instantiate(endGameScript);
		}
		else 	// otherwise if there are more food stuff just destroy the current one that hit the end point
		{
			Destroy(this.gameObject);
		}
	}
}