using UnityEngine;
using System.Collections.Generic;

/**
 * script that handles basic tower behaviors
 */
public class Tower : MonoBehaviour 
{
	// for manging costs
	public int TOWER_BASE_COST = 20;				//!< to set the base cost of placing towers
	public int TOWER_UPGRADE_LEVEL_1_COST = 50;		//!< to set the cost of upgrading a tower from base->lvl1
	public int TOWER_UPGRADE_LEVEL_2_COST = 50;		//!< to set the cost of upgrading a tower from lvl1->lvl2

	// for sounds
	public AudioClip towerShootSound;				//!< to hold the sound that will play when a tower shoots
	public AudioClip upgradeSound;					//!< to hold the sound that will play when a tower is upgraded

	private DebugConfig debugConfig;				//!< to hold a reference to the script on the debugger
	
	public GameObject Projectile;					//!< to hold a reference to the gameobject to spawn as a bullet

	public GameObject wall;							//!< for holding the walls, towers can only be placed on walls

	public string ActiveModelName					//!< the name of the tower model
	{
		get { return m_ActiveModelName; }
	}
	private string m_ActiveModelName;
	
	public float FiringRange;						//!< to store the range that a tower can fire at a target
	
	// Firing cooldown in seconds
	public float BaseCooldown;						//!< the cooldown for firing for a base tower model
	public float Level1Cooldown;					//!< the cooldown for firing for a level 1 speed tower
	public float Level2Cooldown;					//!< the cooldown for firing for a level 2 speed tower

	// for power towers, max blobs killed at once
	public int baseTargets = 1;						//!< the number of blobs a base tower model can hit
	public int level1Targets = 3;					//!< the number of blobs a level 1 power tower can hit
	public int level2Targets = 6;					//!< the number of blobs a level 2 power tower can hit
	
    private Transform m_ActiveModel;

	private Color m_TargetColor;					//!< the color of the tower
	private NutrientManager m_NutrientManager;		//!< to hold a reference to the game's nutrient manager
	private IntestineGameManager m_GameManager;		//!< to hold a reference to the game's game manager

	private Nutrient target;						//!< holds the current target the tower is trying to shoot at

	private float m_Cooldown;						//!< to hold what is the tower's current cooldown
	private int targets;							//!< to hold what is the tower's current # targets
	private float m_CurrentCooldown;				//!< to help count down the time to see if a tower can shoot again
	private bool m_CanFire;							//!< a flag that says whether a tower can currently fire
		
	/**
	 * Use this for initialization
	 * INitializes tower values on spawn
	 */
	void Start () 
	{
		gameObject.layer = LayerMask.NameToLayer("Tower");	// move the tower to the tower layer once placed

		// make sure we aren't in tutorial
		if (Application.loadedLevelName != "SmallIntestineTutorial")
		{
			// if we aren't in the tutorial get the debugger
			debugConfig = ((GameObject)GameObject.Find("Debug Config")).GetComponent<DebugConfig>();
			if (debugConfig.debugActive)		// if we're using the debugger get the costs from there
			{
				TOWER_BASE_COST = debugConfig.TOWER_BASE_COST;
				TOWER_UPGRADE_LEVEL_1_COST = debugConfig.TOWER_UPGRADE_LEVEL_1_COST;
				TOWER_UPGRADE_LEVEL_2_COST = debugConfig.TOWER_UPGRADE_LEVEL_2_COST;
			}
		}
		m_Cooldown = BaseCooldown;		// set the base cooldown
		m_CurrentCooldown = m_Cooldown;	// initialize the cooldown timer
		m_CanFire = true;				// set that the tower can fire

        m_NutrientManager = GameObject.Find("Managers").GetComponent<NutrientManager>();	// find the nutrient manager
		m_GameManager = GameObject.Find ("Managers").GetComponent<IntestineGameManager>();	// find the game manager
	}

