using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zBat : zFlyEnemy
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void PreAbility()
    {
        base.PreAbility();
        Ani.SetInteger("Attack", 1);
    }

    protected override void Pursue()
    {
        base.Pursue();
        Ani.SetInteger("Attack", 2);
    }
}
