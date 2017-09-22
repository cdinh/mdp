using UnityEngine;
using System.Collections;

/**
 * manager to handle and coordinate glow behavior for the si game
 */
public class GlowManager : MonoBehaviour 
{
	private IntestineGameManager intestineGameManager;	// to hold a reference to the intestineGameManager
	private GlowSegment glowScript;						//!< to hold a reference to a glow script we are currently working with
	private EffectParticle effectParticle;				//!< to hold a reference to a particle effect particle to modify it's actions

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// get a reference to the intestine game manager currently being used
		intestineGameManager = GameObject.Find ("Managers").GetComponent<IntestineGameManager>();
	}
	
	/**
	 * Update is called once per frame
	 * Checks for a touch or mouseclick and finds the closest glow segment
	 */
	void Update () 
	{
		// for on pc/mac
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		if (Input.GetButtonDown("Fire1")) 
		{
			checkClickArea();
		}
#endif

		// for ipad
		foreach (Touch touch in Input.touches) 
		{
			if (touch.phase == TouchPhase.Began)
			{
				checkClickArea();
			}
		}
	}

	/**
	 * function that handles analyzing where the user clicked/touched to activate the glow segment that is closest
	 * to the touch under appropriate conditions
	 */
	void checkClickArea()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);	// create a ray at the current location
		RaycastHit hit;													// to store if there is something the ray hits
		GameObject closestSegment = FindClosestSegment(ray);
		Debug.Log (Input.mousePosition.x + ", " + Input.mousePosition.y);

		Debug.Log ("segment found: " + closestSegment.transform.position.x + ", "+ closestSegment.transform.position.z);
		
		// ignore if the bottom menu is clicked on
		if (ray.origin.z < -11.6)
		{
			return;
		}
		
		// check for menu button click
		if (ray.origin.x > 19.7 && ray.origin.z > 16)
		{
			return;
		}
		
		// check for 2x speed click
		if (ray.origin.x < -22 && ray.origin.z < -8.8)
		{
			return;
		}
		
		// if menu box is up don't light but don't do this in tutorial so they can click when hand is up
		if (Time.timeScale < .1f)
		{
			// if we are in the tutorial under the correct conditions make a note for us to use
			if ((Application.loadedLevelName.Equals("SmallIntestineTutorial") &&
				(PlayerPrefs.GetInt("SIStats_towersPlaced") == 2)) ) //&&(PlayerPrefs.GetInt("SIStats_towersUpgraded") == 2) &&!(PlayerPrefs.GetInt("SISpeedTutorial") == 1)))
			{
				
				if (findNutrients(closestSegment.transform.position, closestSegment.transform.localScale.x)) {
					PlayerPrefs.SetInt ("SIGlowTutorial", 1);
					PlayerPrefs.Save ();
				} else {
					Debug.Log ("No nutrients found");
					return;

				}
			} else
			{
				return;
			}
		}
			    
			    // if tower menu or sell box is up don't light
		if (intestineGameManager.getSellBoxUp() || intestineGameManager.getTowerMenuUp())
		{
			return;
		}
		
		// ignore if a tower is directly clicked on
		if (Physics.Raycast(ray, out hit, 20))
		{
			if (hit.transform.gameObject.name.Contains("Tower"))
			{
				return;
			}
		}
		
		//GameObject closestSegment = FindClosestSegment(ray);					// find the closest segment to the ray that was cast
		glowScript = closestSegment.GetComponent<GlowSegment> ();	// get the script from the closest segment

		// make the segment glow
		StartCoroutine(glowScript.onTouch());			// asynchronously make the segment glow to save time

		// check for any nearby nutrients
		// need to pass different values in because the plane is oriented in different ways in the tutorial and regular
		// level
		if (!Application.loadedLevelName.Contains("Tutorial"))
		{
			// if the level isn't the tutorial pass these values into the absorbtion function
			absorbNutrients (closestSegment.transform.position, closestSegment.transform.localScale.z);
		} else
		{
			// if the level is the tutorial pass in these values to the absorbtion function 
			absorbNutrients (closestSegment.transform.position, closestSegment.transform.localScale.x);	
		}
	}

	/**
	 * function to find the closest segment to the ray
	 */
	private GameObject FindClosestSegment(Ray ray) 
	{
		GameObject[] segments = new GameObject[4];		// array to store all candidates for closest segment
		GameObject segmentUp = null;					// to store a reference to the closest segment above the touch
		GameObject segmentDown = null; 					// to store a reference to the closest segment below the touch
		GameObject segmentLeft = null; 					// to store a reference to the closest segment left of the touch
		GameObject segmentRight = null;					// to store a reference to the closest segment right of the touch
		GameObject closest = null;						// to store the final decision of which segment is the closest

		float distance = Mathf.Infinity;				// initially set the distance to infinitely far away
		RaycastHit hit;									// to store a raycast hit

		// reset the y position to be at the desired height (same height as the walls to detect the touches)
		ray.origin = new Vector3 (ray.origin.x, 4.51f, ray.origin.z);

		// look for the closest segment above the click
		ray.direction = new Vector3 (0, 0, 1);
		if (Physics.Raycast(ray, out hit, 20, 1 << LayerMask.NameToLayer("Glow")))
		{
			segmentUp = hit.transform.gameObject;
		}

		// look for the closest segment below the click
		ray.direction = new Vector3 (0, 0, -1);
		if (Physics.Raycast(ray, out hit, 20, 1 << LayerMask.NameToLayer("Glow")))
		{
			segmentDown = hit.transform.gameObject;
		}

		// look for the closest segment to the left of the click
		ray.direction = new Vector3 (-1, 0, 0);
		if (Physics.Raycast(ray, out hit, 20, 1 << LayerMask.NameToLayer("Glow")))
		{
			segmentLeft = hit.transform.gameObject;
		}

		// look for the closest segment to the right of the click
		ray.direction = new Vector3 (1, 0, 0);
		if (Physics.Raycast(ray, out hit, 20, 1 << LayerMask.NameToLayer("Glow")))
		{
			segmentRight = hit.transform.gameObject;
		}

		// store the found segments into the arrays
		segments [0] = segmentUp;
		segments [1] = segmentDown;
		segments [2] = segmentLeft;
		segments [3] = segmentRight;

		// iterate over the 4 segments to find the closest one
		foreach (GameObject seg in segments) 
		{
			if (seg == null)		// the segment might be null, for example, if there was no possible segment to the right
									// of the click. so if this case happens just skip looking over this segment
			{
				continue;
			}

			// find the distance between the touch and the segment
			Vector3 diff = seg.transform.position - ray.origin;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) 		// check if this distance is closer than the current closest distance
			{
				closest = seg;					// if it is store the reference to this segment for later
				distance = curDistance;			// replace the closest distance value with this new value
			}
		}

		return closest;		// finally at the end, return which segment is stored in closest
	}

	/**
	 * function that handles absorbing the nutrients (aka the effect particles) in a certain range of the glow segment
	 */
	private void absorbNutrients(Vector3 center, float radius)
	{
		// set a minimum radius to make sure it's large enough to absorb some nutrients
		if (radius < 3.5f)
		{
			radius = 3.5f;
		}
		//move the center to the same height as the particles we want to check for collisions with
		center = new Vector3 (center.x, .5f, center.z);

		// gather all collisions.  use radius * .75 because the collider seems larger than the value you assign it
		// this value was chosen by trial and error
		UnityEngine.Collider[] nutrientHits = Physics.OverlapSphere (center, radius*.75f,
		                                                             1 << LayerMask.NameToLayer("Ignore Raycast"));

		// iterate over all nutrients found by the collider to handle absorbing them
		for (int i = 0; i < nutrientHits.Length; i++)
		{
			if (nutrientHits[i].gameObject.name.Equals("EffectParticle(Clone)"))		// first make sure the hit particle
																						// if of the correct type (effect 
																						// particles only)
			{
				effectParticle = nutrientHits[i].GetComponent<EffectParticle>();
				if (!effectParticle.getMoveAndDie() && !effectParticle.getFinalMove())	// make sure the particle isn't already in the killing process
				{
					// start the absorbing of the particle asynchronously. this helps reduce performance impact
					StartCoroutine(effectParticle.killParticle(center));	
				}
			}
		}
	}

	private bool findNutrients(Vector3 center, float radius){
		// set a minimum radius to make sure it's large enough to absorb some nutrients
		if (radius < 3.5f)
		{
			radius = 3.5f;
		}
		//move the center to the same height as the particles we want to check for collisions with
		center = new Vector3 (center.x, .5f, center.z);

		// gather all collisions.  use radius * .75 because the collider seems larger than the value you assign it
		// this value was chosen by trial and error
		UnityEngine.Collider[] nutrientHits = Physics.OverlapSphere (center, radius*.75f, 1 << LayerMask.NameToLayer("Ignore Raycast"));
		if (nutrientHits.Length < 2) {
			return false;
		}
		else {
			Debug.Log ("Length: "+ nutrientHits.Length);
			return true;
		}
	}
}