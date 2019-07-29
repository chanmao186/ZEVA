using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")|| collision.CompareTag("Enemy"))
        Debug.Log("被击中");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
            Debug.Log("被击中1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
