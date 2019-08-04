using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Portal : MonoBehaviour
{
    public string sceneName;
    public bool isUnlock = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Player"&&isUnlock  )
        {
            isUnlock = false;
            UIManager.Instance.DarkLoad(sceneName);
        }
    }

    public void Unlock()
    {
        isUnlock = true;
        this.gameObject.GetComponent<Image>().color=new Color(0.6f,1,0.6f,1);

    }
}
