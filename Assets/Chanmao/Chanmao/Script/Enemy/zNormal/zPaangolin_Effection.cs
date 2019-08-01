using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayWay
{
    Enter, Exit
}
public class zPaangolin_Effection : MonoBehaviour
{
    public ParticleSystem Effection;

    public PlayWay playWay;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")&&playWay == PlayWay.Enter)
        {
            EffectionPlay();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")&&playWay == PlayWay.Exit)
        {
            EffectionPlay();
        }
    }
    private void EffectionPlay()
    {
        Effection.transform.position = transform.position;
        Effection.Play();
        //运行完成后将该组件关闭
        gameObject.SetActive(false);
    }
    // Update is called once per frame
}
