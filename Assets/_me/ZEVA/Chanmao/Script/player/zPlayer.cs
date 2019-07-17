using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zPlayer : CCharacter
{
    // Start is called before the first frame update

    

    [Header("玩家的蓄力时间")]
    public float CastTime = 0.3f;

    //public float FirstJumpPower = 700;  
    //public float JumpPower = 120;
    [Header("初次跳跃的速度")]
    public float FirstJumpVeloctiy = 30;

    [Header("持续跳跃的速度")]
    public float JumpVeloctiy = 1;

    [Header("攻击的频率(持续按照攻击键，一秒攻击几次)")]
    public float AttacksFrequence = 2;

    private float AttacksTime;
    private float _AttacksTime = 0;

    private float _CastTime;
    //设置一个变量检查玩家什么时候可以跳起

    private bool isGround;
    public float runSpeed = 1.5f;
    //奔跑的速度
    private float rs;
    private bool CanJump1;
    private bool CanJump2;

    //记录攻击的次数
    private int AttacksNum;
    
    //private Vector2 Veo
    protected override void Start()
    {
        base.Start();
        AttacksTime = 1 / AttacksFrequence;
        isGround = false;
        _CastTime = 0;
        rs = 1f;
        
        CanJump1 = false;
        CanJump2 = true;
        
    }

    private void Jump()
    {
        //CanJump2的功能，防止玩家一直按住跳跃键时，玩家向上跳跃
        if (Input.GetButtonUp("MyJump"))
        {
            //此时播放玩家下落的动画
            CanJump1 = false;
            CanJump2 = true;
            _CastTime = 0;
        }
        if (!CanJump1) return;
        if (Input.GetButtonDown("MyJump"))
        {
            //播放玩家跳跃的动画

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, FirstJumpVeloctiy);
        }
        else if (Input.GetButton("MyJump") && CanJump2)
        {
            //播放玩家跳跃的动画
            _CastTime += Time.fixedDeltaTime;
            //设置蓄力时间的上限
            if (_CastTime > CastTime)
            {
                //播放玩家下落的动画  

                CanJump1 = false;
                CanJump2 = false;
                _CastTime = 0;
            }
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpVeloctiy);
        }
    }

    private void Move()
    {
        float i = Input.GetAxis("Horizontal");
        //控制认为左右移动        
        if (i != 0)
        {
            Ani.SetFloat("Speed", 1f);
            // 获取当前玩家要前进的方向
            float d = i > 0 ? 1 : -1;

            transform.Translate(Vector2.right * WalkSpeed * rs * d * Time.deltaTime, Space.World);

            Scale.x = xScale * -d;
            transform.localScale = Scale;

            GetComponent<Rigidbody2D>().simulated = true;
        }
        else
        {
            Ani.SetFloat("Speed", 0f);
        }
    }

    /// <summary>
    /// 检测玩家周围的障碍物
    /// </summary>
    /// <param name="direction">方向</param>
    /// <param name="distance">距离</param>
    /// <returns></returns>
    private bool GroundCheck(Vector2 direction, float distance)
    {
        return zc.zCheck.Collider2DCheck(new Vector2(transform.position.x, transform.position.y), direction,distance,"Ground");
        //RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y));
        //foreach (RaycastHit2D e in hit)
        //{
        //    if (e.collider.CompareTag("Ground"))
        //    {
        //        return true;
        //    }

        //}
        //return false;
    }

    private void IsGroundUpdate()
    {

        isGround = GroundCheck(Vector2.down, 1f);
        if (!CanJump1)
        {
            CanJump1 = isGround;
        }
        else
        {
            //检测头顶上是否有物体
            CanJump1 = !GroundCheck(Vector2.up, 1f);
        }
    }

    /// <summary>
    /// 根据条件，播放玩家跳跃的动画
    /// </summary>
    private void PlayJumpAimation()
    {
        //检测玩家是否再地面上，若不再地面上则执行跳跃的代码
        if (!isGround)
        {
            bool JumpState = _rigidbody2D.velocity.y > 0;
            Ani.SetBool("JumpUp", JumpState);
            Ani.SetBool("JumpDown", !JumpState);
        }
    }

    /// <summary>
    /// 寻找攻击的方向
    /// </summary>
    private void SeekAttackDirection()
    {
        Vector2 ms = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 d = ms - new Vector2(transform.position.x, transform.position.y);
        float angle = Vector2.Angle(new Vector2(-Scale.x, 0), d);

        if (angle > 105)
        {
            Scale.x = -Scale.x;
            transform.localScale = Scale;
        }
        //待用代码，计算角色360攻击的动画，需要时启用
        //angle = ms.y > transform.position.y ? angle : -angle;
        //Debug.Log("当前的角度为：" + angle);
        //Ani.SetBool("Attack", true);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SeekAttackDirection();
            AttacksNum = 1;
        }
        else if (Input.GetMouseButton(0))
        {
            SeekAttackDirection();
            _AttacksTime += Time.deltaTime;
            AttacksNum = 1;
            if (_AttacksTime > AttacksTime)
            {
                _AttacksTime = 0;
                AttacksNum = 2;
            }
        }
        else
        {
            AttacksNum = 0;
        }
        Ani.SetInteger("Attack", AttacksNum);
        //Debug.Log(AttacksNum);
    }

    /// <summary>
    /// 获取玩家的状态，检测现在玩家是否再地上
    /// </summary>
    /// <returns></returns>
    public bool GetIsGround()
    {
        return isGround;
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        Jump();
        Attack();
        IsGroundUpdate();
        PlayJumpAimation();
    }
}
