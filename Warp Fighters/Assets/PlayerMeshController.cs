using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour {

    MeshFilter mesh;

    private void Awake()
    {
        mesh = gameObject.GetComponentInChildren<MeshFilter>();
        Debug.Log(mesh);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("3"))
        {
            Vector3[] vertices = mesh.mesh.vertices;
            Vector3[] normals = mesh.mesh.normals;
            foreach (Vector3 vertex in vertices)
            {

                Debug.Log(vertex);
            }
            int i = 0;
            while (i < vertices.Length)
            {
                vertices[i] += normals[i];// * Mathf.Sin(Time.time); LUL
                i++;
                
            }
            mesh.mesh.vertices = vertices;
        }

        if (Input.GetKeyDown("4"))
        {
            Vector3[] vertices = mesh.mesh.vertices;

            int i = 0;
            while (i < vertices.Length)
            {
                vertices[i] = Random.insideUnitSphere;
                i++;

            }
            mesh.mesh.vertices = vertices;

        }
    }
}
