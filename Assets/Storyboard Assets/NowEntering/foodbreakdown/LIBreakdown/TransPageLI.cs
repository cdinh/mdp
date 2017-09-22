using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransPageLI : MonoBehaviour {

	public Sprite[] foodLI;
	public float frameTime;
	public Sprite emptyImage;

	private Image i;
	private float timer;
	private int currentFrame;
	private bool animPlayed;
	private bool PlayAnim;
	private bool showAnim;
	private TransPageSI tpsi;
	private TransPageBackground tpbg;

	private int currentLevel;


	// Use this for initialization
	void Start () {
		i = GetComponent<Image> ();
		tpsi = FindObjectOfType (typeof(TransPageSI)) as TransPageSI;
		tpbg = FindObjectOfType (typeof(TransPageBackground)) as TransPageBackground;

		timer = 0;
		currentFrame = 0;
		animPlayed = false;


		currentLevel = PlayerPrefs.GetInt ("CurrentStoryLevel");

		if (currentLevel < 4) {
			PlayAnim = false;
			if (currentLevel == 3) {
				showAnim = true;
				Debug.Log ("show LI");
			} else {
				showAnim = false;
				Debug.Log ("Don't show LI");
			}
		}
		else {
			showAnim = true;
			PlayAnim = true;
		}

	}

	// Update is called once per frame
	void Update () {

		if (tpsi.getPlayed() == true && PlayAnim == true) {
			timer = timer + Time.deltaTime;

			if (currentFrame < foodLI.Length) {
				i.sprite = foodLI [currentFrame];
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
			i.sprite = foodLI [0];

	}

	public bool getPlayed(){
		return animPlayed;
	}


}
