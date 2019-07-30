using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zHurt : MonoBehaviour
{
    [Header("要承受的物体的标签")]
    public string[] AttackedTag;
    [Header("伤害值")]
    public float Hurt = 1;

    [Tooltip("适用于技能撞击墙壁或玩家后消失")]
    [Header("是否销毁自身")]
    public bool isDestroy = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string tag in AttackedTag)
        {
            if (collision.transform.CompareTag(tag))
            {
                if (collision.transform.GetComponent<CCharacter>())
                    collision.transform.GetComponent<CCharacter>().Attacked(Hurt, transform);
                if (isDestroy)
                    Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in AttackedTag)
        {
            if (collision.transform.CompareTag(tag))
            {
                if(collision.GetComponent<CCharacter>())
                    collision.GetComponent<CCharacter>().Attacked(Hurt,transform);
                if (isDestroy)
                    Destroy(gameObject);
            }
        }
    }

}
