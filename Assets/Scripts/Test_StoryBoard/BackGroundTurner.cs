using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BackGroundTurner : MonoBehaviour {

	/***************************************************************/
	public AudioClip[] sounds;			//!< store the storyboard narrations
	public AudioClip pageTurnSound;
	private int currPage = 1;			//!< store the current page
	private bool hasPlayed = false;		//!< remember whether the current sound has played
	private bool buttonClicked = false;	//!< holds if the user clicked in the page corner instead of swiping

	DetectStraightSwipe swipeDetection;	//!< to hold a reference to the script that controls swipe detection

	private bool canSkip = false;		//!< check for playthrough

	private AsyncOperation loader;		//!< for preloading the next level

	/***************************************************************/








	public Sprite[] BackGroundImages;
	private Image image;

	/*
	public int[] CharacterPage;
	public float[] XCharacterPosition;
	public float[] YCharacterPosition;
	public Sprite[] CharacterImage;
*/



	//public List<Pages_Sprites> PageList = new List<Pages_Sprites>();
	private HashSet<int> charSet = new HashSet<int>();



	//private StoryCharacter storyChar;
	private bool charAdded;
	private int charTracker;

	//private Long Movable Pages;
	private bool[] longPages;
	private bool longPageDone;
	private bool pageTurned;




	public int[] animatedZyme;


	// Use this for initialization
	void Start () {

		image = GetComponent<Image>();
		//storyChar = FindObjectOfType (typeof(StoryCharacter)) as StoryCharacter;

		charAdded = false;
		charTracker = 0;
		pageTurned = false;



		/*
		for (int i = 0; i < CharacterPage.Length; i++) {
			charSet.Add (CharacterPage [i]);
			Debug.Log ("Page " + CharacterPage[i] + " added");
		}
		*/


		int tempCharCounter = 0;

		longPages = new bool[BackGroundImages.Length];
		longPageDone = true;
		/*
		for (int i = 0; i < longPages.Length; i++) {
			longPages [i] = true;
		};
		*/

		/*
		for (int i = 0; i < BackGroundImages.Length; i++) {
			PageList.Add (new Pages_Sprites (BackGroundImages [i], i));

			if (charSet.Contains (i)) {
				Sprite[] tempCharSprite = new Sprite[2];

				tempCharSprite [0] = CharacterImage [tempCharCounter*2];
				tempCharSprite [1] = CharacterImage [tempCharCounter*2 + 1];

				PageList [i].setCharacter (tempCharSprite, XCharacterPosition [tempCharCounter], YCharacterPosition [tempCharCounter]);
				tempCharCounter++;
			}

		}
		*/



		/**************************************************************/

		swipeDetection = gameObject.GetComponent<DetectStraightSwipe> ();

		// find out if we can skip without listening
		if (Application.loadedLevelName.Equals("IntroStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt ("PlayedIntroStory") == 1) ? true : false;
		} else if (Application.loadedLevelName.Equals("MouthStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt ("PlayedMouthStory") == 1) ? true : false;
		} else if (Application.loadedLevelName.Equals("MouthEndStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt ("PlayedMouthEndStory") == 1) ? true : false;
		} else if (Application.loadedLevelName.Equals("StomachStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt ("PlayedStomachStory") == 1) ? true : false;
		} else if (Application.loadedLevelName.Equals("StomachEndStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt ("PlayedStomachEndStory") == 1) ? true : false;
		} else if (Application.loadedLevelName.Equals("SmallIntestineStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt ("PlayedSIStory") == 1) ? true : false;
		} else if (Application.loadedLevelName.Equals("SmallIntestineEndStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt("PlayedSIEndStory") == 1) ? true : false;
		} else if (Application.loadedLevelName.Equals("LargeIntestineStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt("PlayedLIStory") == 1) ? true : false;
		} else if (Application.loadedLevelName.Equals("LargeIntestineEndStoryboard"))
		{
			canSkip = (PlayerPrefs.GetInt("PlayedLIEndStory") == 1) ? true : false;
		}



		// preload next scene
		StartCoroutine(loadNextLevel());

		/**************************************************************/


	}



	/**
	 * Starts preloading the next level to avoid delays
	 */
	IEnumerator loadNextLevel() 
	{
		// starts preloading hte next level in the sequence accordingly
		if (Application.loadedLevelName.Equals ("Test_IntroStoryboard")) {
			
			/*
			 * 
			loader = Application.LoadLevelAsync("Test_nowEntering");
		}
		else if (Application.loadedLevelName.Equals("IntroStoryboard"))
		{
			*/
			//loader = Application.LoadLevelAsync("MouthStoryboard");	
			loader = Application.LoadLevelAsync("Test_MouthStoryBoard");
		} else if (Application.loadedLevelName.Equals("MouthStoryboard"))
		{
			loader = Application.LoadLevelAsync("LoadLevelMouth");
		} else if (Application.loadedLevelName.Equals("Test_MouthStoryBoard"))
		{
			loader = Application.LoadLevelAsync("LoadLevelMouth");
		} else if (Application.loadedLevelName.Equals("MouthEndStoryboard"))
		{
			//loader = Application.LoadLevelAsync("StomachStoryboard");
			loader = Application.LoadLevelAsync("Test_nowEntering");
		} else if (Application.loadedLevelName.Equals("StomachStoryboard"))
		{
			loader = Application.LoadLevelAsync("LoadLevelStomach");
		} else if (Application.loadedLevelName.Equals("StomachEndStoryboard"))
		{
			//loader = Application.LoadLevelAsync("SmallIntestineStoryboard");
			loader = Application.LoadLevelAsync("Test_nowEntering");
		} /*
			else if (Application.loadedLevelName.Equals("SmallIntestineStoryboard"))
		{
			*/
		else if (Application.loadedLevelName.Equals("Test_SIStoryBoard")){
			loader = Application.LoadLevelAsync("LoadLevelSmallIntestine");
		} else if (Application.loadedLevelName.Equals("SmallIntestineEndStoryboard"))
		{
			//loader = Application.LoadLevelAsync("LargeIntestineStoryboard");
			loader = Application.LoadLevelAsync("Test_nowEntering");
		} else if (Application.loadedLevelName.Equals("LargeIntestineStoryboard"))
		{
			loader = Application.LoadLevelAsync("LargeIntestine");
		} else if (Application.loadedLevelName.Equals("LargeIntestineEndStoryboard"))
		{
			loader = Application.LoadLevelAsync("EndScreen");
		}



		loader.allowSceneActivation = false;	// set this to mean we don't want the scene to load until we say
		yield return loader;
	}





	// Update is called once per frame
	void Update () {






		/**************************************************************/

		if ((!GetComponent<AudioSource>().isPlaying && longPageDone)|| canSkip) //((!GetComponent<AudioSource>().isPlaying && !longPages[currPage])|| canSkip) //
		{
			if (swipeDetection.getSwipeLeft() || buttonClicked)		// attempt to detect a swipe to the right
			{
				buttonClicked = false;
				swipeDetection.resetSwipe();						// reset the variables to prevent multiple page turns
				//currPage++;											// increment the page since we are going forward
				//hasPlayed = false;
				gotoNextPage();
				charAdded = false;
				
			} else if (swipeDetection.getSwipeRight() == true)			// attempt to detect a swipe to the left
			{
				swipeDetection.resetSwipe();						// reset the varaibel to prevent multiple page turns

				if (currPage - 1 > 0)								// perform bounds checking to make sure we don't go back too far
				{
					gotoPrevPage();
					charAdded = false;


				}
			}
		} else if(GetComponent<AudioSource>().isPlaying || !longPageDone)  //(GetComponent<AudioSource>().isPlaying || longPages[currPage])//
		{
			swipeDetection.resetSwipe();							// if we can't change the page yet forget the swipe
		}
			

		if (!GetComponent<AudioSource>().isPlaying &&!hasPlayed)								// only play the clip once per page
		{
			pageTurned = false;
			if (!((currPage - 1) >= BackGroundImages.Length))
			{
				GetComponent<AudioSource>().clip = sounds [currPage - 1];		// if we haven't played the sound yet load the new audio clip
				playClip();								// play the clip
				hasPlayed = true;						// mark that we have played the clip
			}
		}




		if ((currPage - 1) == BackGroundImages.Length)					// perform bounds checking to see if we should load the next scene
		{

			if (Application.loadedLevelName.Equals ("Test_IntroStoryBoard")) {
				PlayerPrefs.SetInt("PlayedTestIntroStory", 1);
				//PlayerPrefs.SetInt("PlayedIntroStory", 1);		// if we're ready set that we've heard this story segment
			} else if (Application.loadedLevelName.Equals("MouthStoryboard"))
			{
				PlayerPrefs.SetInt("PlayedMouthStory", 1);
			} else if (Application.loadedLevelName.Equals("Test_MouthStoryBoard"))
			{
				PlayerPrefs.SetInt("PlayedTestMouthStory", 1);
			} else if (Application.loadedLevelName.Equals("MouthEndStoryboard"))
			{
				PlayerPrefs.SetInt("PlayedMouthEndStory", 1);
				PlayerPrefs.SetInt ("CurrentStoryLevel", 1);
			} else if (Application.loadedLevelName.Equals("StomachStoryboard"))
			{
				PlayerPrefs.SetInt("PlayedStomachStory", 1);
			} else if (Application.loadedLevelName.Equals("StomachEndStoryboard"))
			{
				PlayerPrefs.SetInt("PlayedStomachEndStory", 1);
				PlayerPrefs.SetInt ("CurrentStoryLevel", 2);
			} else if (Application.loadedLevelName.Equals("SmallIntestineStoryboard"))
			{
				PlayerPrefs.SetInt("PlayedSIStory", 1);
			} else if (Application.loadedLevelName.Equals("SmallIntestineEndStoryboard"))
			{
				PlayerPrefs.SetInt("PlayedSIEndStory", 1);
				PlayerPrefs.SetInt ("CurrentStoryLevel", 3);
			} else if (Application.loadedLevelName.Equals("LargeIntestineStoryboard"))
			{
				PlayerPrefs.SetInt("PlayedLIStory", 1);
			} else if (Application.loadedLevelName.Equals("LargeIntestineEndStoryboard"))
			{
				PlayerPrefs.SetInt("PlayedLIEndStory", 1);
			}

			PlayerPrefs.Save();
			loader.allowSceneActivation = true;				// load the next level

			//Application.LoadLevel ("Test_nowEntering");

		}


		/**************************************************************/


		int tempPageNum = Mathf.Clamp (currPage - 1, 0, BackGroundImages.Length - 1);
		image.sprite = BackGroundImages [tempPageNum];

		/*
		 * 
		if (charSet.Contains (tempPageNum)) {
			//Debug.Log ("Current Page: " + tempPageNum);
			storyChar.setcharOn ();
		} else {
			storyChar.setcharOff ();
		}
		*/
		


	
	}



	private void playClip()
	{
		GetComponent<AudioSource>().Play();
	}

	public int currentPage(){
		return Mathf.Clamp (currPage - 1, 0, BackGroundImages.Length - 1);
	}

	public int totalPages(){
		return BackGroundImages.Length;
	}
	/*
	public Vector2 charPosition(){
		if (charSet.Contains(currentPage())) {
			return new Vector2(PageList [currentPage ()].PageCharacter.posx, PageList[currentPage()].PageCharacter.posy);
		}
		else
			return new Vector2(0,0);
	}
	*/

	public void setLongPageFinish(int n){
		//longPages [n] = false;
		longPageDone = true;
		Debug.Log ("LongPage Finished: " + n);

	}
	public void setLongPageStart(int n){
		//longPages [n] = true;
		longPageDone = false;
		Debug.Log ("LongPage Setuped: " + n);

	}
	public bool getLongPageDon(){
		return longPageDone;
	}
	public void gotoNextPage(){
		GetComponent<AudioSource> ().clip = pageTurnSound;

		Debug.Log (GetComponent<AudioSource> ().clip.name);
		playClip ();
		Debug.Log ("Goto next Page");
		hasPlayed = false;
		currPage++;
		//pageTurned = true;
	}
	public void gotoPrevPage(){
		GetComponent<AudioSource> ().clip = pageTurnSound;

		Debug.Log (GetComponent<AudioSource> ().clip.name);
		playClip ();
		Debug.Log ("Goto next Page");
		hasPlayed = false;
		currPage--;
		//pageTurned = true;
	}

}


public class Pages_Sprites //: NewPageSytemTest1
{
	public int num;
	public Sprite PageTexture;
	//public Texture[] CharacterTexture;
	public extraCharacters_Sprites PageCharacter;
	//public bool hasChar = false;
	public Pages_Sprites(Sprite pageimage, int n){
		PageTexture = pageimage;
		num = n;
	}
	public void setPage(Sprite pageText){
		this.PageTexture = pageText;

	}
	//public void setCharacter(Texture[] charText, float x, float y){
	public void setCharacter(Sprite[] charText, float x, float y){

		//CharacterTexture = charText;
		PageCharacter = new extraCharacters_Sprites(charText, x, y);
		//hasChar = true;
	}
	public Sprite getTexture(){
		return PageTexture;
	}
		
}

public class extraCharacters_Sprites //: NewPageSytemTest1
{
	//public Texture[] charTexture;
	public Sprite[] charSprite;
	public float posx;
	public float posy;
	public float width;
	public float height;
	//public extraCharacters(Texture[] charImage, float x, float y){
	public extraCharacters_Sprites(Sprite[] charImage, float x, float y){
		//charTexture = charImage;
		charSprite = charImage;
		posx = x;
		posy = y;
		width = charImage[0].bounds.size.x;
		height = charImage [0].bounds.size.y;
	}


}