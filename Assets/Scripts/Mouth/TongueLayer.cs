using UnityEngine;
using System.Collections;

/**
 * script that handles drawing the tongue layer on the gui
 */
public class TongueLayer : MonoBehaviour 
{
	public Texture tongue, tongueClosed, jaw, jawClosed;			//!< to hold the tongue texture

	/**
	 * Handles drawing of the tongue inthe mouth game
	 */
	void OnGUI()
	{
		GUI.depth= GUI.depth+5;				// change the GUI depth for proper layering of gui elements

		// draw the texture the same size as the screen
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), tongue);
	}
}
