/**
 * Handles Nutrient instantiation/destruction
 * Makes accessors available for tower targeting
 */

using UnityEngine;
using System.Collections.Generic;

public class NutrientManager : MonoBehaviour 
{
    public GameObject NutrientObj;								//!< hold a nutrient gameobject

    private Dictionary<Color, List<Nutrient>> m_Nutrients;		//!< creates dictionary with list of all nutrients of each color
	private Nutrient Nutrient;									//!< for holding a reference to a nutrient script to alter values

	/**
	 * Makes a new dictionary for the nutrients
	 */
	void Awake () 
	{
        m_Nutrients = new Dictionary<Color, List<Nutrient>>(new ColorComparer());	// create the dictionary
	}

	/**
	 * function that instantiates new nutrients
	 */
    public Nutrient InstantiateNutrient(Color color, Vector3 position, Quaternion rotation)
    {
        GameObject NutrientObject = Instantiate(NutrientObj, position, rotation) as GameObject;	// instantiate a nutrient
        Nutrient = NutrientObject.GetComponent<Nutrient>();	// get the nutrient script from the newly instantiated nutrient
        Nutrient.BodyColor = color;										// set the color correctly

		// keep track of this nutrient in the dictionary
        if (!m_Nutrients.ContainsKey(color))	// if there is no list for this color in the dictionary, add it
        {
            m_Nutrients.Add(color, new List<Nutrient>());
        }

        m_Nutrients[color].Add(Nutrient);	// adds the nutrient to the dictionary

        return Nutrient;	// returns the newly created nutrient
    }

	/**
	 * alternative way to instantiate nutrients
	 */
    public Nutrient InstantiateNutrient(Color color, Vector3 position)
    {
        return InstantiateNutrient(color, position, Quaternion.identity);
    }

	/**
	 * function to remove a nutrient from a food blob
	 */
    public void RemoveNutrient(Nutrient n)
    {
   		 m_Nutrients[n.BodyColor].Remove(n);			// remove the nutrient from the dictionary list

        if (n.transform.parent.childCount == 1)			// if this was the only nutrient on the parent
		{
            Destroy(n.transform.parent.gameObject);		// destroy the parent
		}
        else
		{
            Destroy(n.gameObject);						// otherwise just destroy the nutrient
		}
    }

	/**
	 * function to get a list of nutrients of a specified color
	 */
    public IList<Nutrient> GetNutrients(Color color)
    {
        if (m_Nutrients.ContainsKey(color))			// check if there is an entry in the dictionary for nutrients of the 
													// specified color
		{
            return m_Nutrients[color].AsReadOnly();	// if there is return the list 
		}
        else
		{
            return null;							// otherwise return null
		}
    }

	/**
	 * function to change the color of a nutrient
	 */
    public void ChangeColor(Nutrient n, Color c)
    {
        m_Nutrients[n.BodyColor].Remove(n);		// remove the original nutrient from the original color list
        n.BodyColor = c;						// change the color of the nutrient

        if (!m_Nutrients.ContainsKey(c))		// make sure there is an entry in the dictionary for the new color, if not add one
        {
            m_Nutrients.Add(c, new List<Nutrient>());
        }

        m_Nutrients[c].Add(n);					// add the nutrient to the list corresponding to its new color
    }
}
