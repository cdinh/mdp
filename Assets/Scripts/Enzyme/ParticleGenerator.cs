using UnityEngine;
using System.Collections;

/**
 * script used to generate particles in the enzyme game
 */
public class ParticleGenerator : MonoBehaviour
{	
	// variables to hold all the different types of particles that can spawn
	public GameObject parentCylCyl;			//!< this represents two cylinders stuck together
	public GameObject parentCylCap;			//!< this represents a cylinder and capsule stuck together
	public GameObject parentCylSphere;		//!< this represents a cylinder and a sphere stuck together
	public GameObject parentSphereSphere;	//!< this represents two spheres stuck together
	public GameObject parentSphereCyl;		//!< this represents a sphere and a cylinder stuck together
	public GameObject parentSphereCap;		//!< this represents a sphere and a capsule stuck together
	public GameObject parentCapCap;			//!< this represents two capsules stuck together
	public GameObject parentCapCyl;			//!< this represents a capsule and a cylinder stuck together
	public GameObject parentCapSphere;		//!< this represents a capsule and a sphere stuck together
	
	public int numSpawn;					//!< the number of particles to initially spawn when game starts
	Color[] colors;							//!< array to hold the valid colors that a particle can spawn as
	GameObject[] parents;					//!< array to hold the valid particle types that a particle can spawn as

	/**
	 * Use this for initialization.
	 * Sets valid particle colors and particle types in arrays.
	 * Spawns any initial particles
	 */
	void Start ()
	{
		// populate the color array with the possible choices
		// currently the choices are only red, blue, yellow, green, white
		colors = new Color[5];
		colors [0] = Color.red;
		colors [1] = Color.blue;
		colors [2] = Color.yellow;
		colors [3] = Color.green;
		colors [4] = Color.white;

		// populate the parent array
		// put a reference to each type we can have
		parents = new GameObject[9];
		parents[0] = parentCylCyl;
		parents[1] = parentCylCap;
		parents[2] = parentCylSphere;
		parents[3] = parentSphereSphere;
		parents[4] = parentSphereCyl;
		parents[5] = parentSphereCap;
		parents[6] = parentCapCap;
		parents[7] = parentCapCyl;
		parents[8] = parentCapSphere;

		// if the script is hardcoded from the unity editor to spawn a 
		// certain number of particles on script starting, then do so
		for (int i = 0; i<numSpawn; i++) 
		{
			SpawnParticle();
		}
	}

	/**
	 * function to spawn a new particle
	 */
	public void SpawnParticle ()
	{
		// choose a random x and y location to spawn the particle
		float x = Random.Range(-10.0f, 10.0f);
		float y = Random.Range(-5.0f, 5.0f);

		// choose a random type of particle
		GameObject spawn = (GameObject)Instantiate (parents [Random.Range (0, parents.Length)], 
		                                            new Vector3 (Random.Range (-10f, 10f), Random.Range (-6f, 6f), 0), 
		                                            Quaternion.identity);
		MeshRenderer[] renderers = spawn.GetComponentsInChildren<MeshRenderer> ();

		// choose a random particle color and assign it to all the renderers
		Color color = colors [Random.Range (0, 5)];
		for (int j = 0; j < renderers.Length; j++) 
		{
			renderers [j].material.color = color;
		}

		// give the particle an initial impulse
		spawn.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, 0), ForceMode.Impulse);
	}
}
