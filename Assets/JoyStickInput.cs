using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickInput :IUserInput
{    
    private void Update()
    {
        cUp = -1*Input.GetAxis("Axis5");
        cRight = Input.GetAxis("Axis4");
        targetUp = Input.GetAxis("YAxis");
        targetRight = Input.GetAxis("XAxis");
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
        run = Input.GetButton("BtnA");
        if (Input.GetButtonDown("BtnX"))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        if (Input.GetButtonDown("RB"))
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        
    }
  

}