	/**
	 * Update is called once per frame
	 * Checks for updated values for towers from the debugger.
	 * check if the tower can try to fire
	 */
	void Update () 
	{
		// if we aren't in the tutorial check for new values from the debugger if we're using it
		if (Application.loadedLevelName != "SmallIntestineTutorial" && debugConfig.debugActive)
		{
			// if we are using the debugger load in the values from the debug menu
			switch (m_ActiveModelName)
			{
				case "Base":
					m_Cooldown = debugConfig.BaseCooldown;
					targets = debugConfig.baseTargets;
					break;
				case "Speed1":
					m_Cooldown = debugConfig.Level1Cooldown;
					break;
				case "Speed2":
					m_Cooldown = debugConfig.Level2Cooldown;
					break;
				case "Power1":
					targets = debugConfig.level1Targets;
					break;
				case "Power2":
					targets = debugConfig.level2Targets;
					break;
				default:
					break;
			}
			TOWER_BASE_COST = debugConfig.TOWER_BASE_COST;
			TOWER_UPGRADE_LEVEL_1_COST = debugConfig.TOWER_UPGRADE_LEVEL_1_COST;
			TOWER_UPGRADE_LEVEL_2_COST = debugConfig.TOWER_UPGRADE_LEVEL_2_COST;
		} else 						// otherwise use the regular game values
		{
			switch (m_ActiveModelName)
			{
				case "Base":
					m_Cooldown = BaseCooldown;
					targets = baseTargets;
					break;
				case "Speed1":
					m_Cooldown = Level1Cooldown;
					break;
				case "Speed2":
					m_Cooldown = Level2Cooldown;
					break;
				case "Power1":
					targets = level1Targets;
					break;
				case "Power2":
					targets = level2Targets;
					break;
				default:
					break;
			}
		}

		// check if the tower can file
		if (m_CanFire)	// if the tower can fire
		{
            Fire();		// call the fire() function to check for targets
		} else 			// otherwise if the tower can't currently file
		{
			m_CurrentCooldown -= Time.deltaTime;	// decrement the fire cooldown timer

			if (m_CurrentCooldown <= 0f)			// check if the cooldown is now over
			{										// if it is
				m_CanFire = true;					// change the canFire variable to true
				m_CurrentCooldown = m_Cooldown;		// reset the cooldown timer
			}
		}
	}

	/**
	 * set the color of the tower
	 */
    public void SetColor(Color color)
    {
        m_TargetColor = color;										// set the color to the one passed in the function

		// assign the color properly to the tower and all the possible upgrade towers from the parent
        foreach (Transform child in transform)
        {
            Transform circle = child.Find("Circle");
            circle.GetComponent<Renderer>().materials[0].color = m_TargetColor;

			//fix bazooka color
			Transform sphere = child.Find("Sphere");
			if (sphere != null)
			{
				sphere.GetComponent<Renderer>().materials[0].color = m_TargetColor;
			}
        }
    }

	/**
	 * Get the color of the tower
	 */
	public Color getColor()
	{
		return m_TargetColor;
	}

	/**
	 * to set the active tower model
	 */
	public void SetActiveModel(string name)
	{
		// causes the towers to render properly
		// if these settings are different it can cause models to be visible that are not intended
		if(m_ActiveModelName != null)						// check if the active model name is set
		{
			m_ActiveModel.gameObject.SetActive(false);		// if it is disable the model (so it's not rendered)
		}
		else 												// if the active model name is not set
		{
			foreach (Transform child in transform)			// then set the default model plus all possible upgrades to false
			{
				child.gameObject.SetActive(false);			// make everything not render
			}
		}

		// now we set it so only the proper model will render
		m_ActiveModelName = name;								// set the model name to that passed in
        m_ActiveModel = transform.Find(m_ActiveModelName);	// find this specific model in the tower group
		m_ActiveModel.gameObject.SetActive(true);				// set this model to be the one visible on the screen
	}

