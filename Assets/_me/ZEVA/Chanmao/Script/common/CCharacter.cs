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

    [Tooltip("触碰到那些带标签的物体，玩家会受到伤害")]
    [Header("被攻击的标签")]
    public string[] AttackedTag;
    //小怪的动态生命值
    protected float _Heath;
    protected float _Strenth;
    protected Rigidbody2D _rigidbody2D;
    protected Vector3 Scale;
    protected float xScale;
    protected Animator Ani;
    protected virtual void Start()
    {
        Ani = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Scale = transform.localScale;
        xScale = Scale.x;
        //Debug.Log("CCharater执行");
        _Heath = Heath;
    }

    /// <summary>
    /// 小怪物被攻击,默认受到的伤害为1
    /// </summary>
    public void Attacked()
    {
        Attacked(1);
    }

    /// <summary>
    /// 小怪物被攻击
    /// </summary>
    /// <param name="Hurt">受到的伤害值</param>
    public void Attacked(float Hurt)
    {
        //播放被攻击的效果
        AttackedEffection();
        _Heath -= Hurt;
        if (_Heath < 0)
        {
            Death();
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        foreach (string tag in AttackedTag)
        {
            if (collision.transform.CompareTag(tag))
            {
                Attacked(collision.transform.GetComponent<zHurt>().Hurt);
                if (Heath > 0)
                    AttackedEffection();
            }
        }
    }

    public virtual void AttackedEffection() { }

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
    // Update is called once per frame
}