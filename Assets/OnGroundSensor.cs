using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capCol;
    private Vector3 point1;
    private Vector3 point2;
    private float radius;    
    void Start()
    {
        radius = capCol.radius;
    }
    private void FixedUpdate()
    {
        Collider[] overlapObjs;
        point1 = transform.position + Vector3.up * (radius-0.5f);
        point2 = transform.position + Vector3.up * (capCol.height - radius-0.5f);
        overlapObjs = Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Ground"));
        if (overlapObjs.Length > 0)
        {
            SendMessageUpwards("IsGround");

        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }
}
