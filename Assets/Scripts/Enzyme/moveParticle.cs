using UnityEngine;
using System.Collections;

/**
 * this script controls the movement of particles/particle parents in the enzyme game
 */
public class moveParticle : MonoBehaviour 
{
	/**
	 * Update is called once per frame
	 * Handles the movement of the particles around the screen, controlling their speed to make
	 * sure they don't just stop moving.
	 */
	void Update () 
	{
		// if the speed is too slow we give it a new speed
		if(GetComponent<Rigidbody>().velocity.magnitude < .5)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1, 2),Random.Range(-1,2),0);
		}

		// collisions on the "walls" (left, right), make them bounce off of them
		if(transform.position.x < -13.3)
		{
			transform.position += new Vector3(2, 0, 0);
			GetComponent<Rigidbody>().velocity = new Vector3(30, 0, 0);
		}
		else if(transform.position.x > 13.3)
		{
			transform.position -= new Vector3(2, 0, 0);
			GetComponent<Rigidbody>().velocity = new Vector3(-30, 0, 0);
		}

		// collisions on the "walls" (top, bottom), make them bounce off of them
		if(transform.position.y > 10)
		{
			transform.position += new Vector3(0, -2, 0);
			GetComponent<Rigidbody>().velocity = new Vector3(0, -30, 0);
		}
		else if(transform.position.y < -10)
		{
			transform.position += new Vector3(0, 2, 0);
			GetComponent<Rigidbody>().velocity = new Vector3(0, 30, 0);
		}
	}

	/**
	 * called after leaving a collision
	 * Handles cleaning up movement of the particle after a collision by making sure it doesn't stop
	 */
	void OnCollisionExit(Collision collision)
	{
		// increase the speed if it's too slow
		if(GetComponent<Rigidbody>().velocity.magnitude < 5)
		{
			GetComponent<Rigidbody>().velocity = 1.1f * GetComponent<Rigidbody>().velocity;
		}
	}
}