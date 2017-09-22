using UnityEngine;
using System.Collections;

/*
 * Behavior of the lost nutrients portion of tutorial
 */
public class LostNutrientsTutorial : MonoBehaviour 
{
	// for zyme
	public GameObject zyme;
	private ZymePopupScript zymeScript;
	public Texture zymePopupImage;

	// bool to help control actions in tutorial
	private bool showTutorial = false;

	// circle to bring attention to area
	public Texture circle;

	// Use this for initialization
	void Start () 
	{
		// get reference to the zyme script
		zymeScript = ((GameObject)Instantiate(zyme)).GetComponent<ZymePopupScript> ();
		// we immediately start showing the tutorial once instantiated so set to true
		showTutorial = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// check if the button was pressed to cancel the tutorial
		if (zymeScript.getButtonPressed())
		{
			showTutorial = false;
			zymeScript.setDraw(false);
			Time.timeScale = 1.0f;
		}
	}

	// draw the zyme pop up box and the circle 
	void OnGUI()
	{
		if (showTutorial) 
		{
			zymeScript.setDrawZymeLeft ();
			zymeScript.setImage (zymePopupImage);
			zymeScript.setDraw(true);
			zymeScript.setShowButton (true);
			Time.timeScale = .01f;

			GUI.DrawTexture(new Rect(0.84f * Screen.width, .82f * Screen.height, .16f*Screen.width, .17f * Screen.height), circle);

		}
	}
}
