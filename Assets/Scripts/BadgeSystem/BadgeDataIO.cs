using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


public class BadgeDataIO : MonoBehaviour {

	public int badgeNums;
	private FileStream dataStream;
	private bool[] localBadgeList;
	private GameObject panelCanvas;
	//private GameObject panelList;
	public GameObject panelList;
	public List<GameObject> badgeList;

	private long dataLength; 
	private string tempStream;


	// Use this for initialization
	void Start () {
		badgeNums = 13;
		localBadgeList = new bool[13];
		panelCanvas = GameObject.Find ("Canvas");

		//panelList = GameObject.Find ("PanelList");
		Debug.Log ("panelList Length: " + panelList.transform.childCount);

		Debug.Log ("First Child: " + panelList.transform.GetChild(0).name);

		dataLength = 0;

//		foreach (Transform child in panelList) {
			
		
//		}






		//Save ();

		Debug.Log (" badge Length " + localBadgeList.Length + "before load");
		Load ();

		Debug.Log (" badge Length " + localBadgeList.Length + "after load");

		//Debug.Log (" badge Length " + localBadgeList.Length + "before load");
		//Load ();
		//Debug.Log (" badge Length " + localBadgeList.Length + "after load");


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Load(){
		if (!File.Exists (Application.persistentDataPath + "/badgeData.dat")) {
			/*
			 dataStream = File.Create (Application.persistentDataPath + "/badgeData.dat");
			Debug.Log ("File " + Application.persistentDataPath + "/badgeData.dat" + " created");

			badgeData data = new badgeData (badgeNums);
			*/

			Debug.Log ("File unfound. ");

			Save ();
		}

			

		dataStream = File.Open (Application.persistentDataPath + "/badgeData.dat", FileMode.Open);
		dataLength = dataStream.Length;

		if (dataLength == 0) {
			Debug.Log ("File is empty. ");
			badgeData data = new badgeData ();
			//data.badgeList = localBadgeList; 
			//data.setList (localBadgeList);
			for (int i = 0; i < badgeNums; i++) {
				data.badgeList [i] = localBadgeList [i];
			}

			Debug.Log ("badge Length " + data.badgeList.Length + "before save");

			BinaryFormatter bf = new BinaryFormatter ();
			bf.Serialize (dataStream, data);
			dataStream.Close ();
		}
		else{

			tempStream = Application.persistentDataPath + "/badgeData.dat";
			Debug.Log ("File " + Application.persistentDataPath + "/badgeData.dat" + " loaded");
			Debug.Log ("File " + Application.persistentDataPath + "/badgeData.dat" + "  length:" + dataLength);

			BinaryFormatter bf = new BinaryFormatter ();
			badgeData data = (badgeData)bf.Deserialize( dataStream);

			dataStream.Close();
			localBadgeList = data.badgeList;//localBadgeList = data.returnList ();
			
		}
	}

	void Save(){

		BinaryFormatter bf = new BinaryFormatter ();
		if (!File.Exists (Application.persistentDataPath + "/badgeData.dat")) {
			dataStream = File.Create (Application.persistentDataPath + "/badgeData.dat");
		} else {
			dataStream = File.Open (Application.persistentDataPath + "/badgeData.dat", FileMode.Open);
		}

		dataLength = dataStream.Length;
		tempStream = Application.persistentDataPath + "/badgeData.dat";

		Debug.Log ("File " + Application.persistentDataPath + "/badgeData.dat" + " created");


		badgeData data = new badgeData ();
		//data.badgeList = localBadgeList; 
		//data.setList (localBadgeList);
		for (int i = 0; i < badgeNums; i++) {
			data.badgeList [i] = localBadgeList [i];
		}



		Debug.Log ("badge Length " + data.badgeList.Length + "before save");

		bf.Serialize (dataStream, data);
		dataStream.Close ();

	}

	public void setBadgeByNum(int num){
		if (num >= localBadgeList.Length) {
			Debug.Log (num + "excess badge Length " + localBadgeList.Length + ", set failed.");
			return;
		}
		localBadgeList [num] = localBadgeList[num] == false ? true : false;

		Save ();

	}

	public void popBadgeToTop(GameObject badge){

		badge.transform.SetSiblingIndex (0);

	}

	public bool returnBadgeStatus(int num){
		if (num >= localBadgeList.Length) {
			Debug.Log (num + "excess badge Length " + localBadgeList.Length + ", set failed.");
			return false;
		}
		return localBadgeList [num];
	}



	[Serializable]
	class badgeData{
		public bool[] badgeList = new bool[13];

		/*
		public badgeData(){
			badgeList = new bool[13];
		}


		public bool[] returnList(){
			return badgeList;
		}
		public void setList(bool[] list){
			for (int i = 0; i < list.Length; i++) {
				badgeList [i] = list [i];
			}
		}
		*/

	}

	void OnGUI(){
		GUI.Label(new Rect((10f/1024f)*Screen.width, (10f/768f)*Screen.height, (700f/1024f)*Screen.width,
			(100f/768f)*Screen.height), "Length of the data file: " + dataLength);
		GUI.Label(new Rect((10f/1024f)*Screen.width, (300f/768f)*Screen.height, (700f/1024f)*Screen.width,
			(600f/768f)*Screen.height), "Length of the data file: " + tempStream);
		
	}


}
