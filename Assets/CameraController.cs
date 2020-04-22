using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public IUserInput pi;
    public Transform playerHandle;
    public Transform model;
    public float hMult = 1;
    public float vMult = 1;
    public float cameraDampValue=0.05f;

    private Vector3 cameraDampVelocity;
    public Transform mainCamera;
    private Vector3 tempVec;
    private Vector3 tempEuler;

    void Start()
    {
        IUserInput[] inputs = transform.GetComponentsInParent<IUserInput>();        
        foreach (var i in inputs)
        {
            if (i.enabled)
            {
                pi = i;
                break;
            }
        }
        playerHandle = pi.transform;
        model = playerHandle.GetComponent<ActorController>().model.transform;
        tempEuler = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {

        tempVec = model.eulerAngles;
        playerHandle.localEulerAngles += new Vector3(0, pi.cRight * Time.fixedDeltaTime * hMult, 0);
        model.eulerAngles = tempVec;

        tempEuler -= new Vector3(pi.cUp * Time.fixedDeltaTime * vMult, 0, 0);
        tempEuler.x = Mathf.Clamp(tempEuler.x, -40, 30);
        transform.localEulerAngles = tempEuler;

        //mainCamera.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        mainCamera.eulerAngles = transform.eulerAngles;
        mainCamera.position = Vector3.Lerp(mainCamera.position, transform.position, cameraDampValue);
    }
}
