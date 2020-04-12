using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput playerInput;
    public float walkSpeed = 1;
    public float runMultiPlayer=2;
    public bool isLockPlanar;
    public float jumpSpeed;
    public Vector3 jagSpeed;
    
    private Vector3 jumpVelocity;
    public bool isJagging;
   

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    [SerializeField]
    private Vector3 planarVec;
    
    private void Awake()
    {
        anim = model.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //位移添加线性缓动
        float velocity = playerInput.dMag * (playerInput.run ? 2 : 1);
        anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), velocity, 0.2f));
        //anim.SetFloat("forward", velocity);
        if (playerInput.dMag > 0.1f)
        {
            //为旋转添加球形缓动效果
            Vector3 temp = Vector3.Slerp(model.transform.forward, playerInput.dVec, 0.2f);
            model.transform.forward = temp;
            //Debug.Log("transform" + model.transform.forward + "d:" + playerInput.DVec);

        }
        if (rigid.velocity.magnitude >0.1f )
        {
            anim.SetTrigger("roll");
        }
        if (!isLockPlanar)
        {
            planarVec = model.transform.forward * playerInput.dMag * walkSpeed * (playerInput.run ? runMultiPlayer : 1);
        }
        
        if (playerInput.jump)
        {
            anim.SetTrigger("jump");
   
        }
        
    }
    private void FixedUpdate()        
    {
        //注意使用rigid.position与rigid.velocity的区别
        //rigid.position += movingVec * Time.fixedDeltaTime;
        if (isJagging)
        {
            rigid.velocity = -jagSpeed;
        }
        else
        {
            rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + jumpVelocity ;
        }       
       
        jumpVelocity = Vector3.zero;
        
    }


    ///处理消息
    private void OnJumpEnter()
    {
        playerInput.inputEnabled = false;
        //Debug.Log("jump enter");
        jumpVelocity = new Vector3(0, jumpSpeed, 0);
        isLockPlanar = true;
    }
    private void OnJumpExit()
    {
        
        //Debug.Log("jump exit");
        
    }
    private void IsGround()
    {
       
       
        anim.SetBool("isGround", true);
    }
    private void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }
    private void OnGroundEnter()
    {
        playerInput.inputEnabled = true;
        isLockPlanar = false;
        isJagging = false;
    }
  
    private void OnFallEnter()
    {
        playerInput.inputEnabled = false;
        isLockPlanar = true;       
        
    }
    private void OnJagEnter()
    {
        Debug.Log("jag");
        playerInput.inputEnabled = false;
        isLockPlanar = true;
        isJagging = true;
    }

}
