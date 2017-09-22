using UnityEngine;
using System.Collections;

/**
 * script to handlr drawing the face health indicators in the bottom right corner in the si game
 */
public class DrawHealthFace : MonoBehaviour 
{
	public Texture[] faces;				//!< texture array to hold all the different faces
	private int index;					//!< a variable to keep track of the current index in the array
	private Rect faceRect;				//!< a rectangle to define the coordinates of the area where we draw the face

	/**
	 * Use this for initialization
	 * Sets the location to draw the health face
	 */
	void Start () 
	{
		// set the coordinates of the rectangle based on the screen size
		faceRect = new Rect (Screen.width * .864f, Screen.height * 0.14f - Screen.height * 0.102864583f, 
		                     Screen.width * 0.078125f, Screen.height * 0.102864583f);
		// assign the dimensions to the pixel inset so that the image will actually be drawn in the desired region
		GetComponent<GUITexture>().pixelInset = faceRect;
	}
	
	/**
	 * Update is called once per frame
	 * Selects the proper face
	 */
	void Update () 
	{
		GetComponent<GUITexture>().texture = faces [index];		// update the current texture being displayed
	}

	/**
	 * function that can be called to change the index and therefore change the texture we are showing
	 */
	public void setFace(int index)
	{
		this.index = index;
	}
}
