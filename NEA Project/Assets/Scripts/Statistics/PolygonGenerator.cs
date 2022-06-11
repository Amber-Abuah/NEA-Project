using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonGenerator : MonoBehaviour
{
    Mesh mesh;
    [SerializeField] MeshFilter meshFilter;

    public void GeneratePolygon(Vector3[] vertices)
    {
        // Create a new mesh
        mesh = new Mesh();

        // Assign the vertices of the mesh
        mesh.vertices = vertices;

        // Create triangles based on all five points
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4 };

        // Assign the current mesh to the mesh filter, to make the polygon visible
        meshFilter.mesh = mesh;
    }
}
