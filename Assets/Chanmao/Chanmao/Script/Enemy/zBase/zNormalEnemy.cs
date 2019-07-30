using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zNormalEnemy : zEnemy
{
   [Header("监视距离")]
    public float InspectDistance = 10f;

    [Header("该怪物是否去追加玩家")]
    public bool isPursuePlayer = true;

    [Header("怪物的出生点")]
    public Transform OriginPoint;

    [Header("怪物的巡逻点")]
    public Transform[] PatrolPoint; 

    [Header("怪物的默认状态")]
    public EnemyState DefaultState;

    [Header("玩家离远了是否恢复默认状态")]
    public bool isReturnDefault = true;
    protected EnemyState CurrentState;
    //怪物的初始创建坐标
    protected Vector2 OriginPos;

    protected Transform CurrentPiont;


    protected int _TargetPointIndex;
    protected int _TargetPointLength;

    protected override void Start()
    {
        base.Start();
        IsCheckState = true;
        //isCanChangState= true;
        Direction = 1;
        CurrentState = DefaultState;
        Debug.Log(CurrentState);
        //获取巡逻点的信息
        _TargetPointLength = PatrolPoint.Length;
        _TargetPointIndex = 0;

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
    }

    protected virtual void MoveToPoint(float speed, Transform point) { }
    
    public override void Death()
    {
        base.Death();
        if (CurrentState == EnemyState.Death) return;
        CurrentState = EnemyState.Death;
        Destroy(this.gameObject, DestroyTime);
    }

    protected virtual void Pursue()
    {
        Direction = Player.position.x - transform.position.x > 0 ? 1 : -1;
    }

    protected virtual void Ablility() { }
    protected virtual void EnemyIdle() { }

    /// <summary>
    /// 怪物状态转换
    /// </summary>
    /// <param name="condition">转换条件</param>
    /// <param name="state">要转换的状态</param>
    /// <param name="pre">准备函数</param>
    /// <param name="fun">执行函数</param>
    protected void TranFormChang(bool condition, EnemyState state, Func pre, Func fun)
    {
        _AbilityTime = pre == null ? AbilityTime : _AbilityTime;
        _AbilityTime += Time.fixedDeltaTime;
        if (_AbilityTime > AbilityTime)
        {
            _AbilityTime = AbilityTime;
            fun?.Invoke();
            if (condition)
            {
                _AbilityTime = 0;
                CurrentState = state;
            }
        }
        else
        {
            pre?.Invoke();
        }

    }
    /// <summary>
    /// 技能的准备动作
    /// </summary>
    protected virtual void PreAbility() { }

    protected virtual void Idle() { }
    protected virtual void EndAbility() { }
    protected virtual void ReturnOriginPos() { }

    protected virtual void IdleFun() {
        bool s1 = (Vector2.Distance(transform.position, Player.position) < InspectDistance && isPursuePlayer);
        TranFormChang(s1, EnemyState.Pursue, null, Idle);
    }

    protected virtual void PatrolFun()
    {
        bool s2 = (Vector2.Distance(transform.position, Player.position) < InspectDistance && isPursuePlayer);
        TranFormChang(s2, EnemyState.Pursue, null, Patrol);
    }

    protected virtual void PursueFun()
    {
        bool s3 = Vector2.Distance(transform.position, Player.position) > InspectDistance;
        TranFormChang(s3&& isReturnDefault, EnemyState.Return, PreAbility, Pursue);
    }

    protected virtual void ReturnFun()
    {
        if (Vector2.Distance(transform.position, Player.position) < InspectDistance)
        {
            _AbilityTime = 0;
            CurrentState = EnemyState.Pursue;
        }
        else
        {
            bool s4 = Vector2.Distance(transform.position, OriginPoint.position) < 1;
            TranFormChang(s4, DefaultState, EndAbility, ReturnOriginPos);
            //Debug.Log("怪物恢复默认状态");
        }
    }
    protected virtual void CheckEnemyState()
    {
        //if (!IsCheckState) return;
        //Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case EnemyState.Idle:
                IdleFun();
                break;

            case EnemyState.Patrol:
                PatrolFun();
                break;

            case EnemyState.Pursue:
                PursueFun();
                break;

            case EnemyState.Return:
                ReturnFun();
                break;

            case EnemyState.Ability:
                break;
            case EnemyState.Death:
                break;
        }
    }

    protected virtual void VirtulGriavty()
    {
        if (isGrivaty)
        {
            isGroundUpdate();
            //Debug.Log(isGround);
            if (!isGround)
            {
                transform.Translate(0, -Grivaty * Time.fixedDeltaTime, 0);
            }
        }
    }
    protected virtual void FixedUpdate()
    {
        VirtulGriavty();
        CheckEnemyState();
        DirectionState();
    }
}
