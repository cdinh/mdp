﻿using UnityEngine;
using System.Collections;
using System.IO;

public class TextForSIStory : MonoBehaviour 
{
	SmallIntestineStoryboard SIStoryboard;
	private string[] text;
	private float timer;
	private bool resetTimerPage5;

	// Use this for initialization
	void Start () 
	{
		SIStoryboard = this.gameObject.GetComponent<SmallIntestineStoryboard> ();
		
		TextAsset introText = Resources.Load ("SIText") as TextAsset;
		StringReader reader = new StringReader (introText.text);
		text = introText.text.Split("\n"[0]);	
		
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
		
		GUIStyle statsStyle = GUI.skin.box;
		statsStyle.font = (Font)Resources.Load ("Fonts/JandaManateeSolid");
		statsStyle.normal.textColor = Color.yellow;
		statsStyle.fontSize = (int)(20f / 597f * Screen.height);
		statsStyle.wordWrap = true;
		statsStyle.alignment = TextAnchor.MiddleCenter;
		
		if (SIStoryboard.getCurrPage() == 1)
		{
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
			GUI.Box(new Rect(.05f*Screen.width, (.05f)*Screen.height, .9f*Screen.width,
			                 .15f*Screen.height), text[12], statsStyle);
		}
	}
}