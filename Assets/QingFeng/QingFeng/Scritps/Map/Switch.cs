using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Switch : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Sprite[] sprites;
    public bool isOpen = false;
    public Transform stoneDoor;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isOpen)
            {
                sprite.sprite = sprites[0];
                isOpen = !isOpen;
                stoneDoor.GetComponent<StoneDoor>().CloseTheDoor(10);
            }
            else
            {
                sprite.sprite  = sprites[1];
                isOpen = !isOpen;
               
                stoneDoor.GetComponent<StoneDoor>().OpenTheDoor(10);
            }
        }
    }
}
