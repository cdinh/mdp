﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]

public class AudioRecorder : MonoBehaviour 
{
	// file name of save file
	public string fileName;

	//A boolean that flags whether there's a connected microphone
	private bool micConnected = false;
	
	//The maximum and minimum available recording frequencies
	private int minFreq;
	private int maxFreq;
	
	//A handle to the attached AudioSource
	private AudioSource goAudioSource;
	
	//Use this for initialization
	void Start() 
	{
		for (int i = 0; i < Microphone.devices.Length; i++)
		{
			Debug.Log (Microphone.devices[i]);
		}
		//Check if there is at least one microphone connected
		if(Microphone.devices.Length <= 0)
		{
			//Throw a warning message at the console if there isn't
			Debug.LogWarning("Microphone not connected!");
		}
		else //At least one microphone is present
		{
			//Set 'micConnected' to true
			micConnected = true;
			
			//Get the default microphone recording capabilities
			Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);
			
			//According to the documentation, if minFreq and maxFreq are zero, the microphone supports any frequency...
			if(minFreq == 0 && maxFreq == 0)
			{
				//...meaning 44100 Hz can be used as the recording sampling rate
				maxFreq = 44100;
			}
			
			//Get the attached AudioSource component
			goAudioSource = this.GetComponent<AudioSource>();
		}
	}
	
	void OnGUI() 
	{
		//If there is a microphone
		if(micConnected)
		{
			//If the audio from any microphone isn't being captured
			if(!Microphone.IsRecording(null))
			{
				//Case the 'Record' button gets pressed
				if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-25, 200, 50), "Record"))
				{
					//Start recording and store the audio captured from the microphone at the AudioClip in the AudioSource
					goAudioSource.clip = Microphone.Start(null, true, 20, maxFreq);
				}
			}
			else //Recording is in progress
			{
				//Case the 'Stop and Play' button gets pressed
				if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-25, 200, 50), "Stop"))
				{
					Microphone.End(null); //Stop the audio recording
					AudioClip trimmedClip = SaveWav.TrimSilence(goAudioSource.clip, 0.1f);
					SaveWav.Save(fileName, trimmedClip);
				}
				
				GUI.Label(new Rect(Screen.width/2-100, Screen.height/2+25, 200, 50), "Recording in progress...");
			}
		}
		else // No microphone
		{
			//Print a red "Microphone not connected!" message at the center of the screen
			GUI.contentColor = Color.red;
			GUI.Label(new Rect(Screen.width/2-100, Screen.height/2-25, 200, 50), "Microphone not connected!");
		}
		
	}
}