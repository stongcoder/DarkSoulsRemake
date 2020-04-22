using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTest : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.Rotate(Input.GetAxis("Vertical") * speed * Time.deltaTime,0,0);
       
        transform.eulerAngles=new Vector3 (Mathf.Clamp(transform.eulerAngles.x, -30, 30),0,0);
    }

}