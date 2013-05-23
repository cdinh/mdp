// Container class for a food blob.
// Blob itself has no physical form but
// contains nutrients as children

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodBlob : MonoBehaviour {

	public float Velocity;
	public float RotationSpeed;

    public GameObject Nutrient;
	public int NumNutrients;
	
	private ushort m_FoodLife;	
	private GameObject m_EndPoint;

    private static Color[] s_AvailableColors = { Color.red, Color.white, Color.yellow, Color.green };

    private NutrientManager m_NutrientManager;
    private IntestineGameManager m_GameManager;
		
	// Use this for initialization
	void Start () {
        // End-point object here
       // m_EndPoint = GameObject.Find("End");
		
		GenerateEnzymes();
	}
	
	virtual protected void GenerateEnzymes()
	{
        m_NutrientManager = FindObjectOfType(typeof(NutrientManager)) as NutrientManager;
        m_GameManager = FindObjectOfType(typeof(IntestineGameManager)) as IntestineGameManager;

		for(int i = 0; i < NumNutrients; i++)
        {
            // Place enzyme generation code here
            Vector3 position = transform.position;
            position.x += i * 0.9f;

            int randomIndex = MDPUtility.RandomInt(s_AvailableColors.Length);
			Nutrient nutrient = m_NutrientManager.InstantiateNutrient(s_AvailableColors[randomIndex], position);
            nutrient.Manager = m_GameManager;
			
			// Attach new enzyme as a child object
			nutrient.transform.parent = gameObject.transform;
		}
	}

    void OnTriggerEnter(UnityEngine.Collider obj)
    {
        if (obj.gameObject.tag == "Finish")
        {

            Debug.Log("End reached");
            OnEndPointCollision();
        }
    }
	
	private void OnEndPointCollision()
	{
        foreach (Transform child in transform)
        {
            m_NutrientManager.RemoveNutrient(child.GetComponent<Nutrient>());
        }

        m_GameManager.OnFoodBlobFinish();
	}
	
	virtual public void TakeHit()
	{
		m_FoodLife--;
	}
	
	void OnMouseDown()
	{
        GetComponent<FollowITweenPath>().enabled = false;
	}
	
	void OnMouseUp()
	{
        GetComponent<FollowITweenPath>().enabled = true;
	}
}
