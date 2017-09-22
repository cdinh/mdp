using UnityEngine;
using System.Collections;

/**
 * script that adds a page turn icon to the corner of the storybookonce the audio for a page is done playing
 */
public class PageTurn : MonoBehaviour 
{
	public Texture corner;			//!< texture to hold the page corner
	private bool canSkip = false;	//!< the variable to indicate that the user can skip to the next page before audio is up
									//!< for example, if the story has been listned to at least once before

	/**
	 * Use this for initialization
	 * Figures out if we can skip the story
	 */
	void Start () 
	{
		// figure out if we can skip by looking at which stories we have already played through at least once
		if (Application.loadedLevelName.Equals("IntroStoryboard"))
		{
			canSkip = ((PlayerPrefs.GetInt("PlayedIntroStory") == 1) ? true : false);
		} else if (Application.loadedLevelName.Equals("MouthStoryboard"))
		{
			canSkip = ((PlayerPrefs.GetInt("PlayedMouthStory") == 1) ? true : false);
		} else if (Application.loadedLevelName.Equals("MouthEndStoryboard"))
		{
			canSkip = ((PlayerPrefs.GetInt("PlayedMouthEndStory") == 1) ? true : false);
		} else if (Application.loadedLevelName.Equals("SmallIntestineStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt("PlayedSIStory") == 1) ? true : false;
		}
	}

	/**
	 * Handles actually drawing hte page corner
	 */
	void OnGUI()
	{
		GUI.depth--;		// change the gui depth so the page corner draws on top of the background texture

		// if the audio is done playing OR we can skip the page early, draw the page corner in the top right corner
		if (!GetComponent<AudioSource>().isPlaying || canSkip)
		{
			GUI.DrawTexture(new Rect(Screen.width * .84f, 0, Screen.width * .16f, Screen.width * .16f), corner);
		}
	}
}
