/**
 * Container class for a food blob.
 * Blob itself has no physical form but
 * contains nutrients as children
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodBlob : MonoBehaviour 
{
	public float Velocity;							//!< store the food blob's velocity
	public float RotationSpeed;						//!< store the food blob's rotation speed

	public GameObject Nutrient;						//!< store a reference to a nutrient object
	public int NumNutrients;						//!< counter for the number of nutrients on the food blob

	private NutrientManager m_NutrientManager;		//!< to hold a reference to the nutrient manager
	private IntestineGameManager m_GameManager;		//!< to hold a reference to the game manager

	private Nutrient n;								//!< to hold a reference to nutrients on a foodblob during food blob cleanup
	
	public GameObject nutrientLostSound;			//!< for holding a reference to the nutrient lost sound

	/**
	 * the function to generate what nutrients are on the food blob
	 */
	public void GenerateEnzymes(int minNutrients, int maxNutrients, Color[] availableColors)
	{
		NumNutrients = Random.Range (minNutrients,maxNutrients + 1);		// randomly choose the number of nutrients on the blob

		// find a reference to the current nutrient manager and game manager
        m_NutrientManager = FindObjectOfType(typeof(NutrientManager)) as NutrientManager;
        m_GameManager = FindObjectOfType(typeof(IntestineGameManager)) as IntestineGameManager;

		// populate the number of nutrients decided earlier
		for(int i = 0; i < NumNutrients; i++)
        {
			float radius = .4f;							// choose .4f as a radius to start with
			float angle = ((2 * Mathf.PI)/NumNutrients)*i;	// divide the circle into the right number of angle chunks in rads
			float xPos = radius * Mathf.Cos(angle);		// find the x position as radius*cos(theta)
			float zPos = radius * Mathf.Sin (angle);	// find the y position as radius*sin(theta)
			
			Vector3 position = transform.position;		// 3 dimensional vector for position
			position.x += xPos;							// set the x position of the vector
			position.z += zPos;							// set the z position of the vector
			position.y = .5f; 							// set the y position of the vector

			// randomly choose a color for the nutrient
            int randomIndex = MDPUtility.RandomInt(availableColors.Length);		
			Nutrient nutrient = m_NutrientManager.InstantiateNutrient(availableColors[randomIndex], position);
			nutrient.intestineGameManager = m_GameManager;		// assign the game manager reference on the nutrient to be
																// the same as the one referenced in this class
			
			// Attach new enzyme as a child object
			nutrient.transform.parent = gameObject.transform;
			((Behaviour)nutrient.GetComponent("Halo")).enabled = false;	// halo should be false unless explicitly enabled
		}
	}

	/**
	 * called when nutrient enters a collision
	 * Checks if the blob hit the end of the path and calls clean up function if so
	 */
    void OnTriggerEnter(UnityEngine.Collider obj)
    {
        if (obj.gameObject.tag == "Finish")	// check if the food blob collided with the end point, if it did handle it
        {
            OnEndPointCollision();
        }
    }

	/**
	 * function to handle the end point collision for a food blob
	 * mostly clean up behavior
	 */
	private void OnEndPointCollision()
	{	
		int numNutrientsAlive = 0;					// create a variable to count the live nutrients still on the blob
		
        foreach (Transform child in transform)				// check all children on the blob
        {
			n = child.GetComponent<Nutrient>();	// get the reference to the nutrient script on the child
		
			if (!n.isDead)									// flag if any of the nutrients are still alive
			{
				numNutrientsAlive++;						// increase the counter of the number of nutrients alive
				Instantiate(nutrientLostSound);				// play a sound if a nutrient was alive
			}
    		
			m_NutrientManager.RemoveNutrient(child.GetComponent<Nutrient>());	// remove the child from the blob
        }

		Destroy(this.gameObject);								// when we are finished destroy the parent blob
		m_GameManager.OnFoodBlobFinish(numNutrientsAlive); 		// apply penalty for however many nutrients were alive on blob
	}
}
