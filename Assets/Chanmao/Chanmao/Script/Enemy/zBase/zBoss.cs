using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class zBoss : zEnemy
{
    [Header("是否保持释放的技能不重复")]
    public bool AvoidSameAbility = false;

    [Header("boss可以释放的技能的数量")]
    public int AbilityNumber = 3;
    [Header("闪现后，释放技能前的准备时间")]
    public float PreAbilityTime = 0.3f;

    [Header("boss释放完一个技能后躲藏的时间")]
    public float HideTime = 1;

    [Header("boss释放完技能后，渐隐的时间")]
    public float FadeInTime = 1;

    [Header("释放完一个技能后的停顿时间")]
    public float IdleTime = 1;

    [Header("boss的技能释放点")]
    public Transform[] AbilityPoint;

    [Header("游戏中的左右边界点")]
    public Transform LeftBoundary;
    public Transform RightBoundary;

    [Header("boss隐藏的点")]
    [Tooltip("放置在屏幕外，不影响游戏进程")]
    public Transform HidePoint;
    //[Header("boss要求躲藏的点")]

    [Header("游戏开始的时间")]
    public float StartTime = 1;

    protected EnemyState CurrentState;
    protected Vector2 BossPos;

    protected int lastA;
    protected int currentA;
    protected float PosY;
    //public Transform HidePoint;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CurrentState = EnemyState.Ability;
        lastA = 100;
        BossPos = transform.position;
        PosY = BossPos.y;
        zc.zTime.ScheduleOnce(AbilityEnd, StartTime);
    }

    /// <summary>
    /// boss躲藏起来
    /// </summary>
    protected void Hide()
    {
        zc.zColor.HideIn(sprite);
    }

    protected virtual void AbilityEnd()
    {
        if(CurrentState == EnemyState.Death)
        {
            return;
        }
        zc.zTime.ScheduleOnce(() =>
        {
            //使节点移动到场景外
            SetToPoint(HidePoint);
            //停留一段时间后，boss释放下一个技能
            zc.zTime.ScheduleOnce(AbilityTranForm, HideTime);
            //使节点显示出来
            zc.zColor.ShowIn(sprite);
        }, IdleTime);
    }
    protected virtual void AbilityTemplate(Func ability)
    {

    }
    protected virtual void Ability1() { }
    protected virtual void Ability2() { }
    protected virtual void Ability3() { }
    /// <summary>
    /// boss切换攻击模式
    /// </summary>
    protected virtual void AbilityTranForm()
    {
        currentA = Random.Range(0, AbilityNumber);
        if (AvoidSameAbility)
        {
            while (lastA == currentA)
            {
                currentA = Random.Range(0, AbilityNumber);
            }
            lastA = currentA;
        }
        Debug.Log("释放技能" + (currentA + 1));      //Random.Range(0, AbilityNumber)
        //随机释放一个技能Random.Range(0, AbilityNumber)
        switch (currentA)
        {
            case 0:
                Ability1();
                break;
            case 1:
                Ability2();
                break;
            case 2:
                Ability3();
                break;
        }
    }

    protected override void DeathEffection()
    {
        base.DeathEffection();
        Debug.Log("boss死亡，停止更新动作");
        CurrentState = EnemyState.Death;

        
        //zc.zTime.StopSchedule(_time);
        //主角不在受伤
        //Player.GetComponent<zPlayer>().SetReciveHurtState(false);
    }

}
