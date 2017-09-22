using UnityEngine;
using System.Collections;

/**
 * script that handles moving the food down the "track"
 */
public class MoveFood : MonoBehaviour 
{
	private GameObject flaps;			//!< to hold a reference to the flaps in the game
	private openFlap flap;				//!< to hold a reference to the openFlap script attached to the flaps

	public float pathPosition, reversePosition;	//!< for ITween path position and the reverse position
	public float foodSpeed;						//!< for the speed the food moves along the path
	public float coughSpeed;					//!< for the speed the food moves in reverse down the path (for cough)

	private SmoothQuaternion quaternion;
	public BoxCollider b;

	EsophagusDebugConfig debugConfig;	//!< to hold a reference to the mouth debug config

	/**
	 * Use this for initialization
	 * Finds references and initializes values.
	 */
	void Start () 
	{
		GameObject flaps = GameObject.Find ("Flaps");									// find the reference to the flaps
		flap = flaps.GetComponent<openFlap>();											// find the script on the flaps

		quaternion = transform.rotation;
		quaternion.Duration = .5f;

		debugConfig = GameObject.Find("Debugger").GetComponent<EsophagusDebugConfig>();	// find the reference to the debugger
		b = GameObject.Find ("flap1").GetComponent<BoxCollider> ();
	}
	
	/**
	 * Update is called once per frame
	 * Checks for updated values from the debugger. Checks for coughs and coordinates food movement with cough actions.
	 */
	void Update () 
	{
		//Debug.Log ("Food Time scale:" + Time.timeScale + ", x:" + transform.position.x + ", y:" + transform.position.y);

		// check if we are using the debugger for values, and if we are the food speed is taken from the debugger
		// rather than from the script
		if(debugConfig != null && debugConfig.debugActive)
		{
			foodSpeed = debugConfig.foodSpeed;
		}

		Quaternion q = transform.rotation;
		if(flap.isCough())						// if a cough is currently occuring
		{
			b.enabled = false;
			// reverse the path and set the "corrected" forward path position based on this reversed path
			transform.position = Spline.MoveOnPath(iTweenPath.GetPathReversed("Path"), transform.position, ref reversePosition, coughSpeed,100,EasingType.Linear,false,false);
			pathPosition = 1f - reversePosition;

			if(reversePosition > .99f)			// check if food near the mouth opening "fell out" of the mouth
												// if it did then we destroy this food and don't allow it to come back
			{
				// track stats
				PlayerPrefs.SetInt("MouthStats_foodLost", PlayerPrefs.GetInt("MouthStats_foodLost") + 1);
				PlayerPrefs.Save ();			// needs to be called to write playerprefs data to disk

				Destroy (gameObject);			// destroy the food that fell out
			}
		}
		else 									// if a cough is not occuring
		{
			if(!flap.isEpiglotisOpen())
			{
				b.enabled = true;
			}
			// move the food down the normal path and set the "corrected" reverse path position based  on this 
			transform.position = Spline.MoveOnPath(iTweenPath.GetPath("Path"), transform.position, ref pathPosition, ref q, foodSpeed,100,EasingType.Linear,false,false);
			reversePosition = 1f - pathPosition;
		}
		quaternion.Value = q;
		transform.rotation = quaternion;
	}
}
