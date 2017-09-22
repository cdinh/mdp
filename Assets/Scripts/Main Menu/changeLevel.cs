using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class changeLevel : MonoBehaviour {
	public Text pageNumberButtonText;


	// Update is called once per frame
	void Update()
	{
		if (Application.loadedLevelName == "DebugMenu")
		{
			if (PlayerPrefs.GetInt("ShowPageNumbers") == 0)
			{
				pageNumberButtonText.text = "Show Page #";
			} else
			{
				pageNumberButtonText.text = "Don't Show Page #";
			}
		}
	}


	public void changeScene (string Level) {
		Application.LoadLevel (Level);
	}

 	public void changeSILevel (int l){
		PlayerPrefs.SetInt("DesiredSILevel", l);
		PlayerPrefs.Save();
		Application.LoadLevel("LoadLevelSmallIntestine");
	}

	public void changeMouthLevel(int l){
		PlayerPrefs.SetInt("DesiredMouthLevel", l);
		PlayerPrefs.Save();
		Application.LoadLevel("LoadLevelMouth");
	}

	public void changeStomachLevel(int l){
		PlayerPrefs.SetInt("DesiredStomachLevel", l);
		PlayerPrefs.Save();
		Application.LoadLevel("LoadLevelStomach");
	}

	public void changeLILevel (int l) 
	{
		PlayerPrefs.SetInt ("DesiredLILevel", l);
		PlayerPrefs.Save ();
		Application.LoadLevel ("LoadLevelLargeIntestine");
	}
	
	public void skipStory(int n){
		PlayerPrefs.SetInt ("PlayedIntroStory", n);
		PlayerPrefs.SetInt ("PlayedMouthStory", n);
		PlayerPrefs.SetInt ("PlayedMouthEndStory", n);
		PlayerPrefs.SetInt ("PlayedStomachStory", n);
		PlayerPrefs.SetInt ("PlayedStomachEndStory", n);
		PlayerPrefs.SetInt ("PlayedSIStory", n);
		PlayerPrefs.SetInt ("PlayedLIStory", n);
		PlayerPrefs.Save ();
	}

	public void showPageNumbers()
	{
		if (PlayerPrefs.GetInt("ShowPageNumbers") == 0)
		{
			PlayerPrefs.SetInt ("ShowPageNumbers", 1);
		} else
		{
			PlayerPrefs.SetInt ("ShowPageNumbers", 0);
		}
		PlayerPrefs.Save ();
	}

	public void Reset(){
		PlayerPrefs.DeleteAll ();
	}

	public void ResetScores(){
		PlayerPrefs.DeleteKey("Mouth1");
		PlayerPrefs.DeleteKey("Mouth2");
		PlayerPrefs.DeleteKey("Stomach1");
		PlayerPrefs.DeleteKey("Stomach2");
		PlayerPrefs.DeleteKey("SI1");
		PlayerPrefs.DeleteKey("SI2");
		PlayerPrefs.DeleteKey("SI3");
		PlayerPrefs.DeleteKey("SI4");
		PlayerPrefs.DeleteKey("SI5");
		PlayerPrefs.DeleteKey("SI6");
	}

}
