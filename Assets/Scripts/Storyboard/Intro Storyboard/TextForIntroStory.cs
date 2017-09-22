using UnityEngine;
using System.Collections;
using System.IO;

// all the text classes are really hacked together because they were done late at night at the last minute
// they have the text show up with the audio on the screen.
public class TextForIntroStory : MonoBehaviour 
{
	StoryboardHandler introStoryboard;
	private string[] text;
	private float timer;
	private bool resetTimerPage4, resetTimerPage5, resetTimerPage8;

	// Use this for initialization
	void Start () 
	{
		introStoryboard = this.gameObject.GetComponent<StoryboardHandler> ();

		TextAsset introText = Resources.Load ("1.13.2016NewText/IntroStoryText") as TextAsset;
		text = introText.text.Split(";"[0]);	

		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
	}

	void OnGUI()
	{
		GUI.depth--;

		GUIStyle statsStyle = new GUIStyle(); //GUI.skin.box;
		statsStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");
		statsStyle.normal.textColor = Color.black;
		statsStyle.fontSize = (int)(16f / 597f * Screen.height);
		statsStyle.wordWrap = true;
		statsStyle.alignment = TextAnchor.UpperLeft;

		if (introStoryboard.getCurrPage() == 1)
		{
			GUI.Box(new Rect(0.001f*Screen.width, (665f/768f)*Screen.height, 1.0f*Screen.width,
			                   .2f*Screen.height), text[0], statsStyle);
			timer = 0;
		} 

		if (introStoryboard.getCurrPage() == 2)
		{
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[1], statsStyle);
            timer = 0;
        }

		if (introStoryboard.getCurrPage() == 3)
		{
            /*
			if (timer < 2.8)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[2], statsStyle);
			} else
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[3], statsStyle);
			}
            */
            GUI.Box(new Rect(0.001f * Screen.width, (660f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[2], statsStyle);
            timer = 0;
        }

        /*
		if (introStoryboard.getCurrPage() == 4)
		{
			if (!resetTimerPage4)
			{
				timer = 0;
				resetTimerPage4 = true;
			}

			if (timer < 2)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[4], statsStyle);
			} else if (timer > 2 && timer < 5.3)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[5], statsStyle);
			} else if (timer > 5.3 && timer < 7.8)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[6], statsStyle);
			} else if (timer > 7.8 && timer < 10.9)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[7], statsStyle);
			} else if (timer > 10.9 && timer < 13.3)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[8], statsStyle);
			} else if (timer > 13.3 && timer < 15.5)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[9], statsStyle);
			} else if (timer > 15.5)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[10], statsStyle);
			}
		}

		if (introStoryboard.getCurrPage() == 5)
		{
			if (!resetTimerPage5)
			{
				timer = 0;
				resetTimerPage5 = true;
			}

			if (timer < 3.5)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[11], statsStyle);
			} else
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[12], statsStyle);
			}
		}

		if (introStoryboard.getCurrPage() == 6)
		{
			GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
			                 .15f*Screen.height), text[13], statsStyle);
			timer = 0;
		}

		if (introStoryboard.getCurrPage() == 7)
		{
			if (timer < 1.8)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[14], statsStyle);
			} else
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[15], statsStyle);
			}
		}

		if (introStoryboard.getCurrPage() == 8)
		{
			if (!resetTimerPage8)
			{
				timer = 0;
				resetTimerPage8 = true;
			}

			if (timer < 6.9)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[16], statsStyle);
			} else
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[17], statsStyle);
			}
		}

		if (introStoryboard.getCurrPage() == 9)
		{
			GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
			                 .15f*Screen.height), text[18], statsStyle);
		}
        */

	}
}
