using UnityEngine;
using System.Collections;

/**
 * sample code to animate a background
 * this was used for the animated mouth game example for GLS build
 */
public class AnimatedBackground : MonoBehaviour 
{
	public Texture[] stills;			//!< store the animations stills
	public AudioClip[] audioClips;		//!< store the audio clips to go with animation frames (if necessary)
	public int[] numSlides;

	private int currPage = 0;			//!< store the current page
	private int currGroup = 0;

	private bool allowSwitch = false;	//!< to indicate whether or not the user can switch past the page
										//!< because the animation is over

	public Texture corner;				//!< the texture for the page turn corner

	private int numInGroup;				//!< to store the number in the image group (slides per audio clip)
	private float slideTimeout = 1f;	//!< time to keep a specific frame up
	private int currentSound;			//!< index for the current sound

	public float animationDelay;		//!< the amount of delay between slides in seconds

	// for detecting a swipe
	private float xStart = 0.0f;		//!< will store the starting x coordinate of a swipe gesture
	private float xEnd = 0.0f;			//!< will store the ending x coordinate of a swipe gesture
	private bool swipe = false;			//!< flag to indicate if a swipe happened

	// check for playthrough
	private bool canSkip = false;		//!< indicate whether or not the user can switch past the animation
										//!< before it is finished playing because they have already watched it
	
	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		GetComponent<AudioSource>().clip = audioClips[currGroup];	// set the audio clip to the inital clip
		numInGroup = numSlides[currGroup];	// get the initial number in the current slide group
		numInGroup--;						// decrement the counter
		GetComponent<AudioSource>().Play();						// start playing the audio
	}
	
	/**
	 * Update is called once per frame.
	 * Update in this script handles changing the slide animation with the audio as necessary.
	 * Also checks for swipe gestures.
	 */
	void Update () 
	{
		if(slideTimeout > 0)				// check if the current slide time is up
		{
			slideTimeout -= Time.deltaTime;	// if it wasn't, then increase the timer
		} else if(!allowSwitch)				// if the current slide time is up and the animation is not over
		{
			if(numInGroup > 0)				// check if there are still slides left to show in the current group
			{								// if there is
				numInGroup--;				// decrement the counter
				currPage++;					// increase the current page counter
				slideTimeout = 1f;			// reset the slide timeout
			} else if(!GetComponent<AudioSource>().isPlaying)		// if there are no more slides in the current group
											// and the audio is done playing
			{
				currGroup++;				// move onto the next group; indicate by increasing group counter
				if(currGroup >= audioClips.Length)	// make sure there are still more groups by comparing to the # of audio clips
				{							// if there aren't more groups
					currGroup--;			// decrement the group counter 
					allowSwitch = true;		// throw up a flag to indicate that the user can now switch as it's ove
				}
				else 						// otherwise, there are still more groups so
				{
					GetComponent<AudioSource>().clip = audioClips[currGroup];	// set the next audio clip
					GetComponent<AudioSource>().Play();						// start playing  the next audio clip
					numInGroup = numSlides[currGroup];	// reset the counter of number of slides in the current group for this new group
				}
			}
		}

		// if we are allowed to switch pages
		// the user switches pages by a swipe motion on ipad
		// the user switches pages with the spacebar on pc/mac
		if (allowSwitch | canSkip)
		{
			foreach (Touch touch in Input.touches) 
			{
				if (touch.phase == TouchPhase.Began) 
				{
					xStart = touch.position.x;		// record the starting swipe position
				}
				if (touch.phase == TouchPhase.Moved) 
				{
					xEnd = touch.position.x;		// record the ending swipe position
					
					if ((xStart - xEnd) > 30) 		// if the swipe was a certain "size" or larger, then accept it
					{
						swipe = true;				// indicates a swipe happened
					}
				}
			}
			if(Input.GetKeyDown(KeyCode.Space))
				swipe = true;
		}
		
		// if a swipe was detected, move on to the next scene
		// at the time of using this code, since it was for the mouth sample animation, we moved on to the
		// small intestine storyboard. However, this can be changed if this code is reused for something else.
		if (swipe == true)
		{
			Application.LoadLevel("SmallIntestineStoryboard");
		}
	}

	/**
	 * Draws the background textures and draws an image of a page turn in the corner if necessary
	 */
	void OnGUI()
	{
		// draw the background image the size of the entire screen
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), stills[currPage]);

		// if the user is allowed to switch, indicate so by drawing a page corner on the top right of the screen
		if(allowSwitch | canSkip)
		{
			GUI.DrawTexture(new Rect(Screen.width * .84f, 0, Screen.width * .16f, Screen.width * .16f), corner);
		}
	}
}