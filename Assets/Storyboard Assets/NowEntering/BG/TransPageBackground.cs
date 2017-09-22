using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransPageBackground : MonoBehaviour {

	public Sprite[] BGs;
	public float frameTime;
	public int totalTimes;

	private Image i;

	private bool secLastPlayed;
	private TransPageMouth tpm;
	private TransPageSI tpsi;
	private TransPageStomach tps;
	private TransPageLI tpli;
	//private TransPageMouth tpm;
	private TransQuesMark tqm;


	private int currentFrame;
	private float timer;
	private int counter;
	private int currentLevel;






	// Use this for initialization
	void Start () {
		i = GetComponent<Image> ();

		tpm = FindObjectOfType (typeof(TransPageMouth)) as TransPageMouth;
		tpsi = FindObjectOfType (typeof(TransPageSI)) as TransPageSI;
		tps = FindObjectOfType (typeof(TransPageStomach)) as TransPageStomach;
		tpli = FindObjectOfType (typeof(TransPageLI)) as TransPageLI;
		tqm = FindObjectOfType (typeof(TransQuesMark)) as TransQuesMark;

		timer = 0;
		counter = 0;

		//currentLevel = 3;
		currentLevel = PlayerPrefs.GetInt ("CurrentStoryLevel");
		tqm.setStart (currentLevel);

		/*
		if (PlayerPrefs.GetInt ("CurrentStoryLevel") == 3)
			currentLevel = 3;
		else if (PlayerPrefs.GetInt ("CurrentStoryLevel") == 2)
			currentLevel = 2;
		else if (PlayerPrefs.GetInt ("CurrentStoryLevel") == 1)
			currentLevel = 1;
		else
			currentLevel = 1;
		*/


		Debug.Log (currentLevel);



		currentFrame = (currentLevel-1)*2;
		i.sprite = BGs [currentFrame];



		Debug.Log ("currentlevel: " + currentLevel);

	
	}
	
	// Update is called once per frame
	void Update () {
		secLastPlayed = tps.getPlayed ();

		if(currentLevel == 1)
			secLastPlayed = tpm.getPlayed ();
		else if(currentLevel == 2)
			secLastPlayed = tps.getPlayed ();
		else if(currentLevel == 3)
			secLastPlayed = tpsi.getPlayed ();


		if (secLastPlayed) {
			timer = timer + Time.deltaTime;


			if (currentFrame < (currentLevel-1)*2+2) {
				i.sprite = BGs [currentFrame];

				if (timer >= frameTime) {
					timer = 0;
					currentFrame++;

					counter++;
					tqm.setFrame (1);

				}
			} else {
				currentFrame = (currentLevel-1)*2;
				counter++;
				tqm.setFrame (0);
			}

		}


		if (counter >= totalTimes*2) {

			if(currentLevel == 1)
				Application.LoadLevel("StomachStoryboard");
			else if(currentLevel == 2)
				//Application.LoadLevel("SmallIntestineStoryboard");
				Application.LoadLevel("Test_SIStoryBoard");
			else if(currentLevel == 3)
				Application.LoadLevel("LargeIntestineStoryboard");
		}
			

	
	}

	public int getLevel(){
		Debug.Log ("currentlevel: " + currentLevel);
		return currentLevel;

	}
}
