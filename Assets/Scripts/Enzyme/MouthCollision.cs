using UnityEngine;
using System.Collections;

/**
 * for the collision with the enzyme guy's mouth (to split particle if needed)
 */
public class MouthCollision : MonoBehaviour
{
	/**
	 * on trigger enter is triggered when a collision occurs
	 * handles main behavior of a hit with the enzyme guy's mouth
	 */
	void OnTriggerEnter (UnityEngine.Collider other)
	{
		// check if the object in the collision has a parent
		if (other.gameObject.transform.parent && 
		    other.gameObject.transform.parent.gameObject.name.Contains ("Particle Parent")) 
		{
			MeshRenderer particleRenderer = 
				other.gameObject.transform.parent.gameObject.GetComponentsInChildren<MeshRenderer> () [0];

			// check if the enzyme guy and the particle(s) are the same color
			if (transform.parent.GetComponent<Renderer>().material.color == particleRenderer.material.color) 
			{
				//split food particle
				ParticleSplit split = other.gameObject.transform.parent.gameObject.GetComponent<ParticleSplit> ();
				split.Split ();
				// destroy old one
				Destroy (other.gameObject.transform.parent.gameObject);
			} else 
			{
				// if they weren't the same color then destroy (kill) the enzyme guy
				Destroy (transform.parent.gameObject);
			}
		}
	}
}