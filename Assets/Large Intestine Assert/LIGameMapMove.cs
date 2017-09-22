using UnityEngine;
using System.Collections;

public class LIGameMapMove : MonoBehaviour {
    public Rigidbody2D rb;
    private float x;

    //Offset from canvas
    private float offsetX;
    private float offsetY;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //		Debug.Log (rb.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        x = rb.position.x;
        offsetX = 718f;
        //Debug.Log(rb.position);

            rb.velocity = new Vector2(-30, 0);

    }
}

