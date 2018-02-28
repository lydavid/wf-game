using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public int speed;

    private GameObject prefab;

    private bool init;
    private int size; // half the size of a dimension of the intended cube
    private bool[,,] nonWalls;  // false represents a wall should be built at [x, y, z], since unity defaults bool to false and we are using kruskal's algo to remove walls
    private DisjointSet disjointSet;

    //private List<int> frontier = new List<int>();  // we will be popping from this list in a random order
    // an int v represents a Vertor3(v / 100, v / 10, v % 100)
    //private Stack<int> frontier = new Stack<int>();
    private List<int> frontier = new List<int>();  // should be frontier of non-walls

    int i = 0;

    private void Reset()
    {
        prefab = (GameObject)Resources.Load("Prefabs/Cube", typeof(GameObject));
        speed = 1;

    }

    // Use this for initialization
    void Start () {
		prefab = (GameObject)Resources.Load("Prefabs/Cube", typeof(GameObject));
        //speed = 1;
        init = false;
        size = 10;
        nonWalls = new bool [size, size, size];
        disjointSet = new DisjointSet(size * size * size);
    }
	
	// Update is called once per frame
	void Update () {

        /*for (int i = 0; i < speed; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            GameObject newCube = Instantiate(prefab, pos, Quaternion.identity);
            //newCube.AddComponent<Rigidbody>();
            newCube.transform.position = Vector3.MoveTowards(newCube.transform.position, newCube.transform.position + new Vector3(10, 0, 10), 10);
        }*/

        // while frontier is not empty
        /*if (frontier.Count > 0)
        {
            // get a random member and remove it form list
            int roll = Random.Range(0, frontier.Count);
            int mem = frontier.Pop();
            frontier.Remove(roll);

            Debug.Log("Roll: " + roll + " Mem: " + mem);
        }*/

        if (!init)
        {
            
            // add to frontier
            for (int i = 0; i < size * size * size; i++)
            {
                // only add if x,y,z are even
                int x = i / 100;
                int y = i % 100 / 10;
                int z = i % 10;
                if (x % 2 == 0 && y % 2 == 0 && z % 2 == 0) {
                    frontier.Add(i);
                    nonWalls[x, y, z] = true;
                }
            }

            // Shuffle list
            ListExtensions.Shuffle(frontier);

            /*for (int i = 0; i < frontier.Count; i++)
            {
                Debug.Log(frontier[i]);
            }*/

            for (int i = 0; i < frontier.Count; i++)
            {
                int code = frontier[i];

                Vector3Int vec = codeToVector3(code);

                // check pos x
                Vector3Int vec1 = new Vector3Int(Mathf.Min(vec.x + 2, size - 1), vec.y, vec.z);
                int code1 = vector3ToCode(vec1);

                if (nonWalls[vec1.x, vec1.y, vec1.z])  // confirm that it is a non-wall
                {
                    if (!disjointSet.SameSet(code, code1)) {
                        disjointSet.Union(code, code1);
                        Vector3Int vec2 = new Vector3Int(Mathf.Min(vec.x + 1, size - 1), vec.y, vec.z);
                        nonWalls[vec2.x, vec2.y, vec2.z] = true;
                        int code2 = vector3ToCode(vec2);
                        disjointSet.Union(code, code2);
                    }
                }

                // check neg x
                vec1 = new Vector3Int(Mathf.Max(vec.x - 2, 0), vec.y, vec.z);
                code1 = vector3ToCode(vec1);

                if (nonWalls[vec1.x, vec1.y, vec1.z])  // confirm that it is a non-wall
                {
                    if (!disjointSet.SameSet(code, code1))
                    {
                        disjointSet.Union(code, code1);
                        Vector3Int vec2 = new Vector3Int(Mathf.Max(vec.x - 2, 0), vec.y, vec.z);
                        nonWalls[vec2.x, vec2.y, vec2.z] = true;
                        int code2 = vector3ToCode(vec2);
                        disjointSet.Union(code, code2);
                    }
                }

                // check pos y
                vec1 = new Vector3Int(vec.x, Mathf.Min(vec.y + 2, size - 1), vec.z);
                code1 = vector3ToCode(vec1);

                if (nonWalls[vec1.x, vec1.y, vec1.z])  // confirm that it is a non-wall
                {
                    if (!disjointSet.SameSet(code, code1))
                    {
                        disjointSet.Union(code, code1);
                        Vector3Int vec2 = new Vector3Int(vec.x, Mathf.Min(vec.y + 1, size - 1), vec.z);
                        nonWalls[vec2.x, vec2.y, vec2.z] = true;
                        int code2 = vector3ToCode(vec2);
                        disjointSet.Union(code, code2);
                    }
                }

                // check neg y
                vec1 = new Vector3Int(vec.x, Mathf.Max(vec.y - 2, 0), vec.z);
                code1 = vector3ToCode(vec1);

                if (nonWalls[vec1.x, vec1.y, vec1.z])  // confirm that it is a non-wall
                {
                    if (!disjointSet.SameSet(code, code1))
                    {
                        disjointSet.Union(code, code1);
                        Vector3Int vec2 = new Vector3Int(vec.x, Mathf.Max(vec.y - 1, 0), vec.z);
                        nonWalls[vec2.x, vec2.y, vec2.z] = true;
                        int code2 = vector3ToCode(vec2);
                        disjointSet.Union(code, code2);
                    }
                }

                // check pos z
                vec1 = new Vector3Int(vec.x, vec.y, Mathf.Min(vec.z + 2, size - 1));
                code1 = vector3ToCode(vec1);

                if (nonWalls[vec1.x, vec1.y, vec1.z])  // confirm that it is a non-wall
                {
                    if (!disjointSet.SameSet(code, code1))
                    {
                        disjointSet.Union(code, code1);
                        Vector3Int vec2 = new Vector3Int(vec.x, vec.y, Mathf.Min(vec.z + 1, size - 1));
                        nonWalls[vec2.x, vec2.y, vec2.z] = true;
                        int code2 = vector3ToCode(vec2);
                        disjointSet.Union(code, code2);
                    }
                }

                // check neg z
                vec1 = new Vector3Int(vec.x, vec.y, Mathf.Max(vec.z - 2, 0));
                code1 = vector3ToCode(vec1);

                if (nonWalls[vec1.x, vec1.y, vec1.z])  // confirm that it is a non-wall
                {
                    if (!disjointSet.SameSet(code, code1))
                    {
                        disjointSet.Union(code, code1);
                        Vector3Int vec2 = new Vector3Int(vec.x, vec.y, Mathf.Max(vec.z - 1, 0));
                        nonWalls[vec2.x, vec2.y, vec2.z] = true;
                        int code2 = vector3ToCode(vec2);
                        disjointSet.Union(code, code2);
                    }
                }

            }


            //for each member
            //for (int i = 0; i < size * size * size; i++)
            //    {
            //        int code = frontier[i];

            //        if (code != -1)
            //        {
            //            Vector3Int vec = codeToVector3(code);
            //            //GameObject newCube = Instantiate(prefab, vec, Quaternion.identity);
            //            /*int x = code / 100;
            //            int y = code / 10;
            //            int z = code % 100;*/

            //            // determine whether this cube is currently separating two cubes along the x/y/z axis that are not in the same set

            //            // Check along x-axis
            //            Vector3Int vec1 = new Vector3Int(Mathf.Max(vec.x - 1, 0), vec.y, vec.z);
            //            int code1 = vector3ToCode(vec1);
            //            //GameObject newCube1 = Instantiate(prefab, vec1, Quaternion.identity);
            //            Debug.Log(code1);

            //            Vector3Int vec2 = new Vector3Int(Mathf.Min(vec.x + 1, size - 1), vec.y, vec.z);
            //            int code2 = vector3ToCode(vec2);
            //            //GameObject newCube2 = Instantiate(prefab, vec2, Quaternion.identity);
            //            Debug.Log(code2);

            //            if (!disjointSet.SameSet(code1, code2) && vec1.x > 0 && vec1.x < size - 1 && vec1.y > 0 && vec1.y < size - 1 && vec1.z > 0 && vec1.z < size - 1 && vec2.x > 0 && vec2.x < size - 1 && vec2.y > 0 && vec2.y < size - 1 && vec2.z > 0 && vec2.z < size - 1)
            //            {
            //                disjointSet.Union(code1, code2);
            //                nonWalls[vec1.x, vec1.y, vec1.z] = true;
            //                frontier[frontier.IndexOf(code1)] = -1;
            //                nonWalls[vec2.x, vec2.y, vec2.z] = true;
            //                frontier[frontier.IndexOf(code2)] = -1;

            //                nonWalls[vec.x, vec.y, vec.z] = true;
            //            }

            //            // Check along y-axis
            //            vec1 = new Vector3Int(vec.x, Mathf.Max(vec.y - 1, 0), vec.z);
            //            code1 = vector3ToCode(vec1);
            //            //newCube1 = Instantiate(prefab, vec1, Quaternion.identity);
            //            Debug.Log(code1);

            //            vec2 = new Vector3Int(vec.x, Mathf.Min(vec.y + 1, size - 1), vec.z);
            //            code2 = vector3ToCode(vec2);
            //            //newCube2 = Instantiate(prefab, vec2, Quaternion.identity);
            //            Debug.Log(code2);

            //            if (!disjointSet.SameSet(code1, code2) && vec1.x > 0 && vec1.x < size - 1 && vec1.y > 0 && vec1.y < size - 1 && vec1.z > 0 && vec1.z < size - 1 && vec2.x > 0 && vec2.x < size - 1 && vec2.y > 0 && vec2.y < size - 1 && vec2.z > 0 && vec2.z < size - 1)
            //            {
            //                disjointSet.Union(code1, code2);
            //                nonWalls[vec1.x, vec1.y, vec1.z] = true;
            //                frontier[frontier.IndexOf(code1)] = -1;
            //                nonWalls[vec2.x, vec2.y, vec2.z] = true;
            //                frontier[frontier.IndexOf(code2)] = -1;

            //                nonWalls[vec.x, vec.y, vec.z] = true;
            //            }



            //            // Check along z-axis
            //            vec1 = new Vector3Int(vec.x, vec.y, Mathf.Max(vec.z - 1, 0));
            //            code1 = vector3ToCode(vec1);
            //            ////newCube1 = Instantiate(prefab, vec1, Quaternion.identity);
            //            Debug.Log(code1);

            //            vec2 = new Vector3Int(vec.x, vec.y, Mathf.Min(vec.z + 1, size - 1));
            //            code2 = vector3ToCode(vec2);
            //            //newCube2 = Instantiate(prefab, vec2, Quaternion.identity);
            //            Debug.Log(code2);

            //            if (!disjointSet.SameSet(code1, code2) && vec1.x > 0 && vec1.x < size - 1 && vec1.y > 0 && vec1.y < size - 1 && vec1.z > 0 && vec1.z < size - 1 && vec2.x > 0 && vec2.x < size - 1 && vec2.y > 0 && vec2.y < size - 1 && vec2.z > 0 && vec2.z < size - 1)
            //            {
            //                disjointSet.Union(code1, code2);
            //                nonWalls[vec1.x, vec1.y, vec1.z] = true;
            //                frontier[frontier.IndexOf(code1)] = -1;
            //                nonWalls[vec2.x, vec2.y, vec2.z] = true;
            //                frontier[frontier.IndexOf(code2)] = -1;

            //                nonWalls[vec.x, vec.y, vec.z] = true;
            //            }

            //            Debug.Log("c1: " + disjointSet.Find(code1) + " c2: " + disjointSet.Find(code2));
            //            Debug.Log(disjointSet.Find(code));
            //        }

            //    }



            int count = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        //walls[i, j, k] = true;
                        //Debug.Log(i + ","  + j + "," +  k);
                        if (!nonWalls[i, j, k])// || i == 0 || i == size - 1 || j == 0 || j == size - 1 || k == 0 || k == size -1)
                        {
                            Vector3 pos = new Vector3(i, j, k);
                            GameObject newCube = Instantiate(prefab, pos, Quaternion.identity);
                            count += 1;
                        }
                        
                        
                    }
                }
            }

            Debug.Log(count);

            //bleh.Remove(Random.Range(0, bleh.Count -1));


            /*for (int i = -size; i < size; i++)
            {
                for (int j = -size; j < size; j++)
                {
                    for (int k = -size; k < size; k++)
                    {
                        Vector3 pos = new Vector3(i, j, k);
                        GameObject newCube = Instantiate(prefab, pos, Quaternion.identity);
                    }
                }
            }*/





            init = true;
        }


        

    }

    // Returns the decoded Vector3
    Vector3Int codeToVector3(int value)
    {
        Vector3Int r = new Vector3Int(value / 100, value % 100 / 10, value % 10);
        //Debug.Log("Vec: " + r);
        return r;
        
    }

    // Returns a coded int
    int vector3ToCode(Vector3Int vector)
    {
        return vector.x * 100 + vector.y * 10 + vector.z;
    }

}

// from: https://pastebin.com/NwvLLu4J
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        //Random rnd = new Random();
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, Random.Range(i, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
