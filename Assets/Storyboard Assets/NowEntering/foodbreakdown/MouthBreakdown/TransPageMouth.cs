using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransPageMouth : MonoBehaviour {


	public Sprite[] foodMouth;
	public float frameTime;

	private Image i;
	private float timer;
	private int currentFrame;
	private bool animPlayed;


	// Use this for initialization
	void Start () {
		i = GetComponent<Image> ();
		timer = 0;
		currentFrame = 0;
		animPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer = timer + Time.deltaTime;

		if (currentFrame < foodMouth.Length) {
			i.sprite = foodMouth [currentFrame];
			if (timer >= frameTime) {
				timer = 0;
				currentFrame++;


			}
		} else {
			animPlayed = true;
		}


	}

	public bool getPlayed(){
		return animPlayed;
	}
}
