using UnityEngine;
using System.Collections;

public class backgroundMove : MonoBehaviour {
	public Rigidbody2D rb;
	private float x;

    //Offset from canvas
    private float offsetX;  
    private float offsetY;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
//		Debug.Log (rb.position.x);
	}
	
	// Update is called once per frame
	void Update () {
		x = rb.position.x;
        offsetX = 890f;
		//Debug.Log (rb.position);
		if (x > (-offsetX/5))
			rb.velocity = new Vector2 (-30, 0);
		else {
			rb.position = new Vector2(rb.position.x+offsetX/5*2,rb.position.y);
			//Debug.Log (rb.position);
		}
	}
}
