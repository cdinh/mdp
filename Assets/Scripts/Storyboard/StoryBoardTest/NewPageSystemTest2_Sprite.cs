using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NewPageSystemTest2_Sprite : MonoBehaviour {

	//	public int size;
	public Texture[] PageImage;	
	public int[] CharacterPage;
	public float[] XCharacterPosition;
	public float[] YCharacterPosition;
	public Sprite[] CharacterImage;


	public GameObject StoryBoardCharacter;
	private bool charAdded;


	private List<Pages_2> PageList = new List<Pages_2>();
	private HashSet<int> charSet = new HashSet<int>();





	/***************************************************************/
	public AudioClip[] sounds;			//!< store the storyboard narrations
	private int currPage = 1;			//!< store the current page
	private bool hasPlayed = false;		//!< remember whether the current sound has played
	private bool buttonClicked = false;	//!< holds if the user clicked in the page corner instead of swiping

	DetectStraightSwipe swipeDetection;	//!< to hold a reference to the script that controls swipe detection

	private bool canSkip = false;		//!< check for playthrough

	private AsyncOperation loader;		//!< for preloading the next level

	/***************************************************************/







	// Use this for initialization
	void Start () {

		charAdded = false;


		for (int i = 0; i < CharacterPage.Length; i++) {
			charSet.Add (CharacterPage [i]);
			Debug.Log ("Page " + CharacterPage[i] + " added");
		}

		int tempCharCounter = 0;
		for(int i = 0; i<PageImage.Length; i++){
			PageList.Add (new Pages_2 (PageImage [i],i));
			if (charSet.Contains (i)) {
				//Texture[] tempCharTexture = new Texture[2];

				Sprite[] tempCharTexture = new Sprite[2];
				tempCharTexture [0] = CharacterImage [tempCharCounter];
				tempCharTexture [1] = CharacterImage [tempCharCounter+1];

				PageList [i].setCharacter (tempCharTexture, XCharacterPosition [tempCharCounter], YCharacterPosition [tempCharCounter]);
			}

		}



		/**************************************************************/
		swipeDetection = gameObject.GetComponent<DetectStraightSwipe> ();


		/**************************************************************/









	}

	// Update is called once per frame
	void Update () {
		Debug.Log("Number of Texture:" + PageList.Count);


		/*

		int tempPageNum = Mathf.Clamp (currPage - 1, 0, PageImage.Length - 1);
		if (charSet.Contains (tempPageNum) && charAdded == false) {
			Vector2 tempPosition = new Vector2 (PageList [tempPageNum].PageCharacter.posx, PageList [tempPageNum].PageCharacter.posy);
			GameObject tempCharacter = (GameObject)Instantiate (StoryBoardCharacter, tempPosition, Quaternion.identity);
			//tempCharacter.GetComponent<GeneralStoryBoardCharacter> (PageList [tempPageNum].PageCharacter.charSprite);
			tempCharacter.GetComponent<GeneralStoryBoardCharacter>().setImage (PageList [tempPageNum].PageCharacter.charSprite);
			charAdded = true;
		}
		*/




		/**************************************************************/

		if (!GetComponent<AudioSource>().isPlaying || canSkip)
		{
			if (swipeDetection.getSwipeLeft() || buttonClicked)		// attempt to detect a swipe to the right
			{
				buttonClicked = false;
				swipeDetection.resetSwipe();						// reset the variables to prevent multiple page turns
				currPage++;											// increment the page since we are going forward
				hasPlayed = false;
				charAdded = false;
			} else if (swipeDetection.getSwipeRight() == true)			// attempt to detect a swipe to the left
			{
				swipeDetection.resetSwipe();						// reset the varaibel to prevent multiple page turns

				if (currPage - 1 > 0)								// perform bounds checking to make sure we don't go back too far
				{
					currPage--;
					hasPlayed = false;
					charAdded = false;
				}
			}
		} else if (GetComponent<AudioSource>().isPlaying)
		{
			swipeDetection.resetSwipe();							// if we can't change the page yet forget the swipe
		}

		if (!hasPlayed)								// only play the clip once per page
		{
			if (!((currPage - 1) >= PageList.Count))
			{
				GetComponent<AudioSource>().clip = sounds [currPage - 1];		// if we haven't played the sound yet load the new audio clip
				playClip();								// play the clip
				hasPlayed = true;						// mark that we have played the clip
			}
		}

		/**************************************************************/

	}

	private void playClip()
	{
		GetComponent<AudioSource>().Play();
	}


	void OnGUI()
	{
		if (PlayerPrefs.GetInt("ShowPageNumbers") == 0)
		{
			int tempPageNum = Mathf.Clamp (currPage - 1, 0, PageImage.Length - 1);
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), PageList[tempPageNum].getTexture());

			Debug.Log ("Current Page:" + tempPageNum);
			if (charSet.Contains (tempPageNum)) {

				float x = PageList [tempPageNum].PageCharacter.posx;
				float y = PageList [tempPageNum].PageCharacter.posy;
				float width = PageList [tempPageNum].PageCharacter.width;
				float height = PageList [tempPageNum].PageCharacter.height;


				GUI.DrawTexture (new Rect (x, y, Screen.width/5, Screen.height/5), PageList [tempPageNum].PageCharacter.charSprite[0].texture);

				GUI.Label (new Rect (0,0,10,10),"black");

//				GUIContent tempContent = PageList [tempPageNum].PageCharacter.charSprite[0].texture;

//				GUI.DrawTexture (new Rect (x, y, Screen.width / 5, Screen.height / 5), tempContent);


			}

		} 
		/*
		else
		{
			GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), numberedPages[Mathf.Clamp(currPage - 1, 0, pages.Length - 1)]);
		}
		*/

		// create an invisible button by the page turn
		if(!GetComponent<AudioSource>().isPlaying || canSkip)
		{
			GUI.color = new Color() { a = 0.0f };
			if (GUI.Button(new Rect(Screen.width * .84f, 0, Screen.width * .16f, Screen.width * .16f),""))
			{
				buttonClicked = true;
				hasPlayed = false;
			}
			GUI.color = new Color() { a = 1.0f };
		}
	}


}





public class Pages_2 //: NewPageSytemTest1
{
	public int num;
	public Texture PageTexture;
	//public Texture[] CharacterTexture;
	public extraCharacters_2 PageCharacter;
	public Pages_2(Texture pageimage, int n){
		PageTexture = pageimage;
		num = n;
	}
	public void setPage(Texture pageText){
		this.PageTexture = pageText;

	}
	//public void setCharacter(Texture[] charText, float x, float y){
	public void setCharacter(Sprite[] charText, float x, float y){

		//CharacterTexture = charText;
		PageCharacter = new extraCharacters_2(charText, x, y);
	}
	public Texture getTexture(){
		return PageTexture;
	}

}

public class extraCharacters_2 //: NewPageSytemTest1
{
	//public Texture[] charTexture;
	public Sprite[] charSprite;
	public float posx;
	public float posy;
	public float width;
	public float height;
	//public extraCharacters(Texture[] charImage, float x, float y){
	public extraCharacters_2(Sprite[] charImage, float x, float y){
		//charTexture = charImage;
		charSprite = charImage;
		posx = x;
		posy = y;
		width = charImage[0].bounds.size.x;
		height = charImage [0].bounds.size.y;
	}


}

