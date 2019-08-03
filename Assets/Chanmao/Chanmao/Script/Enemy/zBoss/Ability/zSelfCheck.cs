using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zSelfCheck : MonoBehaviour
{

    public zSelfShow zs;

    private void Start()
    {
        zs = GameObject.FindObjectOfType<zSelfShow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            zs.BossShow();
            Destroy(gameObject);
        }
    }
}
