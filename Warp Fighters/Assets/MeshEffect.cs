using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshEffect : MonoBehaviour {

    MeshFilter[] mfs;
    List<GameObject> GOs = new List<GameObject>();
    List<Vector3> originalPositions = new List<Vector3>();
    List<Quaternion> originalRotations = new List<Quaternion>();
    List<bool> reachedDest = new List<bool>();
    bool warpComplete = false;
    bool inWarp = false;

    private void Awake()
    {
        mfs = gameObject.GetComponentsInChildren<MeshFilter>();

    }

    // Use this for initialization
    void Start () {
        //StartCoroutine(SplitMesh(true));
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("4"))
        {

            foreach (MeshFilter mf in mfs)
            {

                Vector3[] vertices = mf.mesh.vertices;
                Vector3[] normals = mf.mesh.normals;
                int[] triangles = mf.mesh.triangles;

                int i = 0;
                while (i < vertices.Length / 3)
                {
                    vertices[i] += normals[i];
                    vertices[i+1] += normals[i+1];
                    vertices[i+2] += normals[i+2];
                    i += 3;

                }
                mf.mesh.vertices = vertices;
            }
        }

        if (Input.GetKeyDown("space"))
        {
            SplitMesh();
            inWarp = true;
        }

        
        // moves each of the objects towards a destination if they are not there yet

        if (!AllTrue(reachedDest)) {
            
            int i = 0;
            while (i < GOs.Count)
            {
                if (!reachedDest[i])
                {
                    Vector3 dest = originalPositions[i] + Vector3.forward * 10;
                    Quaternion rot = originalRotations[i];
                    //Destroy(GOs[i].GetComponent<Rigidbody>());
                    GOs[i].transform.position = Vector3.MoveTowards(GOs[i].transform.position, dest, 1);
                    GOs[i].transform.rotation = Quaternion.RotateTowards(GOs[i].transform.rotation, rot, 360);
                    if (GOs[i].transform.position == dest)
                    {
                        Destroy(GOs[i].GetComponent<Rigidbody>());
                        reachedDest[i] = true;
                    }
                }
                i++;
            }

        } else if (inWarp)
        {
            warpComplete = true;
        }

        // reactivate our actual character, move it to the warp location and delete all those mesh triangle objects
        if (inWarp && warpComplete)
        {
            transform.position += new Vector3(10, 0, 0);
            GetComponent<MeshRenderer>().enabled = true;
            foreach (GameObject GO in GOs)
            {
                Destroy(GO);
            }
            inWarp = false;
            warpComplete = false;
        }

    }

    // returns whether entire list of booleans are true
    public bool AllTrue(List<bool> lst)
    {
        if (lst.Count > 0)
        {
            Debug.Log(lst.Count);
            foreach (bool e in lst)
            {
                if (!e)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // taken from: https://answers.unity.com/questions/1036438/explode-mesh-when-clicked-on.html
    void SplitMesh()
    {

        /*if (GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null)
        {
            yield return null;
        }*/

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

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                //GO.layer = LayerMask.NameToLayer("Particle");
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                //GO.AddComponent<BoxCollider>();
                Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));

                originalPositions.Add(GO.transform.position);
                originalRotations.Add(GO.transform.rotation);
                GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(300, 500), explosionPos, 5);
                //GO.AddComponent<Rigidbody>().AddForce(normals[i]);
                
                //Destroy(GO, 3);// + Random.Range(0.0f, 5.0f));
                GOs.Add(GO);
                //Debug.Log(GO);
                reachedDest.Add(false);
                //Debug.Log(reachedDest);
                //Destroy(GO.GetComponent<Rigidbody>());
            }
        }

        //GetComponent<Renderer>().enabled = false;

        //yield return new WaitForSeconds(1.0f);
        /*if (destroy == true)
        {
            Destroy(gameObject);
        }*/

    }
}
