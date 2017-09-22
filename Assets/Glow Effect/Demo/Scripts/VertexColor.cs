using UnityEngine;
using System.Collections;

public class VertexColor : MonoBehaviour
{
// GLOWEFFECT_USE_VERTEXCOLOR will be enabled in the demo. Since that is a global shader keyword prior to 4.1, set the vertex color for the attached objects to prevent interference
#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1
    public Color color;

	void Start ()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++) {
            colors[i] = color;
        }
        mesh.colors = colors;
	}
#endif
}
