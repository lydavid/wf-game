using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPadButton : MonoBehaviour
{
    public static bool up;
    public static bool down;
    public static bool left;
    public static bool right;

    private float x, y;
    private bool dpadYPressed = false;
    private bool dpadXPressed = false;
    public static int countX;
    public static int countY;

    void Update()
    {
        x = Input.GetAxisRaw("D-Pad X Axis");
        x += Input.GetAxis("Horizontal");
        x = Mathf.Clamp(x, -1f, 1f);
        y = Input.GetAxis("D-Pad Y Axis");
        y += Input.GetAxisRaw("Vertical");
        y = Mathf.Clamp(y, -1f, 1f);

        if (x != 0 && dpadXPressed == false)
        {


            if (x > 0)//(x == 1)
            {
                countX += 1;
                right = true;
                   
            }
            else if (x < 0)//(x == -1)
            {
                countX -= 1;
                left = true;
            }
            dpadXPressed = true;
        } else
        {
            left = right = false;
        }
        if (x == 0)
        {
            dpadXPressed = false;
            left = right = false;
        }

        if (y != 0 && dpadYPressed == false)
        {
            if (y > 0)//(y == 1)
            {
                countY += 1;
                up = true; 
            }
            else if (y < 0)//(y == -1)
            {
                countY -= 1;
                down = true;
            }
            dpadYPressed = true;
        } else
        {
            up = down = false;
        }
        if (y == 0)
        {
            dpadYPressed = false;
            up = down = false;
        }

    }
}
