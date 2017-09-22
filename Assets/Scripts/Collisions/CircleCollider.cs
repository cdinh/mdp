using UnityEngine;
using System.Collections;

public class CircleCollider : Collider {

    public MDPEntity Entity
    {
        get { return m_Entity; }
    }
    private MDPEntity m_Entity;
	
	public CircleCollider(MDPEntity entity) 
	{
		m_Entity = entity;
		
		float lengthX = m_Entity.gameObject.GetComponent<Renderer>().bounds.size.x * 0.5f;
		float lengthZ = m_Entity.gameObject.GetComponent<Renderer>().bounds.size.z * 0.5f;
		
		Properties.Radius = (lengthX > lengthZ) ? lengthX : lengthZ;
	}
	
	public override bool CollidesWith(GameObject o2)
	{
		MDPEntity entity = o2.GetComponent(typeof(MDPEntity)) as MDPEntity;
		Collider o2Collider = entity.Collider;
				
		if (o2Collider is CircleCollider)
			return Intersection.CircleToCircle(m_Entity, entity);
		else
			return false;
	}
}
