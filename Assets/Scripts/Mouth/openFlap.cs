using UnityEngine;
using System.Collections;

/**
 * script that handles opening and closing the uvula and epiglottis flaps in the mouth game
 */
public class openFlap : MonoBehaviour 
{
	private GameObject bottomFlap, topFlap;		//!< to hold references to the top and bottom flaps
	private bool isOpen;						//!< flag to signify whether the flaps are "open" or "closed"

	private GameObject[] foods;

	private bool cough = false;					//!< flag to signify whether a cough action is currently occuring
	private float coughTimer;					//!< a timer for how long the food should move in a reversed direction during a cough

	// swipe varaibles
	private float xStart = 0.0f;				//!< will hold where a touch starts in the x direction
	private float xEnd = 0.0f;					//!< will hold where a touch ends in the x direction
	private float yStart = 0.0f;				//!< will hold where a touch starts in the y direction
	private float yEnd = 0.0f;					//!< will hold where a touch ends in the y direction
	private bool swipe = false;					//!< flag to hold whether there was any type of swipe
	private bool swipeUp = false;				//!< flag to hold whether there was an upward swipe direction
	private bool swipeDown = false;				//!< flag to hold whether there was a downward swipe direction
	public float swipeSize = 15.0f;

	private int swipeCount;						//!< to keep track of how many times the swipe happened
	private int coughCount;


	/**
	 * Use this for initialization
	 * Finds references to the flaps and orders them correctly
	 */
	void Start () 
	{
		coughTimer = 0f;						// make sure the initial cough timer value is 0
		Debug.Log ("Screen width = " + Screen.width + " Screen height = " + Screen.height);
		foods = GameObject.FindGameObjectsWithTag ("MouthFood");
		swipeCount = 0;
		coughCount = 0;
		// find flaps and properly determine which one is the uvula (top flap) or epiglottis (bottom flap)
		foreach(Transform child in transform)
		{
			// properly assign the right flap to top and bottom
			if(child.gameObject.name == "flap1")
			{
				bottomFlap = child.gameObject;
			}
			else
			{
				topFlap = child.gameObject;
			}
		}
	}
	
