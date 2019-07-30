using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator ani;
    //private float speed;
    void Start()
    {
        //speed = 0;
        ani = GetComponent<Animator>();
    }

    private void Move()
    {
        bool state = (Input.GetButton("Right") || Input.GetButton("Left"));
        ani.SetBool("Move", state);
    }

    /// <summary>
    /// 播放跳跃的动画
    /// </summary>
    public void Jump()
    {       
         ani.SetTrigger("JumpUp");
         ani.SetFloat("jumpUpTime", 2.5f);              
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            ani.SetBool("PlayDown", false);
            ani.SetBool("BackIdle",true);        
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
