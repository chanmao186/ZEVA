using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zMasterA3 : MonoBehaviour
{
    public float PreTime = 0.5f;
    private float _PreTime;
    // Start is called before the first frame update
    void Start()
    {
        _PreTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_PreTime < PreTime)
        {
            _PreTime += Time.deltaTime;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
