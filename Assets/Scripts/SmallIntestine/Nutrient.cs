using UnityEngine;
using System.Collections;

/**
 * script that handles the basic behavior of the nutrients on the food blobs
 */
public class Nutrient : MDPEntity 
{
    public Color BodyColor							//!< for the color of the body of the nutrient (ex red)
    {
        get { return m_BodyColor; }
        set
        {
            m_BodyColor = value;
            GetComponent<Renderer>().materials[0].color = value;
        }
    }

	private Color m_BodyColor;							//!< member variable that holds the body color value

	private Color m_TrueColor;							//!< used to help with color fading when nutrient is shot with bullet
	private Color m_TargetColor;						//!< used to help turn the nutrient brown when it is hit with bullet

	public IntestineGameManager intestineGameManager;	//!< to hold a reference to the intestinegamemanager
	
	public bool IsTargetted;							//!< flag to remember whether a nutrient is targetted by a tower
	
	public bool isDead = false;							//!< flag to indicate if a nutrient is alive or dead
	private float elapsedTime = 0;						//!< to count elapsed time

	protected GameObject m_Parent;						//!< to hold areference to the nutrient's parent foodblob
	
	private NutrientManager nutrientManager;			//!< to hold a reference to the nutrient manager

	// for the effect particles that are spawned
	public FoodBlobEffectParticles effectParticleParent;		//!< hold the template of effectParticleParents
	private FoodBlobEffectParticles foodBlobEffectParticles;	//!< hold a newly spawned effectParticle parent

	private FollowITweenPath path;						//!< to hold a reference to the path that the nutrients are following

	private Color Fats1Color = new Color(37f/255f, 97f/255f, 139f/255f, 1); 	//!< create a new color for the Fats1 Particles



	/**
	 * Use this for initialization
	 * Find manager, set variables, set parent.
	 */
	void Start () 
	{
		nutrientManager = FindObjectOfType(typeof(NutrientManager)) as NutrientManager;	// find the nutrient manager
        Collider = new CircleCollider(this);										// add a circle collider to the nutrient
        IsTargetted = false;														// the nutrient starts off not tragetted

		// assign the parent reference if the nutrient has one (which it should)
        if (gameObject.transform.parent)
		{
		    m_Parent = gameObject.transform.parent.gameObject;
		}
	}

	/**
	 * Counts time passed to control the fading of a nutrient to brown over time
	 */
	void Update()
	{
		// this manages fading the color of the nutrient form the original color to brown
		if(isDead && elapsedTime < 1)
		{
			// to count time accumulation
			elapsedTime += Time.deltaTime / 3;
			// fades the nutrient color from the body color to brown over time
			nutrientManager.ChangeColor(this, Color.Lerp(m_TrueColor, m_TargetColor, elapsedTime));
		}
	}

	/**
	 * Called when a tower bullet strikes a nutrient
	 */
	public void OnBulletCollision ()
	{
		Absorb();			// call the absorb function

		// emit effect particles unless the nutrient was fat1
		if (m_BodyColor != Fats1Color)
		{
			foodBlobEffectParticles = (FoodBlobEffectParticles)Instantiate (effectParticleParent);		// create the particles
			path = this.transform.parent.gameObject.GetComponent<FollowITweenPath> ();	// add particles to the path
			float pathPos = path.pathPosition;						// set the position the particles are on the path
			foodBlobEffectParticles.setPathPosition (pathPos);
			foodBlobEffectParticles.createParticles(m_BodyColor);	// set the color of the emitted particles
			foodBlobEffectParticles.transform.position = gameObject.transform.parent.position;
			foodBlobEffectParticles.transform.rotation = gameObject.transform.parent.rotation;
		}
		
        if (m_BodyColor == Fats1Color)		// if the color of the nutrient was fat1 we need to turn it white
        {
			nutrientManager.ChangeColor(this, Color.white);					// changes the color
			((Behaviour)gameObject.GetComponent("Halo")).enabled = true;	// adds a slight glow effect to the nutrient
        }
        else 	// if the nutrient was not fat1 we handle getting ready to destroy the nutrient
        {
			((Behaviour)gameObject.GetComponent("Halo")).enabled = false;	// make sure the halo is not enabled
			isDead = true;													// set the flag to indicate the nutrient is dead
            m_TargetColor = new Color(92f / 255f, 64f / 255f, 51f / 255f);	// set the new target color for fading
			IsTargetted = true;												// set the isTargetted flag so a tower doesn't try to shoot at this nutrient again
        }
		
		m_TrueColor = m_BodyColor;	// set this to help with fading
	}

	/**
	 * Called when a nutrient absorbs a collision with a bullet
	 */
	virtual protected void Absorb()
	{
        IsTargetted = false;
	}
}
