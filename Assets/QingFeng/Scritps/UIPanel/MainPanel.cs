using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class MainPanel : BasePanel
{
    private CanvasGroup canvasGroup;        
    public  bool isDisPlay=false ;          //面板是否显示
    public float  AnimSpeed = 1;            //动画播放时间
    public AudioSource audio;
    public int audioNum;
    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void Start()
    {
        audioNum = AudioManager.Instance.Init(audio, false);
        AudioManager.Instance.PlayBGM(audioNum);
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
        Tween tween = canvasGroup.DOFade(1, AnimSpeed );
        tween.OnComplete(PanelDisplay);       
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
    public void PanelHide()
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
        PanelHide();
    }
    public override void OnPause()
    {
        PanelHide();
    }
   

    /// <summary>
    /// 进入游戏
    /// </summary>
    public void  StartGame()
    {
        OnClosePanel();
        AudioManager.Instance.StopBGM(audioNum);
        
        foreach (FileInfo file in GameObject.Find ("UIManager") .GetComponent<FileData>().fileInfo )
        {
            if (!file.isSafe)
            {
                UIManager.Instance.nowFileNum = file.fileNum;
                Debug.Log("开始游戏，保存为存档："+file.fileNum);
                break;
            }
            if (UIManager.Instance.nowFileNum == -1)
            {
                UIManager.Instance.nowFileNum = 0;
            }
        }
        OnPushPanel("PictureBook");
    }
    

    
}
