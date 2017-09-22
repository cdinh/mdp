using UnityEngine;
using System.Collections;

/**
 * class to draw the enzyme dude for the enzyme game
 */
public class Drag : MonoBehaviour
{		
	private Vector3 lastPosition;				//!< to hold the (x,y,z) coordinate of the last position
	private bool left;							//!< mark whether the guy should be facing left or right

	/**
	 * Initialization.
	 * Start multitouch.
	 */
	void Start ()
	{
		Input.multiTouchEnabled = true;	// need to enable multitouch since we are using two fingers to drag
		left = true;					// direction the enzyme guy is facing (IPAD ONLY)			
	}

	/**
	 * Called once per frame.
	 * Handles moving the enzyme guy
	 */
	void Update ()
	{
		// go to keyboard input function if we are in the editor
#if UNITY_EDITOR
		GetKeyboardInput();
#endif
		// use touch controls to move the guy if on ipad
		// the touch guesture is a two finger drag
		if (Input.touches.Length == 2) 
		{
			Touch finger1 = Input.touches [0];		// assign the first touch to one variable
			Touch finger2 = Input.touches [1];		// assign the other touch to another
			Camera camera = Camera.main;

			// map the touches and then alter them to the right "height" by altering the z coordinate
			Vector3 finger1Pos = camera.ScreenToWorldPoint (finger1.position);	
			Vector3 finger2Pos = camera.ScreenToWorldPoint (finger2.position);
			finger1Pos = new Vector3 (finger1Pos.x, finger1Pos.y, .1f);
			finger2Pos = new Vector3 (finger2Pos.x, finger2Pos.y, .1f);
	
			// this processes the actual touches
			if (Vector3.Distance (transform.position, finger1Pos) < 10 
				&& Vector3.Distance (transform.position, finger2Pos) < 10) 
			{
				// determine which touch is on top and bottom and find the difference
				Vector3 low = finger1Pos.y < finger2Pos.y ? finger1Pos : finger2Pos;
				Vector3 high = finger1Pos.y < finger2Pos.y ? finger2Pos : finger1Pos;
				Vector3 differenceVector = high - low;

				Vector3 crossVector = new Vector3 (0, 0, 10);
				Vector3 direction = Vector3.Cross (differenceVector, crossVector);
				Vector3 middle = low + (differenceVector / 2);

				// set the direction the enzyme guy is facing (IPAD ONLY)
				if(middle.x - transform.position.x < -.05)
				{
					left = true;
				} else if(middle.x - transform.position.x > .05)
				{
					left = false;
				}

				if (!left)
				{
					direction = direction * -1;
				}

				transform.position = middle;
				float angle = Mathf.Rad2Deg * Mathf.Atan2 (direction.y, direction.x);
				transform.eulerAngles = new Vector3 (0, 0, angle);
			}
		}
	}

	/**
	 * function to move guy with keyboard input in the unity editor
	 */
	void GetKeyboardInput ()
	{
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.position += new Vector3 (-.05f, 0, 0);
		} else if (Input.GetKey (KeyCode.D)) 
		{
			transform.position += new Vector3 (.05f, 0, 0);
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			transform.position += new Vector3 (0, .05f, 0);
		} else if (Input.GetKey (KeyCode.S)) 
		{
			transform.position += new Vector3 (0, -.05f, 0);
		}
	}
}