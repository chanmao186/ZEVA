using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum EnemyState
{
    Idle, Patrol, Pursue, Return
}

public class zEnemy : CCharacter
{
    [Header("监视距离")]
    public float InspectDistance = 10f;

    [Header("死后销毁时间(为0时不销毁)")]
    public float DestroyTime = 3f;

    [Header("该怪物是否去追加玩家")]
    public bool isPursuePlayer = true;

    [Header("怪物的出生点")]
    public Transform OriginPoint;

    [Header("怪物的巡逻点")]
    public Transform[] PatrolPoint;

    //考虑设置个游戏管理器代码，在游戏管理器里赋值
    [Header("玩家节点")]
    public Transform Player;

    [Header("怪物的默认状态")]
    public EnemyState DefaultState;

    protected Vector2 moveDirection;

    protected EnemyState CurrentState;
    //怪物的初始创建坐标
    protected Vector2 OriginPos;

    protected Transform CurrentPiont;
    protected AIPath ai;
    protected AIDestinationSetter seekTarget;

    protected int _TargetPointIndex;
    protected int _TargetPointLength;
    // Start is called before the first frame update
    protected override void Start()
    {
        CurrentState = DefaultState;
        Debug.Log(CurrentState);
        //获取巡逻点的信息
        _TargetPointLength = PatrolPoint.Length;
        _TargetPointIndex = 0;
        ai = GetComponent<AIPath>();
        seekTarget = GetComponent<AIDestinationSetter>();
        //给定初始速度
        ai.maxSpeed = WalkSpeed;
        base.Start();
        moveDirection = new Vector2(1, 0);
        //Debug.Log("zEnemy执行");
    }

    protected virtual void Patrol()
    {

        if (Vector2.Distance(transform.position, PatrolPoint[_TargetPointIndex].position) < 1)
        {
            //到达一个巡逻点后，去往下一个巡逻点
            _TargetPointIndex++;
            _TargetPointIndex = _TargetPointIndex > _TargetPointLength - 1 ? 0 : _TargetPointIndex;
        }
        seekTarget.target = PatrolPoint[_TargetPointIndex];
        //若超过巡逻距离，改变巡逻方向
        //if (Mathf.Abs(transform.position.x - OriginPos.x) > PatrolRdius)
        //{
        //    //若超过距离，则换一个方向运动
        //    moveDirection.x = -moveDirection.x;
        //}

        //transform.Translate(moveDirection * WalkSpeed * Time.fixedDeltaTime);
    }

    public virtual void DirectionState()
    {
        //xScale = _rigidbody2D.velocity.x<0?
        float x = ai.desiredVelocity.x;
        if (x != 0)
        {
            Ani.SetFloat("Speed", 1);
            Scale.x = x < 0 ? xScale : -xScale;
            transform.localScale = Scale;
        }
    }
    public override void Death()
    {
        base.Death();
        Destroy(this.gameObject, DestroyTime);
    }

    protected virtual void Pursue()
    {
        ai.maxSpeed = RunSpeed;
        seekTarget.target = Player;
    }

    protected virtual void EnemyIdle()
    {
        seekTarget.target = null;
    }
    protected virtual void ReturnOriginPos() { }
    protected virtual void CheckEnemyState()
    {
        //Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case EnemyState.Idle:
                if (Vector2.Distance(transform.position, Player.position) < InspectDistance && isPursuePlayer)
                {
                    CurrentState = EnemyState.Pursue;
                    Pursue();
                }
                break;
            case EnemyState.Patrol:

                if (Vector2.Distance(transform.position, Player.position) < InspectDistance && isPursuePlayer)
                {
                    CurrentState = EnemyState.Pursue;
                    Pursue();
                }
                else
                {
                    Patrol();
                }
                break;
            case EnemyState.Pursue:
                if (Vector2.Distance(transform.position, Player.position) > InspectDistance)
                {
                    CurrentState = EnemyState.Return;
                }
                break;
            case EnemyState.Return:
                ReturnOriginPos();
                if (Vector2.Distance(transform.position, OriginPos) < 0.1)
                {
                    Scale.x = xScale;
                    transform.localScale = Scale;
                    CurrentState = DefaultState;
                }
                break;
        }
    }
    protected virtual void FixedUpdate()
    {
        CheckEnemyState();
        DirectionState();
    }
}
