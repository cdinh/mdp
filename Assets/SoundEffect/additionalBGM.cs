using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class additionalBGM : MonoBehaviour {

	public int pageNum;
	public bool soundLoop;
	public AudioClip sounds;
	public float startTime;
	public float endTime;

	private bool soundPlayed;
	private bool charOn = false;
	private BackGroundTurner bgt;
	private AudioSource audio;
	private float counter;

	// Use this for initialization
	void Start () {

		bgt = FindObjectOfType (typeof(BackGroundTurner)) as BackGroundTurner;
		soundPlayed = false;
		audio = GetComponent<AudioSource> ();
		audio.clip = sounds;
		counter = 0;

		
	}
	
	// Update is called once per frame
	void Update () {
		if (bgt.currentPage () == pageNum) {
			charOn = true;
			counter = counter + Time.deltaTime;
		} else {
			charOn = false;
			counter = 0;
		}

		if (charOn && !soundPlayed && counter >= startTime + 0.936f) {			//the offset of page turning sound
			if (soundLoop) {
				playclip ();
				audio.loop = true;
				soundPlayed = true;

			} else if (!soundLoop) {
				playclip ();
				soundPlayed = true;
			}

		} else if (endTime != 0.0 && counter > endTime + 0.936f){				//the offset of page turning sound
			audio.Stop ();
		}
		
	}

	private void playclip(){
		audio.Play();
		Debug.Log ("Sound Played");
	}
}
