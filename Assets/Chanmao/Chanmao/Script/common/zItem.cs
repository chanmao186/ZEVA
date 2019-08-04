using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class zItem : MonoBehaviour
{
    [Header("死亡后执行的事件")]
    public UnityEvent DeathEvent;
    // Start is called before the first frame update
    [Header("游戏管理器")]
    public ZController zc;
    // Start is called before the first frame update
    [Header("生命值")]
    public float Heath = 2;

    [Header("受到伤害时被弹开的力")]
    public float AttackedForce = 0;

    [Header("是否受到伤害")]
    protected bool isReceiveHurt = true;
    //小怪的动态生命值
    protected float _Heath;

    //protected zItem Self;
    protected Rigidbody2D _rigidbody2D;
    protected virtual void Start()
    {
        if (!zc)
        {
            zc = GameObject.Find("GameManager").GetComponent<ZController>();
        }
        //Self = this;
        _Heath = Heath;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    protected virtual void AttackedEffection(Transform AttackPoint)
    {
        Debug.Log(transform.name+"被攻击了");       
    }
    protected virtual void DeathEffection() {
        Debug.Log(transform.name+"死亡");
        DeathEvent?.Invoke();
    }
    /// <summary>
    /// 小怪物死亡
    /// </summary>
    public virtual void Death()
    {
        ResetParameter();
        //Debug.Log("动作重置");
        //_ = this as zBoss;
        DeathEffection();
    }

    /// <summary>
    /// 获得角色当前的血量
    /// </summary>
    /// <returns></returns>
    public float GetCurrentHeath()
    {
        return _Heath;
    }

    public void SetCurrentHeath(float Heath)
    {
        _Heath = Heath;
    }
    /// <summary>
    /// 小怪物被攻击
    /// </summary>
    /// <param name="Hurt">受到的伤害值</param>
    public void Attacked(float Hurt, Transform AttackPoint)
    {
        if (!isReceiveHurt) { return; }
        Debug.Log("受到" + Hurt + "伤害");

        _Heath -= Hurt;

        Debug.Log(transform.name + "当前的血量为:" + _Heath);
        if (_Heath <=0)
        {
            Death();
        }
        else
        {
            //播放被攻击的效果
            AttackedEffection(AttackPoint);
            //transform.position.x - AttackPoint.position.x;
            int de = AttackPoint.position.x - transform.position.x > 0 ? 1 : -1;
            if (AttackedForce > 0 && _rigidbody2D)
                _rigidbody2D.AddForce(Vector2.left*de * AttackedForce);

            _Heath -= Hurt;
        }
    }

    /// <summary>
    /// 状态机重置
    /// </summary>
    protected virtual void ResetParameter() { }


    /// <summary>
    /// 是否接受伤害
    /// </summary>
    /// <param name="State"></param>
    public void SetReciveHurtState(bool State)
    {
        isReceiveHurt = State;
    }
}
