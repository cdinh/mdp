using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransPageSI : MonoBehaviour {
	
	public Sprite[] foodSI;
	public float frameTime;
	public Sprite emptyImage;

	private Image i;
	private float timer;
	private int currentFrame;
	private bool animPlayed;
	private bool PlayAnim;
	private bool showAnim;
	private TransPageStomach tps;
	private TransPageBackground tpbg;

	private int currentLevel;


	// Use this for initialization
	void Start () {
		i = GetComponent<Image> ();
		tps = FindObjectOfType (typeof(TransPageStomach)) as TransPageStomach;
		tpbg = FindObjectOfType (typeof(TransPageBackground)) as TransPageBackground;

		timer = 0;
		currentFrame = 0;
		animPlayed = false;


		currentLevel = PlayerPrefs.GetInt ("CurrentStoryLevel");

		if (currentLevel < 3) {
			PlayAnim = false;
			if (currentLevel == 2) {
				showAnim = true;
				Debug.Log ("show SI");
			} else {
				showAnim = false;
				Debug.Log ("Don't show SI");
			}
		}
		else {
			showAnim = true;
			PlayAnim = true;
		}

	}

	// Update is called once per frame
	void Update () {

		if (tps.getPlayed() == true && PlayAnim == true) {
			timer = timer + Time.deltaTime;

			if (currentFrame < foodSI.Length) {
				i.sprite = foodSI [currentFrame];
				if (timer >= frameTime) {
					timer = 0;
					currentFrame++;

				}
			} else {
				animPlayed = true;
			}
		}

		if (showAnim == false)
			i.sprite = emptyImage;
		else if(PlayAnim == false)
			i.sprite = foodSI [0];

	}

	public bool getPlayed(){
		return animPlayed;
	}


}
