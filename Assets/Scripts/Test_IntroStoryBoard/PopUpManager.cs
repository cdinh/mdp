using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour {

	public Sprite[] popUpImList;
	public int[] popUpPage;
	public float[] popUpTime;

	private Image i;


	// Use this for initialization
	void Start () {
		i = GetComponent<Image> ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
