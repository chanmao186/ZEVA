using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zBuff : MonoBehaviour
{
    public float SliceDistance = 7;
    private zPlayer Player;
    void Start()
    {
        Player = GameObject.FindObjectOfType<zPlayer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.SetSliceDistance(7);

            Destroy(gameObject);
        }
    }
}
