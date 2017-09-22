using UnityEngine;
using System.Collections;

public class FingerPopup : MonoBehaviour
{

	private GameObject flaps;			//!< to hold a reference to the flaps in the game
	private openFlap flap;				//!< to hold a reference to the openFlap script attached to the flaps

	public GameObject swipingDown;
	public GameObject swipingUp;

	// pop up variables
	private int swipePopupStatus;				//!< flag to hold whether the pop up windows should show up
	private float startTime;					//!< to keep track of the time the game start
	private float elapsed;						//!< the elapsed after the game start
	private int swipeCount;						//!< to keep track of how many times the swipe happened
	private float timeStamp;
	private EsophagusGameOver ego;
	private bool notPaused;

	// Use this for initialization
	void Start ()
	{
		flaps = GameObject.Find ("Flaps");									// find the reference to the flaps
		flap = flaps.GetComponent<openFlap>();											// find the script on the flaps
		ego = FindObjectOfType(typeof(EsophagusGameOver)) as EsophagusGameOver;

		startTime = Time.time;
		timeStamp = Time.time;
		swipePopupStatus = 0;
		swipeCount = 0;
		swipingDown.SetActive (false);
		swipingUp.SetActive (false);
		notPaused = true;
	}

	public void setPaused(){
		notPaused = !notPaused;
	}
	
	// Update is called once per frame
	void Update ()
	{
		elapsed = Time.time - startTime;
		swipeCount = flap.getSwipeCounts();
		int foodCounts = flap.getFoodLength();
		if (notPaused) {
			if (elapsed >= 15 && swipeCount <= 0 && foodCounts >= 2) {
//			Debug.Log ("popupstatus is 1");
				swipePopupStatus = 1;
				swipingDown.SetActive (true);
				swipingUp.SetActive (false);
				Time.timeScale = 0;
			} else if (flap.isEpiglotisOpen () && swipeCount <= 1 && elapsed >= 5) {
				if (swipePopupStatus == 1) {
					startTime = Time.time;
					swipePopupStatus = 0;
//				Debug.Log ("popupstatus is 0");
					swipingDown.SetActive (false);
					swipingUp.SetActive (false);
					if (!ego.getGameOver ())
						Time.timeScale = 1;
				} else {
					swipePopupStatus = 2;
//				Debug.Log ("popupstatus is 2");
					swipingDown.SetActive (false);
					swipingUp.SetActive (true);
					Time.timeScale = 0;
				}
			} else {
				swipePopupStatus = 0;
//			Debug.Log ("popupstatus is 0");
				swipingDown.SetActive (false);
				swipingUp.SetActive (false);
				if (!ego.getGameOver ())
					Time.timeScale = 1;
			}
		}
	}
}

