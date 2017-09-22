using UnityEngine;
using System.Collections;

/**
 * Controls buttons in the enzyme game
 */
public class Buttons : MonoBehaviour
{
	//Invulnerability Related Code	
	public GameObject objectGenerator;
	private ParticleGenerator generator;

	private GameObject p;
	public GameObject ColorPrefeb;
	public bool EnzymesExist = false;	//!< used to tell whether we already have an enzyme spawned.
										//!< can only have one enzyme active at once
										//!< currently you have to kill the current enzyme to spawn a new one

	/**
	 * Use this for initialization
	 */
	void Start ()
	{
		generator = objectGenerator.GetComponent<ParticleGenerator> ();	// find the particle generator we're using
	}

	/**
	 * Draws the buttons and handles behavior when they are pressed.
	 */
	void OnGUI ()
	{
		// add a button to return to the main menu
		if (GUI.Button (new Rect (0, 0, 100, 40), "Return")) 
		{
			Time.timeScale = 1;
			Application.LoadLevel ("MainMenu");
		}

		// add a button to generate a red enzyme when pressed
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 100, 100, 40), "RedEnzyme")) 
		{
			if (!EnzymesExist) 
			{
				p = Instantiate (ColorPrefeb, transform.position, transform.rotation) as GameObject;
				p.GetComponent<Renderer>().material.color = Color.red;
				EnzymesExist = true;
			}
		}

		// add a button to generate a green enzyme when pressed
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 150, 100, 40), "GreenEnzyme")) 
		{
			if (!EnzymesExist) 
			{
				p = Instantiate (ColorPrefeb, transform.position, transform.rotation) as GameObject;
				p.GetComponent<Renderer>().material.color = Color.green;
				EnzymesExist = true;
			}
		}

		// add a button to generate a blue enzyme when pressed
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 200, 100, 40), "BlueEnzyme")) 
		{
			if (!EnzymesExist) 
			{
				p = Instantiate (ColorPrefeb, transform.position, transform.rotation) as GameObject;
				p.GetComponent<Renderer>().material.color = Color.blue;
				EnzymesExist = true;
			}
		}

		// add a button to generate a yellow enzyme when pressed
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 250, 100, 40), "YellowEnzyme")) 
		{
			if (!EnzymesExist) 
			{
				p = Instantiate (ColorPrefeb, transform.position, transform.rotation) as GameObject;
				p.GetComponent<Renderer>().material.color = Color.yellow;
				EnzymesExist = true;
			}
		}

		// add a button to generate a white enzyme when pressed
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 300, 100, 40), "WhiteEnzyme")) 
		{
			if (!EnzymesExist) 
			{
				p = Instantiate (ColorPrefeb, transform.position, transform.rotation) as GameObject;
				p.GetComponent<Renderer>().material.color = Color.white;
				EnzymesExist = true;
			}
		}

		// this button spawns random food particles when pressed
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 350, 100, 40), "Generate")) 
		{
			generator.SpawnParticle();
		}
		
	}

	/**
	 * when an enzyme dies, just set the variable to mark that there is not one currently active
	 */
	public void killEnzyme()
	{
		EnzymesExist = false;
	}
}
