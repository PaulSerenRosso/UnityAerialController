using System;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    private Mesh mesh;
    private MeshRenderer meshRenderer;

    private List<Vector3> vertices;
    private List<int> triangles;

    [SerializeField] private Material Material;
    [SerializeField] private float height = 10.0f;
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private int segments = 7;

    private Vector3 pos;

    private float angle = 0.0f;
    private float angleAmount = 0.0f;


    private void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Material;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new List<Vector3>();
        pos = new Vector3();

        angleAmount = 2 * Mathf.PI / segments;
        angle = 0.0f;

        pos.x = 0.0f;
        pos.y = 0.0f;
        pos.z = 0.0f;
        vertices.Add(new Vector3(pos.x, pos.y, pos.z));

        pos.z = height;
        vertices.Add(new Vector3(pos.x, pos.y, pos.z));
        
        for (int i = 0; i < segments; i++)
        {
            pos.x = radius * Mathf.Sin(angle);
            pos.y = radius * Mathf.Cos(angle);
            
            vertices.Add(new Vector3(pos.x, pos.y, pos.z));

            angle -= angleAmount;
        }

        mesh.vertices = vertices.ToArray();

        triangles = new List<int>();

        for (int i = 2; i < segments + 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }
        
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(segments + 1);
        
        for (int i = 2; i < segments + 1; i++)
        {
            triangles.Add(1);
            triangles.Add(i);
            triangles.Add(i + 1);
        }
        
        triangles.Add(1);
        triangles.Add(segments + 1);
        triangles.Add(2);

        mesh.triangles = triangles.ToArray();

    }
}