using UnityEngine;
using System.Collections;

/**
 * Tutorial for tower placement stuff
 */
public class TowerPlacementTutorial : MonoBehaviour 
{
	public Texture arrow;

	// for zyme
	public GameObject zyme;
	private ZymePopupScript zymeScript;

	public Texture zymePopupImage1;
	public Texture zymePopupImage2;

	public float maxArrowTime = 5.0f;
	private float actualArrowTime = 0f; // find how long the arrow has been up
	private bool stopForZyme = false;
	private bool startSecondTimer = false;

	// Use this for initialization
	void Start () 
	{
		zymeScript = ((GameObject)Instantiate(zyme)).GetComponent<ZymePopupScript> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (PlayerPrefs.GetInt ("SITowerPlaceTutorial") == 0) {
			return;
		}

		actualArrowTime += Time.deltaTime;

		if ((actualArrowTime > maxArrowTime && PlayerPrefs.GetInt("SIStats_towersPlaced") == 0) ||
		    (actualArrowTime > maxArrowTime && PlayerPrefs.GetInt("SIStats_towersPlaced") == 1))
		{
			stopForZyme = true;
		}

		if (PlayerPrefs.GetInt("SIStats_towersPlaced") == 1 || PlayerPrefs.GetInt("SIStats_towersPlaced") == 2)
		{
			Time.timeScale = 1;
		}
	}

	void OnGUI()
	{
		if (PlayerPrefs.GetInt ("SITowerPlaceTutorial") == 1) {
			if (PlayerPrefs.GetInt ("SIStats_towersPlaced") == 0) {
				GUI.DrawTexture (new Rect (.25f * Screen.width, .1f * Screen.height, .3f * Screen.width, .85f * Screen.height), arrow);
			}

			if (stopForZyme) {
				zymeScript.setDraw (true);
				zymeScript.setImage (zymePopupImage1);
				Time.timeScale = .01f;
			}

			if (stopForZyme && !startSecondTimer && PlayerPrefs.GetInt ("SIStats_towersPlaced") == 1) {
				stopForZyme = false;
				actualArrowTime = 0f;
				startSecondTimer = true;
				zymeScript.setDraw (false);
			}

			if (PlayerPrefs.GetInt ("SIStats_towersPlaced") == 1) {
				GUI.DrawTexture (new Rect (.3f * Screen.width, .62f * Screen.height, .25f * Screen.width, .30f * Screen.height), arrow);
			}

			if (stopForZyme && startSecondTimer && actualArrowTime > maxArrowTime) {
				zymeScript.setDraw (true);
				zymeScript.setImage (zymePopupImage2);
				Time.timeScale = .01f;
			}

			if (stopForZyme && PlayerPrefs.GetInt ("SIStats_towersPlaced") == 2) {
				stopForZyme = false;
				zymeScript.setDraw (false);
			}
		}
	}
}
