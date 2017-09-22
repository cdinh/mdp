using UnityEngine;
using System.Collections;

/**
 * script that handles behavior for the effect particles in the small intestine game
 */
public class EffectParticle : MonoBehaviour 
{
	private IntestineGameManager intestineGameManager;	//!< to hold a reference to the intestineGameManager

	private Vector3 desiredLocation;					//!< to store the desired location to move an effect particle to
	private Vector3 direction;							//!< to store the direction to move the effect particle

	private Vector3 startingPosition;					//!< stores the coordinate of the starting position of a particle before final move
	private float percentageMoved;						//!< used to help make the particle appear to have a constant speed

	private bool move = true;							//!< variable to move the particle on creation
														//!< the value starts off as move because when an effect particle is
														//!< created we want it to move to the desired location immediately.
														//!< once it gets to the desired location "move" will be set to false

	private bool moveAndDie = false;					//!< variable to move the particle in the absorbtion phase
	private bool finalMove = false;						//!< for the final move and absorbtion into the nutrients text

	private float speed;								//!< speed multiplier

	private float distance;								//!< to store the total distance between an effect particle's 
														//!< starting location and where its desired location is

	private float distanceTravelled;					//!< to store the distance travelled by the particle

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		// get a reference to the intestine game manager currently being used
		intestineGameManager = GameObject.Find ("Managers").GetComponent<IntestineGameManager>();
		speed = Random.Range (.7f, 1f);
	}
	
	/**
	 * Update is called once per frame
	 * Handles all the different movements for the particle
	 */
	void Update () 
	{
		if (move)	// what to do when moving on initial spawn
		{
			transform.localPosition += direction * Time.deltaTime * speed;	// move the particle in the desired direction
			direction = this.desiredLocation - gameObject.transform.localPosition;	// calculate the direction to move

			if (Mathf.Abs(transform.localPosition.x - desiredLocation.x) < .05f &&
			    Mathf.Abs(transform.localPosition.z - desiredLocation.z) < .05f)			// check if the distance travelled is the desired distance
			{
				move = false;				// if it has travelled the specified distance, stop moving it
			}
		}

		if (moveAndDie)	// what to do if moving on death
		{
			transform.position += direction * Time.deltaTime * 2f;	// move the particle in the desired direction
			direction = this.desiredLocation - gameObject.transform.localPosition;	// calculate the direction to move

			if (Mathf.Abs(transform.position.x - desiredLocation.x) < .07f &&
			    Mathf.Abs(transform.position.z - desiredLocation.z) < .07f)		// check if the distance travelled is the desired distance
			{
				moveAndDie = false;										// unflag the moveanddie so we don't go in that code again
				finalMove = true;										// throw the flag that we're ready for the next block of code

				//v1	transform.position = new Vector3(-26f, 0, -13 + Random.Range(-.3f, .4f)); // v1
				//v2	transform.position = new Vector3(-15f, 0, -13 + Random.Range(-.3f, .4f));		// v2
				
				startingPosition = transform.position;		// store the starting location before we begin the final move
				percentageMoved = 0f;

				desiredLocation = new Vector3(-7f, 0, -13f); //v3
				//v1,v2 desiredLocation = new Vector3(-7f, 0, transform.position.z); //v1, v2
				
				direction = this.desiredLocation - transform.position;	// calculate the direction to move


			/*	moveAndDie = false;										// unflag the moveanddie so we don't go in that code again
				finalMove = true;										// throw the flag that we're ready for the next block of code
			//V1	transform.position = new Vector3(-26f, 0, -13 + Random.Range(-.3f, .4f));
			//V2	transform.position = new Vector3(-15f, 0, -13 + Random.Range(-.3f, .4f));
					desiredLocation = new Vector3(-7f, 0, transform.position.z);
				desiredLocation = new Vector3(-7f, 0, -13f); //v3
				direction = this.desiredLocation - gameObject.transform.position;	// calculate the direction to move
				*/
			}
		}

		if (finalMove)
		{
			//v1 percentageMoved =  Mathf.Clamp(percentageMoved + Time.deltaTime * .6f, 0f, 1f); //v1
			//v2 percentageMoved =  Mathf.Clamp(percentageMoved + Time.deltaTime * 1f, 0f, 1f); //v2
			percentageMoved =  Mathf.Clamp(percentageMoved + Time.deltaTime * .8f, 0f, 1f); //v3
			transform.position = startingPosition + percentageMoved * direction;	// move the particle in the desired direction
			if (percentageMoved >= 1.0f)			// check if the distance travelled is the desired distance
			{
				finalMove = false;							// if it has travelled the specified distance, stop moving it
				intestineGameManager.OnNutrientHit();		// add the score to the nutrients
				GetComponent<Renderer>().enabled = false;					// make the particle invisible incase there is a delay on garbage collection
				Destroy(this.gameObject);					// destroy the particle
			}
		}

	/*	if (finalMove)
		{
			transform.position += direction * Time.deltaTime * 1f;	// move the particle in the desired direction
			direction = this.desiredLocation - gameObject.transform.localPosition;	// calculate the direction to move

			if (Mathf.Abs(transform.position.x - desiredLocation.x) < .07f &&
			    Mathf.Abs(transform.position.z - desiredLocation.z) < .07f)			// check if the distance travelled is the desired distance
			{
				finalMove = false;							// if it has travelled the specified distance, stop moving it
				intestineGameManager.OnNutrientHit();		// add the score to the nutrients
				renderer.enabled = false;					// make the particle invisible incase there is a delay on garbage collection
				Destroy(this.gameObject);					// destroy the particle
			}
		}*/
	}

	/**
	 * this is for moving the particles the first time, when they are spawned
	 * sets the variables appropriatly
	 */
	public void setDesiredLocation(Vector3 desiredLocation)
	{
		this.desiredLocation = desiredLocation;		// set the desiredLocation to the value passed in

		direction = this.desiredLocation - gameObject.transform.localPosition;	// calculate the direction to move

		// find the distance between the particle's location and the desired location
		distance = Vector3.SqrMagnitude(this.desiredLocation - gameObject.transform.localPosition); 
	}

	/**
	 * this is for movingg the effect particles on absorbtion
	 * sets the variables appropriately
	 */
	public IEnumerator killParticle(Vector3 desiredLocation)
	{
		if (!moveAndDie)	// only let the particles die once to avoid bouncing around
		{
			move = false;
			if (transform.parent != null)
			{
				Transform parent = transform.parent;	// find a reference to the particle's parent
				transform.parent = null;				// remove the particle from the parent
				parent.GetComponent<FoodBlobEffectParticles>().decreaseAliveChildren();	// to determine when to destroy parent
			}
		
			this.desiredLocation = desiredLocation;	// set the desiredLocation to the value passed in

			direction = this.desiredLocation - gameObject.transform.position;	// calculate the direction to move

			// find the distance between the particle's location and the desired location
			distance = Vector3.SqrMagnitude(this.desiredLocation - gameObject.transform.position);
			yield return distance;	// wait until the above calculation is done to save time

			moveAndDie = true;		// set the moveanddie variable to true to indicate we are ready to kill the particle
		}
	}

	/**
	 * function that can be used to see if we are currently trying to kill the given effect particle
	 */
	public bool getMoveAndDie()
	{
		return moveAndDie;
	}

	/**
	 * function that can be used to see if we are currently trying to kill the given effect particle
	 */
	public bool getFinalMove()
	{
		return finalMove;
	}
}
