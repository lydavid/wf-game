using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from MeshEffect script but applies to all Meshes, including mesh of children
public class MeshExplosion : MonoBehaviour {

    public bool selfControl; // should be false on enemies, true on simple objects like destructible cubes

    List<GameObject> GOs = new List<GameObject>();
    float waitTime;
    bool setToDestroy;

    // Use this for initialization
    void Start () {
        waitTime = 0;
        setToDestroy = false;

	}
	
	// Update is called once per frame
	void Update () {

        if (setToDestroy)
        {
            WaitToDestroy();
            //Debug.Log(waitTime);
        }
		
	}

    private void OnCollisionEnter(Collision other)
    {
        // in control of its own collision and not touching Ground
        if (selfControl && other.gameObject.layer != 9)
        {
            // check existence
            if (other.gameObject.GetComponent<AttackManager>())
            {
                // check player attacked it
                if (other.gameObject.GetComponent<AttackManager>().initiatedAttack) {
                    SplitMesh(3.0f);
                }
            }
            
        }
    }

    // modified from: https://answers.unity.com/questions/1036438/explode-mesh-when-clicked-on.html
    public void SplitMesh(float waitTime)
    {

        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }


        List<Mesh> M = new List<Mesh>();
        //Mesh[] M = new Mesh[0];
        //if (GetComponent<MeshFilter>())
        //{
        foreach (MeshFilter mf in GetComponentsInChildren<MeshFilter>())
        {
            M.Add(mf.mesh);
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

        List<Material[]> materials = new List<Material[]>();

        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            materials.Add(mr.materials);
            mr.enabled = false;
        }

        // make actual object invisible before we generate a copy of its mesh as objects and explode them
        int maxTriangles = 300;
        int trianglesCount = 0;

        for (int j = 0; j < M.Count; j++)
        {
            if (trianglesCount > maxTriangles)
            {
                break;
            }

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
                    //GO.layer = 8; // it's own layer, prevents it from colliding with other objects
                    //GO.AddComponent<BoxCollider>();
                    float variance = 2.0f;
                    Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance * 2, variance * 2), transform.position.y + Random.Range(-variance, 0), transform.position.z + Random.Range(-variance * 2, variance * 2));
                    //Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance * 4, -variance * 2), transform.position.y, transform.position.z);
                    //Vector3 explosionPos = Vector3.zero;
                    GOs.Add(GO);

                    // explode the triangle mesh objects
                    GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(400, 500), explosionPos, 10);
                    //mesh.RecalculateNormals();
                    //GO.transform.Translate(mesh.normals[1] * Random.Range(2, 5)); // translate along normal
                    trianglesCount += 1;

                }
            }

            // Slow down time
            //Time.timeScale = 0.5f;
        }
        setToDestroy = true;
        this.waitTime = waitTime;
    }

    void WaitToDestroy()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            foreach (GameObject GO in GOs)
            {
                Destroy(GO);
            }
            Destroy(gameObject);
        }
    }

}
