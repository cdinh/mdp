using UnityEngine;
using System.Collections;

/**
 * Handles the enzyme collisions with the enzyme guy and particles
 */
public class EnzymeCollision : MonoBehaviour 
{	
	private GameObject gui;		//!< to hold the gui
	private Buttons buttons;	//!< for the buttons
	
	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		gui = GameObject.Find("EnzymeGUI");			// find the gui to store the reference
		buttons = gui.GetComponent<Buttons> ();		// store the reference to the buttons
	}

	/**
	 * find out if there was a collision between the enzyme dude and the particle
	 */
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name.Contains("Particle"))
		{
			MeshRenderer particleRenderer = collision.collider.gameObject.GetComponentsInChildren<MeshRenderer>()[0];

			// if the enzyme collided into a particle of the incorrect color (not matching)
			// the enzyme guy dies
			if(GetComponent<Renderer>().material.color != particleRenderer.material.color)
			{
				Destroy(gameObject);
			} else
			{
				// otherwise split or move the particle
				Vector3 direction = collision.transform.position - transform.position;
				direction.Normalize();
				collision.rigidbody.velocity = direction * 10;
			}
		}
	}

	/**
	 * called when the enzyme guy is killed
	 */
	void OnDestroy()
	{
		buttons.killEnzyme();
	}
}