	/**
	 * controls firing
	 * this is called for a tower whenever the cooldown is up
	 */
	private void Fire()
	{
       target = AcquireTarget();		// first check if there are any valid targets for this tower

        if (target)								// if a valid target was found
        {
			// set the audio clip to the shooting sound which will be played after the bullet is shot
			GetComponent<AudioSource>().clip = towerShootSound;

			// track stats
			PlayerPrefs.SetInt ("SIStats_enzymesFired", PlayerPrefs.GetInt("SIStats_enzymesFired") + 1);
			PlayerPrefs.Save();

			transform.Rotate(new Vector3(90,0,0), -40, Space.World);
            // Look at target but lock rotation on x axis
            transform.LookAt(target.transform);
			transform.Rotate(new Vector3(90,0,0), 40, Space.World);
			// play the shooting animation
            transform.Find(m_ActiveModelName).GetComponent<Animation>().Play("Default Take", PlayMode.StopAll);

			// create the bullet that will seek out the target
            GameObject bulletObject = Instantiate(Projectile, transform.position, transform.rotation) as GameObject;
            bulletObject.GetComponent<Renderer>().material.color = m_TargetColor;			// set the bullet color to the correct one

            Bullet bullet = bulletObject.GetComponent<Bullet>();			// get the script on the bullet
            bullet.Target = target.gameObject;								// set the target reference in the bullet script
			bullet.targets = targets;										// set the number of targets one bullet can hit

            target.IsTargetted = true;			// mark the current target as targetted so that it can't be shot more than once

			GetComponent<AudioSource>().Play ();						// play the sound

            m_CanFire = false;					// reset the canFire flag to false until the next cooldown is up
        }
	}

    /**
     * Returns closest nutrient within range of TargetColor's type 
     * that isn't already targeted and is in direct line of sight
     * or null if there are no valid targets
     */
    private Nutrient AcquireTarget()
    {
		// get all the nutrients of the target color from the nutrient manager
        IList<Nutrient> nutrients = m_NutrientManager.GetNutrients(m_TargetColor);
        if (nutrients == null)	// if there are no nutrients of that color then there are no targets
        {
            return null;		// return null to indicate there are no targets
        }

		// now we need to find the closest, untargeted nutrient as that is what we will shoot at
        Nutrient closestNutrient = null;			// initially set the closest nutrient to null
        float closestDistance = float.MaxValue;		// set the closest distance to infinity

		// now iterate through the nutrients to find the closest one
        foreach (Nutrient e in nutrients)
        {
            if (!e.IsTargetted)		// first check that the nutrient being examined is not already targetted
            {
				// now find the distance to that target
                float distance = MDPUtility.DistanceSquared(gameObject, e.gameObject);

				// check if this distance is less than the next known closest distance, and that the target is in LOS
                if (distance < closestDistance && distance < FiringRange * FiringRange && IsInLineOfSight(e.transform))
                {
					// if it passed the check
                    closestNutrient = e;			// assign this nutrient to be the closest one
                    closestDistance = distance;		// assign this nutrient's distance to be the closest distance
                }	
            }
        }

		// after we are done iterating through the nutrients then return what we found was the closest one as the target
        return closestNutrient;
    }

	/**
	 * function that will check if a target is in LOS with a tower
	 * this will use a linecast to see if a wall gets in the way. this prevents towers from shooting through a wall
	 */
	private bool IsInLineOfSight(Transform target)
	{
		Vector3 direction = (target.position - wall.transform.position).normalized;				// find the shooting direction

		// draw the line and see if it hits a wall, if it can be drawn return true, otherwise return false
        if (!Physics.Linecast (wall.transform.position + direction * .7f, target.position)) 
		{
			Debug.DrawLine(wall.transform.position + direction * .5f, target.position, Color.red, 5.0f);
			return true;
		}
		return false;
	}

