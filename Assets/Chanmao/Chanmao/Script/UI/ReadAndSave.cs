using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadAndSave : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Read = false;
    public bool Save = false;

    public zPlayer Player;
    void Start()
    {
        Player = FindObjectOfType<zPlayer>();  
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.CanSave = Save;
            Player.CanRead = Read;
        }
    }
    // Update is called once per frame

}
