using UnityEngine;
using System.Collections;

/**
 * script for layering some mouth gui elements over top of the main background
 */
public class MouthOverlay : MonoBehaviour 
{
//	public Texture mouthOverlay, mouthOverlayClosed, sideBar, tongue, tongueClosed; //jaw, jawClosed;	//!< hold the textures for the mouth overlay and the side bar
	private ArrayList passedFoodStuff;
	private int stateCode;
	private float timeStamp;
	private GameObject mouthOverlay, mouthOverlayClosed, tongue, tongueClosed;

	void Start()
	{
		passedFoodStuff = new ArrayList();
		stateCode = 0;
		mouthOverlay = GameObject.Find ("MouthOverlay");
		mouthOverlayClosed = GameObject.Find ("MouthOverlayClosed");
		tongue = GameObject.Find ("Tongue");
		tongueClosed = GameObject.Find ("TongueClosed");
		mouthOverlay.SetActive (true);
		mouthOverlayClosed.SetActive (false);
		tongue.SetActive (true);
		tongueClosed.SetActive (false);
	}

	void Update()
	{
		if (stateCode == 0) 
		{
			GameObject[] foods = GameObject.FindGameObjectsWithTag ("MouthFood");
			foreach (GameObject food in foods) 
			{
				int i = food.GetInstanceID ();
				float x = food.transform.position.x;
				if (passedFoodStuff.IndexOf (i) == -1 && ((x > -2.5f) && (x < -2.4f))) 
				{
					passedFoodStuff.Add (i);
					stateCode = 1;
					timeStamp = Time.time;
				}
			}
		}

		if (stateCode == 0) 
		{
			mouthOverlay.SetActive (true);
			mouthOverlayClosed.SetActive (false);
			tongue.SetActive (true);
			tongueClosed.SetActive (false);
		} 
		else if (stateCode == 1) 
		{
			mouthOverlay.SetActive (false);
			mouthOverlayClosed.SetActive (true);
			tongue.SetActive (false);
			tongueClosed.SetActive (true);
			if (Time.time - timeStamp >= 0.3f) 
			{
				stateCode = 2;
				timeStamp = Time.time;
			}} 
		else if (stateCode == 2) 
		{
			mouthOverlay.SetActive (true);
			mouthOverlayClosed.SetActive (false);
			tongue.SetActive (true);
			tongueClosed.SetActive (false);
			if (Time.time - timeStamp >= 0.3f) 
			{
				stateCode = 3;
				timeStamp = Time.time;
			}
		} 
		else if (stateCode == 3) 
		{
			mouthOverlay.SetActive (false);
			mouthOverlayClosed.SetActive (true);
			tongue.SetActive (false);
			tongueClosed.SetActive (true);
			if (Time.time - timeStamp >= 0.3f) 
			{
				stateCode = 0;
				timeStamp = Time.time;
			}
		}
			
	}
	/**
	 * Draws the mouthOverlay and sidebar
	 */
//	void OnGUI()
//	{
//		
//		GUI.depth= GUI.depth + 2;
//		// draw the texture for the mouth overlay the size of the entire screen
//		if (stateCode == 0) {
//			//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), jaw);
//			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tongue);
//			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), mouthOverlay);
//		}
//
//		else if (stateCode == 1) 
//		{
//			//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), jawClosed);
//			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tongueClosed);
//			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mouthOverlayClosed);
//			if (Time.time - timeStamp >= 0.3f) 
//			{
//				stateCode = 2;
//				timeStamp = Time.time;
//			}
//		}
//
//		else if (stateCode == 2) 
//		{
//			//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), jaw);
//			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tongue);
//			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mouthOverlay);
//			if (Time.time - timeStamp >= 0.3f) 
//			{
//				stateCode = 3;
//				timeStamp = Time.time;
//			}
//		}
//
//		else if (stateCode == 3) 
//		{
//			//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), jawClosed);
//			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), tongueClosed);
//			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mouthOverlayClosed);
//			if (Time.time - timeStamp >= 0.3f) 
//			{
//				stateCode = 0;
//				timeStamp = Time.time;
//			}
//		}
//
//		// draw the side bar on the right side of the screen taking up the specified space
//		GUI.DrawTexture(new Rect(Screen.width * .87f, 0, Screen.width * .13f, Screen.height), sideBar);
//	}
}
