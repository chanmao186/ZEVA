using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("玩家移动的速度")]
    public float Speed = 2;

    [Header("玩家所受重力")]
    public float Grivaty = 9.8f;

    [Header("玩家的初次跳跃的高度")]
    public float FirstJumpHeight = 13;

    [Header("玩家持续跳跃的速度")]
    public float JumpSpeed = 18;

    [Header("玩家的蓄力时间")]
    public float JumpTime = 0.5f;

    [Header("玩家x轴的缩放比例")]
    public float XScale = 0.2f;

    private float _Grivaty;
    CharacterController cc;
    private float Height;
    private float Width;
    private float _JumpTime = 0;

    private Vector2 PlayerScale;
    private Vector2 PlayerPos;
    private bool CanJump = false;
    // Start is called before the first frame update

    public bool IsGround = true;

    void Start()
    {
        cc =GetComponent<CharacterController>();
        PlayerScale = transform.localScale;
        PlayerPos = transform.position;
    }

    /// <summary>
    /// 物体重力系统
    /// </summary>
    private void GrivatyFunction()
    {
        IsGround = GroundCheck(Vector2.down, cc.height);
        if (!IsGround)
        {
            _Grivaty += Grivaty * Time.fixedDeltaTime;
            if (_Grivaty > Grivaty) _Grivaty = Grivaty;
            PlayerPos.y -= _Grivaty * Time.fixedDeltaTime;           
        }
    }

    private void Move()
    {
        float d = Input.GetAxis("Horizontal");
        if (d != 0)
        {
            //获得玩家要行进的方向
            Vector2 direction = d > 0 ? Vector2.right : Vector2.left;
            float xScale = d > 0 ? this.XScale : -this.XScale;

            PlayerScale.x = xScale;
            
            if(!GroundCheck(direction,cc.radius))
                PlayerPos.x += d * Speed * Time.fixedDeltaTime;            
        }        
    }

    /// <summary>
    /// 检测周围是否有不可穿越的障碍物
    /// </summary>
    /// <param name="direction">方向</param>
    /// <param name="distance">距离</param>
    /// <returns></returns>
    private bool GroundCheck(Vector2 direction, float distance)
    {

        RaycastHit2D hit = Physics2D.Raycast(PlayerPos, direction, distance);
        if (hit)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
    private void Jump()
    {
        //判断玩家是否可以跳跃
        if (!CanJump) return;

        if (Input.GetButtonDown("MyJump"))
        {
            Debug.Log("玩家开始跳跃");
            PlayerPos.y += FirstJumpHeight;
        }
        else if (Input.GetButton("MyJump"))
        {
            _Grivaty = 3;
            _JumpTime += Time.fixedDeltaTime;
            if (_JumpTime < JumpTime)
            {
                PlayerPos.y += (JumpSpeed + Grivaty) * Time.fixedDeltaTime;
            }
            else
            {
                CanJump = false;
            }
        }
        else if (Input.GetButtonUp("MyJump"))
        {
            CanJump = false;
        }
    }

    private void PlayerStateUnpdate()
    {
        transform.position = PlayerPos;

        transform.localScale = PlayerScale;
        if (!CanJump) CanJump = IsGround;

        //在跳跃状态下，检测头顶是否有不可穿越的障碍物
        if (CanJump)
            CanJump = !GroundCheck(Vector2.up, cc.height);
        else
        {
            _JumpTime = 0;
        }
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
        GrivatyFunction();
        PlayerStateUnpdate();
    }
}
