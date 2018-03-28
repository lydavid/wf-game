using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltCameraClipping : MonoBehaviour {

    GameObject player;
    Camera cam;
    string playerTag;
    float defaultDistance;
    public float closestDistance;
    float curDistance;
    float distToPlayer;

    bool obstructed;

    HumanBullet humanBullet;
    Vector3 originalLocalPosition;

    bool canZoomBackOut = true; // set to false when zooming in, after a time period, set back to true
    float timer = 0.5f;
    public float canZoomBackOutTimer;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponentInChildren<Camera>();
        playerTag = player.tag;
        // default dist to player
        defaultDistance = transform.localPosition.z; //Vector3.Distance(player.transform.position, transform.position);//
        closestDistance = 0.0f; // -1.0f;
        curDistance = defaultDistance;
        distToPlayer = defaultDistance;
        //Debug.Log(distToPlayer);

        humanBullet = player.GetComponent<HumanBullet>();
        originalLocalPosition = transform.localPosition;

        canZoomBackOutTimer = timer;
	}



    // Update is called once per frame
    void FixedUpdate () {

        if (!canZoomBackOut)
        {
            canZoomBackOutTimer -= Time.fixedDeltaTime;
            if (canZoomBackOutTimer <= 0)
            {
                canZoomBackOut = true;
            }
        }

        // update distance to player
        distToPlayer = transform.localPosition.z;//Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(distToPlayer);

        // raycast from player to camera, when something comes between them, move in until it is not
        /*Vector3 pos = cam.WorldToScreenPoint(player.transform.position);
        Ray ray = cam.ScreenPointToRay(pos);
        ray.direction = -ray.direction;
        ray.origin = player.transform.position;*/

        // raycast from camera to player
        // above code runs into problem that the raycast always goes through the player (starts from its front)
        Vector3 pos;
        GameObject rep;
        if (humanBullet.bulletMode)
        {
            rep = player.transform.GetChild(0).gameObject;
        }
        else
        {
            
            rep = player.transform.GetChild(1).gameObject;
        }
        pos = cam.WorldToScreenPoint(rep.transform.position);
        Ray ray = cam.ScreenPointToRay(pos);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

        RaycastHit playerHit;
        //int layerMask1 = 1 << 10; // LayerMask.NameToLayer("Player"); //
        //layerMask1 = ~layerMask1;
        //bool hit = Physics.Raycast(ray, out hitInfo, 10);

        // raycast from camera to player
        if (Physics.Raycast(ray, out playerHit))  // mark certain objects such as crystals/destructible cubes as something to not zoom in for
        {
            //Debug.Log(playerHit.transform.gameObject.name);
            if (playerHit.transform.gameObject.tag != playerTag && playerHit.transform.gameObject.layer != 10) // don't zoom in when covered by an interactable obj, may want to have a list of obj type we don't want to zoom in on, since some interactables like launch pad may obscure player vision
            {
                    // if the camera is not at closest distance from player, zoom in
                    if (distToPlayer < closestDistance)
                    {
                        //Debug.Log(transform.TransformDirection(Vector3.forward));
                        //transform.Translate(transform.TransformDirection(Vector3.forward));
                        TranslateAlongZ(1f);
                        canZoomBackOut = false;
                        canZoomBackOutTimer = timer;
                    }
                
            } else
            {

                if (canZoomBackOut)
                {
                    // we need a condition before we can execute this part, or else it will keep going back and forth between
                    // this section and the section above when player is against wall
                    RaycastHit objHit;
                    Vector3 backwards = transform.TransformDirection(-Vector3.forward) * 100;

                    //Debug.DrawRay(transform.position, backwards, Color.red);
                    //Vector3 pos = cam.WorldToScreenPoint(player.transform.position);
                    //ray = cam.ScreenPointToRay(pos);
                    ray.direction = -ray.direction;
                    ray.origin = rep.transform.position;
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
                    //if (Physics.Raycast(transform.position, backwards, out objHit))

                    // hit all layers except "Player" layer
                    int layerMask = 1 << 12; // LayerMask.NameToLayer("Player"); //
                    layerMask = ~layerMask;

                    if (Physics.Raycast(ray, out objHit, 100, layerMask))
                    {

                        // the problem now seems to be that when the camera is through the wall, the closest obj to cam is
                        // actually whatever is behind that wall, discounting the wall...

                        // one sol is to raycast from player, through camera
                        // problem is that if player is behind a not very tall thing, it won't reset
                        // let's try anyways
                        //Debug.Log(objHit.transform.gameObject.name);
                        float distToClosestObjFromCam = Vector3.Distance(transform.position, objHit.point);
                        //Debug.Log(distToClosestObjFromCam);
                        // if the distance to closest obj from behind cam is bigger than that of the distance needed
                        // to return from current cam dist to player towards default distance to player, then do so
                        if (distToPlayer > defaultDistance && distToClosestObjFromCam >= distToPlayer - defaultDistance)
                        {
                            TranslateAlongZ(-1f);
                        }
                    }
                }
            }
        }

        // reset to default position when no longer the case
        // raycast from camera to some distance behind itself, if whatever it hit's distance is far enough, move
        // camera back up to default distance
        //pos = cam.WorldToScreenPoint(player.transform.position);
        //ray = cam.ScreenPointToRay(pos);
        //ray.direction = -ray.direction;

        //RaycastHit objHit;

        //if (!obstructed)
        //{
        //    Vector3 backwards = transform.TransformDirection(-Vector3.forward) * 10;

        //    Debug.DrawRay(transform.position, backwards, Color.red);
        //    if (Physics.Raycast(transform.position, backwards, out objHit))
        //    {
        //        //Debug.Log(hit.transform.gameObject.name);
        //        //Debug.Log("Square Distance of: " + Mathf.Sqrt((Mathf.Pow(transform.position.z, 2) -  Mathf.Pow(hit.point.z, 2))));

        //        float distToClosestObjFromCam = Vector3.Distance(transform.position, objHit.point);
        //        Debug.Log(distToClosestObjFromCam);
        //        // if the distance is far enough, move camera back towards original position

        //        // if the distance to closest obj from behind cam is bigger than that of the distance needed
        //        // to return from current cam dist to player to default, then do so
        //        if (distToPlayer < defaultDistance && distToClosestObjFromCam >= defaultDistance - distToPlayer)
        //        {
        //            /*if (transform.localPosition.z - 1 > defaultDistance)
        //            {

        //            }*/
        //            //Debug.Log("e");
        //            //transform.Translate(transform.TransformDirection(-Vector3.forward));
        //            transform.Translate(new Vector3(0, 0, -1));

        //        }
        //    }
        //}

    }

    void TranslateAlongZ(float amount)
    {
        //Debug.Log(amount);
        //amount = Mathf.Clamp(transform.localPosition.z + amount, defaultDistance, closestDistance);
        //Debug.Log(amount);
        transform.Translate(new Vector3(0, 0, amount));
        
        // Main relative postion to player
        transform.localPosition = new Vector3(originalLocalPosition.x, originalLocalPosition.y, Mathf.Clamp(transform.localPosition.z, defaultDistance, closestDistance));
        distToPlayer = transform.localPosition.z;
    }
}
