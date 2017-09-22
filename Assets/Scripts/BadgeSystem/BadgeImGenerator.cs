using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BadgeImGenerator : MonoBehaviour {

	public string badgeName;
	public Sprite[] imList;
	public int num;

	private Image im;
	private BadgeDataIO bdataIO;


	// Use this for initialization
	void Start () {
		im = GetComponent<Image> ();
		bdataIO = FindObjectOfType (typeof(BadgeDataIO)) as BadgeDataIO;

	
	}
	
	// Update is called once per frame
	void Update () {

		im.sprite = bdataIO.returnBadgeStatus (num) ? imList [1] : imList [0];
	
	}
}
