using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zPangolin : zGroundEnemy
{
    [Header("进入洞穴的粒子效果释放的检测点")]
    public GameObject EnterCheck;
    [Header("跳出洞穴的粒子效果释放的检测点")]
    public GameObject ExiterCheck;
    //[Header("跳入洞穴的特效")]
    //public ParticleSystem effection;
    protected bool CanChangState;
    protected SpriteRenderer sr;
    protected Collider2D c2;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        CanChangState = true;
        c2 = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Idle()
    {
        if (Random.Range(0, 600) > 580)
        {
            Ani.SetTrigger("Blink");
        }
    }
    protected override void Pursue()
    {
        //将角色转换为释放技能的状态
        CurrentState = EnemyState.Ability;
        //IsCheckState = false;
        //将状态转换器终止
        //关闭重力系统
        //isGrivaty = false;
        //CanChangState = false;
        //先确定要跳跃的方向
        Ani.SetBool("JumpIn", true);
        //停止走路的动作
        Ani.SetFloat("Speed", 0);
        //穿山甲跳入地下
        pJump(()=> {
            //开启跳跃的检测
            Debug.Log("激活跳入洞穴的碰撞检测");
            EnterCheck.SetActive(true);
        },()=> {
            //停止播放跳入动作
            Ani.SetBool("JumpIn", false);
            //sr.enabled = false;

            
            //一段时间后准备跳出           
            _time = zc.zTime.ScheduleOnce(() => {
                transform.position = new Vector2(Player.position.x, transform.position.y);
                //effection.transform.position = new Vector2(Player.position.x, effection.transform.position.y);
                //跳出点开始播放动画
                _time = zc.zTime.ScheduleOnce(() => {
                    Ani.SetInteger("JumpOut", 1);
                    //effection.Play();

                    //激活跳出洞穴的碰撞检测
                    Debug.Log("激活跳出洞穴的碰撞检测");
                    ExiterCheck.SetActive(true);
                    //执行跳出洞穴的动作
                    pJump(() => {                       
                        Ani.SetInteger("JumpOut", 2);
                    }, () => {
                        Ani.SetInteger("JumpOut", 3);
                        _time = zc.zTime.ScheduleOnce(() => {
                            CurrentState = EnemyState.Return;
                            Ani.SetInteger("JumpOut", 0);
                        }, 1);
                    }, 8, 5);
                }, 1);               
            }, 2);
        },5,8);
    }

    protected void pJump(Func Peak, Func CallBack, float Up,float Down)
    {
        Direction = Player.position.x - transform.position.x > 0 ? 1 : -1;
       
        action = zc.zMove.MoveToPosition(new Vector3(transform.position.x + 2 * Direction, transform.position.y + Up, 0), 10, () => {
            //Debug.Log("穿山甲移动到了目标位置");
            Peak?.Invoke();
            action = zc.zMove.MoveToPosition(new Vector3(transform.position.x + 2 * Direction, transform.position.y -Down, 0), 7, () => {
                //Debug.Log("穿山甲落到了地下");
                //zc.zTime.ScheduleOnce(CallBack, 0.2f);
                CallBack?.Invoke();
            }, transform);
        }, transform);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
