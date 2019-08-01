using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zItem : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("游戏管理器")]
    public ZController zc;
    // Start is called before the first frame update
    [Header("生命值")]
    public float Heath = 2;

    [Header("受到伤害时被弹开的力")]
    public float AttackedForce = 0;
    //小怪的动态生命值
    protected float _Heath;

    protected Rigidbody2D _rigidbody2D;
    protected virtual void Start()
    {
        if (!zc)
        {
            zc = GameObject.Find("GameManager").GetComponent<ZController>();
        }
        _Heath = Heath;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public virtual void AttackedEffection()
    {
        Debug.Log("玩家被攻击了");
    }
    protected virtual void DeathEffection() { }
    /// <summary>
    /// 小怪物死亡
    /// </summary>
    public virtual void Death()
    {
        DeathEffection();
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

        if (AttackedForce > 0&&_rigidbody2D)
            _rigidbody2D.AddForce(de * AttackedForce);

        _Heath -= Hurt;
        if (_Heath < 0)
        {
            Death();
        }
    }
}
