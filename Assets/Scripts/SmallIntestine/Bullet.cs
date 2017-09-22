using UnityEngine;
using System.Collections;

/**
 * script that controls the bullets shot out by the towers in the si game
 */
public class Bullet : MDPEntity
{
    public GameObject Target;		//!< to store a reference to the bullet's target
	public Color BulletColor;		//!< to store the desired color of the bullet (same color as tower)
	
	public float Velocity;			//!< the velocity of the bullet

	public int targets;				//!< the number of objects one bullet can destroy (varies based on tower type)
	
	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		Collider = new CircleCollider(this);	// create a circle collider for the bullet
	}
	
	/**
	 * Update is called once per frame
	 * Checks if there is a target and handles accordingly
	 */
	void Update () 
	{
		if (Target)			// if there is a target then move towards it and check for collisions
		{
			transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 
			                                         Time.deltaTime * Velocity);
			transform.LookAt(Target.transform);
			
			CheckCollisions();
		}
		else 				// otherwise destroy the bullet
			Destroy(gameObject);
	}

	/**
	 * function to check for the collisions with the bullet
	 */
	protected override void CheckCollisions ()
	{
		if (Collider.CollidesWith(Target))							// if the bullet collides with a target
		{
			Destroy(gameObject);									// destroy the bullet

			// get the information on the nutrient target that was hit
			Nutrient target = Target.GetComponent<Nutrient>();
			Color targetColor = target.BodyColor;
			GameObject parent = Target.transform.parent.gameObject;

			target.OnBulletCollision();								// call the function on the target to handle a collision
			targets--;												// decrement the targets counter

			Nutrient[] nutrients = parent.GetComponentsInChildren<Nutrient>();	// get other nutrients with the same parent
																				// as the original target

			// iterate over the nutrients to see if there are any other valid targets with the same color if the bullet
			// is able to destroy more targets
			foreach (Nutrient nutrient in nutrients)
			{
				if (nutrient.BodyColor == targetColor && targets > 0 && !nutrient.IsTargetted)
				{
					targets--;
					nutrient.OnBulletCollision();
				}
			}
		}
	}
}
