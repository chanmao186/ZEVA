using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public enum PlayerState
{
    Idle, Walk, Jump, Death, Attaked,Read
}
public class zPlayer : CCharacter
{
    //public static event Func DeathEvent;
    // Start is called before the first frame update    
    [Header("玩家的蓄力时间")]
    public float CastTime = 0.3f;
    //public class fc{}
    //public float FirstJumpPower = 700;  
    //public float JumpPower = 120;
    [Header("初次跳跃的速度")]
    public float FirstJumpVeloctiy = 30;

    [Header("持续跳跃的速度")]
    public float JumpVeloctiy = 1;

    [Header("被攻击后的无敌时间")]
    public float InvincibleTime = 0.5f;

    [Header("攻击的频率(持续按照攻击键，一秒攻击几次)")]
    public float AttacksFrequence = 2;

    [Header("玩家是否可以操纵角色")]
    public bool isOperation = true;

    [Header("主角是否可以按R读取数据")]
    public bool CanRead = false;

    [Header("主角是否可以存档")]
    public bool CanSave = false;

    [Header("主角的奔跑速度")]
    public float runSpeed = 1.5f;

    //public bool isEffectionBySlice;
    //[Header("储存数据的文件")]
    //public JsonObject PlayData;
    [Header("跳起音效")]
    public AudioSource JumpUpAudio;
    private int JumpUpAudioNum;

    [Header("落下音效")]
    public AudioSource JumpDownAudio;
    private int JumpDownAuioNum;

    [Header("攻击音效")]
    public AudioSource AttackAudio;
    private int AttackAudioNum;

    [Header("被攻击音效")]
    public AudioSource AttackedAudio;
    private int AttackedAudioNum;

    [Header("走路的音效")]
    public AudioSource WalkAudio;
    private int WalkAudioNum;

    [Header("死亡的音效")]
    public AudioSource DeathAudio;
    private int DeathAudioNum;

    private float AttacksTime;
    private float _AttacksTime = 0;

    private float _CastTime;
    //设置一个变量检查玩家什么时候可以跳起

    //奔跑的速度
    private float rs;
    private bool CanJump1;
    private bool CanJump2;

    private PlayerState CurrentState;
    //private GameObject _Weapon;
    //记录攻击的次数
    private int AttacksNum;

    //private Vector2 Veo
    protected ZSlice zs;
    //检测玩家是否转身
    private float checkturn;

    public Root PlayerData;
    private int SliceNum = 1;

    //private bool TransState = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    protected override void Start()
    {
        base.Start();
        zs = GetComponent<ZSlice>();
        AttacksTime = 1 / AttacksFrequence;
        AttacksTime = AttacksTime < 0.3f ? 0.3f : AttacksTime;
        isGround = false;
        _CastTime = 0;
        rs = 1f;

        CurrentState = PlayerState.Idle;
        //Time.timeScale = 0;
        CanJump1 = false;
        CanJump2 = true;

        ResetParameter();

        //JsonConfig();
        LevelStart();              
        AudioConfig();      
    }

    public void GameStart()
    {
        //UIManager.Instance.GameStart();
        //Debug.Log(UIManager.Instance.GameStart());
        transform.position = UIManager.Instance.GameStart();
    }

    private void JsonConfig()
    {
        string JsonFilePath = Application.dataPath + "/Chanmao/Chanmao/Script/Json/PlayerData.json";
        StreamReader streamreader = new StreamReader(JsonFilePath);
        JsonReader js = new JsonReader(streamreader);//再转换成json数据
        PlayerData = JsonMapper.ToObject<Root>(js);//读取
    }
    /// <summary>
    /// 关卡开始，读取数据
    /// </summary>
    public void LevelStart()
    {
        ReadCurentData();     
    }

    public Animator GetPlayAni()
    {
        return Ani;
    }

    /// <summary>
    /// 将json文件初始化
    /// </summary>
    public void InitJson()
    {
        PlayerData state = PlayerData.PlayerStates[1];
        PlayerData Default = PlayerData.PlayerStates[0];

        state._Heath = Default. _Heath;
        state.Heath = Default. Heath;

        state.SliceDistance = Default.SliceDistance;
        state.SliceNum = Default.SliceNum;

        string json = JsonMapper.ToJson(PlayerData.PlayerStates);

        string JsonFilePath = Application.dataPath + "/Chanmao/Chanmao/Script/Json/PlayerData.json";
        StreamWriter sw = File.CreateText(JsonFilePath);
        sw.Close();
        File.WriteAllText(JsonFilePath, json, Encoding.UTF8);

        //将玩家的数据同步
        JsonConfig();
    }
    /// <summary>
    /// 关卡结束，保存数据
    /// </summary>
    public void LevelEnd()
    {
        WirteDateToJson();
    }

