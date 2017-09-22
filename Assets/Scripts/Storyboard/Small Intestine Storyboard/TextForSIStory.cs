using UnityEngine;
using System.Collections;
using System.IO;

// all the text classes are really hacked together because they were done late at night at the last minute
// they have the text show up with the audio on the screen.
public class TextForSIStory : MonoBehaviour
{
    StoryboardHandler SIStoryboard;
    private string[] text;
    private float timer;
    private bool resetTimerPage4, resetTimerPage5, resetTimerPage8;

    // Use this for initialization
    void Start()
    {
        SIStoryboard = this.gameObject.GetComponent<StoryboardHandler>();

        TextAsset introText = Resources.Load("1.13.2016NewText/SIText") as TextAsset;
        text = introText.text.Split(";"[0]);
        Debug.Log(text);

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnGUI()
    {
        GUI.depth--;

        GUIStyle statsStyle = new GUIStyle(); //GUI.skin.box;
        statsStyle.font = (Font)Resources.Load("Fonts/JandaManateeSolid");
        statsStyle.normal.textColor = Color.black;
        statsStyle.fontSize = (int)(16f / 597f * Screen.height);
        statsStyle.wordWrap = true;
        statsStyle.alignment = TextAnchor.UpperLeft;


        
            if (SIStoryboard.getCurrPage() == 1)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[0], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 2)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[1], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 3)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[2], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 4)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[3], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 5)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[4], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 6)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[5], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 7)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[6], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 8)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[7], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 9)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[8], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 10)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[9], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 11)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[10], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 12)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[11], statsStyle);
                timer = 0;
            }
            if (SIStoryboard.getCurrPage() == 13)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[12], statsStyle);
                timer = 0;
            }
            /*
			if (timer < 2.7)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[0], statsStyle);
			} else if (timer < 6.0)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[1], statsStyle);
			} else
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[2], statsStyle);
			}
            
        }

		if (SIStoryboard.getCurrPage() == 2)
		{
			GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
			                 .15f*Screen.height), text[3], statsStyle);
			timer = 0;
		}

		if (SIStoryboard.getCurrPage() == 3)
		{
			GUI.Box(new Rect(.05f*Screen.width, (550f/768f)*Screen.height, .9f*Screen.width,
			                 .25f*Screen.height), text[4], statsStyle);
			timer = 0;
		}

		if (SIStoryboard.getCurrPage() == 4)
		{
			if (timer < 3.8)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[5], statsStyle);
			} else if (timer < 7.5)
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[6], statsStyle);
			} else
			{
				GUI.Box(new Rect(.05f*Screen.width, (625f/768f)*Screen.height, .9f*Screen.width,
				                 .15f*Screen.height), text[7], statsStyle);
			}
		}

		if (SIStoryboard.getCurrPage() == 5)
		{
			if (!resetTimerPage5)
			{
				timer = 0;
				resetTimerPage5 = true;
			}

			if (timer < 15)
			{
				GUI.Box(new Rect(.05f*Screen.width, (550f/768f)*Screen.height, .9f*Screen.width,
				                 .25f*Screen.height), text[8], statsStyle);
			} else if (timer < 27)
			{
				GUI.Box(new Rect(.05f*Screen.width, (550f/768f)*Screen.height, .9f*Screen.width,
				                 .25f*Screen.height), text[9], statsStyle);
			} else if (timer < 33)
			{
				GUI.Box(new Rect(.05f*Screen.width, (550f/768f)*Screen.height, .9f*Screen.width,
				                 .25f*Screen.height), text[10], statsStyle);
			} else
			{
				GUI.Box(new Rect(.05f*Screen.width, (550f/768f)*Screen.height, .9f*Screen.width,
				                 .25f*Screen.height), text[11], statsStyle);
			}
		}
	
		if (SIStoryboard.getCurrPage() == 6)
		{
			GUI.Box(new Rect(.05f*Screen.width, (450f/768f)*Screen.height, .9f*Screen.width,
			                 .2f*Screen.height), text[12], statsStyle);
		}

    */
        

    }
}