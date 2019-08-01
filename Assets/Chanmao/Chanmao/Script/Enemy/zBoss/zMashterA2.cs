using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zMashterA2 : MonoBehaviour
{
    [Header("冰锥的飞行速度")]
    public float Speed = 1;

    [Header("飞行之后，多长时间内自动销毁")]
    public float destroyTime = 2;

    [Header("撞到哪些标签后物体要停下")]
    public string[] StopTag;
    private float _destroyTime = 0;
    public bool Rush1 = false;
    // Start is called before the first frame update
    private Animator Ani;
    private void Start()
    {
        Ani = GetComponent<Animator>();
        if(Ani)
        Ani.SetTrigger("Show");
        _destroyTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(string tag in StopTag)
        {
            if (collision.transform.CompareTag(tag)) {
                Rush1 = false;
            }
        }
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
