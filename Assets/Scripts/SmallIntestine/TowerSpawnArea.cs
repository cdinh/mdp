using UnityEngine;
using System.Collections;

// script that handles creating the tower spawn
public class TowerSpawnArea : MonoBehaviour 
{
	private TowerSpawner towerSpawner;	// to hold a reference to the tower spawner

	void Start () 
	{
		// find a reference to the tower spawner
		towerSpawner = FindObjectOfType(typeof(TowerSpawner)) as TowerSpawner;
	}

	// checks if the location where you are dragging a tower over is valid
	// this will effect whether the green or red circle is shwon around the tower
    void OnMouseOver()
    {
	    towerSpawner.IsMouseOverWall = true;	// set the value if the tower is currently hovering over a wall
		towerSpawner.wall = gameObject;			// assign the wall in the tower spawner to the one you are hovering over
    }

    void OnMouseExit()
    {
		towerSpawner.IsMouseOverWall = false;	// when we leave hovering over the wall reset the ismouseoverwall to false
    }
}
