using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class zFlyEnemy : zNormalEnemy
{
    protected AIPath ai;
    protected AIDestinationSetter MoveTarget;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ai = GetComponent<AIPath>();
        ai.maxSpeed = WalkSpeed;
        MoveTarget = GetComponent<AIDestinationSetter>();
    }

    protected override void Pursue()
    {
        base.Pursue();
        MoveTarget.target = Player;
    }

    protected override void ReturnOriginPos()
    {
        MoveTarget.target = OriginPoint;
    }
    protected override void Patrol()
    {
        base.Patrol();
        MoveTarget.target = PatrolPoint[_TargetPointIndex];
    }
    // Update is called once per frame
}
