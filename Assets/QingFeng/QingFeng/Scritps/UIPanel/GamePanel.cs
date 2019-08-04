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
    public bool isUp;

    public List<Image> healths;
    public FileData file;
    public int healthCur = 0;
    public bool isPause=false ;
    public void ReduceHealth()
    {
        if (healthCur == 1)
        {
            healths[healthCur - 1].color = new Color(0.3f, 0.3f, 0.3f);
            healthCur--;
            file.ReadFile(UIManager.Instance.nowFileNum);
            isUp = true;
            return;
        }
        healths[healthCur-1].color = new Color(0.3f, 0.3f, 0.3f);
        healthCur--;
    }
    public void RaiseHealth()
    {
        if (healthCur == 5)
            return;
        healths[healthCur].color = Color.white;
        healthCur++;
    }

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        file =GameObject.Find ("UIManager"). GetComponent<FileData>();
    }
    public void Start()
    {
        for (int i = 0; i <=file.health; i++)
        {
            healths[i].color = Color.white;
            healthCur = i+1;
            Debug.Log(i);
        }
    }
    public void healthUD()
    {
        if (isUp)
        {
            for (int i = 0; i <= file.health; i++)
            {
                healths[i].color = Color.white;
                healthCur = i + 1;
                Debug.Log(i);
            }
            isUp = false;
        }
    }
    public void Update()
    {
        healthUD();
        if (Input.GetKeyDown (KeyCode.Escape)&&isPause ==false )
        {
            isPause = true;
            OnClosePanel();
            PauseGame();
            OnPushPanel("Suspend");
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
