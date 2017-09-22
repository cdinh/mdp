using UnityEngine;
using System.Collections;

/**
 * just a random class mostly to use for debugging right now
 * This could possibly be adapted in the future if there is a desire to 
 * show high score screens for multiple levels :)
 */
public class DisplayHighScores : MonoBehaviour 
{
	/**
	 * Pulls up high scores from player prefs and draws them
	 */
	void OnGUI()
	{
		GUIStyle scoreStyle = new GUIStyle ();
		scoreStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");
		scoreStyle.normal.textColor = Color.yellow;

		// draw title
		scoreStyle.fontSize = (int)(34f / 597f * Screen.height);
		scoreStyle.alignment = TextAnchor.MiddleCenter;
		GUI.Label(new Rect(0, 0, Screen.width,
		                   (100f/768f)*Screen.height), 
		          "High Scores", scoreStyle);

		// draw scores
		scoreStyle.fontSize = (int)(32f / 597f * Screen.height);
		scoreStyle.alignment = TextAnchor.UpperLeft;

		// just pulls up the saved high scores for all the levels and displays them
		GUI.Label(new Rect(0, (100f/768f)*Screen.height, Screen.width,
		                   (700f/768f)*Screen.height), 
		          "Mouth Level 1: " + PlayerPrefs.GetInt("Mouth1") + "\n" +
		          "Mouth Level 2: " + PlayerPrefs.GetInt("Mouth2") + "\n" +
		          "\n" +
		          "SI Level 1: " + PlayerPrefs.GetInt("SI1") + "\n" +
		          "SI Level 2: " + PlayerPrefs.GetInt("SI2") + "\n" +
		          "SI Level 3: " + PlayerPrefs.GetInt("SI3") + "\n" +
		          "SI Level 4: " + PlayerPrefs.GetInt("SI4") + "\n" +
		          "SI Level 5: " + PlayerPrefs.GetInt("SI5") + "\n" +
		          "SI Level 6: " + PlayerPrefs.GetInt("SI6") + "\n"
		          , scoreStyle);

	}
}
