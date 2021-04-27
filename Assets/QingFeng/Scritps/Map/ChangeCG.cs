using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeCG : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public List<GameObject> sprites;
    public int index=0;
    public List<string> tips;
    public StoneTablet BG;
    public AudioSource audio;
    public int audioNum;
    public List < Animator >anim;
    public float  num;
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BG = GameObject.Find("BG").GetComponent<StoneTablet>();
        audioNum = AudioManager.Instance.Init(audio, false);
        AudioManager.Instance.PlayBGM(audioNum);
        anim[index].speed = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player"&&index <=2)
        {
            //TODO 动画
            player.position = startPoint.position;
            sprites[index].SetActive(false); 
        }
        if (collision.collider.tag == "Player" && index >= 3)
        {
            AudioManager.Instance.StopBGM(audioNum);
            SceneManager.LoadScene("Card_2_Map_4");
        }
    }
    public void PlayAnim()
    {
        anim[index].speed = 1;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" )
        {
            index++;
            sprites[index].SetActive(true );
            anim[index].speed =0;
        }
    }
}
