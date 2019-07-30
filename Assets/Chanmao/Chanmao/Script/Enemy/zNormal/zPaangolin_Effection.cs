using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayWay
{
    Enter,Exit
}
public class zPaangolin_Effection : MonoBehaviour
{
    public ParticleSystem Effection;

    public PlayWay playWay;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            EffectionPlay();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            EffectionPlay();
        }
    }
    private void EffectionPlay()
    {
        if(playWay == PlayWay.Enter)
        {
            Effection.transform.position = transform.position;
            Effection.Play();
        }     
    }
    // Update is called once per frame
}