    private void WirteDateToJson()
    {
        PlayerData state = PlayerData.PlayerStates[1];

        state._Heath = _Heath;
        state.Heath = Heath;

        state.SliceDistance = zs.SliceDistance;
        state.SliceNum = zs.SliceNum;

        string json = JsonMapper.ToJson(PlayerData.PlayerStates);

        string JsonFilePath = Application.dataPath + "/Chanmao/Chanmao/Script/Json/PlayerData.json";
        StreamWriter sw = File.CreateText(JsonFilePath);
        sw.Close();
        File.WriteAllText(JsonFilePath, json, Encoding.UTF8);
        //Debug.Log("写入");
    }
    /// <summary>
    /// 读取默认值
    /// </summary>
    public void ReadCurentData()
    {
        _PlayerDateConfig(0);
    }

    /// <summary>
    /// 读取当前值
    /// </summary>
    public void ReadDefaultData()
    {
        _PlayerDateConfig(1);
    }

    /// <summary>
    /// 设置斩击次数
    /// </summary>
    /// <param name="num"></param>
    public void SetSliceNum(int num)
    {
        zs.SliceNum = num;
    }

    /// <summary>
    /// 设置斩击的长度
    /// </summary>
    /// <param name="Distance"></param>
    public void SetSliceDistance(float Distance)
    {
        zs.SliceDistance = Distance;
    }
    /// <summary>
    /// 进行游戏配置
    /// </summary>
    /// <param name="type">0为配置为默认版本，1为配置为当前版本</param>
    private void _PlayerDateConfig(int type)
    {
        //_Heath = PlayerData.PlayerStates[type]._Heath;
        Heath = PlayerData.PlayerStates[type].Heath;

        zs.SliceDistance = PlayerData.PlayerStates[type].SliceDistance;
        zs.SliceNum = PlayerData.PlayerStates[type].SliceNum;
    }
   
    /// <summary>
    /// 进行音乐配置
    /// </summary>
    private void AudioConfig()
    {
        JumpUpAudioNum = AudioManager.Instance.Init(JumpUpAudio, true);
        JumpDownAuioNum = AudioManager.Instance.Init(JumpDownAudio, true);
        AttackAudioNum = AudioManager.Instance.Init(AttackAudio, true);
        AttackedAudioNum = AudioManager.Instance.Init(AttackedAudio, true);
        WalkAudioNum = AudioManager.Instance.Init(WalkAudio, true);
        DeathAudioNum = AudioManager.Instance.Init(DeathAudio, true);
    }

