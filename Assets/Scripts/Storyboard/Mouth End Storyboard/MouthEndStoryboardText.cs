using UnityEngine;
using System.Collections;
using System.IO;


// all the text classes are really hacked together because they were done late at night at the last minute
// they have the text show up with the audio on the screen.
public class MouthEndStoryboardText : MonoBehaviour 
{
	StoryboardHandler mouthStoryboard;
	private string[] text;
	private float timer;
	
	// Use this for initialization
	void Start () 
	{
		mouthStoryboard = this.gameObject.GetComponent<StoryboardHandler> ();
		
		TextAsset mouthText = Resources.Load ("1.13.2016NewText/MouthEndStory") as TextAsset;
		text = mouthText.text.Split(";"[0]);	
		
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
        statsStyle.font = (Font)Resources.Load("Fonts/JandaManateeSolid");
        statsStyle.normal.textColor = Color.black;
        statsStyle.fontSize = (int)(16f / 597f * Screen.height);
        statsStyle.wordWrap = true;
        statsStyle.alignment = TextAnchor.UpperLeft;

        if (mouthStoryboard.getCurrPage() == 1)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[0], statsStyle);
            timer = 0;
        }
     
    }
}
