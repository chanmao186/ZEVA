using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zSelf_Shade : MonoBehaviour
{
    public float OffSetX = 0.5f;
    public float OffSerY = 0.5f;
    // Start is called before the first frame update
    public Animator Ani;

    public Animator pAni;

    public Transform Player;

    void Start()
    {
        if (Ani == null)
            Ani = GetComponent<Animator>();

        
        if (pAni == null)
            pAni = GameObject.FindObjectOfType<zPlayer>().GetPlayAni();
        if (Player == null)
        {
            Player = pAni.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Player.position.x + OffSetX, Player.position.y + OffSerY);

        transform.localScale = Player.localScale;

        Ani.SetFloat("Speed", pAni.GetFloat("Speed"));
        Ani.SetBool("JumpUp", pAni.GetBool("JumpUp"));
        Ani.SetBool("JumpDown", pAni.GetBool("JumpDown"));
        Ani.SetBool("Death", pAni.GetBool("Death"));
        Ani.SetBool("Deathing", pAni.GetBool("Deathing"));
        Ani.SetInteger("Attack", pAni.GetInteger("Attack"));
        Ani.SetInteger("Attacked", pAni.GetInteger("Attacked"));
        Ani.SetInteger("Turn", pAni.GetInteger("Turn"));
        Ani.SetInteger("Slice", pAni.GetInteger("Slice"));
    }
}
