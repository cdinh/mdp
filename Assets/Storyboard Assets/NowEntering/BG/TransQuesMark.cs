using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransQuesMark : MonoBehaviour {

	public Sprite[] imageList;
	private Image i;

	private bool animationStart;
	private float offset = 0.0752245f;


	// Use this for initialization
	void Start () {
		i = GetComponent<Image> ();
		i.sprite = imageList [0];
		animationStart = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		//For Debug use
		//i.sprite = imageList[1];
	
	}

	public void setStart(int n){
		animationStart = true;
		if(n == 1)
			transform.position = new Vector3(-247f*offset, 532f*offset,90f);
		else if(n == 2)
			transform.position = new Vector3(-747f*offset, 194f*offset,90f);
		else if(n == 3)
			transform.position = new Vector3(-241.3f*offset, 24f*offset,90f);


	}

	public void setFrame(int n){
		i.sprite = imageList [n];
	}

}
