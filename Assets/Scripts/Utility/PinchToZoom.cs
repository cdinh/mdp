using UnityEngine;
using System.Collections;

// script found online
// allows zooming in using a pinching mechanism on touch
// simulated by key pressed on the pc/mac for debugging
public class PinchToZoom : MonoBehaviour 
{
	public int speed = 1;
	public Camera selectedCamera;
	public float minPinchSpeed = 5.0f;
	public float varianceInDistances = 5.0f;
	private float touchDelta = 0.0f;
	private Vector2 prevDist = new Vector2(0, 0);
	private Vector2 curDist = new Vector2(0, 0);
	private int maxFOV = 60;
	private int minFOV = 1;
	private float maxMove = 8000f;
	
	// Use this for initialization
	void Start () 
	{
		// set the camera to orthographic mode, so that we can use the fieldOfView 
		// property to create the zooming effect.
		// the initial fieldOfView setting should be chosen to show the level
		// "fully zoomed out", and can vary with the level design.
		selectedCamera.GetComponent<Camera>().orthographic = false;
		selectedCamera.GetComponent<Camera>().fieldOfView = maxFOV;
	}
	
	// Update is called once per frame
	void Update () 
	{
	//	transform.Rotate(0, 5*Time.deltaTime, 0);
		
		// keyboard commands to simulate zooming in and out, for testing purposes.
		// this allows us to zoom in and out without having to deploy on the device & use multitouch.
		// each press of the "I" or "O" key will zoom in / out one setp at a time.
		if (Input.GetKeyDown(KeyCode.I)) 
		{
			selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView - 
			                                         (1 + selectedCamera.fieldOfView * speed * 0.01f), minFOV, maxFOV);	
		}
		
		if (Input.GetKeyDown(KeyCode.O)) 
		{
			selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView + 
			                                         (1 + selectedCamera.fieldOfView * speed * 0.01f), minFOV, maxFOV);	
		}
		
		// check for 2 finger touch, and that both fingers have reached the "moved" touch phase
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && 
		    Input.GetTouch(1).phase == TouchPhase.Moved) 
		{
			curDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
			// use the delta position property to determine the difference in previous finger locations
			prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition));
			touchDelta = curDist.magnitude - prevDist.magnitude;

			selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView + 
			                                         (-touchDelta * (maxFOV / maxMove) * selectedCamera.fieldOfView), 
			                                         minFOV, maxFOV);
		}	
		
		// if we reach the minimum zoom, we start the game
		if (selectedCamera.fieldOfView <= minFOV)
		{
			Application.LoadLevel("loading");
		}
	}
}
