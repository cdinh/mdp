using UnityEngine;

/**
 * some initialization for the overall game
 */
public class GameConfig : MonoBehaviour 
{
	/**
	 * Use this for initialization
	 */
	void Start () 
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
        GuiUtility.CachedScaledMatrix = GuiUtility.ScaledMatrix();
	}
}
