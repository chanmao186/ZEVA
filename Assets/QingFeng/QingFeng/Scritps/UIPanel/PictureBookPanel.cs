using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PictureBookPanel : BasePanel
{

    private CanvasGroup canvasGroup;
    public List<Sprite> gameCG;
    public Image image1;
    public Image image2;

    public int index = 0;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    /// <summary>
    /// 点击按钮，绘本向前翻页
    /// </summary>
    public void pictureForward()
    {
        if (index <= 13)
        {
            index++;
            image2.enabled = false;
            if (index == 1)
            {
                image2.enabled = true;
                image2.sprite = gameCG[index];
            }
            else if (index == 5)
            {
                image2.enabled = true;
                image2.sprite = gameCG[index];
            }
            else if (index != 1 && index != 5)
            {
                image1.sprite = gameCG[index];
            }
        }
        else if (index == 14)
        {
            index = 0;
            image1.sprite = gameCG[index];
            OnClosePanel();
            OnPushPanel("Game");
            SceneManager.LoadScene("Card_1_Map_1");
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

}
