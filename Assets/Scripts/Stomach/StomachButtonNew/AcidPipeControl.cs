using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AcidPipeControl : MonoBehaviour
{

    private bool isClicked = false;
    private Image i;
    public Sprite[] ButtonStateImages;

    private PhBar PhB;
    private AcidFlowControl AFC;

	//timer
	private float startTime;
	private float timeElapsed;
	private float maxInterval = 5f;

	// popup variables for finger on acid button
	private bool popup;
	private float initialTime;
	private float elapsed;
	private int clickCount;
	public Texture tapPopUp;


	private StomachGameManager sgm;


    public void ButtonToggle()
    {
        AFC.ButtonToggle();
        if (isClicked)
        {
            isClicked = false;
            //swap texture to OFF
        }
        else
        {
            isClicked = true;
            //swap texture to ON
            PhB.addAcid();
			startTime = Time.time;
        }
    }

    public bool getClick()
    {
        return isClicked;
    }

    // Use this for initialization
    void Start()
    {
        AFC = FindObjectOfType(typeof(AcidFlowControl)) as AcidFlowControl;
        PhB = FindObjectOfType(typeof(PhBar)) as PhBar;
		sgm = FindObjectOfType (typeof(StomachGameManager)) as StomachGameManager;
        i = GetComponent<Image>();
		popup = false;
		initialTime = Time.time;
		clickCount = 0;
	


    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log (sgm.gettotalfood ());
		elapsed = Time.time - initialTime;
		if (elapsed > 8 && sgm.getCurrentAcidLevel() != "acidic" && clickCount == 0) {
			popup = true;
		} else {
			popup = false;
		}

		if (isClicked == false)
        {
            i.sprite = ButtonStateImages[0];
            return;
        }
        if (isClicked == true)
        {
			if(sgm.gettotalfood()>0 && sgm.getCurrentAcidLevel() == "acidic")	clickCount++;
			i.sprite = ButtonStateImages[1];
			timeElapsed = Time.time - startTime;
			if (timeElapsed > maxInterval) isClicked = false;
            return;
        }
		
    }

	void OnGUI()
	{
		if(popup)
		{
			GUI.DrawTexture (new Rect(Screen.width * 0.09f, 
				Screen.height * 0.30515625f, 
				Screen.width * 0.2093359375f, 
				Screen.height * 0.300697917f),tapPopUp);
		}
	}
}
