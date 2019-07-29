using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPanel : BasePanel 
{
    private CanvasGroup canvasGroup;

    public void Awake()
    {
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
    /// 当前面板显示
    /// </summary>
    private void PanelDisplay()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    /// <summary>
    /// 当前面板隐藏
    /// </summary>
    private void PanelHide()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    public override void OnEnter()
    {
        PanelDisplay();
    }
    public override void OnExit()
    {
        PanelHide();
    }


    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("退出游戏");
        Application.Quit();
    }
}
