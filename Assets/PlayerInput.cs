﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("=====KeySetting")]
    public string keyUP = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA="left shift";
    public string keyB="space";
    public string keyC;
    public string keyD;

    [Header("=====Output Signals")]
    public float dUp;
    public float dRight;
    public float dMag;
    public Vector3 dVec;
    //1.pressing signal
    public bool run;
    //2.trigger once signal
    public bool jump;
    //3.double trigger

    private float targetUp;
    private float targetRight;
    private float velocityUp;
    private float velocityRight;
    public bool inputEnabled = true;
    private void Update()
    {
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
    }
    //方形转化为圆坐标
    private Vector2 SquareToCircle(float Dright, float Dup)
    {
        Vector2 output = Vector2.zero;
        output.x = Dright * Mathf.Sqrt(1 - (Dup * Dup) / 2.0f);
        output.y = Dup * Mathf.Sqrt(1 - (Dright * Dright) / 2.0f);
        return output;
    }

}
