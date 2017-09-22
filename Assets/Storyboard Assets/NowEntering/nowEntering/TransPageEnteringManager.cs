using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransPageEnteringManager : MonoBehaviour {

	public Sprite[] Entering;
	public float frameTime;

	private Image i;
	private float timer;
	private int currentFrame;

	// Use this for initialization
	void Start () {
		i = GetComponent<Image> ();
		timer = 0;
		currentFrame = 0;
	}
	
	// Update is called once per frame
	void Update () {
		i.sprite = Entering [currentFrame];
		timer = timer + Time.deltaTime;

		if (timer >= frameTime) {
			timer = 0;
			currentFrame++;
			if (currentFrame >= Entering.Length)
				currentFrame = 0;

		}


	
	}
}
