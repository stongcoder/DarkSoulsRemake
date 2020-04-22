using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    [Header("=====Output Signals")]
    public float dUp;
    public float dRight;
    public float cRight;
    public float cUp;
    public float dMag;
    public Vector3 dVec;
    //1.pressing signal
    public bool run;
    //2.trigger once signal
    public bool attack;
    public bool jump;
    //3.double trigger

    protected float targetUp;
    protected float targetRight;
    protected float velocityUp;
    protected float velocityRight;
    public bool inputEnabled = true;

    //方形转化为圆坐标
    protected Vector2 SquareToCircle(float Dright, float Dup)
    {
        Vector2 output = Vector2.zero;
        output.x = Dright * Mathf.Sqrt(1 - (Dup * Dup) / 2.0f);
        output.y = Dup * Mathf.Sqrt(1 - (Dright * Dright) / 2.0f);
        return output;
    }
}
