using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZSlice : MonoBehaviour
{
    [Header("游戏管理器的节点")]
    public GameObject GameMaster;
    // Start is called before the first frame update

    [Header("切割的距离")]
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

    [Header("玩家的斩击不可穿越部分障碍物")]
    private bool IsThrounGround = true;

    [Header("玩家斩击技能的冷却")]
    public Image CDImage;

    private float _CoolingTime = 0;

    private CTime CT;
    private float _IntervalTime = 0;
    /// <summary>
    /// 玩家当前斩击的次数
    /// </summary>
    private int _SliceNum;

    /// <summary>
    /// 鼠标开始点击的位置
    /// </summary>
    private Vector2[] MousePos;

    private GameObject _knite;
    private float _kniteLength;

    private Vector2 PlayerPos;

    private Vector2 SliceDirection;

    private float _SliceDistance;

    private float PlayerLength;
    //private GameObject Enemy[];
    void Start()
    {
        PlayerLength = GetComponent<SpriteRenderer>().size.x * transform.localScale.x;
        _kniteLength = 5.45f;
        Debug.Log("斩击的默认长度为：" + _kniteLength);
        CT = GameMaster.GetComponent<CTime>();
        MousePos = new Vector2[10];
        _SliceNum = 0;
        PlayerPos = new Vector2(transform.position.x, transform.position.y);
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

    /// <summary>
    /// 开始进行斩击
    /// </summary>
    private void SliceStart()
    {
        if (_SliceNum == 0) return;
        //技能开始冷却
        _CoolingTime = CoolingTime;
        int i = 0;
        CT.Schedule(() => {
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
                        _SliceDistance = _SliceDistance < element.distance ? _SliceDistance : element.distance;
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
        if ((SliceNum + 1) == _SliceNum)
        {
            unlength = PlayerLength * 0.5f;
            _SliceNum = 0;
        }
        //将刀光创建出来
        _knite = Instantiate(Knite, new Vector2(transform.position.x, transform.position.y), q);

        //根据距离改变斩击的长度
        float scale = (_SliceDistance- unlength) / _kniteLength;
        scale = scale < 0.1f ? 0.1f : scale;
        _knite.transform.localScale = new Vector2(scale, 1);
        Destroy(_knite, 0.5f);
    }

    //技能冷却时间更新
    private void CoolTimeUpdate()
    {
        GameMaster.GetComponent<CTime>().TimeUpdate(ref _CoolingTime);
        //Debug.Log(_CoolingTime);
        CDImage.fillAmount = _CoolingTime / CoolingTime;
    }

    //回合技能冷却更新
    private void IntervalTimeUpdate()
    {
        GameMaster.GetComponent<CTime>().TimeUpdate(ref _IntervalTime, () => {
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
        GetComponent<Rigidbody2D>().simulated = false;
        transform.Translate(SliceDirection * _SliceDistance);
        GameMaster.GetComponent<CTime>().ScheduleOnce(() =>
        {
            Debug.Log("重力恢复");
            GetComponent<Rigidbody2D>().simulated = true;
        }, 1);
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
