﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SoundPanel : BasePanel 
{
    private CanvasGroup canvasGroup;
    public Slider audioTotal;
    public Slider audioBGM;
    public Slider audioEffect;
    public bool isInited=false ;
    public bool isDisPlay = false;          //面板是否显示
    public float AnimSpeed = 1;            //动画播放时间

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        audioTotal.value = AudioManager.Instance.totalVolume;
        audioEffect.value = AudioManager.Instance.effectVolume;
        audioBGM.value = AudioManager.Instance.bgmVolume;
        isInited = true;
    }
    public void TotalVolume()
    {
        if (isInited)
        {
            AudioManager.Instance.totalVolume = audioTotal.value;
            BgmVolume();
            EffectVolume();
        }
    }

    public void BgmVolume()
    {
        if (isInited)
        {
            AudioManager.Instance.AdjustBgmVolume(audioBGM.value);
        }
    }
    public void EffectVolume()
    {
        if (isInited)
        {
            AudioManager.Instance.AdjustEftVolume(audioEffect.value);
        }
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
    public override void OnExit()
    {
        PanelAnim();
    }

    /// <summary>
    /// 设置为默认值
    /// </summary>
    public void SetDefault()
    {
        Debug.Log("音量恢复默认");
    }
}