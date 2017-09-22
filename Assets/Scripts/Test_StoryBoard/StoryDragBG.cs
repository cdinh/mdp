using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryDragBG : MonoBehaviour {

	public Sprite transPage;
	public Sprite BG;
	public int pageNum;
	public bool dragable;
	public float movingSpeed;
	public bool directionX;
	public bool directionY;
	public bool directionYUp;
	public float constantScale;


	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 originalP;//= new Vector3(0f, 0f, 0f);

	private Image image;
	private BackGroundTurner bgt;

	private float startY, startX;
	private bool pageSet;
	private float tempx, tempy;



	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		bgt = FindObjectOfType (typeof(BackGroundTurner)) as BackGroundTurner;
		originalP = transform.position;
		pageSet = false;


	}
	
	// Update is called once per frame
	void Update () {
		if (!pageSet) {

			bgt.setLongPageStart (pageNum);
			pageSet = true;
		}
		if (bgt.currentPage () == pageNum) {
			image.sprite = BG;
			tempx = directionX ? (transform.position.x - Time.deltaTime * (Screen.width/movingSpeed)) : originalP.x;
			tempy = directionY ? (transform.position.y - Time.deltaTime * movingSpeed) : originalP.y;
			tempy = directionYUp ? (transform.position.y + Time.deltaTime * movingSpeed) : originalP.y;

			if(directionX && tempx< -2048f * constantScale) {
				tempx = -2048f * constantScale;
				bgt.setLongPageFinish (pageNum);
			}

			if(directionY && tempy< -1536f * constantScale) {
				tempy = -1536f * constantScale;
				bgt.setLongPageFinish (pageNum);
			}

			if(directionYUp && tempy> 1536f * constantScale) {
				tempy = 1536f * constantScale;
				bgt.setLongPageFinish (pageNum);
			}

			transform.position = new Vector3(	
				tempx, 
				tempy,
				originalP.z);

		} else {
			image.sprite = transPage;

		}
			
	}

	void OnMouseDown(){
		Debug.Log("on mouse down");
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		//isdrag = false;
		//image.sprite = BucketList[1];
		startX = Input.mousePosition.x;
		startY = Input.mousePosition.y;
		Debug.Log (offset);
	}

	void OnMouseDrag(){
		Debug.Log ("on mouse drag");
		//Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		float inputX = Input.mousePosition.x;
		float inputY = Input.mousePosition.y;

		if (directionX) {
			//inputX = (inputX <= startX) ? inputX : startX;
			if (inputX <= startX) {
				inputX = inputX;
			} else {
				inputX = startX;
			}
		}

		if (directionY) {
			//inputX = (inputX <= startX) ? inputX : startX;
			if (inputY <= startY) {
				inputY = inputY;
			} else {
				inputY = startY;
			}
		}

		if (directionYUp) {
			//inputX = (inputX <= startX) ? inputX : startX;
			if (inputY >= startY) {
				inputY = inputY;
			} else {
				inputY = startY;
			}
		}

		Vector3 cursorPoint = new Vector3(inputX, inputY, screenPoint.z);

		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		float newX = directionX ? cursorPosition.x : originalP.x;
		float newY = (directionY||directionYUp) ? cursorPosition.y : originalP.y;

		//newX = (newX > -2048f * constantScale) ? newX : -2048f * constantScale;
		/*
		if (newX < -2048f * constantScale) {
			newX = -2048f * constantScale;
			bgt.setLongPageFinish (pageNum);
		}
		*/

		//newY = (newY > -1536f * constantScale) ? newY : -1536f * constantScale;


		transform.position = new Vector3 (newX, newY, originalP.z);


		//image.sprite = BucketList[1];
		//isdrag = true;
		//Debug.Log (cursorPosition);

		startX = inputX;

		//Debug.Log(transform.position);

	}

	void OnMouseUp()
	{
		Debug.Log("on mouse up");
		//Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		//Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

		//isdrag = true;

		//transform.position = originalP;


		//CB.checkMouseClick(CellNum);

		//image.sprite = BucketList[0];
		Debug.Log(transform.position);
	}
}
