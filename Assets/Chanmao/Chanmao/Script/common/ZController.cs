using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate IEnumerable Action();

public class ZController : MonoBehaviour
{

    // Start is called before the first frame update
    public ZTime zTime;
    public ZCheck zCheck;
    public zMoveTo zMove;
    public zColor zColor;
    void Start()
    {
        if (zColor == null)
            zColor = GetComponent<zColor>();
        if (zMove == null)
            zMove = GetComponent<zMoveTo>();
        if (zCheck == null)
            zCheck = GetComponent<ZCheck>();
        if (zTime == null)
            zTime = GetComponent<ZTime>();
    }


}
