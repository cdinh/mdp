using UnityEngine;
using System.Collections;

public class LargeIntstBacteria : MonoBehaviour {

	public LargeIntestGameManager lgm;
	//public int WaterSubValue = 10;
	public bool FillFlag;
	
	// Use this for initialization
	void Start () {
		lgm = FindObjectOfType (typeof(LargeIntestGameManager)) as LargeIntestGameManager;
		FillFlag = false;
	}
	
	// Update is called once per frame
	
	/*
	void Update () {
	
	}
	*/
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("Bacteria Entered");
		lgm.fillwater();	
		
	}


}
