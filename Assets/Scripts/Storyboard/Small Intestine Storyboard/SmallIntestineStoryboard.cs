﻿using UnityEngine;
using System.Collections;

public class SmallIntestineStoryboard : MonoBehaviour 
{
	public Texture[] pages;			// story the storyboard pages
	public AudioClip[] sounds;		// store the storyboard narrations
	private int currPage = 1;		// store the current page
	private bool hasPlayed = false;	// remember whether the current sound has played

	// for detecting a swipe
	private float xStart = 0.0f;
	private float xEnd = 0.0f;
	private bool swipe = false;

	// check for playthrough
	GameObject skipStory;
	private bool canSkip = false;
	SkipStoryEnablerScript skipStoryScript;

	// Use this for initialization
	void Start () 
	{
		skipStory = GameObject.Find("SkipStoryEnabler(Clone)");
		skipStoryScript = skipStory.GetComponent<SkipStoryEnablerScript> ();
		if (skipStoryScript != null)
		{
			canSkip = skipStoryScript.getSkipStory();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!hasPlayed)		// only play the clip once per page
		{
			audio.clip = sounds [currPage - 1];
			playClip();
			hasPlayed = true;
		}

		if (!audio.isPlaying | canSkip)
		{
			foreach (Touch touch in Input.touches) 
			{
				if (touch.phase == TouchPhase.Began) 
				{
					xStart = touch.position.x;
				}
				if (touch.phase == TouchPhase.Moved) 
				{
					xEnd = touch.position.x;
					
					if ((xStart - xEnd) > 30) 
					{
						swipe = true;
					}
				}
			}
			if(Input.GetKeyDown(KeyCode.Space))
				swipe = true;

			// set variables for next page
			if (swipe == true)
			{
				currPage++;
				if ((currPage - 1) == pages.Length)
				{
					//TODO: MOVE THIS IN THE FUTURE
					skipStoryScript.setSkipStory(true);
					Application.LoadLevel("LoadLevelSmallIntestine");
				}
				swipe = false;
				xStart = 0;
				xEnd = 0;
				hasPlayed = false;
			}
		}
	}

	/*
	 * play the audio clip
	 * */
	private void playClip()
	{
		audio.Play();
	}

	void OnGUI()
	{
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), pages[Mathf.Clamp(currPage - 1, 0, pages.Length - 1)]);

		// create an invisible button by the page turn
		if(!audio.isPlaying)
		{
			GUI.color = new Color() { a = 0.0f };
			if (GUI.Button(new Rect(Screen.width * .84f, 0, Screen.width * .16f, Screen.width * .16f),""))
			{
				swipe = true;
			}
			GUI.color = new Color() { a = 1.0f };
		}
	}

	public int getCurrPage()
	{
		return currPage;
	}
}
