using UnityEngine;
using System.Collections;

public class LargeIntestGameManager : MonoBehaviour {

	public GameObject PlayerPoo;

    private float timer;
	public  int WaterValue;

    private float bacteriaTimer;
    private int bacTouched;         //0 = not, 1 = touched

    public float CloseTimer;          //Time to Close suctions

    private poopmeter pm;
    private LI_WaterScript WS;

	public AudioClip ouch;
	public GameObject endGameScript;

	private int bacteriaCount;



    // Use this for initialization
    void Start () {
        pm = FindObjectOfType(typeof(poopmeter))as poopmeter;
        WS = FindObjectOfType(typeof(LI_WaterScript)) as LI_WaterScript;
		WaterValue = 100;
        bacTouched = 0;
        bacteriaTimer = 0;
        timer = 0;
		Time.timeScale = 1;
		Debug.Log ("TIme scale: " + Time.timeScale);
		bacteriaCount = 0;
		PlayerPrefs.SetInt ("LargeIntestineBacteriaTouched", bacteriaCount);

	}
	
	// Update is called once per frame
	void Update () {
        timer = timer + Time.deltaTime;
        //Debug.Log(timer);
        if(bacTouched == 1)
        {
            bacteriaTimer = bacteriaTimer + Time.deltaTime;
            //Debug.Log("Touched, time=");
            //Debug.Log(bacteriaTimer);

        }
        if(bacteriaTimer > CloseTimer)
        {
            bacteriaTimer = 0;
            bacTouched = 0;
        }
        //pm.setPosition(WaterValue);

        if (timer > 59f)
        { 
			timer = 0;
			Instantiate(endGameScript);	
        }

    }

	void SubPlayerWater(){

	}

	public void subwater(int a){
		
        int temp;
		temp = WaterValue - a;
        if (temp > 0)
            WaterValue = temp;
		//Debug.Log ("Water Substracted");
		
        WS.subStart();

        
	}
	
	public void addwater(int a){
		WaterValue = WaterValue + a;
	}
	
	public void fillwater(){
		WaterValue = 100;
        bacTouched = 1;
		bacteriaCount++;
		PlayerPrefs.SetInt ("LargeIntestineBacteriaTouched", bacteriaCount);
		GetComponent<AudioSource> ().clip = ouch;
		playclip ();
    }

	public int getWaterValue(){
		return WaterValue; 
	}

    public int getBacTouched()
    {
        return bacTouched;
    }

	private void playclip(){
		GetComponent<AudioSource>().Play();
		Debug.Log ("Sound Played");
	}




}
