using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : CCharacter
{
    [Header("监视距离")]
    public float InspectDistance = 10f;

    [Header("死后销毁时间(为0时不销毁)")]
    public float DestroyTime = 3f;

    /// <summary>
    /// 怪物开始追击玩家
    /// </summary>
    protected virtual void PursuePlayer()
    {

    }
}
