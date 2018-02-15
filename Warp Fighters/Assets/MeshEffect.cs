using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshEffect : MonoBehaviour {

    // parallel lists
    List<GameObject> triangleMeshes = new List<GameObject>();
    List<Vector3> originalPositions = new List<Vector3>();
    List<Quaternion> originalRotations = new List<Quaternion>();
    List<bool> reachedDest = new List<bool>();

    bool warpComplete = false;  // indicates that object has just completed its warp
    bool inWarp = false;  // indicates object is in warp

    private float spaceTimer;
    private bool hasPressedSpace;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown("space"))
        {
            SplitMesh();
            inWarp = true;

            hasPressedSpace = true;
            spaceTimer = 0.6f;
        }



        if (hasPressedSpace)
        {
            spaceTimer -= Time.deltaTime;
            Debug.Log(spaceTimer);

            if (spaceTimer <= 0f)
            {
                //do something
                // moves each of the triangle mesh objects towards a destination if they are not there yet
                if (!AllTrue(reachedDest))
                {

                    int i = 0;
                    while (i < triangleMeshes.Count)
                    {
                        if (!reachedDest[i])
                        {
                            // Move towards warp destination
                            Vector3 dest = originalPositions[i] + Vector3.forward * 10;
                            triangleMeshes[i].transform.position = Vector3.MoveTowards(triangleMeshes[i].transform.position, dest, 1);

                            // Rotate back towards original rotation before bursting
                            Quaternion rot = originalRotations[i];
                            triangleMeshes[i].transform.rotation = Quaternion.RotateTowards(triangleMeshes[i].transform.rotation, rot, 10);

                            // Get rid of rigidbody once the triangle object reaches its destination
                            if (triangleMeshes[i].transform.position == dest && triangleMeshes[i].transform.rotation == rot)
                            {
                                Destroy(triangleMeshes[i].GetComponent<Rigidbody>());
                                reachedDest[i] = true;
                            }
                        }
                        i++;
                    }

                }
                else if (inWarp)
                {
                    // have reached our destination and currently still in warp, flag our warp as complete
                    warpComplete = true;
                    hasPressedSpace = false;
                }

                
            }
        }


        

        // reactivate our actual character, move it to the warp location and delete all those mesh triangle objects
        if (inWarp && warpComplete)
        {
            transform.position += new Vector3(0, 0, 10);
            GetComponent<MeshRenderer>().enabled = true;
            foreach (GameObject triangleMesh in triangleMeshes)
            {
                Destroy(triangleMesh);
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

    // modified from: https://answers.unity.com/questions/1036438/explode-mesh-when-clicked-on.html
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

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                //GO.layer = LayerMask.NameToLayer("Particle");
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                //GO.AddComponent<BoxCollider>();
                float variance = 2.0f;
                Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance*2, variance*2), transform.position.y + Random.Range(-variance, 0), transform.position.z + Random.Range(-variance*2, variance*2));
                // track its current position and rotation
                originalPositions.Add(GO.transform.position);
                originalRotations.Add(GO.transform.rotation);

                // explode the triangle mesh objects
                GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(400, 500), explosionPos, 10);
                
                // track the triangle mesh and flag that it has not reach it's destination
                triangleMeshes.Add(GO);
                reachedDest.Add(false);
            }
        }

    }
}
