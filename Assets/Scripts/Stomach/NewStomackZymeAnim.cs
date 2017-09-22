using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NewStomackZymeAnim : MonoBehaviour {


	private Image i;

	public Sprite[] zymeFlail;					//!< holds the texture that will be drawn for happy zyme
	public Sprite[] zymeRelief;					//!< holds the texture that will be drawn for concerned zyme
	public Sprite[] zymeSurprise;	


	private StomachEnzyme stEz;
	private float timer;
	private float subtimer;
	public float frameTime;

	// popup variables for basic button
	public Texture tapPopUp;
	private bool popup;
	private bool popped;

	// Use this for initialization
	void Start () {
		stEz = FindObjectOfType (typeof(StomachEnzyme)) as StomachEnzyme;
		i = GetComponent <Image>();

		timer = 0;
		subtimer = 0;
		popup = false;
		popped = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (stEz.isAttacking ()) {
			timer = timer + Time.deltaTime;
			if (timer < frameTime * 4) {
				popup = false;
				subtimer = subtimer + Time.deltaTime;
				if (subtimer > frameTime * 3) {
					i.sprite = zymeSurprise [3];
				} else if (subtimer > frameTime * 2) {
					i.sprite = zymeSurprise [2];
				} else if (subtimer > frameTime * 1) {
					i.sprite = zymeSurprise [1];
				} else if (subtimer > frameTime * 0) {
					i.sprite = zymeSurprise [0];
				}

				
			} else {
				subtimer = subtimer + Time.deltaTime;
				if (subtimer > frameTime * 1) {
					i.sprite = zymeFlail [1];
					if (subtimer > frameTime * 2) {
						subtimer = 0;
						popup = true;
					}
				} else if (subtimer < frameTime * 1) {
					i.sprite = zymeFlail [0];
				}

			}
		} 
		else {
			popup = false;
			if(timer>0){

				subtimer = subtimer + Time.deltaTime;
				float tempOffSet = frameTime * 8;
				if (subtimer > frameTime * 6 + tempOffSet) {
					i.sprite = zymeSurprise [0];
					timer = 0;
				} else if (subtimer > frameTime * 5 + tempOffSet)
					i.sprite = zymeRelief [3];
				else if (subtimer > frameTime * 4 + tempOffSet)
					i.sprite = zymeRelief [2];
				else if (subtimer > frameTime * 3 + tempOffSet)
					i.sprite = zymeRelief [1];
				else if (subtimer > frameTime * 0 + tempOffSet) {
					i.sprite = zymeRelief [0];	
					popped = true;
				}
			}
		}


	}

	void OnGUI()
	{
		if(popup && !popped)
		{
			GUI.DrawTexture (new Rect(Screen.width * 0.79f, 
				Screen.height * 0.30515625f, 
				Screen.width * 0.2093359375f, 
				Screen.height * 0.300697917f),tapPopUp);
		}
	}

}
