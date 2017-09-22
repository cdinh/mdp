using UnityEngine;
using System.Collections;

/**
 * script to handle the behavior of the foodblobeffectparticleparents
 */
public class FoodBlobEffectParticles : MonoBehaviour 
{
	public GameObject effectParticle;					//!< to hold a reference to an effectParticle object

	public int numParticlesMin;							//!< the minimum number of generated effect particles
	public int numParticlesMax;							//!< the maximum number of generated effect particles
	private int numAliveChildren;						//!< the actual number of effect particles generated

	private Color particleColor;						//!< to store the color the effect particles should be
														//!< (same color as nutrient they are spawned from)

	private GameObject[] instantiatedEffectParticles;	//!< an array to hold all the effect particles generated from a nutrient

	private FollowITweenPath path;						//!< to hold a reference to the path in the si game for particles to follow
	private EffectParticle particle;					//!< to hold a reference to an individual effect particle

	/**
	 * function that handles the creation of the effect particles
	 */
	public void createParticles(Color color)
	{
		if (Random.value > .5f)		// choose a random value between 0 and 1 and make a fair decision based on it
		{
			// if the value was more than .5, then spawn the minimum number of particles
			instantiatedEffectParticles = new GameObject[numParticlesMin];
			numAliveChildren = numParticlesMin;
		} else
		{
			// if the value was less than .5, then spawn the maximum number of particles
			instantiatedEffectParticles = new GameObject[numParticlesMax];
			numAliveChildren = numParticlesMax;
		}

		particleColor = color;	// assign the proper color to the effect particles

		// spawn particles in a "cloud" formation
		for (int i = 0; i < instantiatedEffectParticles.Length; i++)
		{
			// instantiate the effect particle and assign its attributes
			instantiatedEffectParticles[i] = (GameObject)Instantiate(effectParticle);
			instantiatedEffectParticles[i].GetComponent<Renderer>().material.color = particleColor;
			instantiatedEffectParticles[i].transform.parent = gameObject.transform;

			// choose randomly if the particles go "up" or "down"
			if (Random.Range (0,2) == 1)
			{
				if (!Application.loadedLevelName.Contains ("Tutorial"))
				{
					// if it's the regular SI game
					particle = instantiatedEffectParticles[i].GetComponent<EffectParticle>();
					particle.setDesiredLocation
						(instantiatedEffectParticles[i].transform.position + 
							new Vector3(Random.Range(-2.5f, -1.5f), 0f, Random.Range (-2f, 2f)));
				} else
				{
					// if it's the SI tutorial game
					particle = instantiatedEffectParticles[i].GetComponent<EffectParticle>();
					particle.setDesiredLocation
						(instantiatedEffectParticles[i].transform.position + 
							new Vector3(Random.Range(-6.0f, -4.5f), 0f, Random.Range (-3f, 3f)));
				}
			} else
			{
				if (!Application.loadedLevelName.Contains("Tutorial"))
				{
					// if it's the regular SI game
					particle = instantiatedEffectParticles[i].GetComponent<EffectParticle>();
					particle.setDesiredLocation
						(instantiatedEffectParticles[i].transform.position + 
						 new Vector3(Random.Range(2.5f, 1.5f), 0f, Random.Range (-2f, 2f)));
				} else
				{
					// if it's the SI tutorial game
					particle = instantiatedEffectParticles[i].GetComponent<EffectParticle>();
					particle.setDesiredLocation
						(instantiatedEffectParticles[i].transform.position + 
						 new Vector3(Random.Range(6.0f, 4.5f), 0f, Random.Range (-3f, 3f)));
				}
			}
		}
	}

	/**
	 * function to set the path position where the effect particles should start following the path
	 */
	public void setPathPosition(float position)
	{
		path = gameObject.GetComponent<FollowITweenPath> ();	// get the itween path script
		path.pathPosition = position;											// set the path position
	}

	/**
	 * destroy when the end of the path is reached
	 */
	void OnTriggerEnter(UnityEngine.Collider obj)
	{
		if (obj.gameObject.tag == "Finish")
		{
			Destroy(this.gameObject);
		}
	}

	/**
	 * function that decreases the count of the alive children on the parent as effect particles are absorbed
	 */
	public void decreaseAliveChildren()
	{
		numAliveChildren--;					// decrement the counter

		if (numAliveChildren == 0)			// if there are no more alive children, destroy the parent
		{
			Destroy(this.gameObject);
		}
	}
}
