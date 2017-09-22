using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSoundPlayer : MonoBehaviour {

	public int pageNum;
	public bool hasSound;
	public bool soundLoop;
	public AudioClip sounds;
	public bool clickable;


	private bool soundPlayed;
	private bool charOn = false;
	private BackGroundTurner bgt;



	// Use this for initialization
	void Start () {

		bgt = FindObjectOfType (typeof(BackGroundTurner)) as BackGroundTurner;

		if (hasSound) {
			soundPlayed = false;

			GetComponent<AudioSource> ().clip = sounds;
		}


		
	}
	
	// Update is called once per frame
	void Update () {
		if (bgt.currentPage() == pageNum)
			setcharOn ();
		else
			setcharOff ();

		if (!clickable && charOn && !soundPlayed) {
			if (soundLoop) {
				playclip ();
				GetComponent<AudioSource> ().loop = true;
				soundPlayed = true;

			} else if (!soundLoop) {
				playclip ();
				soundPlayed = true;
			}
			
		} else {
			
		}
		
	}
	public void setcharOn(){
		charOn = true;
	}

	public void setcharOff(){
		charOn = false;
		GetComponent<AudioSource> ().Stop ();
	}
	private void playclip(){
		GetComponent<AudioSource>().Play();
		Debug.Log ("Sound Played");
	}

	void OnMouseDown(){
		if (charOn) {
			Debug.Log ("on mouse down");
			Debug.Log ("Current Page:" + bgt.currentPage ());
		}

	}

	void OnMouseUp(){
		if (clickable && charOn) {
			Debug.Log ("on mouse up");

			Debug.Log ("Current status: Audio Play");


			if (charOn) {
				playclip ();
				soundPlayed = true;
			}
		}
	}
}
