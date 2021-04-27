using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FilePanel : BasePanel 
{
    private CanvasGroup canvasGroup;
    public bool isDisPlay = false;          //面板是否显示
    public float AnimSpeed = 1;            //动画播放时间
    public FileData fileData;
    public List<Text> texts;

    public void Awake()
    {
        fileData = GameObject.Find ("UIManager"). GetComponent<FileData>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

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
        UpdateFile();
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
        OnClosePanel();
        PanelAnim();
    }
    public override void OnExit()
    {
        PanelAnim();
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 加载存档
    /// </summary>
    public void ReadFile(int index)
    {
        if (fileData.fileInfo[index ].isSafe == true)
        {
            OnClosePanel();
            OnPushPanel("Game");
            AudioManager.Instance.StopBGM(0);
            fileData.ReadFile(index);
            UIManager.Instance.nowFileNum = index;
        }else 
        {
            OnClosePanel();
            OnPushPanel("Main");
        }
    }
    /// <summary>
    /// 清除存档
    /// </summary>
    /// <param name="index"></param>
    public void ClearFile(int index)
    {
        UIManager.Instance.clearFileNum = index;
    }
    
    public void UpdateFile()
    {
        foreach (FileInfo file in fileData.GetComponent<FileData>().fileInfo)
        {
            if (file.isSafe == true)
            {
                texts[file.fileNum].color = Color.white;
            }
            else
            {
                texts[file.fileNum].color = Color.clear;
            }
        }
    }
}
