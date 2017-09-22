using UnityEngine;
using System.Collections;

public class MeterPointer : MonoBehaviour {

	private Vector2 currentPosition;
	private int Watervalue;
	private LargeIntestGameManager lgm;

	// Use this for initialization
	void Start () {
		lgm = FindObjectOfType (typeof(LargeIntestGameManager)) as LargeIntestGameManager;
		currentPosition = transform.position;
		//transform.position = new Vector2 (currentPosition.x - 10f, currentPosition.y);


	}
	
	// Update is called once per frame
	void Update () {
		Watervalue = lgm.getWaterValue () * 4 / 100 * 20;
		transform.position = new Vector2 (currentPosition.x + 40f - Watervalue, currentPosition.y);

	}
}
