﻿using UnityEngine;
using System.Collections;

/**
 * script to handle drawing the uvula and epiglottis textures
 */
public class UvulaEpiglottisLayer : MonoBehaviour 
{
	public Texture[] epiglottis, uvula;			//!< to hold the textures for the epiglottis and uvula
	private openFlap epiglottisObject;			//!< to hold the reference to the openFlap script on the flaps

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// find the reference to the flaps and get the script reference from it
		epiglottisObject = GameObject.Find("Flaps").GetComponent<openFlap>();
	}

	/**
	 * Handles drawing of the proper uvula and epiglottis images
	 */
	void OnGUI()
	{
		GUI.depth=GUI.depth+5;		// change the gui depth to draw at for proper layering of gui elements
	
		// draw the proper epiglottis texture, the size of the screen, based on whether the flaps are open or closed
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),
		                epiglottis[epiglottisObject.isEpiglotisOpen() ? 1 : 0]);

		// draw the proper uvula texture in the size of the screen, based on whether the flaps are open or closed
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),
		                uvula[epiglottisObject.isEpiglotisOpen() ? 1 : 0]);
	}
}
