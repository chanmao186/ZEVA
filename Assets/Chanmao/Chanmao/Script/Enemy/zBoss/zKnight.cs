using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zKnight : zBoss
{
    [Header("A2特效的位置")]
    public Transform A2Point;

    [Header("技能2的预制体")]
    public GameObject A2Prefabs;
    // Start is called before the first frame update       
    [Header("骑士的二技能离玩家的距离")]
    public float ToPlayerDistance = 5;

    [Header("骑士的三技能，升高的距离")]
    public float Hight = 5;

    [Header("技能三的粒子特效")]
    public ParticleSystem A3Effection;

    [Header("技能3投剑后的等待时间")]
    public float A3IdleTime = 0.3f;

    [Header("骑士的剑的投掷点")]
    public Transform SwrodPoint;

    [Header("剑的飞行速度")]
    public float SwordSpeed = 15;
    
    protected override void Start()
    {
        base.Start();
        Debug.Log("骑士");
        //Self = this as zKnight;
        //Ability2();
    }

    protected override void Ability1()
    {
        int origin = Random.Range(0, 2);
        int destination = 1 - origin;
        Direction = origin == 1 ? -1 : 1;
        DirectionState();
        SetToPoint(AbilityPoint[origin]);
        _time = zc.zTime.ScheduleOnce(() =>
        {
            Ani.SetInteger("Sprint", 1);
            _time = zc.zTime.ScheduleOnce(() =>
            {
                Ani.SetInteger("Sprint", 2);

                zc.zMove.MoveToNode(AbilityPoint[destination], RunSpeed, () =>
                {
                    Ani.SetInteger("Sprint", 0);
                    AbilityEnd();
                }, transform);
            }, 0.3f);
            //播放动画            
        }, PreAbilityTime);
    }

    protected override void Ability2()
    {
        //确定玩家将要朝向的方向
        float d = Random.Range(0, 2) > 0 ? ToPlayerDistance : -ToPlayerDistance;
        BossPos.x = Player.position.x + d;
        BossPos.x = BossPos.x < LeftBoundary.position.x ? LeftBoundary.position.x : BossPos.x;
        BossPos.x = BossPos.x > RightBoundary.position.x ? RightBoundary.position.x : BossPos.x;
        BossPos.y = PosY;
        SetToPosition(BossPos);
        Direction = d > 0 ? -1 : 1;
        DirectionState();

        _time = zc.zTime.ScheduleOnce(() =>
        {
            //播放斩击动画动画
            //Ani
            Ani.SetInteger("Slash", 1);
            _time = zc.zTime.ScheduleOnce(() => {
                GameObject a2 = Instantiate(A2Prefabs, A2Point.position, A2Point.rotation);
                a2.transform.localScale = new Vector3(Direction, a2.transform.localScale.y, a2.transform.localScale.z);
                _time = zc.zTime.ScheduleOnce(() =>
                {
                    Destroy(a2);
                    //转换未正常方式的动画
                    Ani.SetInteger("Slash", 0);
                    AbilityEnd();
                }, 2);
            }, 0.5f);
            
        }, PreAbilityTime);
    }

    protected override void Ability3()
    {
        BossPos.x = Player.position.x;
        BossPos.x = BossPos.x < LeftBoundary.position.x ? LeftBoundary.position.x : BossPos.x;
        BossPos.x = BossPos.x > RightBoundary.position.x ? RightBoundary.position.x : BossPos.x;
        BossPos.y = Hight + PosY;

        SetToPosition(BossPos);
        Ani.SetInteger("Throw", 2);

        GameObject sword = Instantiate(Weapon, SwrodPoint.position, SwrodPoint.rotation);
        sword.SetActive(false);

        _time = zc.zTime.ScheduleOnce(() =>
        {
            sword.SetActive(true);
            BossPos.y -= Hight;
            action = zc.zMove.MoveToPosition(new Vector2(SwrodPoint.position.x, SwrodPoint.position.y-Hight), SwordSpeed, () =>
            {
                _time = zc.zTime.ScheduleOnce(() =>
                {
                    
                    action = zc.zMove.MoveToPosition(BossPos, 10, () =>
                    {
                        Ani.SetInteger("Throw", 0);
                        Destroy(sword);
                        //动画变化

                        //A3Effection.gameObject.SetActive(true);

                        _time = zc.zTime.ScheduleOnce(() =>
                        {
                            //Effection.gameObject.SetActive(false);
                            AbilityEnd();
                            Ani.SetInteger("Throw", 0);
                        }, IdleTime);
                    }, transform);
                }, 0.5f);

            }, sword.transform);
        }, PreAbilityTime);
    }

    protected override void DeathEffection()
    {
        base.DeathEffection();
        //Debug.Log("boss死亡，停止更新动作");
        CurrentState = EnemyState.Death;

        UIManager.Instance.UnlockDoor();
    }

}
