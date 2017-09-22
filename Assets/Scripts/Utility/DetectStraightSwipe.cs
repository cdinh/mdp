using UnityEngine;
using System.Collections;

/**
 * this script detects a swipe and what the direction of the swipe is anywhere on the screen
 */
public class DetectStraightSwipe : MonoBehaviour 
{
	// to remember the direction of a swipe gesture
	private bool swipeUp;						//!< will be true if the swipe was upwards
	private bool swipeDown;						//!< will be true if the swipe was downwards
	private bool swipeRight;					//!< will be true if the swipe was right
	private bool swipeLeft;						//!< will be true if the swipe was left

	// for detecting a swipe
	private float xStart = 0.0f;				//!< will store the starting x coordinate of a touch
	private float xEnd = 0.0f;					//!< will store the ending x coordinate of a touch
	private float yStart = 0.0f;				//!< will store the starting y coordinate of a touch
	private float yEnd = 0.0f;					//!< will store the ending y coordinate of a touch
	
	/**
	 * Update is called once per frame
	 * Looking for touch updates and determines swipe direction
	 */
	void Update () 
	{
		foreach (Touch touch in Input.touches) 				// go through the touches detected
		{
			if (touch.phase == TouchPhase.Began) 			// if the touch began set the initial coordinates
			{
				xStart = touch.position.x;
				yStart = touch.position.y;
			}
			if (touch.phase == TouchPhase.Ended) 			// when the finger moves update the end coordinates to see 
															// if they moved enough to count as a page turn
			{
				xEnd = touch.position.x;
				yEnd = touch.position.y;
				
				if ((xStart - xEnd) > 30) 					// a leftwards swipe was detected
				{
					swipeLeft = true;						// change the indicator variable to indicate a left swipe
				}

				if ((yStart - yEnd) > 30)					// a downwards swipe was detected
				{
					swipeDown = true;						// change the indicator variable to indicate a down swipe
				}

				if ((xEnd - xStart) > 30)					// a rightwards swipe was detected
				{
					swipeRight = true;						// change the indicator variable to indicate a right swipe
				}

				if ((yEnd - yStart) > 30)					// an upwards swipe was detected
				{
					swipeUp = true;							// change the indicator variable to indicate an up swipe
				}
			}
		}
		
		// for pc/mac
		// this code corresponds exactly to the ipad code except checks mouse position
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		if(Input.GetMouseButtonDown(0))
		{
			xStart = Input.mousePosition.x;
			yStart = Input.mousePosition.y;
		}
		if (Input.GetMouseButtonUp(0)) 
		{
			xEnd = Input.mousePosition.x;
			yEnd = Input.mousePosition.y;

			if ((xStart - xEnd) > 30) 
			{
				swipeLeft = true;
			}
			
			if ((yStart - yEnd) > 30)
			{
				swipeDown = true;
			}
			
			if ((xEnd - xStart) > 30)
			{
				swipeRight = true;
			}
			
			if ((yEnd - yStart) > 30)
			{
				swipeUp = true;
			}
		}
#endif
	}

	/**
	 * function that can be called to check if there was an upwards swipe
	 */
	public bool getSwipeUp()
	{
		return swipeUp;
	}

	/**
	 * function that can be called to check if there was a downwards swipe
	 */
	public bool getSwipeDown()
	{
		return swipeDown;
	}

	/**
	 * function that can be called to check if there was a right swipe
	 */
	public bool getSwipeRight()
	{
		return swipeRight;
	}

	/**
	 * function that can be called to check if there was a left swipe
	 */
	public bool getSwipeLeft()
	{
		return swipeLeft;
	}

	/**
	 * function to reset the variables after they were read so that next time they can be reassigned correctly
	 */
	public void resetSwipe()
	{
		swipeUp = false;
		swipeDown = false;
		swipeLeft = false;
		swipeRight = false;
	}
}
