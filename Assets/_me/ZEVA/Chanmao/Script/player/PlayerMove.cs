using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2.0f;
    Rigidbody2D _rigidbody2D;

    [Header("玩家的蓄力时间")]
    public float CastTime = 0.3f;

    [Header("初次跳跃的力")]
    public float FirstJumpPower = 700;

    [Header("持续跳跃的力")]
    public float JumpPower = 120;
    private float Force;

    private float _CastTime;
    //设置一个变量检查玩家什么时候可以跳起

    private bool isGround;
    public float runSpeed = 1.5f;
    //奔跑的速度
    private float rs;
    private bool CanJump1;
    private bool CanJump2;
    private PlayAnimation Ani;
    void Start()
    {
        isGround = false;
        _CastTime = 0;
        rs = 1f;
        Ani = GetComponent<PlayAnimation>();
        CanJump1 = false;
        CanJump2 = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Jump()
    {
        if (Input.GetButtonUp("MyJump"))
        {
            CanJump1 = false;
            CanJump2 = true;
            _CastTime = 0;
        }
        if (!CanJump1) return;
        if (Input.GetButtonDown("MyJump"))
        {
            _rigidbody2D.AddForce(Vector2.up * FirstJumpPower);
        }
        else if (Input.GetButton("MyJump") && CanJump2)
        {
            _CastTime += Time.fixedDeltaTime;
            //设置蓄力时间的上限
            if (_CastTime > CastTime)
            {
                CanJump1 = false;
                CanJump2 = false;
                _CastTime = 0;
            }
            _rigidbody2D.AddForce(Vector2.up * JumpPower);
        }       
    }

    private void Move()
    {
        //判断玩家当前是否在奔跑
        if (Input.GetButton("Jump"))
        {
            rs = runSpeed;
        }
        else
        {
            rs = 1;
        }

        float i = Input.GetAxis("Horizontal");
        //控制认为左右移动        
        if (i != 0)
        {
            float s = i > 0 ? 1 : -1;
            transform.Translate(Vector2.right * speed * rs * s * Time.deltaTime, Space.World);
            GetComponent<Rigidbody2D>().simulated = true;
            transform.localScale = new Vector2(s * 2, 2);
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

        RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), direction, distance);
        foreach (RaycastHit2D e in hit)
        {
            if (e.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    private void IsGroundUpdate()
    {
        isGround = GroundCheck(Vector2.down, 1f);
        if (!CanJump1)
        {
            CanJump1 = isGround;
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {  
        Jump();
        IsGroundUpdate();
    }
}
