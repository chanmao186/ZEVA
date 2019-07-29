using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zEarthColler : MonoBehaviour
{
    [Header("x轴要放大的倍数")]
    public float xScale = 12.5f;

    [Header("放到所需的时间")]
    public float ExpendTime = 1.2f;

    [Header("存在的时间")]
    public float ExistTime = 2.8f;
    private float _ExpendTime;

    private bool isRun = true;
    private Vector2 scale;
    // Start is called before the first frame update
    void Start()
    {
        _ExpendTime = 0;

        scale = transform.position;        
    }

    public void Init()
    {
        _ExpendTime = 0;
        isRun = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            _ExpendTime += Time.deltaTime;
            if (_ExpendTime <= ExpendTime)
            {
                scale.x = xScale * (_ExpendTime / ExpendTime);
                transform.localScale = scale;
            }
            else if (_ExpendTime > ExistTime)
            {
                scale.x = 0;
                transform.localScale = scale;
                isRun = false;
            }
        }
        
    }
}
