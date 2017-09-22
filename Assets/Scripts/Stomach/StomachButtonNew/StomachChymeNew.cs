using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StomachChymeNew : MonoBehaviour {

    public float ACIDIC;                    //!< to hold the number to define an acidic environment
    public float BASIC;                     //!< to hold the number to define a basic environment


    private int x = 0;
    private int counter = 0;
    public int ImSpeed;

    public Sprite[] AcidChymeImage;
    public Sprite[] BasicChymeImage;
    public Sprite[] NeutralChymeImage;


    
    public RectTransform acidHeight;        //!< reference to the rect transform drawing the acid level bar

    private StomachGameManager gm;          //!< hold a reference to the stomach game manager
    private Image i;                        //!< to hold a reference to the image

    //public Sprite neutralChyme;             //!< holds the texture for the chyme when stomach is "neutral"
    //public Sprite acidicChyme;              //!< holds the texture for the chyme when stomach is "acidic"
    //public Sprite basicChyme;               //!< holds the texture for the chyme when stomach is "basic"

    private float acidityLevel;             //!< to hold the acidity level value


    

    // Use this for initialization
    void Start ()
    {
        i = GetComponent<Image>();
        gm = FindObjectOfType(typeof(StomachGameManager)) as StomachGameManager;

    }
	
	// Update is called once per frame
	void Update () {

        //acidityLevel = acidHeight.anchoredPosition.y;

        if (gm.getCurrentAcidLevel()=="acidic")
        {
            i.sprite = AcidChymeImage[x];
            //gm.setCurrentAcidLevel("acidic");
            counter++;
        }
        else if (gm.getCurrentAcidLevel() == "neutral")
        {
            i.sprite = NeutralChymeImage[x];
            //gm.setCurrentAcidLevel("neutral");
            counter++;
        }
        else 
        {
            i.sprite = BasicChymeImage[x];
            //gm.setCurrentAcidLevel("basic");
            counter++;
        }

        if (counter > ImSpeed)
        {
            counter = 0;
            x++;
            if (x > 9)
            {
                x = 0;
            }
        }


    }
}
