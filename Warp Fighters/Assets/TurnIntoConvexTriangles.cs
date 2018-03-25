using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIntoConvexTriangles : MonoBehaviour {

    List<GameObject> GOs;

    PhysicMaterial mat;

	// Use this for initialization
	void Start () {
        //GOs = new List<GameObject>();
        mat = (PhysicMaterial)Resources.Load("FrictionlessMaterial", typeof(PhysicMaterial));
        if (gameObject.tag == "Wall")
        {
            SplitMesh(mat);
        } else
        {
            SplitMesh();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // modified from: https://answers.unity.com/questions/1036438/explode-mesh-when-clicked-on.html
    public void SplitMesh(PhysicMaterial mat = null)
    {

        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }

        if (GetComponent<MeshCollider>())
        {
            GetComponent<MeshCollider>().enabled = false;
        }


        List<Mesh> M = new List<Mesh>();
        //Mesh[] M = new Mesh[0];
        //if (GetComponent<MeshFilter>())
        //{
        foreach (MeshFilter mf in GetComponentsInChildren<MeshFilter>())
        {
            M.Add(mf.mesh);
        }

        foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            M.Add(smr.sharedMesh);
        }

        //M = GetComponentsInChildren<MeshFilter>().mesh;
        /*}
        else if (GetComponent<SkinnedMeshRenderer>())
        {
            M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }*/

        //Material[] materials = new Material[0];
        /*if (GetComponent<MeshRenderer>())
        {
            materials = GetComponent<MeshRenderer>().materials;
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if (GetComponent<SkinnedMeshRenderer>())
        {
            materials = GetComponent<SkinnedMeshRenderer>().materials;
            GetComponent<SkinnedMeshRenderer>().enabled = false;
        }*/

        // make actual object invisible before we generate a copy of its mesh as objects and explode them
        // and copy their materials for our triangles
        List<Material[]> materials = new List<Material[]>();

        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            materials.Add(mr.materials);

        }

        foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            materials.Add(smr.materials);

        }




        for (int j = 0; j < M.Count; j++)
        {


            Vector3[] verts = M[j].vertices;
            Vector3[] normals = M[j].normals;
            Vector2[] uvs = M[j].uv;
            for (int submesh = 0; submesh < M[j].subMeshCount; submesh++)
            {
                int[] indices = M[j].GetTriangles(submesh);

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


                    GO.AddComponent<MeshRenderer>().material = materials[j][submesh];
                    

                    GO.AddComponent<MeshFilter>().mesh = mesh;

                    GO.AddComponent<BoxCollider>();
                    
                    //GO.GetComponent<MeshCollider>().inflateMesh = true;
                    //GO.GetComponent<MeshCollider>().skinWidth = 0.01f;
                    //GO.GetComponent<BoxCollider>().convex = true;
                    GO.GetComponent<BoxCollider>().material = mat;
                    GO.tag = gameObject.tag;
                    //GO.AddComponent<Rigidbody>();
                    //GO.GetComponent<Rigidbody>().isKinematic = true;
                    //GO.layer = 8; // it's own layer, prevents it from colliding with other objects
                    //GO.AddComponent<BoxCollider>();
                    //float variance = 5.0f;
                    //Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance * 2, variance * 2), transform.position.y + Random.Range(-variance, 0), transform.position.z + Random.Range(-variance * 2, variance * 2));
                    //Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance * 4, -variance * 2), transform.position.y, transform.position.z);
                    //Vector3 explosionPos = Vector3.zero;

                    // Explodes in a circle around the player, looks much cooler than the other ones

                    //Vector3 explosionPos;
                    //GOs.Add(GO);

                    // explode the triangle mesh objects
                    //GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(600, 800), explosionPos, 25);
                    //mesh.RecalculateNormals();
                    //GO.transform.Translate(mesh.normals[1] * Random.Range(2, 5)); // translate along normal



                }
            }

            // Slow down time
            //Time.timeScale = 0.5f;
        }

        gameObject.SetActive(false);
    }
}
