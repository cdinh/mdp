using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Generic script that handles stomach cell behavior, relevant to any cell
 */
public class StomachCell : MonoBehaviour 
{
	private Image i;						//!< hold the reference to the image to change it
	public Image timer;						//!< to hold the current timer image
	
	public Sprite[] timers;					//!< array of timer images
	public Sprite[] cellStateImages;		//!< array of cell state images
	
	private string cellState = "normal";	//!< default cell state is normal
	private bool cellRefresh = false;

    public int CellNum;
//    private mouseDrag MD;
	private mouseDragChild MDC;


    private StomachGameManager gm;
    private float burnToDeathTime;

    private float burnTimer;


	public AudioClip[] cellAudios;
	AudioSource audio;
	private bool[] cellAudioBoolean;



	/**
	 * For initialization
	 */
	void Start()
	{
		// get a reference to the image
		i = GetComponent<Image> ();

//        MD = FindObjectOfType(typeof(mouseDrag)) as mouseDrag;
		MDC = FindObjectOfType(typeof(mouseDragChild)) as mouseDragChild;
        gm = FindObjectOfType(typeof(StomachGameManager)) as StomachGameManager;
        burnToDeathTime = gm.TIME_TO_DIE;

        burnTimer = 0;


		audio = GetComponent<AudioSource>();
		cellAudioBoolean = new bool[2];
		cellAudioBoolean[0] = false;
		cellAudioBoolean[1] = false;

    }
	
	/**
	 * Update... called every frame.
	 * Draws the correct cell image based on state
	 */
	void Update()
	{
		if (!cellAudioBoolean[1] && cellState == "burning") {
			audio.PlayOneShot (cellAudios[1], 1.0f);
			//GetComponent<AudioSource>().Play();
			Debug.Log ("audio played");
			cellAudioBoolean[1] = true;
		}

		if (cellState == "normal")
		{
			i.sprite = cellStateImages[0];
			return;
		}
		
		if (cellState == "burning")
		{
			
			if (gm.getCurrentAcidLevel () == "acidic") {
				burnTimer = burnTimer + Time.deltaTime;
				//Debug.Log ("burning");
			}
            if(burnTimer > burnToDeathTime * 3 / 4)
            {
                i.sprite = cellStateImages[6];
            }
            else if(burnTimer > burnToDeathTime * 2 / 4)
            {
                i.sprite = cellStateImages[5];
            }
            else if (burnTimer > burnToDeathTime / 4)
            {
                i.sprite = cellStateImages[4];
            }
            else
            {
                i.sprite = cellStateImages[1];
            }
			return;
		}
		
		if (cellState == "slimed")
		{
			i.sprite = cellStateImages[2];
			return;
		}
		
		if (cellState == "dead")
		{
			i.sprite = cellStateImages[3];
			return;
		}
	}
	
	/**
	 * set the correct cell timer image for the cell
	 */
	public void setTimerImage(int index)
	{
		//if (index != 5)
						timer.sprite = timers [index];

		/*
		else
						timer.sprite = null;
		*/				
	}
	
	/**
	 * Allows outside classes to alter the cell state based on events
	 */
	public void setCellState(string newState)
	{
		if (getCellState() != "dead") {
			cellState = newState;
			if (!cellAudioBoolean [0] && newState == "slimed") {
				audio.PlayOneShot (cellAudios [0], 1f);
				Debug.Log ("audio played");
				cellAudioBoolean [0] = true;
				cellAudioBoolean [1] = false;
			} else {
				cellAudioBoolean [0] = false;
			}
		}
		else
			cellState = "dead";
	}
	
	/**
	 * To return the currect state of the cell 
	 */
	public string getCellState()
	{
		return cellState;
	}

	/**
	 * Mark to refresh slime
	 */
	public void setCellRefresh(bool refresh)
	{
		cellRefresh = refresh;
		Debug.Log ("cellrefresh setup:" + refresh);
	}

	/**
	 * Return whether or not we need to refresh the cell
	 */
	public bool getCellRefresh()
	{
		return cellRefresh;
	}

	/*
    void OnMouseEnter()
    {
        //MD.setCellNum(CellNum);
        Debug.Log("mouse enter:"+CellNum);

    }
    */

	void OnTriggerEnter2D(Collider2D other){
		
		if (other.name == "Bucket") {
			MDC.setCellNum (CellNum);
			Debug.Log (other.name + "enter:" + CellNum);
		}


		//Debug.Log (other.name + "enter:" + CellNum);

	}

}