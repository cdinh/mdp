using UnityEngine;
using System.Collections;

/**
 * Handles small intestine tutorial management
 */
public class SmallIntestineTutorialManager : MonoBehaviour 
{
	private SmallIntestineLoadLevelCounter level;
	public int levelnum;


	// Use this for initialization
	void Start () 
	{
		GameObject counter = GameObject.Find ("ChooseBackground");
		level = counter.GetComponent<SmallIntestineLoadLevelCounter> ();
		levelnum = level.getTutorialNum ();


		if (levelnum == 0) {
			PlayerPrefs.SetInt ("SITowerPlaceTutorial", 1);
			PlayerPrefs.SetInt ("SIGlowTutorial", 0);
			PlayerPrefs.SetInt ("SIUpdateTutorial", 0);
			PlayerPrefs.SetInt ("SINutrientsTutorial", 1);
			PlayerPrefs.SetInt ("SISpeedTutorial", 1);
			PlayerPrefs.SetInt ("SIFatsTutorial", 0);
		}

		if (levelnum == 1) {
			PlayerPrefs.SetInt ("SITowerPlaceTutorial", 0);
			PlayerPrefs.SetInt ("SIGlowTutorial", 0);
			PlayerPrefs.SetInt ("SIUpdateTutorial", 0);
			PlayerPrefs.SetInt ("SINutrientsTutorial", 0);
			PlayerPrefs.SetInt ("SISpeedTutorial", 0);
			PlayerPrefs.SetInt ("SIFatsTutorial", 1);
		}

		if (levelnum == 2) {
			PlayerPrefs.SetInt ("SITowerPlaceTutorial", 1);
			PlayerPrefs.SetInt ("SIGlowTutorial", 0);
			PlayerPrefs.SetInt ("SIUpdateTutorial", 1);
			PlayerPrefs.SetInt ("SINutrientsTutorial", 0);
			PlayerPrefs.SetInt ("SISpeedTutorial", 0);
			PlayerPrefs.SetInt ("SIFatsTutorial", 0);
		}



		PlayerPrefs.Save();
	}
}
