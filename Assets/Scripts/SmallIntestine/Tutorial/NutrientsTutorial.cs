using UnityEngine;
using System.Collections;

/**
 * Tutorial stuff for nutrients
 */
public class NutrientsTutorial : MonoBehaviour 
{
	// for zyme
	public GameObject zyme;
	private ZymePopupScript zymeScript;

	public Texture zymePopupImage;

	public float tutorialDelay;
	private float elapsedTime;

	private bool showTutorial = false;
	private bool tutorialOver = false;

	public Texture circle;
	private bool circleDone;
	private bool showCircle;
	public Texture finger;

	private Rect handRectangle;
	private bool chooseHandLocation;

	// Use this for initialization
	void Start () 
	{
		zymeScript = ((GameObject)Instantiate(zyme)).GetComponent<ZymePopupScript> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (PlayerPrefs.GetInt ("SINutrientsTutorial") == 0) {
			return;
		}

		if (!tutorialOver && PlayerPrefs.GetInt("SIStats_towersPlaced") > 1)
		{ 
			elapsedTime += Time.deltaTime;

			if (elapsedTime > tutorialDelay)
			{
				if (!chooseHandLocation)
				{
					getHandLocation();
				}
				showTutorial = true;
			}
		}

		if ((PlayerPrefs.GetInt("SIGlowTutorial") == 1) && showTutorial)
		{
			showTutorial = false;
			zymeScript.setDraw(false);
			tutorialOver = true;
			Time.timeScale = 1;
		}

		if (tutorialOver)
		{
			PlayerPrefs.SetInt("SISpeedTutorial", 1);
			PlayerPrefs.Save();
		}
	}

	private void getHandLocation()
	{
		EffectParticle[] nutrients = FindObjectsOfType (typeof(EffectParticle)) as EffectParticle[];
		EffectParticle target = null;

		for (int i = 0; i < nutrients.Length; i++) 
		{
			if (nutrients[i].transform.position.z > 1.7)
			{
				target = nutrients[i];
				chooseHandLocation = true;
				break;
			}
		}

		handRectangle = new Rect ((target.transform.position.x + 26)/52f * Screen.width - .1f*Screen.width, 
		                          Screen.height - ((target.transform.position.z + 19)/38f * Screen.height) - .12f * Screen.height, 
		                         .25f * Screen.width, .415f * Screen.height);
	}

	void OnGUI()
	{
		if (showTutorial)
		{
			zymeScript.setDraw(true);
			zymeScript.setImage(zymePopupImage);
			Time.timeScale = .01f;

			// circle nutrients text
			GUI.DrawTexture(new Rect(.32f*Screen.width, .8f*Screen.height, .2f*Screen.width, .1f*Screen.height), circle);
			GUI.DrawTexture(handRectangle, finger);
		}
	}
}