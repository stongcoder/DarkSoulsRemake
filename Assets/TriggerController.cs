using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ResetTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
        Debug.Log("reset");
    }
}
