using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Helper script to disable to main stomach game canvas for showing the debugger
 */
public class DisableMainGUI : MonoBehaviour 
{
	public Canvas c;						//!< reference to canvas
	public UnityEngine.UI.Button debug;		//!< refernece to debug button
	
	/**
	 * Called to disable the main canvas
	 */
	public void disable()
	{
		c.enabled = false;
		debug.enabled = false;
	}
	
	/**
	 * Called to reenable the main canvas
	 */
	public void enable()
	{
		c.enabled = true;
		debug.enabled = true;
	}
}