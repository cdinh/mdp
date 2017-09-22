using UnityEngine;
using System.Collections;

/**
 * Handles the menu functions for when a cell is clicked on
 */
public class CellButtons : MonoBehaviour 
{
	public GUIStyle singButton;					//!< guistyle for the sing button (old gui)
	public GUIStyle mucousButton;				//!< guistyle for the mucous button (old gui)
	public GUIStyle dieButton;					//!< guistyle for the die button (old gui)
	
	public GUIStyle bucket;						//!< guistyle for the button showed after mucous button pressed (old gui)
	public GUIStyle scythe;						//!< guistyle for the button showed after die button pressed (old gui)
	private bool showBucket;					//!< flag to indicate if we are showing the bucket 
	private bool showScythe;					//!< flag to indicate if we are showing the scythe
	
	private bool wasMouseClicked;				//!< keep track of if mouse was clicked
	private bool mouseDownLastFrame;			//!< keep track of if mouse was clicked on the last update
	private bool isEnabled;						//!< keep track of if the button menu is up or not
	private Vector2 mouseClickLocation;			//!< record the 2D position of a mouse click (x and y)
	
	private Vector2 buttonSize;					//!< for the button size
	private Vector2 button1SpawnLocation;		//!< to hold the location to draw the sing button
	private Vector2 button2SpawnLocation;		//!< to hold the location to draw the mucous button
	private Vector2 button3SpawnLocation;		//!< to hold the location to draw the die button
	
	private int menuSemaphore;					//!< helper variable to prevent menu showing up/disappearing on the same click
	
	private CellManager cellManager;			//!< hold a reference to the cell manager script
	public MainCell mainCell;					//!< hold a reference to the main cell script
	private StomachGameManager gm;	//!< hold a reference to the stomach game manager


//    private mouseDrag MD;
	private mouseDragChild MDC;


	
	/**
	 * For initialization
	 */
	void Start()
	{
		// find reference to the cell manager
		cellManager = FindObjectOfType (typeof(CellManager)) as CellManager;
		gm = FindObjectOfType (typeof(StomachGameManager)) as StomachGameManager;
        //MD = FindObjectOfType(typeof(mouseDrag)) as mouseDrag;

		MDC = FindObjectOfType(typeof(mouseDragChild)) as mouseDragChild;
		
		// calculate button size based on screen size
		buttonSize.x = Screen.width * (186f / 1024f);
		buttonSize.y = Screen.height * (253f / 768f);
	}
	
	/**
	 * Update is called once per frame
	 */
	void Update () 
	{
		// we don't want to do any updates if the game is paused
		if (Time.timeScale == 0)
		{
			isEnabled = false;
			return;
		}
		
		// save if the mouse is clicked currently
		wasMouseClicked = Input.GetMouseButton (0);
		
		// check if the mouse was down last frame, but is not currently pressed
		if (mouseDownLastFrame && !wasMouseClicked)
		{
			mouseClickLocation.x = Input.mousePosition.x;
			mouseClickLocation.y = Input.mousePosition.y;
		}
		
		// copy over the value for this frame to check next frame
		mouseDownLastFrame = wasMouseClicked;
		
		// update the counter for the semaphore
		if (menuSemaphore > 0)
		{
			menuSemaphore--;
		}
	}
	
