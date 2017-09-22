using UnityEngine;

/**
 * Static class containing utility functions (usually math)
 * that can be used throughout the project
 */

public static class MDPUtility 
{
    private static System.Random s_Random = new System.Random();
	
	public static float DistanceSquared(GameObject o1, GameObject o2)
	{
        Vector3 diff = o1.transform.position - o2.transform.position;
		
		return diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
	}

    public static float DistanceSquared(Vector3 o1, Vector3 o2)
    {
        Vector3 diff = o1 - o2;

        return diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
    }

    public static Vector3 MouseToWorldPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.y = 0;

        return position;
    }

    public static Vector3 WorldToScreenPosition(Vector3 worldPos)
    {
        Vector3 position = Camera.main.WorldToScreenPoint(worldPos);
        position.y = Screen.height - position.y;

        return position;
    }

    public static double RandomDouble()
    {
        return s_Random.NextDouble();
    }

    public static float RandomFloat()
    {
        return (float)s_Random.NextDouble();
    }

    public static int RandomInt(int maxValue)
    {
        return s_Random.Next(maxValue);
    }

    public static int RandomInt(int minValue, int maxValue)
    {
        return s_Random.Next(minValue, maxValue);
    }
}