	/**
	 * function to handle upgrading a tower to a speed tower, either from base or from a level 1 speed tower
	 */
	public void UpgradeSpeed()
	{
		switch (m_ActiveModelName)		// check the model name and then switch based on the name
		{
		case "Base":					// if the model name is "base" we upgrade to a level1 speed if possible
			if (m_GameManager.nutrients - TOWER_UPGRADE_LEVEL_1_COST >= 0)  // if you have enough nutrients to upgrade
			{
				SetActiveModel("Speed1");				// change the model name to the new name
				m_Cooldown = Level1Cooldown;			// set the cooldown to the new cooldown
				AdjustAnimationSpeed(m_Cooldown);		// call the function to correct the animation speed
				m_GameManager.nutrients = m_GameManager.nutrients - TOWER_UPGRADE_LEVEL_1_COST;	// upgrade costs  nutrients (for test)

				// play sounds
				GetComponent<AudioSource>().clip = upgradeSound;
				GetComponent<AudioSource>().Play();

				// track stats
				PlayerPrefs.SetInt ("SIStats_towersUpgraded", PlayerPrefs.GetInt("SIStats_towersUpgraded") + 1);
				PlayerPrefs.SetInt ("SIStats_nutrientsSpent", PlayerPrefs.GetInt("SIStats_nutrientsSpent") + TOWER_UPGRADE_LEVEL_1_COST);
				PlayerPrefs.Save();
			}
			break;
		case "Speed1":			// if the model name is "speed1" we upgrade to a level2 speed if possible
			if (m_GameManager.nutrients - TOWER_UPGRADE_LEVEL_2_COST >= 0)  // if you have enough nutrients to upgrade
			{
				SetActiveModel("Speed2");				// set the name to that of the new model
				m_Cooldown = Level2Cooldown;			// update the tower cooldown time
				AdjustAnimationSpeed(m_Cooldown);		// call the function to correct the tower animation cooldown
				m_GameManager.nutrients = m_GameManager.nutrients - TOWER_UPGRADE_LEVEL_2_COST;		// upgrade costs  nutrients (for test)

				// play sounds
				GetComponent<AudioSource>().clip = upgradeSound;
				GetComponent<AudioSource>().Play();

				// track stats
				PlayerPrefs.SetInt ("SIStats_towersUpgraded", PlayerPrefs.GetInt("SIStats_towersUpgraded") + 1);
				PlayerPrefs.SetInt ("SIStats_nutrientsSpent", PlayerPrefs.GetInt("SIStats_nutrientsSpent") + TOWER_UPGRADE_LEVEL_2_COST);
				PlayerPrefs.Save();
			} 
			break;
		default:				// there are no other valid ways to upgrade speed so all others fall to default case
			break;
		}
	}

	/**
	 * function that is called to correct the visual animation speed of a speed tower to go along with the 
	 * updated speed
	 */
	private void AdjustAnimationSpeed(float newCooldown)
	{
        foreach (AnimationState state in transform.Find(m_ActiveModelName).GetComponent<Animation>())
        {
            state.speed = BaseCooldown / newCooldown;
        }
	}

	/**
	 * function that is called to upgrade the power level of a tower
	 */
	public void UpgradePower()
	{
		switch (m_ActiveModelName)		// switch to the right case based on the current name of the active tower model
		{
		case "Base":					// if the active name is "base" we upgrade to power level 1
			if (m_GameManager.nutrients - TOWER_UPGRADE_LEVEL_1_COST >= 0)  // if the upgrade is successful
			{
				SetActiveModel("Power1");	// update the tower model name
				targets = level1Targets;	// update the number of targets one bullet can hit
				m_GameManager.nutrients = m_GameManager.nutrients - TOWER_UPGRADE_LEVEL_1_COST;		// upgrade costs nutrients (for test

				// play sounds
				GetComponent<AudioSource>().clip = upgradeSound;
				GetComponent<AudioSource>().Play();

				// track stats
				PlayerPrefs.SetInt ("SIStats_towersUpgraded", PlayerPrefs.GetInt("SIStats_towersUpgraded") + 1);
				PlayerPrefs.SetInt ("SIStats_nutrientsSpent", PlayerPrefs.GetInt("SIStats_nutrientsSpent") + TOWER_UPGRADE_LEVEL_1_COST);
				PlayerPrefs.Save();
			} 
			break;
		case "Power1":					// if the active name is "power1" then we upgrade to power level 2
			if (m_GameManager.nutrients - TOWER_UPGRADE_LEVEL_2_COST >= 0) // if the upgrade is successful
			{
				SetActiveModel("Power2");	// update the tower model name
				targets = level2Targets;	// update the number of targets one bullet can hit
				m_GameManager.nutrients = m_GameManager.nutrients - TOWER_UPGRADE_LEVEL_2_COST;		// upgrade costs  nutrients (for test

				// play sounds
				GetComponent<AudioSource>().clip = upgradeSound;
				GetComponent<AudioSource>().Play();

				// track stats
				PlayerPrefs.SetInt ("SIStats_towersUpgraded", PlayerPrefs.GetInt("SIStats_towersUpgraded") + 1);
				PlayerPrefs.SetInt ("SIStats_nutrientsSpent", PlayerPrefs.GetInt("SIStats_nutrientsSpent") + TOWER_UPGRADE_LEVEL_2_COST);
				PlayerPrefs.Save();
			} 
			break;
		default:			// there is no other valid way to upgrade to a different power level so all others fall to the base case
			break;
		}
	}
}
