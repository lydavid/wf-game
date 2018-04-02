using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpLimiter : MonoBehaviour {

    //public Texture warpChargeBarSide;
    //public Texture warpChargeBarMiddle;

    public bool canWarp;  // other scripts in player game object must confirm with this that they are able to warp before doing so
    public int maxWarpCharges;
    int warpCharges;
    float warpRechargeTime;
    float warpRechargeTimeProgress;

    Text NumWarpChargesText;
    //Text WarpChargesDisplayText;
    //RectTransform WarpChargesDisplayBackdrop;

    TPSPlayerController TPSPlayerController;

    public int kills; // weird to put here but since enemies calls a method here on death, it is most convenient

    // Use this for initialization
    void Start() {
        canWarp = true;

        if (maxWarpCharges == 0) // allows for manually setting this in editor
        {
            maxWarpCharges = 5;
        }

        warpCharges = maxWarpCharges;
        warpRechargeTime = 0.5f;
        warpRechargeTimeProgress = warpRechargeTime;


        NumWarpChargesText = GameObject.Find("NumWarpChargesText").GetComponent<Text>();
       // WarpChargesDisplayText = GameObject.Find("WarpChargesDisplayText").GetComponent<Text>();
       // WarpChargesDisplayBackdrop = GameObject.Find("WarpChargesDisplayBackdrop").GetComponent<RectTransform>();

        TPSPlayerController = GetComponent<TPSPlayerController>();

        UpdateUI();
    }


    // Update is called once per frame
    void Update() {

        // Need to be grounded and not in warp to recharge
        // Prevents infinite charge up if the player warps to high places and waits for charge midfall
        if (warpCharges < maxWarpCharges && (TPSPlayerController.grounded || gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero))
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


    // Try to not do this every frame, only call this when we know it will change
    void UpdateUI()
    {
        // NumWarpChargesText
        //string zeroInFront = "";
        //if (warpCharges < 10)
        //{
        //    zeroInFront = "0";
        //}
        // NumWarpChargesText.text = zeroInFront + warpCharges.ToString();

        // WarpChargesDisplayText
        // WarpChargesDisplayText.text = "";
        // for (int i = 0; i < warpCharges; i++)
        //{
        //     WarpChargesDisplayText.text += "|";
        //}

        NumWarpChargesText.text = warpCharges.ToString(); 
        // WarpChargesDisplayTextBackdrop
        // size varies on maxWarpCharges
        //WarpChargesDisplayBackdrop.sizeDelta = new Vector2(maxWarpCharges * 8, 40);
    }


    void Recharge()
    {
        warpRechargeTimeProgress -= Time.deltaTime;
        if (warpRechargeTimeProgress <= 0)
        {
            warpCharges += 1;
            warpRechargeTimeProgress = warpRechargeTime;
            UpdateUI();
        }
    }


    public void ConsumeCharge()
    {
        warpCharges -= 1;
        UpdateUI();
    }


    // Called upon defeating an enemy
    // cause this is the only scenario this is called, increment kill count
    public void GainMaxChargeAndRefill(int charge = 1)
    {
        kills++;
        maxWarpCharges += charge;
        warpCharges = maxWarpCharges;
        UpdateUI();
    }


    //void OnGUI()
    //{
    //    for (int i = 0; i < warpCharges; i++)
    //    {
    //        int barSize = 10;
    //        /*if (i > warpCharges)
    //        {
    //            barSize = Mathf.RoundToInt(Mathf.Floor(warpRechargeTime - warpRechargeTimeProgress) * 10 / warpRechargeTime);
    //        } else
    //        {
    //            barSize = 10;
    //        }*/
    //        for (int j = 0; j < barSize; j++)
    //        {
    //            if (j == 0 || j == barSize - 1)
    //            {
    //                GUI.DrawTexture(new Rect(i * 10 * 5 + j * 5, 50, 5, 15), warpChargeBarSide);
    //            }
    //            else
    //            {
    //                GUI.DrawTexture(new Rect(i * 10 * 5 + j * 5, 50, 5, 15), warpChargeBarMiddle);
    //            }
    //        }
    //    }
    //}
}
