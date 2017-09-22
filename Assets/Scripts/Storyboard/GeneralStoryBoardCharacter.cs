using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GeneralStoryBoardCharacter : MonoBehaviour {

	private Image image;
	private Sprite[] CharacterImage;
	private bool Setted;

	/*
	public GeneralStoryBoardCharacter(Sprite[] tempImage){
		CharacterImage = tempImage;

	}
	*/

	// Use this for initialization
	void Start () {
		Setted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Setted) image.sprite = CharacterImage[0];
	
	}

	public void setImage(Sprite[] tempImage){
		CharacterImage = tempImage;
		Setted = true;
	}
}
