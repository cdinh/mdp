using UnityEngine;
using System.Collections;

public class StomachCameraRay : MonoBehaviour {

	//public Camera mainCamera;
//	private bool start;
//	public float tempC;
	private Vector3 origin;

	private Ray tempRay;
	private mouseDragChild mdc;

	// Use this for initialization
	void Start () {


//		start = true;
//		Debug.Log ("Camera Script Started");
//		tempC = 0;

		mdc = FindObjectOfType (typeof(mouseDragChild)) as mouseDragChild;


		origin = new Vector3(0f, 0f, -10f);

	
	}
	
	// Update is called once per frame
	void Update(){
		/*
		tempC = tempC + Time.deltaTime;
		if (tempC > 1) {
			Debug.Log ("Camera Script Here: " + tempC);
			tempC = 0;
		}

*/




		Debug.DrawRay (origin, tempRay.direction * 10000f);


		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hitRay;
			//Ray vRay = mainCamera.ScreenPointToRay(Input.mousePosition);
			Ray cRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			//origin = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f);

			//origin = Camera.main.ScreenToViewportPoint(Input.mousePosition);

			//origin.z = -10f;

			//Ray cRay = new Ray(origin, Vector3.forward);

			tempRay = cRay;
			

			//Debug.DrawRay (mainCamera.transform.position, vRay.direction * 10f);
			//Debug.DrawRay (Camera.main.transform.position, cRay.direction * 100f);

			//Debug.DrawRay (origin, cRay.direction * 100f);
			Debug.Log ("Mouse position: " + Input.mousePosition.x + " " + Input.mousePosition.y + " " + Input.mousePosition.z);


			if (Physics.Raycast (cRay, out hitRay, 10000f)) {
//				Debug.Log ("Ray hits" + hitRay.collider.name);
//				Debug.Log ("Name = " + hitRay.collider.name);
//				Debug.Log ("Tag = " + hitRay.collider.tag);
//				Debug.Log ("Hit Point = " + hitRay.point);
//				Debug.Log ("Object position = " + hitRay.collider.gameObject.transform.position);
//				Debug.Log ("--------------");


				if (hitRay.collider.name == "PailChild3D") {
					Debug.Log ("Gotcha!!");


				}

			}
		}





	}
}
