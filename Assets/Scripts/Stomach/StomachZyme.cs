using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Handles drawing the zyme avatar with the proper graphic based on what is going on in the game
 */
public class StomachZyme : MonoBehaviour 
{
	private Image i;

	public Sprite zymeHappy;					//!< holds the texture that will be drawn for happy zyme
	public Sprite zymeConcerned;				//!< holds the texture that will be drawn for concerned zyme
	public Sprite zymeSlimed;					//!< holds the texture that will be drawn for slimed zyme

	private bool drawHappyZyme = true;			//!< flag to indicate we should draw happy zyme
	private bool drawConcernedZyme = false;		//!< flag to indicate we should draw concerned zyme
	private bool drawSlimedZyme = false;		//!< flag to indicate we should draw slimed zyme

	public StomachTextBoxes stomachTextBoxes;
	private StomachGameManager gm;
	
	void Start () 
	{
		i = GetComponent<Image> ();
		gm = FindObjectOfType (typeof(StomachGameManager)) as StomachGameManager;
	}

	void Update()
	{
		// all below code just handles which zyme to draw
		if (drawHappyZyme)
		{
			i.sprite = zymeHappy;
			drawConcernedZyme = false;
			drawSlimedZyme = false;
		}

		if (drawConcernedZyme)
		{
			i.sprite = zymeConcerned;
			drawHappyZyme = false;
			drawSlimedZyme = false;
		}

		if (drawSlimedZyme)
		{
			i.sprite = zymeSlimed;
			drawHappyZyme = false;
			drawConcernedZyme = false;
		}
	}

	/**
	 * Accessor function so we can change to happy zyme from other scripts
	 * if necessary
	 */
	public void setDrawHappyZyme()
	{
		drawHappyZyme = true;
		drawConcernedZyme = false;
		drawSlimedZyme = false;
	}

	/**
	 * Accessor function so we can change to concerned zyme from other scripts
	 * if necessary
	 */
	public void setDrawConcernedZyme()
	{
		drawHappyZyme = false;
		drawConcernedZyme = true;
		drawSlimedZyme = false;
	}

	/**
	 * Accessor function so we can change to slimed zyme from other scripts
	 * if necessary
	 */
	public void setDrawSlimedZyme()
	{
		drawHappyZyme = false;
		drawConcernedZyme = false;
		drawSlimedZyme = true;
	}

	/**
	 * complex logic for which textbox to show for zyme's speech based
	 * on game conditions...
	 */
	public void clickOnZyme()
	{
		if (gm.getCurrentAcidLevel() == "neutral")
		{
			/**
			 * Stomach is not acidic and cell is not slimed
			 */
			stomachTextBoxes.setTextbox (6);

		} else if (gm.getCurrentAcidLevel() == "acidic")
		{
			bool cellSlimed = false;
			for (int i = 0; i < gm.cellManager.cellScripts.Length; i++)
			{
				if (gm.cellManager.cellScripts[i].getCellState() == "slimed")
				{
					cellSlimed = true;
					break;
				}
			}

			if (cellSlimed)
			{
			/**
			 * Stomach is acidic but cell is not slimed
			 */
				stomachTextBoxes.setTextbox(8);

			} else
			{
			/**
			 * Stomach is acidic and some cells are slimed
			 */
				stomachTextBoxes.setTextbox(13);

			}
		}
	}
}