    private void Jump()
    {
        //CanJump2的功能，防止玩家一直按住跳跃键时，玩家向上跳跃
        if (Input.GetButtonUp("MyJump"))
        {
            //此时播放玩家下落的动画
            CanJump1 = false;
            CanJump2 = true;
            _CastTime = 0;
        }
        if (!CanJump1) return;
        if (Input.GetButtonDown("MyJump"))
        {
            //AudioManager.Instance.StopEffect(JumpUpAudioNum);
            AudioManager.Instance.PlayEffect(JumpUpAudioNum);

            //播放玩家跳跃的动画

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, FirstJumpVeloctiy);
        }
        else if (Input.GetButton("MyJump") && CanJump2 && SliceNum == 0)
        {
            //播放玩家跳跃的动画
            _CastTime += Time.fixedDeltaTime;
            //设置蓄力时间的上限
            if (_CastTime > CastTime)
            {
                //播放玩家下落的动画  

                CanJump1 = false;
                CanJump2 = false;
                _CastTime = 0;
            }
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpVeloctiy);
        }
    }

    private void Move()
    {
        float i = Input.GetAxis("Horizontal");
        //控制认为左右移动        
        if (i != 0)
        {
            Ani.SetFloat("Speed", 1f);
            // 获取当前玩家要前进的方向
            float Direction = i > 0 ? 1 : -1;
            if (checkturn != Direction)
            {
                //如果玩具转向的话，刀刃立即消失
                Weapon.SetActive(false);
            }

            if (!Ani.GetBool("JumpDown") && !Ani.GetBool("JumpUp"))
            {
                AudioManager.Instance.PlayEffect(WalkAudioNum);
            }
           

            checkturn = Direction;
            transform.Translate(Vector2.right * WalkSpeed * rs * Direction * Time.deltaTime, Space.World);

            Scale.x = xScale * -Direction;
            transform.localScale = Scale;

            GetComponent<Rigidbody2D>().simulated = true;
        }
        else
        {
            Ani.SetFloat("Speed", 0f);
        }
    }

    bool HeadCheck()
    {
        return zc.zCheck.Collider2DCheck(new Vector2(Head.position.x, Head.position.y), Vector2.up,0.5f, "Ground", "Ground");
    }
    private void CanJumpUpdate()
    {
        if (!CanJump1)
        {
            CanJump1 = isGround;
        }
        else
        {
            //检测头顶上是否有物体
            CanJump1 = !HeadCheck();
        }
    }

    /// <summary>
    /// 根据条件，播放玩家跳跃的动画
    /// </summary>
    private void PlayJumpAimation()
    {

        //检测玩家是否再地面上，若不再地面上则执行跳跃的代码
        if (!isGround)
        {
            bool JumpState = _rigidbody2D.velocity.y > 0;
            Ani.SetBool("JumpUp", JumpState);
            Ani.SetBool("JumpDown", !JumpState);
        }
        else
        {
            if (Ani.GetBool("JumpDown"))
            {
                //AudioManager.Instance.PlayEffect(JumpDownAuioNum);
            }
            Ani.SetBool("JumpDown", false);
            SliceNum = 0;
            if (CurrentState == PlayerState.Death)
            {
                PlayerDeath();
            }
        }
    }

    /// <summary>
    /// 寻找攻击的方向
    /// </summary>
    private void SeekAttackDirection()
    {
        Vector2 ms = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 d = ms - new Vector2(transform.position.x, transform.position.y);
        float angle = Vector2.Angle(new Vector2(-Scale.x, 0), d);

        if (angle > 105)
        {
            Scale.x = -Scale.x;
            transform.localScale = Scale;
        }
        //待用代码，计算角色360攻击的动画，需要时启用
        //angle = ms.y > transform.position.y ? angle : -angle;
        //Debug.Log("当前的角度为：" + angle);
        //Ani.SetBool("Attack", true);
    }

    private void Attack()
    {
        if (_AttacksTime < AttacksTime)
            _AttacksTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _AttacksTime >= AttacksTime)
        {

            SeekAttackDirection();
            Ani.SetInteger("Attack", AttacksNum);
            AttacksNum = 1;

            //播放普通攻击的音效
            AudioManager.Instance.StopEffect(AttackAudioNum);
            AudioManager.Instance.PlayEffect(AttackAudioNum);

            zc.zTime.ScheduleOnce(() =>
            {
                Weapon.SetActive(true);
                zc.zTime.ScheduleOnce(() => { Weapon.SetActive(false); }, 0.15f);
                Ani.SetInteger("Attack", 0);
            }, 0.1f);
            _AttacksTime = 0;
        }
        else if (Input.GetMouseButton(0))
        {

        }

        //Debug.Log(AttacksNum);
    }

    /// <summary>
    /// 获取玩家的状态，检测现在玩家是否再地上
    /// </summary>
    /// <returns></returns>
    public bool GetIsGround()
    {
        return isGround;
    }
    private void FixedUpdate()
    {
        if (isOperation)
        {
            Move();
            Jump();
            Attack();
        }
        else
        {
            OpearationAwake();
        }
        
        CanJumpUpdate();
        PlayJumpAimation();
        SeekzController();
    }


    void Update()
    {
        isGroundUpdate();
        //GameStart();
        if (isOperation)
        {       
            ZSlice();          
            ReadStroy();
        }
        
    }

    private void OpearationAwake()
    {
        if (CurrentState == PlayerState.Read)
        {
            if (Input.GetButton("Horizontal"))
            {
                CurrentState = PlayerState.Idle;
                ReadStory_End();
            }
        }
    }

    private void SeekzController()
    {
        if (zc == null)
        {
            GameStart();
            zc = GameObject.FindObjectOfType<ZController>();
        }
    }
    /// <summary>
    /// 鼠标开始执行切割的动作
    /// </summary>
    private void ZSlice()
    {
        //else 
        if (Input.GetMouseButton(1) && !isGround && SliceNum == 0)
        {
            _rigidbody2D.velocity = new Vector2(0, 0);
            //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            zs.SetSlicePoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
            SliceNum++;
            SliceStart();
        }
    }


    private void SliceStart()
    {
        ResetParameter();
        SetOperationState(false);
        SetReciveHurtState(false);
        Ani.SetInteger("Slice", 1);
        _time = zc.zTime.ScheduleOnce(() =>
        {
            //开始进行斩击
            zs.SliceStart();

            _time = zc.zTime.ScheduleOnce(() =>
            {
                Ani.SetInteger("Slice", 2);

                _time = zc.zTime.ScheduleOnce(() =>
                {
                    //动作恢复
                    Ani.SetInteger("Slice", 0);
                    SetOperationState(true);
                    SetReciveHurtState(true);
                }, zs.EndSliceTime);
            }, 0.1f);
        }, 0.1f);
        //zs.SliceStart();
    }
    /// <summary>
    /// 设置玩家是否可以进行操作
    /// </summary>
    /// <param name="State"></param>
    public void SetOperationState(bool State)
    {
        isOperation = State;
    }

    /// <summary>
    /// 主角开始读取数据
    /// </summary>
    private void ReadStroy()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (CanRead)
            {
                ResetParameter();
                //播放转身的动画
                Ani.SetInteger("Turn", 1);
                //终止玩家的其他操作
                isOperation = false;
                CurrentState = PlayerState.Read;
            }
            if (CanSave)
            {
                Debug.Log("游戏存档，血量回满");
                _Heath = Heath;
            }                      
        }
    }

    public void ResetAni()
    {
        ResetParameter();
    }
    /// <summary>
    /// 动作状态机重置
    /// </summary>
    protected override void ResetParameter()
    {
        Ani.SetFloat("Speed", 0);
        Ani.SetBool("JumpUp", false);
        Ani.SetBool("JumpDown", false);
        Ani.SetInteger("Attack", 0);
        Ani.SetInteger("Attacked", 0);
        Ani.SetInteger("Turn", 0);
        Ani.SetInteger("Slice", 0);

    }

    /// <summary>
    /// 读取故事结束
    /// </summary>
    public void ReadStory_End()
    {
        //播放转身回正的动画
        Ani.SetInteger("Turn", 2);

        //0.2秒后玩家可以正常操作
        _time = zc.zTime.ScheduleOnce(() =>
        {
            Ani.SetInteger("Turn", 0);
            isOperation = true;
        }, 0.2f);
    }

    /// <summary>
    /// 设置主角是否可以读取剧情
    /// </summary>
    /// <param name="State">读取的状态</param>
    public void SetReadState(bool State)
    {
        CanRead = State;
    }

    /// <summary>
    /// 角色保持死亡的状态
    /// </summary>
    public void PlayerDeath()
    {
        ResetParameter();

        SetOperationState(false);
        //主要目的是取消玩家的碰撞，并让主角回到地面上
        GetComponent<BoxCollider2D>().enabled = false;
        _rigidbody2D.velocity = new Vector2(0, 0);
        _rigidbody2D.gravityScale = 0;
        Ani.SetBool("Deathing", true);

        Destroy(gameObject, 1);


    }

    /// <summary>
    /// 角色复活
    /// </summary>
    public void PlayerResurgence()
    {
        //玩家播放复活的动作
        Ani.SetBool("Deathing", true);

        _time = zc.zTime.ScheduleOnce(() =>
        {
            SetOperationState(true);
        }, 0.5f);
    }

    protected override void AttackedEffection(Transform AttackPoint)
    {
        base.AttackedEffection(AttackPoint);
        ResetParameter();
        Ani.SetInteger("Attacked", 1);

        SetReciveHurtState(false);
        //Debug.Log(transform.name + "攻击影响代码执行");
        SetOperationState(false);
        _time = zc.zTime.ScheduleOnce(() =>
        {
            Ani.SetInteger("Attacked", 0);
            SetOperationState(true);
        }, 0.5f);

        zc.zTime.ScheduleOnce(() => { SetReciveHurtState(true); }, InvincibleTime);

        Debug.Log("玩家被攻击");

        //if (!isReceiveHurt) { return; }
        UIManager.Instance.ReduceHP();
    }

    protected override void DeathEffection()
    {
        base.DeathEffection();
        // Debug.Log("执行死亡的动作" + Ani.GetBool("Death"));
        Ani.SetBool("Death", true);

        Debug.Log("玩家死亡");
        CurrentState = PlayerState.Death;

        UIManager.Instance.ReduceHP();
        //Debug.Log("执行死亡的动作" + Ani.GetBool("Death"));
        SetOperationState(false);

        zc.zTime.ScheduleOnce(() => {
            ResetParameter();
        }, 0.5f);
        _Heath = Heath;
    }


    /// <summary>
    /// 获得玩家的朝向
    /// </summary>
    /// <returns></returns>
    public int GetDirection()
    {
        return Direction;
    }

    /// <summary>
    /// 角色立刻死亡死亡
    /// </summary>
    public void PlayerToDeath()
    {
        Debug.Log("玩家摔死了");
        //CurrentState = PlayerState.Death;
        //_Heath = 0;
        UIManager.Instance.ReadFile();
        _Heath = Heath;
        //Death();
    }
}
