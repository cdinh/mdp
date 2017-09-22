using UnityEngine;
using System.Collections;

/**
 * Keep track of general cell information
 */
public class CellManager : MonoBehaviour 
{
	public GameObject[] cells;              //!< for references to all the cell game objects

    /*Everytime you change the size of cells, remember to change the size of cellScripts*/

    public StomachCell[] cellScripts;		//!< for references to all the stomach cell scripts
	public CellButtons cellButtons;			//!< for a reference to the cell buttons script

	public Texture[] tapFigure;				//!< image of tap fingure 
	private bool bucketPopUp;						//!< boolean value to check if the pop up should appear
	private int cellNumber;					//!< cell number of first burned cell, -1 if no one burns

	private float animCounter;
	public float flameTime;


	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// initialize the script array
		cellScripts = new StomachCell[6];
		
		// get the scripts on each of the cells
		for (int i = 0; i < 6; i++)
		{
			cellScripts[i] = cells[i].GetComponent<StomachCell>();
		}

		//initialize the values for pop up
		bucketPopUp = false;
		cellNumber = -1;
		animCounter = 0;
	}

	void Update()
	{
		//handle popup when the stomach wall is burning
		for(int i = 0; i < 6; i++)
		{
			if (i != 3) {
				StomachCell cell = cellScripts [i];
				if ((cell.getCellState () == "burning") && (cellNumber == -1)) {
					bucketPopUp = true;
					cellNumber = i; 
				}
				if (cell.getCellState () == "slimed")
					bucketPopUp = false;
			}
		}
	}

	// display the tap popup
	void OnGUI()
	{
		if(bucketPopUp)
		{
			animCounter = animCounter + Time.deltaTime;
			if (animCounter >= flameTime * tapFigure.Length)
				animCounter = 0;
			
			GUI.DrawTexture (new Rect(Screen.width * 0.125f, 
				Screen.height * 0.45f,//0f,
				Screen.width * 0.5576f, 
				Screen.height * 0.5143f),tapFigure[(int)(animCounter/flameTime)]);

			/*
			GUI.DrawTexture (new Rect(Screen.width * 0.125f, 
				Screen.height * 0.715f, 
				Screen.width * 0.2093359375f, 
				Screen.height * 0.300697917f),tapFigure);
			*/
		}
	}
	
	/**
	 * Function called when a cell is clicked on to get the number of the cell clicked on
	 */

	public void clickOnCell(int cellNum)
	{
		cellButtons.checkMouseClick(cellNum);
	}
    public int getCellNumber()
    {
        return cellScripts.Length;
    }
}