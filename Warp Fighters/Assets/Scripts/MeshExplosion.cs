using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// Adapted from MeshEffect script but applies to all Meshes, including mesh of children
public class MeshExplosion : MonoBehaviour {

    // Determines whether this script placed in a GameObject should function on its own
    // or will be called form outside
    public bool selfControl; // should be false on enemies, true on simple objects like destructible cubes

    List<GameObject> GOs = new List<GameObject>();  // track GOs created by this script to destroy and remove clutter
    float waitTime;  // countdown to destroy self
    bool setToDestroy;  // when this is set true, after waitTime seconds, destroy all spawned GO and itself

    Vector3 hittedObjectPos; // position of object that hits this, whether the player or wall, should not count grounds


    [Header("Triangle Optimization")]
    public bool limitTriangles;
    public bool limitTrianglesPerChild;
    public int maxTriangles = 10000;
    int trianglesCount = 0;
    public int maxTrianglesFromOneChild = 25;
    int maxTrianglesFromOneChildCount = 0;


    // Use this for initialization
    void Start () {
        waitTime = 0;
        setToDestroy = false;
        hittedObjectPos = Vector3.zero;
	}
	

	// Update is called once per frame
	void Update () {

        if (setToDestroy)
        {
            WaitToDestroy();
        }
	}


    private void OnCollisionEnter(Collision other)
    {
        // Get position of object we collided into for our explosion origin
        // as long as it's not of the Ground layer (otherwise it may explode on the spot)
        if (other.gameObject.layer != 9)
        {
            hittedObjectPos = other.transform.position;
        }

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
    public void SplitMesh(float waitTime, float scale = 1.0f)
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
            Debug.Log(mf.name);
        }
        
        foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            M.Add(smr.sharedMesh);
            Debug.Log(smr.name);
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
            //mr.enabled = false;
            mr.gameObject.SetActive(false);
        }

        foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            materials.Add(smr.materials);
            //mr.enabled = false;
            smr.gameObject.SetActive(false);
        }


        if (limitTriangles)
        {
            trianglesCount = 0;
        }

        for (int j = 0; j < M.Count; j++)
        {
            if (limitTriangles && trianglesCount > maxTriangles)
            {
                break;
            }
            if (limitTrianglesPerChild)
            {
                maxTrianglesFromOneChildCount = 0;
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
                    GO.transform.position = transform.localPosition;
                    GO.transform.rotation = transform.rotation;
                    GO.transform.localScale = transform.localScale * scale;
                    
                    GO.AddComponent<MeshRenderer>().material = materials[j][submesh];
                    GO.AddComponent<MeshFilter>().mesh = mesh;
                    //GO.layer = 8; // it's own layer, prevents it from colliding with other objects
                    //GO.AddComponent<BoxCollider>();
                    float variance = 2.0f;
                    //Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance * 2, variance * 2), transform.position.y + Random.Range(-variance, 0), transform.position.z + Random.Range(-variance * 2, variance * 2));
                    //Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-variance * 4, -variance * 2), transform.position.y, transform.position.z);
                    //Vector3 explosionPos = Vector3.zero;

                    // Explodes in a circle around the player, looks much cooler than the other ones

                    Vector3 explosionPos;
                    if (gameObject.tag == "Player")
                    {
                        // explode from center instead
                        explosionPos = new Vector3(gameObject.transform.position.x + Random.Range(-variance * 2, variance * 2), gameObject.transform.position.y + Random.Range(-variance * 2, variance * 2), gameObject.transform.position.z + Random.Range(-variance * 2, variance * 2));

                    }
                    else
                    {
                        explosionPos = new Vector3(hittedObjectPos.x + Random.Range(-variance * 2, variance * 2), hittedObjectPos.y + Random.Range(-variance * 2, variance * 2), hittedObjectPos.z + Random.Range(-variance * 2, variance * 2));

                    }

                    GOs.Add(GO);

                    // explode the triangle mesh objects
                    GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(100, 1000), explosionPos, 25);
                    //mesh.RecalculateNormals();
                    //GO.transform.Translate(mesh.normals[1] * Random.Range(2, 5)); // translate along normal

                    if (limitTriangles)
                    {
                        trianglesCount += 1;
                    }

                    if (limitTrianglesPerChild)
                    {
                        maxTrianglesFromOneChildCount += 1;
                        if (maxTrianglesFromOneChildCount >= maxTrianglesFromOneChild)
                        {
                            break;
                        }
                    }

                }
            }

            // Slow down time
            //Time.timeScale = 0.5f;
        }

        //Debug.Break();

        setToDestroy = true;
        this.waitTime = waitTime;
    }


    /* Let the explosion controlled by physics move around a bit before deleting all obj
     * created by this script and the original obj itself. */
    void WaitToDestroy()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            foreach (GameObject GO in GOs)
            {
                Destroy(GO);
            }

            /* End the game once the player has exploded */
            if (gameObject.tag == "Player")
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
