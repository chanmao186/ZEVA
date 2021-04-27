using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class zEnemyController : MonoBehaviour
{
    [Header("小怪物的数量")]
    public int EnemyNum = 1;

    [Header("是否开始执行计算操作")]
    public bool isCount = false;

    [Header("消灭完指定数量的小怪后要执行的事件")]
    public UnityEvent EnemyEmpty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnemtCount()
    {
        if (isCount)
        {
            EnemyNum--;
            Debug.Log("怪物死亡1只，还剩" + EnemyNum);
            if (EnemyNum == 0)
            {
                
                //UIManager.Instance.UnlockStone();
                EnemyEmpty?.Invoke();
            }
        }
    }
    // Update is called once per frame
}
