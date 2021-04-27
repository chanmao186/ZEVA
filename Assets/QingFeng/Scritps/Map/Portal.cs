using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Portal : MonoBehaviour
{
    public string sceneName;
    public bool isUnlock = false;
    public void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && isUnlock)
        {
            isUnlock = false;
            UIManager.Instance.DarkLoad(sceneName);
        }
    }
    public void Unlock()
    {
        isUnlock = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6f, 1, 0.6f, 1);

    }
}