using UnityEngine;
using System.Collections;

/**
 * Handles splitting a particle made up of two pieces (for example, two sphere) into two individual
 * particles floating around the game independently
 */
public class ParticleSplit : MonoBehaviour 
{
	// variables for the children of a parent and to monitor if it is split
	public GameObject child1;			//!< to hold a reference to one of the particles on the parent after splitting
	public GameObject child2;			//!< to hold a reference to one of the particles on the parent after splitting
	private bool isSplit;				//!< flag to indicate if the particles were sucessfully split

	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		isSplit = false;	// when we begin the particle will not be split
							// it will need to be split with an appropriate collision
	}

	/**
	 * function to split a particle into it's two constituent particles
	 */
	public void Split()
	{
		if(!isSplit)	// first make sure it's not already split
		{
			isSplit = true;

			// cause the particles to split off from the parent
			foreach(Transform child in transform)
			{
				GameObject toCreate = child.gameObject.name == child1.gameObject.name ? child1 : child2;
				GameObject created = (GameObject)Instantiate(toCreate, 
					transform.position,
					transform.rotation);

				// assign proper color, zero the velocity, and give each split child an impulse force to start moving
				created.GetComponent<Renderer>().material.color = child.gameObject.GetComponent<Renderer>().material.color;
				created.GetComponent<Rigidbody>().velocity = Vector3.zero;
				created.GetComponent<Rigidbody>().AddForce(created.transform.right * (10 * child.position.x), ForceMode.Impulse);
			}
		}
	}
}
