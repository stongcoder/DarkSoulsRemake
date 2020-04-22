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

    private float targetUp;
    private float targetRight;
    private float velocityUp;
    private float velocityRight;
    public bool inputEnabled = true;
}
