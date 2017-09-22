using UnityEngine;
using System.Collections;

public class testStomachStoryboardHandler : MonoBehaviour {

    public Texture[] pages;             //!< story the storyboard pages
    public Texture[] numberedPages;     //!< same storybook pages, but numbered
    public AudioClip[] sounds;          //!< store the storyboard narrations
    private int currPage = 1;           //!< store the current page
    private bool hasPlayed = false;     //!< remember whether the current sound has played
    private bool buttonClicked = false; //!< holds if the user clicked in the page corner instead of swiping


    DetectStraightSwipe swipeDetection; //!< to hold a reference to the script that controls swipe detection

    private bool canSkip = false;       //!< check for playthrough

    private AsyncOperation loader;      //!< for preloading the next level

    /**
	 * Use this for initialization
	 * Start detecting touch inputs and figure out if we can skip the level.
	 */


    public int movePageNum;
    private Texture2D moveImageTexture2D;
    private Rect moveImageRect;

    void Start()
    {
        //Setup moving page status
        
        moveImageRect = new Rect(0, 0, Screen.width, Screen.height*2);



        // find the script for detecting touch
        swipeDetection = gameObject.GetComponent<DetectStraightSwipe>();

        // find out if we can skip without listening
        if (Application.loadedLevelName.Equals("IntroStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedIntroStory") == 1) ? true : false;
        }
        else if (Application.loadedLevelName.Equals("MouthStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedMouthStory") == 1) ? true : false;
        }
        else if (Application.loadedLevelName.Equals("MouthEndStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedMouthEndStory") == 1) ? true : false;
        }
        else if (Application.loadedLevelName.Equals("StomachStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedStomachStory") == 1) ? true : false;
        }
        else if (Application.loadedLevelName.Equals("StomachEndStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedStomachEndStory") == 1) ? true : false;
        }
        else if (Application.loadedLevelName.Equals("SmallIntestineStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedSIStory") == 1) ? true : false;
        }
        else if (Application.loadedLevelName.Equals("SmallIntestineEndStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedSIEndStory") == 1) ? true : false;
        }
        else if (Application.loadedLevelName.Equals("LargeIntestineStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedLIStory") == 1) ? true : false;
        }
        else if (Application.loadedLevelName.Equals("LargeIntestineEndStoryboard"))
        {
            canSkip = (PlayerPrefs.GetInt("PlayedLIEndStory") == 1) ? true : false;
        }



        // preload next scene
        StartCoroutine(loadNextLevel());
    }

    /**
	 * Starts preloading the next level to avoid delays
	 */
    IEnumerator loadNextLevel()
    {
        // starts preloading hte next level in the sequence accordingly
        if (Application.loadedLevelName.Equals("IntroStoryboard"))
        {
            loader = Application.LoadLevelAsync("MouthStoryboard");
        }
        else if (Application.loadedLevelName.Equals("MouthStoryboard"))
        {
            loader = Application.LoadLevelAsync("LoadLevelMouth");
        }
        else if (Application.loadedLevelName.Equals("MouthEndStoryboard"))
        {
            loader = Application.LoadLevelAsync("StomachStoryboard");
        }
        else if (Application.loadedLevelName.Equals("StomachStoryboard"))
        {
            loader = Application.LoadLevelAsync("LoadLevelStomach");
        }
        else if (Application.loadedLevelName.Equals("StomachEndStoryboard"))
        {
            loader = Application.LoadLevelAsync("SmallIntestineStoryboard");
        }
        else if (Application.loadedLevelName.Equals("SmallIntestineStoryboard"))
        {
            loader = Application.LoadLevelAsync("LoadLevelSmallIntestine");
        }
        else if (Application.loadedLevelName.Equals("SmallIntestineEndStoryboard"))
        {
            loader = Application.LoadLevelAsync("LargeIntestineStoryboard");
        }
        else if (Application.loadedLevelName.Equals("LargeIntestineStoryboard"))
        {
            loader = Application.LoadLevelAsync("LargeIntestine");
        }
        else if (Application.loadedLevelName.Equals("LargeIntestineEndStoryboard"))
        {
            loader = Application.LoadLevelAsync("EndScreen");
        }




        loader.allowSceneActivation = false;    // set this to mean we don't want the scene to load until we say
        yield return loader;
    }

    /**
	 * Update is called once per frame
	 * Allows page turns when appropriate.
	 */
    void Update()
    {
        //move the page if it is setup
        //if(currPage == movePageNum)
        //{
        //    moveImageRect.y += 10f * Time.deltaTime;
        //}


        if (!GetComponent<AudioSource>().isPlaying || canSkip)
        {
            if (swipeDetection.getSwipeLeft() || buttonClicked)     // attempt to detect a swipe to the right
            {
                buttonClicked = false;
                swipeDetection.resetSwipe();                        // reset the variables to prevent multiple page turns
                currPage++;                                         // increment the page since we are going forward
                hasPlayed = false;
            }
            else if (swipeDetection.getSwipeRight() == true)            // attempt to detect a swipe to the left
            {
                swipeDetection.resetSwipe();                        // reset the varaibel to prevent multiple page turns

                if (currPage - 1 > 0)                               // perform bounds checking to make sure we don't go back too far
                {
                    currPage--;
                    hasPlayed = false;
                }
            }
        }
        else if (GetComponent<AudioSource>().isPlaying)
        {
            swipeDetection.resetSwipe();                            // if we can't change the page yet forget the swipe
        }

        if (!hasPlayed)                             // only play the clip once per page
        {
            if (!((currPage - 1) >= pages.Length))
            {
                GetComponent<AudioSource>().clip = sounds[currPage - 1];        // if we haven't played the sound yet load the new audio clip
                playClip();                             // play the clip
                hasPlayed = true;                       // mark that we have played the clip
            }
        }

        if ((currPage - 1) == pages.Length)                 // perform bounds checking to see if we should load the next scene
        {
            if (Application.loadedLevelName.Equals("IntroStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedIntroStory", 1);      // if we're ready set that we've heard this story segment
            }
            else if (Application.loadedLevelName.Equals("MouthStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedMouthStory", 1);
            }
            else if (Application.loadedLevelName.Equals("MouthEndStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedMouthEndStory", 1);
            }
            else if (Application.loadedLevelName.Equals("StomachStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedStomachStory", 1);
            }
            else if (Application.loadedLevelName.Equals("StomachEndStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedStomachEndStory", 1);
            }
            else if (Application.loadedLevelName.Equals("SmallIntestineStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedSIStory", 1);
            }
            else if (Application.loadedLevelName.Equals("SmallIntestineEndStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedSIEndStory", 1);
            }
            else if (Application.loadedLevelName.Equals("LargeIntestineStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedLIStory", 1);
            }
            else if (Application.loadedLevelName.Equals("LargeIntestineEndStoryboard"))
            {
                PlayerPrefs.SetInt("PlayedLIEndStory", 1);
            }

            PlayerPrefs.Save();
            loader.allowSceneActivation = true;             // load the next level
        }
    }

    /**
	 * play the audio clip
	 */
    private void playClip()
    {
        GetComponent<AudioSource>().Play();
    }

    /**
	 * Handles drawing the background texture and the invisible button in the corner.
	 */
    void OnGUI()
    {
        if (PlayerPrefs.GetInt("ShowPageNumbers") == 0)
        {
            //if we want to move the page
            if(currPage == movePageNum)
            {
                if (moveImageRect.y > -Screen.height)
                {
                    moveImageRect.y -= (Screen.height/20) * Time.deltaTime;
                    Debug.Log(moveImageRect.y);
                }
                GUI.DrawTexture(moveImageRect, pages[Mathf.Clamp(currPage - 1, 0, pages.Length - 1)]);
            }
            else
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pages[Mathf.Clamp(currPage - 1, 0, pages.Length - 1)]);


        }
        else
        {
            if (currPage == movePageNum)
            {
                if (moveImageRect.y > -Screen.height)
                {
                    moveImageRect.y -= (Screen.height / 20) * Time.deltaTime;
                    Debug.Log(moveImageRect.y);
                }
                GUI.DrawTexture(moveImageRect, pages[Mathf.Clamp(currPage - 1, 0, pages.Length - 1)]);
            }
            else
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pages[Mathf.Clamp(currPage - 1, 0, pages.Length - 1)]);
        }

        // create an invisible button by the page turn
        if (!GetComponent<AudioSource>().isPlaying || canSkip)
        {
            GUI.color = new Color() { a = 0.0f };
            if (GUI.Button(new Rect(Screen.width * .84f, 0, Screen.width * .16f, Screen.width * .16f), ""))
            {
                buttonClicked = true;
                hasPlayed = false;
            }
            GUI.color = new Color() { a = 1.0f };
        }
    }

    /**
	 * Returns the current page
	 */
    public int getCurrPage()
    {
        return currPage;
    }
}
