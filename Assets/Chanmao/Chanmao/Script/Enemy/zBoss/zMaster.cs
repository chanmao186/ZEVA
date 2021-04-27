using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zMaster : zBoss
{
    [Header("法师的技能1所引发的爆炸")]
    public GameObject A1Prefabs;

    [Header("法师的技能1爆炸的次数")]
    public int ExplosionNum;

    [Header("法师的技能1的间隔")]
    public float A1Interval;

    [Header("技能2的释放位置")]
    public Transform[] A2Pos;

    [Header("技能2的预制体")]
    public GameObject A2Prefab;

    [Header("技能2的释放间隔")]
    public float A2Interval;

    [Header("技能2的冰花出现的间隔")]
    public float A2Interval1;

    [Header("技能3的几个发射位置")]
    public Transform[] A3Pos;

    [Header("技能3的预制体")]
    public GameObject A3Prefab;

    [Header("技能3的飞行速度")]
    public float A3FlySpeed = 20;

    [Header("技能三的时间间隔")]
    public float A3Interval;

    [Header("技能三的准备时间")]
    public float A3PreTime= 0.5f;

    [Header("技能三的飞行速度")]
    public float A3Speed = 15;
    protected Vector2 A3EffectionPos;
    //A3再上方出来的位置

    private Vector2 Ability1ExplosionPos;
    float up, down;

    private int A2Num;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        float num =(A2Pos[1].position.x - A2Pos[0].position.x) / 4;
        A2Num = Mathf.CeilToInt(num);
        Debug.Log(A2Num);
    }

    protected override void Ability1()
    {
        SetToPoint(AbilityPoint[0]);
        //播放动画
        Ability1ExplosionPos = Player.position;
        Ani.SetTrigger("Attack");
        Quaternion rotate = new Quaternion();
        _time = zc.zTime.ScheduleOnce(() =>
        {
            Ani.SetTrigger("Attack");
            _time = zc.zTime.Schedule(() =>
            {
                GameObject e = Instantiate(A1Prefabs, Ability1ExplosionPos, rotate);
                Destroy(e, 1);
                //将爆炸位置更新
                Ability1ExplosionPos = Player.position;
            }, A1Interval, ExplosionNum, AbilityEnd);
        }, PreAbilityTime);
    }

    private void A2Fun(Vector2 pos)
    {
        GameObject a = Instantiate(A2Prefab, pos, A2Pos[0].rotation);
        Destroy(a, 1.5f);
    }

    /// <summary>
    /// 技能2释放顺序
    /// </summary>
    /// <param name="originPosIndex">起始点的坐标</param>
    /// <param name="OperationWay">运算方式，true向右，false向做</param>
    /// <param name="CallBack">回调函数</param>
    private void A2Fun1(int originPosIndex,bool OperationWay,Func CallBack)
    {
        Ani.SetTrigger("Attack");
        Vector2 pos = A2Pos[originPosIndex].position;
        _time = zc.zTime.Schedule(() => {
            A2Fun(pos);

            if (OperationWay)
                pos.x += 4;
            else
                pos.x -= 4;
        }, 0.5f, A2Num, CallBack);
    }

    protected override void Ability2()
    {
        SetToPoint(AbilityPoint[1]);
        //播放动画
        //Ani.SetTrigger("Attack");
        _time = zc.zTime.ScheduleOnce(() =>
        {
            
            A2Fun1(0, true, () => {
                _time = zc.zTime.ScheduleOnce(() =>
                {
                    A2Fun1(1,false,()=> {
                        //技能而结束
                        _time = zc.zTime.ScheduleOnce(AbilityEnd, A2Interval + 0.2f);
                    });                   
                }, A2Interval1);
            });

            
            
        }, PreAbilityTime);
    }

    protected override void Ability3()
    {
        SetToPoint(AbilityPoint[2]);
        //播放相关的动画
        Ani.SetTrigger("Attack");
        //float x = Player.position.x;
        _time = zc.zTime.ScheduleOnce(() =>
        {
            A3Fun(0);
            //x = Player.position.x;
            _time = zc.zTime.ScheduleOnce(() =>
            {
                A3Fun(1);
                _time = zc.zTime.ScheduleOnce(AbilityEnd, A3Interval);
            }, A3Interval);
        }, PreAbilityTime);
    }

    /// <summary>
    /// 发射冰锥
    /// </summary>
    /// <param name="pos"></param>
    private void A3Fun(int pos)
    {
        GameObject a1 = Instantiate(A3Prefab, A3Pos[pos].position, A3Pos[pos].rotation);
        A3Pos[2].position =new Vector2(Player.position.x,A3Pos[2].position.y);
        GameObject a2 = Instantiate(A3Prefab, A3Pos[2].position, A3Pos[2].rotation);
        _time = zc.zTime.ScheduleOnce(() => {
            a1.GetComponent<zMashterA2>().Rush(A3FlySpeed);
            a2.GetComponent<zMashterA2>().Rush(A3FlySpeed);
        },A3PreTime);
    }

    protected override void DeathEffection()
    {
        base.DeathEffection();
        UIManager.Instance.UnlockDoor();
    }
}
