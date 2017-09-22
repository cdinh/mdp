/**
 * MDPEntity must be attached onto all game objects
 * This class ensures that the game object will have
 * required components (such as a collider)
 */


// I don't use this method any more for any of the new stuff in the game (anything in the 
// 2013-2014 school year) because it's much more complicated to do things object oriented
// than just using the unity gameobjects and scripting, imo
using UnityEngine;
using System.Collections;

abstract public class MDPEntity : MonoBehaviour {
	public Collider Collider;

    virtual protected void CheckCollisions() { }
}
