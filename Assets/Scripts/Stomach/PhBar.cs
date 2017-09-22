using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * script that controls drawing the phBar and level indicators for the stomach game
 */
public class PhBar : MonoBehaviour 
{
	public RectTransform currentLevelRect;			//!< to hold the draw location and size data for the current level bar4
	public RectTransform phBarLocation;
	private float currentLevelHeight;				//!< store the indicator level for currentLevelRect

	public float startingAcidSpeed;					//!< the speed the bar initially moves when acid is added
	public float acidSpeedDecayTime;				//!< the time for the bar to slow to 0 after acid is added
	
	public float startingBaseSpeed;					//!< the speed the bar initially moves when base is added
	public float baseSpeedDecayTime;				//!< the time for the bar to slow to 0 after base is added

	private bool startAddAcid;						//!< flag to mark whether we are currently adding acid
	private bool startAddBase;						//!< flag to mark whether we are currently adding base
	private float elapsedTime;						//!< to count the time spent adding acid or base for velocity vector

	public float decaySpeed;

	public StomachTextBoxes textboxes;
	private StomachGameManager gm;
	private StomachFoodManager fm;

    private AcidPipeControl APC;
    private BasicPipeControl BPC;

	private const float TOP = 1390;
	private const float BOTTOM = 150;



	/**
	 * Use this for initialization
	 * Sets all dimenstions relative to screen size
	 * Chooses a semi-random starting height for the indicator bar
	 */
	void Start () 
	{
		gm = FindObjectOfType (typeof(StomachGameManager)) as StomachGameManager;
		fm = FindObjectOfType (typeof(StomachFoodManager)) as StomachFoodManager;
        APC = FindObjectOfType(typeof(AcidPipeControl)) as AcidPipeControl;
        BPC = FindObjectOfType(typeof(BasicPipeControl)) as BasicPipeControl;


        // starting level of the current level indicator
        currentLevelHeight = 700;

		currentLevelRect.anchoredPosition = new Vector2(currentLevelRect.anchoredPosition.x, currentLevelHeight);
	}
	
	/**
	 * Update is called once per frame
	 * Handles adding acid/base if the button has been pressed
	 * Controls acid decay over time
	 */
	void Update () 
	{
		
		// first check if the time that the acid/base should be added is up
		if (elapsedTime > acidSpeedDecayTime && startAddAcid)			// check if we're adding acid and time is up
		{																// if it is...
			startAddAcid = false;										// set the flag to indicate we aren't adding acid
			elapsedTime = 0f;											// reset the elapsed timer
			return;
		} else if (elapsedTime > baseSpeedDecayTime && startAddBase)	// check if we're adding base and the time is up
		{																// if it is...
			startAddBase = false;										// set the flag to indicate we aren't adding base
			elapsedTime = 0f;											// reset the elapsed timer
			return;
		}

		// check if we are currently adding acid
		if (startAddAcid)
		{
			// increment the timer
			elapsedTime += Time.deltaTime;

			// set the new destination the current acid level indicator should be drawn at
			// it should move at a decreasing speed so to do so we multiply the desired speed by the percentage of time
			// that is left to move the indicator line
			moveCurrentLevelRect(startingAcidSpeed * ( 1 - (elapsedTime / acidSpeedDecayTime)));

			return;
		}

		// check if we are currently adding base
		if (startAddBase)
		{
			// increment the timer
			elapsedTime += Time.deltaTime;

			// set the new destination the current base level indicator should be drawn at
			// it should move at a decreasing speed so to do so we multiply the desired speed by the percentage of time
			// that is left to move the indicator line
			moveCurrentLevelRect(-(startingBaseSpeed * ( 1 - (elapsedTime / baseSpeedDecayTime))));
			
			return;
		}

		// if we aren't adding acid or base decay the bar somewhat
		moveCurrentLevelRect(-1f * decaySpeed);

	}

	/**
	 * Handles moving the current level of acidity bar
	 */
	private void moveCurrentLevelRect(float speed)
	{
		currentLevelHeight = currentLevelRect.anchoredPosition.y + (speed * Time.deltaTime * 1536f / Screen.height);
		currentLevelHeight = Mathf.Min (Mathf.Max (currentLevelHeight, BOTTOM), TOP);
		//Debug.Log (currentLevelHeight);
		currentLevelRect.anchoredPosition = new Vector3 (currentLevelRect.anchoredPosition.x, currentLevelHeight, 0f);
	}

	/**
	 * function that can be called to simulate adding acid to the stomach and its effect on the ph bar
	 */
	public void addAcid()
	{
        
		startAddAcid = true;		// throw the flag to indicate we should start adding acid
		startAddBase = false;		// if we were adding base, override the decision (no longer add base)
		elapsedTime = 0f;			// reset elapsed time


        if(BPC.getClick() == true)
        {
            BPC.ButtonToggle();
        }
        if(gm.getCurrentAcidLevel() == "basic")
        {
            gm.setCurrentAcidLevel("neutral");
        }
        else if(gm.getCurrentAcidLevel() == "neutral")
        {
            gm.setCurrentAcidLevel("acidic");
        }
        
	}

	/**
	 * function that can be called to simulate adding base to the stomach and its effect on the ph bar
	 */
	public void addBase()
	{
        
		if (gm.getCurrentAcidLevel() != "acidic" && fm.getNumFoodBlobs() != 0)
		{
			textboxes.setTextbox(4);
		}
         
		startAddBase = true;		// throw the flag to indicate that we should start adding base
		startAddAcid = false;		// if we were adding acid, override the decision (no longer add acid)
		elapsedTime = 0f;			// reset elapsed time

        if (APC.getClick() == true)
        {
            APC.ButtonToggle();
        }



        if (APC.getClick() == true)
        {
            APC.ButtonToggle();
        }
        if (gm.getCurrentAcidLevel() == "acidic")
        {
            gm.setCurrentAcidLevel("neutral");
        }
        else if (gm.getCurrentAcidLevel() == "neutral")
        {
            gm.setCurrentAcidLevel("basic");
        }

    }

	/**
	 * Function that can be used to get the current level of the acidity level in the stomach
	 */
	public float getCurrentLevelRectHeight()
	{
		return currentLevelHeight;
	}
}
