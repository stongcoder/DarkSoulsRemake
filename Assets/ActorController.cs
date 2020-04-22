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
    public float jabMultiplier=1;
    public float attackMultiplier = 1;
    public Vector3 thrustVelocity;
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;
    public float lerpTarget;
    public Vector3 deltaPos;

    
    private Vector3 jumpVelocity;  
    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    [SerializeField]
    private Vector3 planarVec;
    private CapsuleCollider col;
   
    private void Awake()
    {
        anim = model.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
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
        if (playerInput.attack)
        {

            if (CheckState("walkBlend") && !CheckNextState("jump"))
            {

                anim.SetTrigger("attack");
            }
          
        }
        
    }
    private void FixedUpdate()        
    {
        //注意使用rigid.position与rigid.velocity的区别
        //rigid.position += movingVec * Time.fixedDeltaTime;
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + jumpVelocity+thrustVelocity;
        deltaPos = Vector3.zero;
        jumpVelocity = Vector3.zero;
        thrustVelocity = Vector3.zero;
    }
    private bool CheckState(string stateName,string layerName="Base")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        //var temp = anim.GetCurrentAnimatorStateInfo(layerIndex);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);        //缺点：状态机过渡时，状态不变
       
        return result;
    }
    private bool CheckNextState(string stateName, string layerName = "Base")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        //var temp = anim.GetCurrentAnimatorStateInfo(layerIndex);       
        bool result = anim.GetNextAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }
    #region OnAnimationStateChange
    ///处理消息
    private void OnJumpEnter()
    {
        playerInput.inputEnabled = false;

        jumpVelocity = new Vector3(0, jumpSpeed, 0);
        isLockPlanar = true;
    }
    private void OnJumpExit()
    {

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
        col.material = frictionOne;
    }
    private void OnGroundExit()
    {
        col.material = frictionZero;
    }
    private void OnFallEnter()
    {
        playerInput.inputEnabled = false;
        isLockPlanar = true;

    }
    private void OnJabEnter()
    {
        playerInput.inputEnabled = false;
        isLockPlanar = true;

    }
    private void OnJabUpdate()
    {
        //Debug.Log(anim.GetFloat("jabVelocity"));
        thrustVelocity = anim.GetFloat("jabVelocity") * -model.transform.forward * jabMultiplier;
        //Debug.Log(thrustVelocity);
    }
    private void OnAttackEnter()
    {
        playerInput.inputEnabled = false;
        //isLockPlanar = true;
 
        lerpTarget = 1.0f;
    }

    private void OnAttackExit()
    {
        
    }   
    private void OnAttackUpdate()
    {
        //Debug.Log(anim.GetFloat("attackVelocity"));
        thrustVelocity = anim.GetFloat("attackVelocity") * model.transform.forward * attackMultiplier;
        float value = Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"),value);
    }
    private void OnAttackIdleEnter()
    {
        playerInput.inputEnabled = true;
        lerpTarget = 0;
    }
    private void OnAttackIdleUpdate()
    {
        float value = Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), value);
    }
    public void OnUpdateRootMotion(object deltaPos)
    {
        if (CheckState("slash3", "Attack"))
        {
            this.deltaPos = (Vector3)deltaPos;
            print((Vector3)deltaPos);
        }
    }
    #endregion
}
