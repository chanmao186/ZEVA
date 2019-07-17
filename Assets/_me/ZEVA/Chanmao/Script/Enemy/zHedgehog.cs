using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zHedgehog : zEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //Debug.Log("zHedgehog执行");
    }

    
    protected override void Pursue()
    {
        base.Pursue();
        Ani.SetInteger("Attack", 1);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
