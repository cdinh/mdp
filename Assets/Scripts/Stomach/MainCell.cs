using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Script to handle behavior specific to the main cell (2)
 */
public class MainCell : MonoBehaviour 
{
	private StomachCell mainCellScript;		//!< main cell script to use for state changes
	public StomachTextBoxes textboxes;		//!< reference to the stomach text boxes script
	
	public Image face;						//!< reference to the image drawing the face on cell 2
	public Sprite[] cellFaces;				//!< array holding all possible cell faces
	
	private bool wasSingClicked;			//!< flag to indicate if sing was pressed (for textbox behavior)
	private float elapsedTime = 0f;			//!< to count time
	private float maxElapsedTime = 2f;		//!< max amount of time allowed to pass before event change
	
	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// get the reference to the main cell script
		mainCellScript = gameObject.GetComponent<StomachCell> ();
	}
	
	/**
	 * Update functions.Called every frame
	 */
	void Update()
	{
		/**
		 * Special logic for if the sing button was clicked
		 */
		if (wasSingClicked)
		{
			elapsedTime += Time.deltaTime;
			
			if (elapsedTime < maxElapsedTime)
			{
				face.sprite = cellFaces[0];
				return;
			} else
			{
				wasSingClicked = false;
				textboxes.setTextbox (1);
				face.sprite = cellFaces[3];
			}
		}
		
		/**
		 * Sets the main cell face under normal conditions
		 */
		switch(mainCellScript.getCellState())
		{
		case "normal":
		{
			face.sprite = cellFaces[3];
			break;
		}
		case "slimed":
		{
			face.sprite = cellFaces[3];
			break;
		}
		case "burning":
		{
			face.sprite = cellFaces[1];
			break;
		}
		case "dead":
		{
			face.sprite = cellFaces[2];
			break;
		}
		default:
		{
			break;
		}
		}
	}
	
	/**
	 * Function called when the sing button is clicked on the
	 * main cell
	 */
	public void singClicked()
	{
		Debug.Log ("sing clicked");
		wasSingClicked = true;
		elapsedTime = 0f;
	}
	
	/**
	 * Function called when the die button is clicked on the
	 * main cell
	 */
	public void dieClicked()
	{
		Debug.Log ("die clicked");
		textboxes.setTextbox (2);
	}
	
	/**
	 * Function called when the mucous button is clicked on the
	 * main cell
	 */
	public void mucousClicked()
	{
		Debug.Log ("mucous clicked");
		textboxes.setTextbox(3);
	}
}