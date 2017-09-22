using UnityEngine;
using System.Collections;

/**
 * This class programs the effects of the plus signs in the SI game.
 */
public class GrowAndDie : MonoBehaviour 
{
	private float timeAlive = 0;		//!< the time it takes for the plus sign to "grow and die"
	private Vector3 originalScale;		//!< vector to store the original size of the plus
	public Texture plus;				//!< the texture for the plus sign

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		originalScale = transform.localScale;
	}
	
	/**
	 * Update is called once per frame
	 * Makes sure the plus sign is only shown for the desired time
	 */
	void Update () 
	{
		timeAlive += Time.deltaTime;			// increament the counter of the time the plus has been alive

		if (timeAlive < 1.0f) 					// if the time alive is less than 1 sec, grow the plus sign larger
		{
			transform.localScale = originalScale + new Vector3(timeAlive, timeAlive, timeAlive);
		}
		else 									// if the time alive is over 1 sec then just destroy the plus sign
		{
			Destroy(this.gameObject);
		}
	}

	/**
	 * Handles drawing the plus sign at the correct location
	 */
	void OnGUI()
	{
		GUI.depth = -1000;		// change the gui layering to make sure the plus draws on top of all gui elements

		// draw plus sign by nutrients text
		GUI.DrawTexture(new Rect(34.5f/100f * Screen.width, 
		                         83.3f/100f * Screen.height, 
		                         (1f + timeAlive)/80f * 3f/4f * Screen.width,
		                         (1f + timeAlive)/80f * Screen.height), plus);
	}
}
