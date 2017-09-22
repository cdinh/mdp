using UnityEngine;
using System.Collections;

public class TextforSIEnd : MonoBehaviour {

    // Use this for initialization
    StoryboardHandler SIEndStoryboard;
    private string[] text;
    private float timer;

    // Use this for initialization
    void Start()
    {
        SIEndStoryboard = this.gameObject.GetComponent<StoryboardHandler>();

        TextAsset SIEText = Resources.Load("1.13.2016NewText/SIEndText") as TextAsset;
        text = SIEText.text.Split(";"[0]);

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

        if (SIEndStoryboard.getCurrPage() == 1)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[0], statsStyle);
            timer = 0;
        }
        if (SIEndStoryboard.getCurrPage() == 2)
        {
            GUI.Box(new Rect(0.001f * Screen.width, (665f / 768f) * Screen.height, 1.0f * Screen.width,
                               .2f * Screen.height), text[1], statsStyle);
            timer = 0;
        }
    }
}
