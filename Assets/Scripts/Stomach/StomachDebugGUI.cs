using UnityEngine;
using System.Collections;

/**
 * Class with basic scripting functions to control the 
 * behavior of the debug gui in the stomach game
 */
public class StomachDebugGUI : MonoBehaviour 
{
	public Canvas ui;			//!< the actual ui canvas
	public RectTransform r;		//!< the rect transform to change the size and scale of the ui

	// Use this for initialization
	void Start () 
	{
		ui.enabled = false;
	}

	/**
	 * When the debug ui is enabled, pause the game and set 
	 * the values required for it to render properly
	 */
	public void enable()
	{
		Time.timeScale = 0f;
		ui.enabled = true;
		r.anchoredPosition = new Vector2 (1023f/2f, 768f/2f);
		r.localScale = new Vector3 (1f, 1f, 1f);
	}

	/**
	 * When the debug ui is disabled, unpause the game
	 */
	public void disable()
	{
		Time.timeScale = 1f;
		ui.enabled = false;
	}
}
