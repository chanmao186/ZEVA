using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zKnight : zBoss
{
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

    protected override void Start()
    {
        base.Start();
        //Ability2();
    }

    protected override void Ability1()
    {
        int origin = Random.Range(0, 2);
        int destination = 1 - origin;
        Direction = origin == 1 ? -1 : 1;
        DirectionState();
        SetToPoint(AbilityPoint[origin]);
        _time = zc.zTime.ScheduleOnce(()=> {
            Ani.SetInteger("Sprint", 1);
            _time = zc.zTime.ScheduleOnce(() => {
                Ani.SetInteger("Sprint", 2);

                zc.zMove.MoveToNode(AbilityPoint[destination], RunSpeed, () => {
                    Ani.SetInteger("Sprint", 0);
                    AbilityEnd();
                }, transform);
            }, 0.3f);
            //播放动画            
        },PreAbilityTime);
    }

    protected override void Ability2()
    {
        //确定玩家将要朝向的方向
        float d = Random.Range(0, 2) > 0 ? ToPlayerDistance : -ToPlayerDistance;
        BossPos.x = Player.position.x+d;
        BossPos.x = BossPos.x < LeftBoundary.position.x ? LeftBoundary.position.x : BossPos.x;
        BossPos.x = BossPos.x > RightBoundary.position.x ? RightBoundary.position.x : BossPos.x;
        BossPos.y = PosY;
        SetToPosition(BossPos);
        Direction = d > 0 ? -1 : 1;
        DirectionState();

        _time = zc.zTime.ScheduleOnce(() => {
            //播放斩击动画动画
            //Ani
            Ani.SetInteger("Slash", 1);
            _time = zc.zTime.ScheduleOnce(() => {
                //转换未正常方式的动画
                Ani.SetInteger("Slash", 0);
                AbilityEnd();
            },2);
        }, PreAbilityTime);
    }

    protected override void Ability3()
    {        
        BossPos.x = BossPos.x < LeftBoundary.position.x ? LeftBoundary.position.x : BossPos.x;
        BossPos.x = BossPos.x > RightBoundary.position.x ? RightBoundary.position.x : BossPos.x;
        BossPos.y = Hight + PosY;

        SetToPosition(BossPos);

        GameObject sword = Instantiate(Weapon, SwrodPoint.position, SwrodPoint.rotation);
        //播放boss扔剑的动作
        _time = zc.zTime.ScheduleOnce(() => {
            BossPos.y -= Hight;
            action = zc.zMove.MoveToPosition(BossPos, 10,()=> {

                action = zc.zMove.MoveToPosition(BossPos, 10, () => {
                    Destroy(sword);
                    //动画变化

                    A3Effection.gameObject.SetActive(true);

                    _time = zc.zTime.ScheduleOnce(() => {
                        A3Effection.gameObject.SetActive(false);
                        AbilityEnd();
                    }, IdleTime);
                },transform);
            }, sword.transform);
        }, PreAbilityTime);
    }
}
