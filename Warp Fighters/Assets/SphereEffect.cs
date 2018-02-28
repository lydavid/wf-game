using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEffect : MonoBehaviour {

    Vector3 center;

	// Use this for initialization
	void Start () {
        center = GetComponent<Renderer>().bounds.center;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            SplitHalfOfMesh();
        }
		
	}


    void SplitHalfOfMesh()
    {

        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }

        Mesh M = new Mesh();
        if (GetComponent<MeshFilter>())
        {
            M = GetComponent<MeshFilter>().mesh;
        }
        else if (GetComponent<SkinnedMeshRenderer>())
        {
            M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }

        Material[] materials = new Material[0];
        if (GetComponent<MeshRenderer>())
        {
            materials = GetComponent<MeshRenderer>().materials;
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if (GetComponent<SkinnedMeshRenderer>())
        {
            materials = GetComponent<SkinnedMeshRenderer>().materials;
            GetComponent<SkinnedMeshRenderer>().enabled = false;
        }

        // make actual object invisible before we generate a copy of its mesh as objects and explode them



        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;
        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {

            int[] indices = M.GetTriangles(submesh);

            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 }; // comment out the last 3 ints for backface culling, somewhat improves performance

                GameObject GO = new GameObject("Triangle " + (i / 3));
                //GO.layer = LayerMask.NameToLayer("Particle");
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.transform.localScale = transform.lossyScale;
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.layer = 8; // it's own layer, prevents it from colliding with other objects


                //GO.AddComponent<MeshCollider>();
                //GO.GetComponent<MeshCollider>().convex = true;

                GO.AddComponent<BoxCollider>();

                float variance = 2.0f;
                //Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance * 2, variance * 2), transform.position.y + Random.Range(-variance, 0), transform.position.z + Random.Range(-variance * 2, variance * 2));
                Vector3 explosionPos = center;

                // explode the triangle mesh objects
                if (GO.GetComponent<BoxCollider>().GetComponent<Renderer>().bounds.center.x >= center.x)
                {
                    //Debug.Log(GO.GetComponent<MeshCollider>().GetComponent<Renderer>().bounds.center);
                    //Debug.Log(center.x);
                    //Debug.Log("---");

                    GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(400, 500), explosionPos, 10);
                    //GO.AddComponent<Rigidbody>().AddForce(GO.transform.up * 10);
                }
                //mesh.RecalculateNormals();
                //GO.transform.Translate(mesh.normals[1] * Random.Range(2, 5)); // translate along normal



            }
        }

        // Slow down time
        Time.timeScale = 0.5f;

    }
}
