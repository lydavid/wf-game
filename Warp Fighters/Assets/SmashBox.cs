using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashBox : MonoBehaviour {

    float duration; // after duration and smashed, delete all triangles then remove this object
    bool smashed;
    List<GameObject> triangles;

	// Use this for initialization
	void Start () {
        smashed = false;
        triangles = new List<GameObject>();
        duration = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckCloseToTag("Player", 2) && !smashed)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SplitMesh();
                //ReduceToTriangles();
                smashed = true;
            }
        }

        if (smashed)
        {
            if (duration <= 0)
            {
                foreach(GameObject triangle in triangles) {
                    Destroy(triangle);
                }
                Destroy(gameObject);
            }
            duration -= Time.deltaTime;
        }
	}

    bool CheckCloseToTag(string tag, float minimumDistance)
    {
        GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 0; i < goWithTag.Length; ++i)
        {
            if (Vector3.Distance(transform.position, goWithTag[i].transform.position) <= minimumDistance)
                return true;
        }

        return false;
    }

    void ReduceToTriangles()
    {
        //gameObject.GetComponent<MeshFilter>
    }

    void SplitMesh()
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
        }
        else if (GetComponent<SkinnedMeshRenderer>())
        {
            materials = GetComponent<SkinnedMeshRenderer>().materials;
        }

        // make actual object invisible before we generate a copy of its mesh as objects and explode them
        GetComponent<MeshRenderer>().enabled = false;


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
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.transform.localScale = transform.lossyScale;
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.layer = 8; // it's own layer, prevents it from colliding with other objects
                GO.AddComponent<BoxCollider>();
                float variance = 2.0f;
                Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance * 2, variance * 2), transform.position.y + Random.Range(-variance, 0), transform.position.z + Random.Range(-variance * 2, variance * 2));

                triangles.Add(GO);

                // explode the triangle mesh objects
                GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(400, 500), explosionPos, 10);
                //mesh.RecalculateNormals();
                //GO.transform.Translate(mesh.normals[1] * Random.Range(2, 5)); // translate along normal
            }
        }

        // Slow down time
        //Time.timeScale = 0.5f;

    }
}
