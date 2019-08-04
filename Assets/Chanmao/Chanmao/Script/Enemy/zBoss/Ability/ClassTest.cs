using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        A aa = new C();
        aa.Fun();
    }

    // Update is called once per fram
}

public class A
{
    public virtual void Fun()
    {
        Debug.Log("A");
    }
}

public class B:A
{
    public override void Fun()
    {
        base.Fun();
        Debug.Log("B");
    }
}

public class C : B
{
    public override void Fun()
    {
        base.Fun();
        Debug.Log("c");
    }
}