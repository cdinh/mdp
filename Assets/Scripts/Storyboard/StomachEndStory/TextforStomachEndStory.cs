using UnityEngine;
using System.Collections;

public class TextforStomachEndStory : MonoBehaviour {

    StoryboardHandler StomachEndStoryboard;
    private string[] text;
    private float timer;
    private bool resetTimerPage4, resetTimerPage5, resetTimerPage8;

    // Use this for initialization
    void Start()
    {
        StomachEndStoryboard = this.gameObject.GetComponent<StoryboardHandler>();

        TextAsset introText = Resources.Load("1.13.2016NewText/StomachEndStory") as TextAsset;
        text = introText.text.Split(";"[0]);

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

        if (StomachEndStoryboard.getCurrPage() == 1)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[0], statsStyle);
            timer = 0;
        }

        if (StomachEndStoryboard.getCurrPage() == 2)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[1], statsStyle);
            timer = 0;
        }
        if (StomachEndStoryboard.getCurrPage() == 3)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[2], statsStyle);
            timer = 0;
        }
        if (StomachEndStoryboard.getCurrPage() == 4)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[3], statsStyle);
            timer = 0;
        }
    }

}