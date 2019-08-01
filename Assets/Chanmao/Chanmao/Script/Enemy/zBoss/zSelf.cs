using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zSelf : zBoss
{
    [Header("斩击的技能点")]
    public ZSlice Slice;

    [Header("进行一字字斩的几个点为")]
    public Transform[] A1SlicePoint;
    public int A1Time;
    
    public int A1Num;
    public float A1Interval;

    [Header("进行z字斩的几个点为")]
    public Transform[] A2SlicePoint;

    [Header("技能三的预制体")]
    public GameObject A3Prefab;

    protected GameObject Carmera;
    protected override void Start()
    {
        base.Start();
        //寻找场景里的主摄像机
        Carmera = GameObject.Find("Main Camera");
    }

    protected override void Ability1()
    {
        Debug.Log("一字斩");
        SetToPoint(AbilityPoint[0]);
        //播放动画
        
        _time=zc.zTime.ScheduleOnce(() => {
            //角色瞬移到场景外的隐藏点
            SetToPoint(HidePoint);
            SliceOnce();
            _time = zc.zTime.Schedule(SliceOnce, A1Interval, A1Num - 1, AbilityEnd);
        }, PreAbilityTime);
    }

    private void SliceOnce()
    {
        SetSliceOrignPos(A1SlicePoint[0]);
        Slice.SetSlicePoint(A1SlicePoint[1].position);
        Slice.SliceStart();
    }
    // Start is called before the first frame update
    protected override void Ability2()
    {
        Debug.Log("Z字斩");
        //玩家闪现的目标位置
        SetToPoint(AbilityPoint[1]);
        //播放动画

        //设置斩击的起始点坐标
        SetSliceOrignPos(A2SlicePoint[0]);
        
        _time = zc.zTime.ScheduleOnce(() => {
            SetToPoint(HidePoint);
            for(int i = 1; i < 4; i++)
            {
                Slice.SetSlicePoint(A2SlicePoint[i].position);
            }
            Slice.SliceStart();
            _time = zc.zTime.ScheduleOnce(AbilityEnd, 1);
        }, PreAbilityTime);
    }


    protected override void Ability3()
    {
        //玩家闪现的目标位置
        SetToPoint(AbilityPoint[1]);
        //播放动画
        _time = zc.zTime.ScheduleOnce(() => {
            Hide();
            GameObject a3 = Instantiate(A3Prefab,Carmera.transform);
            a3.transform.position = new Vector3(Carmera.transform.position.x, Carmera.transform.position.y, 10);
            zc.zColor.FadeOut(0.6f, a3, () => {
                Transform _a3 = a3.transform.GetChild(0);
                //释放该技能期间，玩家不可操作角色
                Player.GetComponent<zPlayer>().SetOperationState(false);

                //唤醒技能的节点
                _a3.gameObject.SetActive(true);

                zc.zTime.ScheduleOnce(() => {

                    //释放完之后隐藏该节点
                    _a3.gameObject.SetActive(false);
                    zc.zColor.FadeIn(0.6f, a3, (() => {

                        Destroy(a3);
                        Player.GetComponent<zPlayer>().SetOperationState(true);
                        AbilityEnd();
                    }));
                }, 0.6f);
            });
            //全屏的斩击动画
        }, PreAbilityTime);
    }

    /// <summary>
    /// 设置斩击的起始点
    /// </summary>
    /// <param name="point">起始点</param>
    private void SetSliceOrignPos(Vector3 point)
    {
        Slice.transform.position = point;
    }
    /// <summary>
    /// 设置斩击的起始点
    /// </summary>
    /// <param name="point">起始点</param>
    private void SetSliceOrignPos(Transform point)
    {
        SetSliceOrignPos(point.position);
    }
}
