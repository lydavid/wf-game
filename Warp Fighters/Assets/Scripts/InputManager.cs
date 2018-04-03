using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public enum ControllerType { pc, xbox, ps };

    public static bool WarpButton(ControllerType controllerType)
	{
        bool warp = false;
        switch (controllerType)  // we do it this way to avoid conflicts of same input button across xbox/ps
        {
            case (ControllerType.xbox):
                warp |= Input.GetButtonDown("A Button");
                break;
                
            case (ControllerType.ps):
                warp |= Input.GetButtonDown("X Button");
                break;

            default:
                warp |= Input.GetMouseButtonDown(0);
                break;
        }
        return warp || Input.GetMouseButtonDown(0); // no matter what controller type we are using, pc controls should be included
    }

    public static bool LockOnButton(ControllerType controllerType)
    {
        bool lockOn = false;
        switch (controllerType)  // we do it this way to avoid conflicts of same input button across xbox/ps
        {
            case (ControllerType.xbox):
                lockOn |= Input.GetAxis("Right Trigger") > 0;
                break;

            case (ControllerType.ps):
                lockOn |= Input.GetAxis("R2 (PS4)") > 0;
                break;
            default:
                lockOn |= Input.GetMouseButton(1);
                break;
        }
        return lockOn || Input.GetMouseButton(1);
    }

    public static float MoveX()
    {
        float moveX = 0.0f;
        moveX += Input.GetAxis("Horizontal");
        return Mathf.Clamp(moveX, -1.0f, 1.0f);
    }

    public static float MoveY()
    {
        float moveY = 0.0f;
        moveY += Input.GetAxis("Vertical");
        return Mathf.Clamp(moveY, -1.0f, 1.0f);
    }

    public static float LookX(ControllerType controllerType)
    {
        float lookX = 0.0f;

        switch (controllerType)
        {
            case ControllerType.xbox:
                lookX += Input.GetAxis("Right Stick X");
                break;
            case ControllerType.ps:
                lookX += Input.GetAxis("Right Stick X (PS4)");
                break;
        }
        
        lookX += Input.GetAxis("Mouse X");
        return Mathf.Clamp(lookX, -1.0f, 1.0f);
    }

    public static float LookY()
    {
        float lookY = 0.0f;
        lookY += Input.GetAxis("Right Stick Y");
        lookY += Input.GetAxis("Mouse Y");
        return Mathf.Clamp(lookY, -1.0f, 1.0f);
    }


    public static bool MenuButton()
    {
        return Input.GetButtonDown("Menu Button") || Input.GetKeyDown("return");
    }

    public static bool WindowButton()
    {
        return Input.GetButtonDown("Window Button") || Input.GetKeyDown("left shift");
    }
}
