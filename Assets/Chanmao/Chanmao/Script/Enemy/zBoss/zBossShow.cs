using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zBossShow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Boss;

    private void Start()
    {
        Boss.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Boss.SetActive(true);
            Debug.Log(Boss.name + "被激活");
            Destroy(gameObject);
        }
    }
}
