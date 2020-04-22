using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IUserInput
{
    [Header("=====KeySetting")]
    public string keyUP = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA="left shift";
    public string keyB="space";
    public string keyC="j";
    public string keyD;

    public string ArrowUp = "up";
    public string ArrowDown = "down";
    public string ArrowLeft = "left";
    public string ArrowRight = "right";

    private void Update()
    {
        cUp = (Input.GetKey(ArrowUp) ? 1 : 0 + (Input.GetKey(ArrowDown) ? -1 : 0));
        cRight = (Input.GetKey(ArrowRight) ? 1:0 + (Input.GetKey(ArrowLeft) ? -1 : 0));
        targetUp = (Input.GetKey(keyUP) ? 1 : 0) + (Input.GetKey(keyDown) ? -1 : 0);
        targetRight = (Input.GetKey(keyLeft) ? -1 : 0) + (Input.GetKey(keyRight) ? 1 : 0);
        if (!inputEnabled)
        {
            targetUp = 0;
            targetRight = 0;
        }

        dUp = Mathf.SmoothDamp(dUp, targetUp, ref velocityUp, 0.1f);        
        dRight = Mathf.SmoothDamp(dRight, targetRight, ref velocityRight, 0.1f);
        Vector2 temp = SquareToCircle(dRight, dUp);
        dUp = temp.y;
        dRight = temp.x;
        dMag = Mathf.Sqrt(dUp * dUp + dRight * dRight);       
        dVec = dRight * transform.right + dUp * transform.forward;
        run = Input.GetKey(keyA);
        if (Input.GetKeyDown(keyB))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        if (Input.GetKeyDown(keyC))
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        
    }
   
}
