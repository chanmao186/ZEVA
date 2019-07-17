using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZController : MonoBehaviour
{
    // Start is called before the first frame update
    public ZTime zTime;
    public ZCheck zCheck;
    void Start()
    {
        zCheck = GetComponent<ZCheck>();
        zTime = GetComponent<ZTime>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
