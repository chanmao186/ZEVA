using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZSlice : MonoBehaviour
{
    
    [Header("游戏管理器的节点")]
    public GameObject GameMaster;
    // Start is called before the first frame update

    [Header("切割的最大距离")]
    public float SliceDistance = 5.0f;

    [Header("斩击造成的伤害")]
    public float SliceHurt = 10;

    [Header("斩击的预制体")]
    public GameObject Knite;

    [Header("玩家可以斩击的次数")]
    public int SliceNum = 1;

    [Header("进行斩击的冷却时间")]
    public float CoolingTime = 10;

    [Header("进行多段斩击的间隔时间")]
    public float IntervalTime = 0.05f;

    [Header("斩击不可穿越部分障碍物")]
    public bool IsThrounGround = true;

    [Header("斩击技能的冷却")]
    public Image CDImage;

    [Header("斩击后，角色身上的特效")]
    public ParticleSystem SliceEffection;

    [Header("玩家完成动作后的滞留时间")]
    public float EndSliceTime = 1f;

    Rigidbody2D r2;
    private float _CoolingTime = 0;

    private zPlayer zp;
    private float _IntervalTime = 0;
    /// <summary>
    /// 玩家当前斩击的次数
    /// </summary>
    private int _SliceNum;

    /// <summary>
    /// 鼠标点击的位置
    /// </summary>
    private Vector2[] MousePos;

    private GameObject _knite;

    private float _kniteLength;

    private Vector2 PlayerPos;

    private Vector2 SliceDirection;

    private float _SliceDistance;

    private float PlayerLength;
    //private GameObject Enemy[];

    private ZController zm;

    //斩击后的回调函数
    private Func SliceCallBack;
    private float xScale;
    private Vector3 localScale;
    private Coroutine mtime;

    public float GetCurrentCoolTime()
    {
        return _CoolingTime;
    }
    void Start()
    {
        mtime = null;
        localScale = transform.localScale;
        xScale = localScale.x;
        zm = GameMaster.GetComponent<ZController>();
        zp = GetComponent<zPlayer>();
        PlayerLength = GetComponent<SpriteRenderer>().size.x * transform.localScale.x;
        _kniteLength = 2.13f;
        Debug.Log("斩击的默认长度为：" + _kniteLength);
        MousePos = new Vector2[10];
        _SliceNum = 0;
        r2 = GetComponent<Rigidbody2D>();
        PlayerPos = new Vector2(transform.position.x, transform.position.y);
    }

    public void SetSlicePoint(Vector2 point)
    {
        MousePos[_SliceNum] = point;
        _SliceNum++;
    }
    /// <summary>
    /// 鼠标开始执行切割的动作
    /// </summary>
    private void MouseSlice()
    {
        if (_CoolingTime > 0) return;
        if (Input.GetButton("Space"))
        {
            if (Input.GetMouseButtonDown(1))
            {
                MousePos[_SliceNum] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _SliceNum++;
                if (SliceNum == _SliceNum)
                {
                    SliceStart();
                }
            }
        }
        else if (Input.GetButtonUp("Space"))
        {
            SliceStart();
        }
        //确保释放技能在冷却范围内
        else if (Input.GetMouseButton(1))
        {
            MousePos[_SliceNum] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _SliceNum++;
            SliceStart();
        }
    }

    public void CheckStart()
    {
        if (SliceNum == _SliceNum)
        {
            SliceStart();
        }
    }
    /// <summary>
    /// 开始进行斩击
    /// </summary>
    public void SliceStart()
    {
        if (_SliceNum == 0) return;
        //技能开始冷却
        _CoolingTime = CoolingTime;
        int i = 0;
        zm.zTime.Schedule(() =>
        {
            ///进行下列判断的功能是：
            ///再鼠标到玩家的距离，玩家到障碍物的距离，默认的切割距离间，获取一个最小值
            PlayerPos = new Vector2(transform.position.x, transform.position.y);
            _SliceDistance = Vector2.Distance(PlayerPos, MousePos[i]);
            _SliceDistance = _SliceDistance < SliceDistance ? _SliceDistance : SliceDistance;
            SliceDirection = (MousePos[i] - PlayerPos).normalized;
            //检测前方是否有障碍物
            if (IsThrounGround)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(PlayerPos, SliceDirection, SliceDistance);
                foreach (RaycastHit2D element in hit)
                {

                    if (element.collider.CompareTag("Ground"))
                    {
                        float d = element.distance - PlayerLength * 0.5f;
                        _SliceDistance = _SliceDistance < d ? _SliceDistance : d;
                        break;
                    }
                }
            }
            PlayerSlice(i);
            PlayerMove();
            i++;
        }, IntervalTime, _SliceNum);
    }

    /// <summary>
    /// 玩家执行斩击的动作
    /// </summary>
    /// <param name="direction">要斩击的方向</param>
    private void PlayerSlice(int SliceNum)
    {
        Debug.Log("玩家开始进行斩击");
        Quaternion q = Quaternion.FromToRotation(new Vector2(1, 0), SliceDirection);

        float unlength = 0;

        //获得最后一次斩击
        if ((SliceNum + 1) == _SliceNum)
        {
            if(SliceEffection)
            PlaySliceEffections();
            unlength = PlayerLength * 0.5f;
            _SliceNum = 0;
        }
        //将刀光创建出来
        _knite = Instantiate(Knite, new Vector2(transform.position.x, transform.position.y), q);

        //根据距离改变斩击的长度
        float scale = (_SliceDistance - unlength) / _kniteLength;
        scale = scale < 0.1f ? 0.1f : scale;
        _knite.transform.localScale = new Vector2(scale, 1);
        Destroy(_knite, 0.4f);
    }

    //技能冷却时间更新
    private void CoolTimeUpdate()
    {
        zm.zTime.TimeUpdate(ref _CoolingTime);
        //Debug.Log(_CoolingTime);
        if (CDImage)
            CDImage.fillAmount = _CoolingTime / CoolingTime;
    }

    //回合技能冷却更新
    private void IntervalTimeUpdate()
    {
        zm.zTime.TimeUpdate(ref _IntervalTime, () =>
        {
            _SliceNum = 0;
            _CoolingTime = CoolingTime;
        });
    }

    /// <summary>
    /// 玩家移动到目标位置
    /// </summary>
    private void PlayerMove()
    {
        //玩家进行斩击后停顿一段时间
        Debug.Log("重力消失");


        Vector2 move = SliceDirection * _SliceDistance;
        //增加判断，防止玩家移动到地下
        if(zp)
        move.y = zp.GetIsGround() && move.y < 0 ? 0 : move.y;

        transform.Translate(move);

        localScale.x = move.x < 0 ? xScale : -xScale;
        transform.localScale = localScale;
        //停止上一帧的延时函数
        if (r2)
        {
            GetComponent<Rigidbody2D>().simulated = false;
            zm.zTime.StopSchedule(mtime);
         
            mtime = zm.zTime.ScheduleOnce(() =>
            {
                Debug.Log("重力恢复");

                GetComponent<Rigidbody2D>().simulated = true;
            }, EndSliceTime);
        }
    }

    /// <summary>
    /// 播放斩击效果 
    /// </summary>
    private void PlaySliceEffections()
    {
        //停止上一个动作
        SliceEffection.Stop();

        SliceEffection.Play();
    }
    // Update is called once per frame
    void Update()
    {
        MouseSlice();
    }

    private void FixedUpdate()
    {
        CoolTimeUpdate();
        IntervalTimeUpdate();
    }
}
