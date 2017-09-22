using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manager for stomach food
 */
public class StomachFoodManager : MonoBehaviour 
{
	public GameObject stomachFoodBlob;				//!< to hold the gameobject for stomach food blobs
	
	// create a list to hold all the stomach food blobs currently in the game
	private List<StomachFoodBlob> spawnedFoodBlobs = new List<StomachFoodBlob>();
	
	public float timeBetweenFoodSpawns;				//!< for the time between food spawns
	private float elapsedTime;						//!< to count elapsed time to see if we should create a new food blob
	
	public int foodBlobsPerRound;
	public int slowFoodNum;

	private StomachFoodBlob nextToDigest = null;

	private StomachGameManager stomanager;
	private float actualTimeBetFood;
	private float[] timeMap;
	private int currentFood;


	void Start(){
		stomanager = FindObjectOfType (typeof(StomachGameManager)) as StomachGameManager;
		timeMap = new float[8]{2f, 2f, 30f, 2f, 2f, 2f, 40f, 2f};
		currentFood = 0;
	}

	/**
	 * Update is called once per frame
	 * 
	 */
	void Update () 
	{
		elapsedTime += Time.deltaTime;
		
		// keeps track of food blobs
		// when a food blob is digested it will need to be removed from the list of spawned blobs
		/*
		if(getNumFoodBlobs()<slowFoodNum) {
			actualTimeBetFood = 2f*timeBetweenFoodSpawns;
		}
		else {
			actualTimeBetFood = timeBetweenFoodSpawns;
		}
		*/

		//if (elapsedTime > actualTimeBetFood && spawnedFoodBlobs.Count <= foodBlobsPerRound && stomanager.gettotalfood() <= stomanager.MAX_FOOD_DROPED)
		if(stomanager.gettotalfood() < timeMap.Length)
		{
			if (elapsedTime > timeMap [currentFood]) {
				//for food phase Debug
				/*
				Debug.Log ("Food Number:" + stomanager.gettotalfood ());
				Debug.Log ("actualTimeBetFood: " + elapsedTime);
				Debug.Log ("Current Food Num: " + getNumFoodBlobs ());
				*/

				GameObject temp = (GameObject)Instantiate (stomachFoodBlob);
				temp.GetComponent<StomachFoodBlob> ().parent = temp;
				spawnedFoodBlobs.Add (temp.GetComponent<StomachFoodBlob> ());
				elapsedTime = 0f;
				currentFood++;
			}
		}
	}
	
	/**
	 * Returns the number of food blobs current in the list
	 */
	public int getNumFoodBlobs()
	{
		return spawnedFoodBlobs.Count;
	}

	/**
	 * Returns if there is any food blobs or not
	 */
	public bool noFoodBlobs()
	{
		return (spawnedFoodBlobs.Count == 0);
	}

	public StomachFoodBlob getOldestFoodBlob()
	{
		return spawnedFoodBlobs [0];
	}

	/*
	 * Returns the location of the oldest food blob
	 */
	public Vector2 locOldestFoodBolb()
	{
		Vector2 p;
		int i = 0;
		if (noFoodBlobs())
		{
			p = new Vector3(0f,0f);
		} 
		else 
		{
			p = spawnedFoodBlobs[0].transform.position;
		}
		
		return p;
	}

	public StomachFoodBlob getNextFoodBlobToDigest()
	{
		if (nextToDigest == null) {
			float maxY = System.Single.MinValue;
			StomachFoodBlob nextFood = null;
			for (int i = 0; i < spawnedFoodBlobs.Count; i++) {
				if (maxY < spawnedFoodBlobs [i].transform.position.y) {
					maxY = spawnedFoodBlobs [i].transform.position.y;
					nextFood = spawnedFoodBlobs [i];
				}
			}
			nextToDigest = nextFood;
		}

		return nextToDigest;
	}

	public void removeFood(StomachFoodBlob f)
	{
		stomanager.disolvedonefood();
		if (f == nextToDigest)
			nextToDigest = null;
		spawnedFoodBlobs.Remove (f);
        f.transform.SetParent(null);// = null;
		Object.Destroy (f);
	}
}