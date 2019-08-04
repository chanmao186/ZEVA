using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zDeathLimited : MonoBehaviour
{
    private zPlayer Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindObjectOfType<zPlayer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player.PlayerToDeath();
        }
    }
    // Update is called once per fram
}