	/**
	 * Update is called once per frame
	 * Checks for touch inputs and opens/closes the flaps accordingly.
	 */
	void Update () 
	{
		foods = GameObject.FindGameObjectsWithTag ("MouthFood");
		// update the coughing variable accordingly to maintain proper collision detection
		if (coughTimer > 0)					// if there is time left in the cough
		{
			coughTimer -= Time.deltaTime;	// decrement the cough timer by the time passed between update
		} else 								// if the coughTimer is 0 or less, end the cough by switching the cough flag
		{
			cough = false;
		}

		// ipad touch detection
		// the touch is only valid in a certain range on the screen
		// this is so that the player has to make the swipe motion somewhere near the flaps for it to count
		foreach (Touch touch in Input.touches) 
		{
			// when a touch begins, check whether the toucch was in the valid range
			// if it was assign the x and y start positions
			if (touch.phase == TouchPhase.Began) 
			{
				if ((touch.position.x >= .7f*Screen.width && touch.position.x <= Screen.width) && 
				    (touch.position.y <= .8f*Screen.height && touch.position.y >= 0))
				{
					xStart = touch.position.x;
					yStart = touch.position.y;
				}
			}

			// when a touch ends, check whether the touch ended in a valid range
			// if it was then assign the x and y end positions
			if (touch.phase == TouchPhase.Ended)
			{
				if ((touch.position.x >= .7f*Screen.width && touch.position.x <= Screen.width) && 
				    (touch.position.y <= .8f*Screen.height && touch.position.y >= 0))
				{
					xEnd = touch.position.x;
					yEnd = touch.position.y;
				}

				// find the size of the swipe and check if it was larger than a certain size (in this case 10)
				if (Mathf.Sqrt((xStart - xEnd)*(xStart - xEnd)+(yStart - yEnd)*(yStart - yEnd)) > swipeSize) 
				{
					swipe = true;						// if the swipe size was large enough, mark that a swipe happened
					
					if (yStart < yEnd)					// if the yStart is lower than the yEnd, the swipe was up
					{
						swipeUp = true;					// throw the swipe up flag
					} else if (yStart > yEnd)			// otherwise if the yStart is more than the yEnd, the swipe was down
					{
						swipeDown = true;				// throw the swipe down flag
					} else 								// otherwise the swipe was horizontal so just reverse the flags
														// and assume the user knew what they were doing
					{
						swipeDown = !swipeDown;
						swipeUp = !swipeUp;
					}
				}
			}
		}

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		// for PC/MAC version
		// this does the same thing as the ipad version except that you can use the mouse to replace the touch
		if(Input.GetMouseButtonDown(0))
		{
//			Debug.Log ("Touch Began: x = " + Input.mousePosition.x + " y = " + Input.mousePosition.y);
			if ((Input.mousePosition.x >= .7f*Screen.width && Input.mousePosition.x <= Screen.width) && 
			    (Input.mousePosition.y <= .8f*Screen.height && Input.mousePosition.y >= 0))
			{
				xStart = Input.mousePosition.x;
				yStart = Input.mousePosition.y;
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
//			Debug.Log ("Touch End: x = " + Input.mousePosition.x + " y = " + Input.mousePosition.y);
			if ((Input.mousePosition.x >= .7f*Screen.width && Input.mousePosition.x <= Screen.width) && 
			    (Input.mousePosition.y <= .8f*Screen.height && Input.mousePosition.y >= 0))
			{
				xEnd = Input.mousePosition.x;
				yEnd = Input.mousePosition.y;
			}

			if (Mathf.Sqrt((xStart - xEnd)*(xStart - xEnd)+(yStart - yEnd)*(yStart - yEnd)) > swipeSize) 
			{
				swipe = true;
			
				if (yStart < yEnd)
				{
					swipeUp = true;
				} else if (yStart > yEnd)
				{
					swipeDown = true;
				} else
				{
					swipeDown = !swipeDown;
					swipeUp = !swipeUp;
				}
			}
		}
#endif
	
		// if a swipe was detected then handle the type of swipe accordingly
		if (swipe)
		{
			if (swipeUp)													// if the swipe was upwards
			{
				isOpen = false;												// close the flaps
				bottomFlap.GetComponent<BoxCollider>().enabled = true;		// enable the collider for coughing on bottomFlap
				topFlap.GetComponent<BoxCollider>().enabled = true;			// endable the collider for coughing on topFlap

				// reset variables
				xStart = 0.0f;
				xEnd = 0.0f;
				yStart = 0.0f;
				yEnd = 0.0f;
				swipeUp = false;

				// swipe counter +1
				swipeCount += 1;

			} else if (swipeDown)											// if the swipe was downwards
			{
				isOpen = true;												// open the flaps
				bottomFlap.GetComponent<BoxCollider>().enabled = false;		// remove the collider for coughing on bottomFlap
				topFlap.GetComponent<BoxCollider>().enabled = false;		// remove the collider for coughing on topFlap
				
				// reset variables
				xStart = 0.0f;
				xEnd = 0.0f;
				yStart = 0.0f;
				yEnd = 0.0f;
				swipeDown = false;

				// swipe counter +1
				swipeCount += 1;
			}
		}
	}


	/**
	 * function that can be called to return whether the flaps are currently open
	 */
	public bool isEpiglotisOpen()
	{
		return isOpen;
	}

	/**
	 * function that can be called to indicate that a cough is happening
	 */
	public void setCough()
	{
		if(!GetComponent<AudioSource>().isPlaying)		// start playing the couugh audio if it's not already playing
		{
			GetComponent<AudioSource>().Play();
		}
		coughTimer = 1.950f;			// set the cough timer to the length of the cough
		cough = true;				// throw the flag to indicate a cough is happening
		coughCount++;
	}

	/**
	 * function that can be called to return whether there is currently a cough happening
	 */
	public bool isCough()
	{
		return cough;
	}

	/**
	 * function that can be called to return the number of foods that has generated
	 */
	public int getFoodLength()
	{
		return foods.Length;
	}

	public int getSwipeCounts()
	{
		return swipeCount;
	}
	public int getCoughCounts(){
		return coughCount;
	}
}
