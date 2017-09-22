using UnityEngine;
using System.Collections;

/**
 * class that just handles cleanup of the bullet splash
 */
public class BulletSplash : MonoBehaviour 
{
	/**
	 * Update is called once per frame
	 * Destroy the splash effect
	 */
	void Update () 
	{
        Destroy(gameObject);
	}
}
