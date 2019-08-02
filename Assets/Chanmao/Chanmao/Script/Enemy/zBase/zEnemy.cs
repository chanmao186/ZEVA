using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public enum EnemyState
{
    Idle, Patrol, Pursue, Return, Death,Ability
}

public class zEnemy : CCharacter
{  
    [Header("死后销毁时间(为0时不销毁)")]
    public float DestroyTime = 3f;

    //考虑设置个游戏管理器代码，在游戏管理器里赋值
    [Header("玩家节点")]
    public Transform Player;

    [Header("怪物技能的预备时间")]
    public float AbilityTime = 0.5f;

    protected float _AbilityTime = 0;

    protected zEnemyController zec;

    /// <summary>
    /// 是否启用状态转换机
    /// </summary>
    protected bool IsCheckState;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        IsCheckState = true;
        //isCanChangState= true;
        Direction = 1;
        Player = FindObjectOfType<zPlayer>().transform;
        zec = GetComponent<zEnemyController>();
        //Debug.Log("zEnemy执行");
    }

    protected virtual void DirectionState()
    {
        Scale.x = -xScale * Direction;
        transform.localScale = Scale;
        //xScale = _rigidbody2D.velocity.x<0?
    }

    protected override void DeathEffection()
    {
        base.DeathEffection();
        zec.EnemtCount();
    }
}
