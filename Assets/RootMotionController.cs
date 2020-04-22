using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    private Animator anim;    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        SendMessageUpwards("OnUpdateRootMotion", (object)anim.deltaPosition);
    }

}
