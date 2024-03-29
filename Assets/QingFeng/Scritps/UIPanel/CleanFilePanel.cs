﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CleanFilePanel : BasePanel 
{
    private CanvasGroup canvasGroup;
    public bool isDisPlay = false;          //面板是否显示
    public float AnimSpeed = 1;            //动画播放时间
    public int clearNum = -1;
    public FileData fileData;

    public void Awake()
    {
        fileData = GameObject.Find ("UIManager"). GetComponent<FileData>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 当前面板关闭
    /// </summary>
    public void OnClosePanel()
    {
        UIManager.Instance.PanelPop();
    }
    /// <summary>
    /// 跳转至指定UI面板
    /// </summary>
    /// <param name="panelTypeString">跳转面板类型</param>
    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PanelPush(panelType);
    }


    /// <summary>
    /// 面板显示的动画效果
    /// </summary>
    private void PanelAnim()
    {
        isDisPlay = !isDisPlay;
        if (isDisPlay)
        {
            Tween tween = canvasGroup.DOFade(1, AnimSpeed);
            tween.OnComplete(PanelDisplay);
        }
        else
        {
            PanelHide();
        }
    }

    /// <summary>
    /// 当前面板显示
    /// </summary>
    private void PanelDisplay()
    {
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 当前面板隐藏
    /// </summary>
    private void PanelHide()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    //面板基本状态
    public override void OnEnter()
    {
        PanelAnim();
    }
    public override void OnResume()
    {
        PanelAnim();
    }
    public override void OnExit()
    {
        PanelAnim();
    }
    public override void OnPause()
    {
        PanelAnim();
    }

    /// <summary>
    /// 清除存档
    /// </summary>
    public void CleanFile()
    {
        clearNum = UIManager.Instance.clearFileNum;
        if (clearNum < 0)
        {
            return;
        }
        fileData.ClearFile(clearNum);
        GameObject.Find("FilePanel(Clone)").GetComponent<FilePanel>().UpdateFile();
    }
}
