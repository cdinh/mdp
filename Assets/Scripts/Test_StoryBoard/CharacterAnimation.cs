using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {

	public Sprite[] animFrames;


	private Image image;
	private BackGroundTurner bgt;
	private bool charOn = false;
	private BoxCollider2D charBox;

	private int charTracker;



	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		charBox = GetComponent<BoxCollider2D> ();
		bgt = FindObjectOfType (typeof(BackGroundTurner)) as BackGroundTurner;


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log("on mouse down");
		Debug.Log ("Current Page:" + bgt.currentPage ());

	}

	void OnMouseUp(){
		Debug.Log ("on mouse up");
		if (charTracker == 0)
			charTracker = 1;
		else
			charTracker = 0;

	}

}
