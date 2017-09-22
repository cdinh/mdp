using UnityEngine;
using System.Collections;

public class poopmeter : MonoBehaviour {

    private float xposition;
    private float yposition;
    private float xnewP;

    public float speed;

    private LargeIntestGameManager lgm;
    private float water;

	// Use this for initialization
	void Start () {
        lgm = FindObjectOfType(typeof(LargeIntestGameManager)) as LargeIntestGameManager;
        xposition = transform.position.x;
        yposition = transform.position.y;
        
	}
	
	// Update is called once per frame
	void Update () {
        water = lgm.getWaterValue() * 1f;
        //Debug.Log(water);

        float step = speed * Time.deltaTime;
        float offset = -11f / 5;
        xnewP = xposition + (100-water) * (offset);
        //if (xnewP < -130) xnewP = -110;
        Vector2 newposition = new Vector2(xnewP, yposition);
        if (transform.position.x>-100)
            transform.position = Vector2.MoveTowards(transform.position, newposition, step);
        //Debug.Log(newposition);
        //Debug.Log(xposition);
       
    }

    public void setPosition(int water)
    {
        float step = speed * Time.deltaTime;
        xposition = water * 10;
        Vector2 newposition = new Vector2(xposition, yposition);
        transform.position = Vector2.MoveTowards(transform.position, newposition, step);
    }
}
