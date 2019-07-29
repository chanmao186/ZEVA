using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zGroundEnemy : zNormalEnemy
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Patrol()
    {

        base.Patrol();
        Ani.SetFloat("Speed", 1);
        //Debug.Log("朝目标点" + PatrolPoint[_TargetPointIndex].name + "前进");
        //Debug.Log(PatrolPoint[_TargetPointIndex].position);
        //Direction = PatrolPoint[_TargetPointIndex].position.x - transform.position.x > 0 ? 1 : -1;

        MoveToPoint(WalkSpeed, PatrolPoint[_TargetPointIndex]);
        //seekTarget.target = PatrolPoint[_TargetPointIndex];
        //若超过巡逻距离，改变巡逻方向
        //if (Mathf.Abs(transform.position.x - OriginPos.x) > PatrolRdius)
        //{
        //    //若超过距离，则换一个方向运动
        //    moveDirection.x = -moveDirection.x;
        //}

        //transform.Translate(moveDirection * WalkSpeed * Time.fixedDeltaTime);
    }

    protected override void MoveToPoint(float speed, Transform point)
    {
        WalkGroundCheck();
        float distance = point.position.x - transform.position.x;
        if (Mathf.Abs(distance) > 0.2f)
        {
            Direction = distance > 0 ? 1 : -1;
            //Debug.Log(distance);
            //播放走路的动画       
            transform.Translate(speed * Time.fixedDeltaTime * Direction, 0, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
