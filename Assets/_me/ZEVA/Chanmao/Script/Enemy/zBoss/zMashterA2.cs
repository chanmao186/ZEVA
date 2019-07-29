using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zMashterA2 : MonoBehaviour
{
    [Header("冰锥的飞行速度")]
    public float Speed = 1;

    [Header("飞行之后，多长时间内自动销毁")]
    public float destroyTime = 2;
    private float _destroyTime = 0;
    public bool Rush1 = false;
    // Start is called before the first frame update
    private Animator Ani;
    private void Start()
    {
        Ani = GetComponent<Animator>();
        Ani.SetTrigger("Show");
        _destroyTime = 0;
    }
    public void Rush()
    {
        Rush1 = true;        
    }

    public void Rush(float speed)
    {
        Rush();
        Ani.SetTrigger("Rush");
        Speed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        if (Rush1)
        {
            transform.Translate(Speed * Time.deltaTime * Vector2.down);
            _destroyTime += Time.deltaTime;
            if (_destroyTime > destroyTime)
            {
                Destroy(gameObject);
            }
        }
           
    }
}
