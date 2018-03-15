﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpLimiter : MonoBehaviour {

    public Texture warpChargeBarSide;
    public Texture warpChargeBarMiddle;


    public bool canWarp;  // other scripts in player game object must confirm with this that they are able to warp before doing so
    public int maxWarpCharges;
    public int warpCharges;
    public float warpRechargeTime;
    public float warpRechargeTimeProgress;

    // Use this for initialization
    void Start() {
        canWarp = true;
        maxWarpCharges = 5;
        warpCharges = maxWarpCharges;
        warpRechargeTime = 1.5f;
        warpRechargeTimeProgress = warpRechargeTime;

    }

    // Update is called once per frame
    void Update() {

        // Need to be grounded and not in warp to recharge
        // Prevents infinite charge up if the player warps to high places and waits for charge midfall
        if (warpCharges < maxWarpCharges && gameObject.GetComponent<Rigidbody>().velocity.y == 0)
        {
            Recharge();
        }


        if (gameObject.GetComponent<HumanBullet>().bulletMode || warpCharges <= 0)
        {
            canWarp = false;
        } else
        {
            canWarp = true;
        }
		
	}


    void Recharge()
    {
        warpRechargeTimeProgress -= Time.deltaTime;
        if (warpRechargeTimeProgress <= 0)
        {
            warpCharges += 1;
            warpRechargeTimeProgress = warpRechargeTime;
        }
    }


    public void ConsumeCharge()
    {
        warpCharges -= 1;
    }


    // Called upon defeating an enemy
    public void GainMaxChargeAndRefill(int charge = 1)
    {
        maxWarpCharges += charge;
        warpCharges = maxWarpCharges;
    }


    void OnGUI()
    {
        for (int i = 0; i < warpCharges; i++)
        {
            int barSize = 10;
            /*if (i > warpCharges)
            {
                barSize = Mathf.RoundToInt(Mathf.Floor(warpRechargeTime - warpRechargeTimeProgress) * 10 / warpRechargeTime);
            } else
            {
                barSize = 10;
            }*/
            for (int j = 0; j < barSize; j++)
            {
                if (j == 0 || j == barSize - 1)
                {
                    GUI.DrawTexture(new Rect(i * 10 * 5 + j * 5, 50, 5, 15), warpChargeBarSide);
                }
                else
                {
                    GUI.DrawTexture(new Rect(i * 10 * 5 + j * 5, 50, 5, 15), warpChargeBarMiddle);
                }
            }
        }
    }
}
