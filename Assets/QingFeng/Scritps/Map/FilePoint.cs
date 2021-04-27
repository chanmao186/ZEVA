﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilePoint : MonoBehaviour
{ public string str = "按R键存档";
    public string tipStr = "数据已保存";
    public Vector3 vector=new Vector3 (0,1.2f,0);
    public GamePanel gamePanel;
    public Transform player;
    public bool isOpen = false;
    public FileData file;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)&&isOpen)
        {
            file.SafeFile(UIManager.Instance.nowFileNum);
            gamePanel.isUp = true;
            SafeComp();
        }
    }
    public void SafeComp()
    {
        gamePanel.TipsHide();
        gamePanel.TipChange(tipStr);
    }

    private void Start()
    {
        file = GameObject.Find ("UIManager"). GetComponent<FileData>();
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