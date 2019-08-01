using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTablet : MonoBehaviour
{
    public string str = "按R键查看";
    public string tipStr = "这是一个石碑";
    public Vector3 vector=new Vector3 (0,1.2f,0);
    public GamePanel gamePanel;
    public Transform player;
    public bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)&&isOpen)
        {
            gamePanel.TipsHide();
            gamePanel.TipChange(tipStr);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gamePanel = GameObject.Find("GamePanel(Clone)").GetComponent<GamePanel>();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gamePanel.TipsDisplay(str, collision.GetComponent<Transform>().position + vector);
            isOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gamePanel.TipsHide();
            gamePanel.tip.enabled  = false;
            isOpen = false;
        }
    }

}
