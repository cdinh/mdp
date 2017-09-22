using UnityEngine;
using System.Collections;

public class TextforLIStoryboard : MonoBehaviour {

    StoryboardHandler LIStoryboard;
    private string[] text;
    private float timer;

    // Use this for initialization
    void Start()
    {
        LIStoryboard = this.gameObject.GetComponent<StoryboardHandler>();

        TextAsset LIText = Resources.Load("1.13.2016NewText/LIStoryText") as TextAsset;
        text = LIText.text.Split(";"[0]);

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

        if (LIStoryboard.getCurrPage() == 1)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[0], statsStyle);
            timer = 0;
        }
        if (LIStoryboard.getCurrPage() == 2)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[1], statsStyle);
            timer = 0;
        }
        if (LIStoryboard.getCurrPage() == 4)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[2], statsStyle);
            /*
            if (timer > 4.0f)
            {
                GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                                   .2f * Screen.height), text[3], statsStyle);
                timer = 0;
            }
            */
        }
        if (LIStoryboard.getCurrPage() == 5)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[3], statsStyle);
            timer = 0;
        }
        if (LIStoryboard.getCurrPage() == 6)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[4], statsStyle);
            timer = 0;
        }
        if (LIStoryboard.getCurrPage() == 7)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[5], statsStyle);
            timer = 0;
        }

    }
}
