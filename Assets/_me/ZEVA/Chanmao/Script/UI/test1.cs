using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    // Start is called before the first frame update
    CTime time;
    void Start()
    {
        time = GetComponent<CTime>();
        string a, b;
        a = "a";
        b = "b";
        time.ScheduleOnce(() => { Debug.Log(a); }, 9);
        time.ScheduleOnce(() => { Debug.Log(b); }, 5);
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        Debug.Log("在fixedUpdate中执行");
        Debug.Log("time:" + Time.time);
        Debug.Log("deltatime" + Time.deltaTime);
        Debug.Log("fixedtime:" + Time.fixedTime);
        Debug.Log("fixedDeltatimetime:" + Time.fixedDeltaTime);
    }
}
