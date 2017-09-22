using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LagreInstSub : MonoBehaviour {

	public LargeIntestGameManager lgm;
	public int WaterSubValue = 10;
	public bool subFlag;

    public Sprite[] SuctionImage;

    private Image i;
    private int suctionstatus;      //0=open, 1=close;

	// Use this for initialization
	void Start () {
		lgm = FindObjectOfType (typeof(LargeIntestGameManager)) as LargeIntestGameManager;
        i = GetComponent<Image>();
        suctionstatus = 0;
		subFlag = false;
	}
	
	// Update is called once per frame

	
	void Update () {
        suctionstatus = lgm.getBacTouched();
        i.sprite = SuctionImage[suctionstatus];
    }
	

	void OnTriggerEnter2D(Collider2D other){
        if (suctionstatus == 0)
		{
			Debug.Log ("Item Entered.");
			Debug.Log ("Item Name:" + other.gameObject.name);
            lgm.subwater(WaterSubValue);
        }

	}

    void setsuction()
    {
        if (suctionstatus == 0)
        {
            suctionstatus = 1;
        }
        else suctionstatus = 0;

    }



}
