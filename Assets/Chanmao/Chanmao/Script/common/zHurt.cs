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

    public AudioSource AttackedAudio;

    private void Start()
    {
        AttackedAudio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string tag in AttackedTag)
        {
            if (collision.transform.CompareTag(tag))
            {
                if (AttackedAudio)
                    AttackedAudio.Play();
                if (collision.transform.GetComponent<zItem>())
                    collision.transform.GetComponent<zItem>().Attacked(Hurt, transform);
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
                if (AttackedAudio)
                    AttackedAudio.Play();
                if (collision.GetComponent<zItem>())
                    collision.GetComponent<zItem>().Attacked(Hurt, transform);
                if (isDestroy)
                    Destroy(gameObject);
            }
        }
    }
}
