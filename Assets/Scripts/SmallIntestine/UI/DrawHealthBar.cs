using UnityEngine;
using System.Collections;

/**
 * script that handles drawing the health bar in the bottom right corner in the si game
 */
public class DrawHealthBar : MonoBehaviour 
{
	private Rect healthRect;			//!< to store the coordinates of the health rectangle
	private float percent;				//!< to store the percentage of health remaining (between 0 and 1)

	/**
	 * Use this for initialization
	 * Sets the location to draw the health rect
	 */
	void Start () 
	{
		// set the coordinates of the health bar rectangle relative to the screen size
		healthRect = new Rect (Screen.width * 0.952f, Screen.height * (1f - 0.8622f) - Screen.height * 0.0948f, 
		                       Screen.width * 0.032f, Screen.height * 0.0948f);
	}
	
	/**
	 * Update is called once per frame
	 * Updates the drawing based on amount of health left
	 */
	void Update () 
	{
		// update the pixel inset value for the health bar to set where it will draw
		// height * percent will cause the bar to grow or shrink based on % of health remaining
		GetComponent<GUITexture>().pixelInset = new Rect(healthRect.xMin, healthRect.yMin, 
		                                 healthRect.width, percent*healthRect.height);
	}

	/**
	 * function that can be used to change the variable holding the percentage of health remaining
	 */
	public void setPercent(float percent)
	{
		this.percent = percent;
	}
}
