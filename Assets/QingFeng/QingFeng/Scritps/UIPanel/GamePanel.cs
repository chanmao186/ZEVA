using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GamePanel : BasePanel
{
    private CanvasGroup canvasGroup;
    public Text text;
    public Text tip;
    public bool isOpenTip;

    public List<Image> healths;
    public FileData file;
    public int healthCur = 0;
    public bool isPause=false ;
    public void ReduceHealth()
    {
        if (healthCur==0)
        {
            healths[healthCur].color = new Color(0.3f, 0.3f, 0.3f);
        }else if (healthCur >0)
        {
            healths[healthCur].color = new Color(0.3f, 0.3f, 0.3f);
            healthCur--;
        }
    }
    public void RaiseHealth()
    {
        if (healthCur <4)
        {
            healthCur++;
            healths[healthCur].color = Color.white ;
        }
    }

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        file = GetComponent<FileData>();
    }
    public void Start()
    {
        
    }
    public void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape)&&isPause ==false )
        {
            isPause = true;
            OnClosePanel();
            PauseGame();
            OnPushPanel("Suspend");
        }
        if (Input .GetKeyDown(KeyCode.J))
        {
            ReduceHealth();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RaiseHealth();
        }
    }
    public void TipChange(string str)
    {
        isOpenTip = true;
        tip.text = str;
        tip.enabled = true ;
        tip.color = Color.clear;
        tip.DOColor(Color.white, 1);
    }

    public void  TipsDisplay(string str ,Vector3 pos)
    {
        if (!isOpenTip)
        {
            text.text = str;
            text.transform.position = Camera.main.WorldToScreenPoint(pos);
            text.enabled = true;
        }
    }
    public  void TipsHide()
    {
        text.enabled = false ;
        isOpenTip = false;
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
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void PauseGame()
    {
        Debug.Log("暂停游戏");
    }
}