	/**
	 * What to do on a mouse click
	 */
	public void checkMouseClick(int cell)
	{	
		// check if it's ok to update the menu on a mouse click on the main cell (2)
//		float x = mouseClickLocation.x;
//		float y = mouseClickLocation.y;
//		if (x <= 528.0 && x >= 83.0 && y >= 441.0 &&  y<= 794.0) 
//		{
//			isInCell2 = true;
//		}

/*
		if (menuSemaphore <= 0 && !isEnabled && cell==2) 
		{
			// clicking on the main cell either cancels or brings up the menu, so handle correctly
			if (showBucket || showScythe)
			{
				showBucket = false;
				showScythe = false;
			} else
			{
				isEnabled = true;
			}
 
		} else if (menuSemaphore <= 0 && showBucket)
		{
*/


        if(MDC.getDrag() == true) { 
			// if we click on a cell with the bucket, the cell is now slimed
			if (cellManager.cellScripts[cell - 1].getCellState() == "slimed")
			{
				cellManager.cellScripts[cell - 1].setCellRefresh(true);
			} else
			{
				cellManager.cellScripts[cell - 1].setCellState("slimed");
			}
		} else if (menuSemaphore <= 0 && showScythe)
		{
			// if we click on a cell with the scythe, the cell is now dead
			if(cellManager.cellScripts[cell-1].getCellState()!="dead")
			{
				gm.KillOneCell();
			}
			cellManager.cellScripts[cell - 1].setCellState("dead");

		} else
		{
			// any other case make sure menu is disabled, and reset the menu block timer
			isEnabled = false;
			menuSemaphore = 20;
		}
	}
	
	/**
	 * Handles drawing of related gui components
	 * ***LEGACY GUI*****
	 */
	void OnGUI()
	{
		GUI.depth = GUI.depth - 20;	// for gui layering
		
		// if we clicked on the "make mucous" button, show the mucous bucket
		if (showBucket)
		{
			GUI.RepeatButton(new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, buttonSize.x, buttonSize.y),
			                 "",
			                 bucket);
		}
		
		// if we clicked on the "die" button, show the scythe
		if (showScythe)
		{
			GUI.RepeatButton(new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, buttonSize.x, buttonSize.y),
			                 "",
			                 scythe);
		}
		
		// check if we should draw the menu
		if (!isEnabled)	// we shouldn't
		{
//			isEnabled = false;
			return;
		} else 			// we should
		{
			// get the 3 buttons
			showButton1(mouseClickLocation.x, Screen.height - mouseClickLocation.y);
			showButton2(mouseClickLocation.x, Screen.height - mouseClickLocation.y);
			showButton3(mouseClickLocation.x, Screen.height - mouseClickLocation.y);
//			isEnabled = true;
		}
	}
	
	/**
	 * Function to find location of and create the "sing" button
	 */
	private void showButton1(float mouseX, float mouseY)
	{
		if (GUI.Button (new Rect (mouseX - buttonSize.x - .02f * Screen.width, mouseY, buttonSize.x, buttonSize.y), "", singButton))
		{
			// display the zyme text box corresponding to clicking on sing
			mainCell.singClicked();
		}
	}
	
	/**
	 * Function to find location of and create the "mucous" button
	 */
	private void showButton2(float mouseX, float mouseY)
	{
		if (GUI.Button (new Rect (mouseX, mouseY, buttonSize.x, buttonSize.y), "", mucousButton))
		{
			// display the zyme text box corresponding to clicking on mucous
			mainCell.mucousClicked();
			
			// for proper menu behavior
			isEnabled = false;
			menuSemaphore = 20;
			// show the bucket icon
			showScythe = false;
			showBucket = true;

			// check if refreshing
			if (cellManager.cellScripts[1].getCellState() == "slimed")
			{
				cellManager.cellScripts[1].setCellRefresh(true);
			}

			//slime main cell
			if (cellManager.cellScripts[1].getCellState() != "dead")
			{
				cellManager.cellScripts[1].setCellState("slimed");
			}
		}
	}
	
	/**
	 * Function to find location of and create the "die" button
	 */
	private void showButton3(float mouseX, float mouseY)
	{
		if (GUI.Button (new Rect (mouseX + buttonSize.x + .02f * Screen.width, mouseY, buttonSize.x, buttonSize.y), "", dieButton))
		{
			// display the zyme text box corresponding to clicking on die
			mainCell.dieClicked();
			
			// for proper menu behavior
			isEnabled = false;
			menuSemaphore = 20;
			// show the bucket icon
			showBucket = false;
			showScythe = true;
			
			//kill main cell
			if(cellManager.cellScripts[1].getCellState()!="dead")
			{
				gm.KillOneCell();
			}
			cellManager.cellScripts[1].setCellState("dead");
		}
	}
}