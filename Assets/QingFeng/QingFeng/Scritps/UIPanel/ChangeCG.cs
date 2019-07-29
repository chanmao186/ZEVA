using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeCG : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public List<Sprite> sprites;
    public int index=0;
    public GameObject   image;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player"&&index <=2)
        {
            //TODO 动画
            player.position = startPoint.position;
            index++;
            image.GetComponent<SpriteRenderer>().sprite = sprites[index];
        }
        if (index >= 4)
        {
            Debug.Log(1);
            SceneManager.LoadScene("Card_2_Map_4");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && index > 2)
        {
            index++;
        }
    }
}
