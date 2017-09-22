using UnityEngine;
using System.Collections;

// helper script that helps things follow the ITweenPath
public class FollowITweenPath : MonoBehaviour 
{	
	public float pathPosition;					// to hold the path position
	public float nutrientSpeed;					// to hold the speed things move along the path

	private SmoothQuaternion quaternion;

	DebugConfig debugConfig;
	
	void Start() 
	{
		quaternion = transform.rotation;
		quaternion.Duration = .5f;

		// get the debug config unless we are in the small intestine tutorial
		if (Application.loadedLevelName != "SmallIntestineTutorial")
		{
			debugConfig = ((GameObject)GameObject.Find("Debug Config")).GetComponent<DebugConfig>();
		}
	}

	void Update() 
	{
		// if we are using the debugger, we need to get the nutrient speed from the debugger
		if(debugConfig != null && debugConfig.debugActive)
		{
			nutrientSpeed = debugConfig.NutrientSpeed;
		}

		if (nutrientSpeed == 0)		// here to solve a weird issue where if speed is 0 the nutrient will 
									// rotate 90* to the left
		{
			return;
		}

		Quaternion q = transform.rotation;
	 	transform.position = Spline.MoveOnPath(iTweenPath.GetPath("Path"), 
		                                       transform.position, ref pathPosition, 
		                                       ref q, nutrientSpeed, 100, EasingType.Linear,
		                                       false, false);
		quaternion.Value = q;
		transform.rotation = quaternion;
	}
}
