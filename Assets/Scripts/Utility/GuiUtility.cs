using UnityEngine;
using System.Collections;

// this calss is only used in game config
public static class GuiUtility
{
    public static float ORIG_SCREEN_WIDTH = 1024f;
    public static float ORIG_SCREEN_HEIGHT = 768f;

    public static Matrix4x4 CachedScaledMatrix;

    // Given percentages as input, produces
    // Rect with adjusted values
    public static Rect PercRect(float x, float y, float width, float height)
    {
        return new Rect(ORIG_SCREEN_WIDTH * x, ORIG_SCREEN_HEIGHT * y, ORIG_SCREEN_WIDTH * width, ORIG_SCREEN_HEIGHT * height);
    }

    public static Rect CenteredXRect(float y, float width, float height)
    {
        return new Rect((ORIG_SCREEN_WIDTH - ORIG_SCREEN_WIDTH * width) / 2, ORIG_SCREEN_HEIGHT * y, ORIG_SCREEN_WIDTH * width, ORIG_SCREEN_HEIGHT * height);
    }

    public static Rect CenteredYRect(float x, float width, float height)
    {
        return new Rect(ORIG_SCREEN_WIDTH * x, (ORIG_SCREEN_HEIGHT - ORIG_SCREEN_HEIGHT * height) / 2, ORIG_SCREEN_WIDTH * width, ORIG_SCREEN_HEIGHT * height);
    }

    public static Matrix4x4 ScaledMatrix()
    {
        Vector3 scale = new Vector3(0, 0, 1);
        scale.x = Screen.width / ORIG_SCREEN_WIDTH;
        scale.y = Screen.height / ORIG_SCREEN_HEIGHT;

        return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
    }
}
