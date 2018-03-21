using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPadButton : MonoBehaviour
{
    public static bool up;
    public static bool down;
    public static bool left;
    public static bool right;


    private bool dpadPressed = false;

    void Update()
    {
        float x = Input.GetAxis("D-Pad X Axis");
        float y = Input.GetAxis("D-Pad Y Axis");

        if (x != 0 && dpadPressed == false)
        {

            dpadPressed = true;
            right = (x > 0) ? true : false;
            left = (x < 0) ? true : false;
            
        }
        if (y != 0 && dpadPressed == false)
        {
            
            dpadPressed = true;
            up = (y > 0) ? true : false;
            down = (y < 0) ? true : false;
            
        }
        if (x == 0 && y == 0)
        {
            dpadPressed = false;
            up = down = left = right = false;
        }
    }
}
