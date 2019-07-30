using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacter : MonoBehaviour
{
    [Header("游戏管理器")]
    public ZController zc;
    // Start is called before the first frame update
    [Header("生命值")]
    public float Heath = 2;

    [Header("体力值")]
    public float Strenth = 1;

    [Header("移动速度")]
    public float WalkSpeed = 5;

    [Header("跑步速度")]
    public float RunSpeed = 5;

    [Header("所持有的武器")]
    public GameObject Weapon;

    [Header("身体节点节点")]
    public Transform Head;
    public Transform foot;

    [Header("怪物是否受到重力影响")]
    public bool isGrivaty = true;

    [Header("怪物受到的重力影响")]
    public float Grivaty = 9.8f;

    [Header("受到伤害时被弹开的力")]
    public float AttackedForce = 0;
    //小怪的动态生命值
    protected float _Heath;
    protected float _Strenth;
    protected bool isGround;
    protected Vector3 Scale;
    protected float xScale;
    protected Animator Ani;
    protected Coroutine _time;
    protected Coroutine action;
    protected Vector2 currentPos;
    protected SpriteRenderer sprite;
    protected Collider2D coll;
    protected bool isWalk;
    protected int Direction;
    protected float currentYSpeed;
    protected float checkDistance;
    protected Rigidbody2D _rigidbody2D;

    protected virtual void Start()
    {
        if (!zc)
        {
            zc = GameObject.Find("GameManager").GetComponent<ZController>();
        }
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _time = null;
        checkDistance = GetComponent<SpriteRenderer>().size.x * transform.localScale.x * 0.5f;
        Ani = GetComponent<Animator>();
        Scale = transform.localScale;
        xScale = Scale.x;
        //Debug.Log("CCharater执行");
        _Heath = Heath;
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentYSpeed = 0;
    }



    /// <summary>
    /// 小怪物被攻击
    /// </summary>
    /// <param name="Hurt">受到的伤害值</param>
    public void Attacked(float Hurt, Transform AttackPoint)
    {
        Debug.Log("受到" + Hurt + "伤害");
        //播放被攻击的效果
        AttackedEffection();
        Vector2 de = AttackPoint.position - transform.position;

        if(AttackedForce>0)
        _rigidbody2D.AddForce(de * AttackedForce);

        _Heath -= Hurt;
        if (_Heath < 0)
        {
            Death();
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    foreach (string tag in AttackedTag)
    //    {
    //        if (collision.transform.CompareTag(tag))
    //        {
    //           // Attacked(collision.transform.GetComponent<zHurt>().Hurt);
    //            //if (Heath > 0)
    //                AttackedEffection();
    //        }
    //    }
    //}
    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag(tag))
    //    {
    //        // Attacked(collision.transform.GetComponent<zHurt>().Hurt);
    //        //if (Heath > 0)
    //        Debug.Log("2");
    //        //AttackedEffection();
    //    }
    //}

    //protected virtual void OnGround()
    //{
    //    if (isGround)
    //    {
    //        transform.Translate(0, -Grivaty, 0);
    //    }
    //}

    public virtual void AttackedEffection()
    {
        Debug.Log("玩家被攻击了");
    }

    /// <summary>
    /// 死亡的动画
    /// </summary>
    public virtual void DeathEffection() { }
    /// <summary>
    /// 小怪物死亡
    /// </summary>
    public virtual void Death()
    {
        DeathEffection();
    }

    /// <summary>
    /// 将物体移动到指定的位置
    /// </summary>
    /// <param name="pos">指定的位置</param>
    protected void SetToPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    /// <summary>
    /// 将物体移动到指定的点
    /// </summary>
    /// <param name="point">指定的点</param>
    protected void SetToPoint(Transform point)
    {
        transform.position = point.position;
    }

    /// <summary>
    /// 模拟的重力代码
    /// </summary>
    protected void myGrivaty()
    {
        if (isGrivaty && !isGround)
        {
            currentYSpeed -= Grivaty;
            //VerticalChange(-Grivaty);
            //currentYSpeed = 0;
        }
    }
    protected void WalkGroundCheck()
    {
        isWalk = !zc.zCheck.Collider2DCheck(new Vector2(Head.position.x, Head.position.y), Vector2.right * Direction, checkDistance, "Ground", "Ground")
        && !zc.zCheck.Collider2DCheck(new Vector2(foot.position.x, foot.position.y), Vector2.right * Direction, checkDistance, "Ground", "Ground");
    }

    protected void VerticalChange()
    {
        VerticalChange(currentYSpeed);
        currentYSpeed = 0;
    }
    /// <summary>
    /// 垂直方向的位置变化
    /// </summary>
    /// <param name="speed"></param>
    protected void VerticalChange(float speed)
    {
        currentPos = transform.position;
        currentPos.y += speed * Time.fixedDeltaTime;
        transform.position = currentPos;
    }
    protected void isGroundUpdate()
    {
        isGround = zc.zCheck.Collider2DCheck(new Vector2(foot.position.x, foot.position.y), Vector2.down, 0.1f, "Ground", "Ground");
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
    // Update is called once per frame
}