using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zHedgehog : zGroundEnemy
{
    [Header("刺猬要伸长的刺的节点")]
    public Transform Thorn;

    [Header("刺猬穿刺的长度")]
    public float ThornHeight = 5;

    [Tooltip("刺猬在刺伸长的状态下的攻击范围")]
    [Header("攻击检测点")]
    public GameObject Attack1Area;
    // Start is called before the first frame update
    protected float AbilityDistance;

    protected float prePursueTime = 0.5f;
    protected float _prePursueTime = 0;
    Quaternion ThornQuanternion;
    Vector3 ThornScale;

    //private bool Blink = false;

    protected override void Start()
    {
        base.Start();
        //ThornScale = Thorn.localScale;
        //ThornQuanternion = Thorn.rotation;
        AbilityDistance = GetComponent<SpriteRenderer>().size.x * xScale;
        //Debug.Log("zHedgehog执行");
    }


    protected override void Idle()
    {
        base.Idle();
        //随机眨眼睛
        if (Random.Range(0, 600) > 590&&!Ani.GetBool("Blink"))
        {
            //Blink = true;
            Ani.SetBool("Blink", true);
            zc.zTime.ScheduleOnce(() => {
                Ani.SetBool("Blink", false);
            }, 0.5f);
        }
    }
    protected override void PreAbility()
    {
        base.PreAbility();
        Ani.SetBool("Blink", false);
        Ani.SetInteger("Attack", 1);
    }
    protected override void Pursue()
    {
        //float distance = Player.position.x - transform.position.x;
        //int d = distance > 0 ? 1 : -1;
        //transform.Translate(RunSpeed * Time.fixedDeltaTime * d, 0, 0);
        //Direction = distance > 1 ? d : Direction;
        MoveToPoint(RunSpeed, Player);
        Ablility();
    }

    protected override void EndAbility()
    {
        base.EndAbility();
        Ani.SetFloat("Speed", 0);
        Ani.SetInteger("Attack", 0);
    }
    protected override void ReturnOriginPos()
    {
        //Debug.Log("刺猬恢复原来的状态");             
        MoveToPoint(WalkSpeed, OriginPoint);
        Ani.SetFloat("Speed", 1);

    }
    protected override void Ablility()
    {
        //Vector3 _s = Thorn.localScale;
        if (Mathf.Abs(Player.position.x - transform.position.x) < AbilityDistance)
        {
            //Debug.Log("刺猬释放技能，伸长了刺");
            Ani.SetInteger("Attack", 2);
            Attack1Area.SetActive(true);
            //_s.y = ThornHeight;
            //var q = Thorn.rotation;
        }
        else
        {
            Ani.SetInteger("Attack", 1);
            Attack1Area.SetActive(false);
        }
        //Thorn.localScale = _s;
    }

    protected override void DeathEffection()
    {
        base.DeathEffection();
        Ani.SetBool("Blink", false);
        Ani.SetFloat("Speed", 0);
        Ani.SetInteger("Attack", 0);
        Ani.SetBool("Death", true);
    }
    //protected override void
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
