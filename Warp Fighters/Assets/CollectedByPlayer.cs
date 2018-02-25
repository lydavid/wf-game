using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedByPlayer : MonoBehaviour
{


    public float speed;

    GameObject player;


    // Use this for initialization
    void Start()
    {
        speed = 1f;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Moves towards player, then upon contact, destroy this after granting player an ability
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("bleh");
            Destroy(gameObject);
        }
    }
}
