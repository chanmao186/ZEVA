using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager {

    //使用单例模式管理UI
    private static UIManager _instance;
    public static UIManager Instance {
        get {
            if (_instance==null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
        
    }
    public UIManager ()
    {
        ParseUIPanelTypeJson();
    }

    //利用Json存储UI面板的类型和对应路径，用LitJson解析
    private Dictionary<UIPanelType, string> panelPathDict;//存储UI面板信息
    private List<UIPanelInfo> panelInfos;

    private Dictionary<UIPanelType, BasePanel> panelDict;//存储实例化的UI面板

    private Stack<BasePanel> panelStack;//UI页面栈


    private Transform canvasTransform;//UI实例化后在的Canvas的位置
    private Transform CanvasTransform {
        get
        {
            if (canvasTransform==null)
            {
                canvasTransform = GameObject.Find("Canvas").GetComponent<Transform>() ;
            }
            return canvasTransform;
        }
    }


    /// <summary>
    /// 解析储存面板类型和路径的Json文件,并将信息存储到字典中
    /// </summary>
    private void ParseUIPanelTypeJson()
   {
        if (panelPathDict==null )
            panelPathDict = new Dictionary<UIPanelType, string>();
        panelInfos = new List<UIPanelInfo>();

        TextAsset textAsset = Resources.Load<TextAsset>("UIPanelType");
        string panelJson = textAsset.text;
        panelInfos = JsonMapper.ToObject<List<UIPanelInfo>>(panelJson);
        foreach (UIPanelInfo info in panelInfos)
        {
            panelPathDict.Add(info.panelType, info.path);
        }
   }

    /// <summary>
    /// 获取面板类型并且实例化面板，将其储存在字典中
    /// </summary>
    /// <param 需要实例化的面板类型="panelType"></param>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict==null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel panel;
        panelDict.TryGetValue(panelType,out panel);
        if (panel==null)
        {
            string panelPath;
            panelPathDict.TryGetValue(panelType, out panelPath);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(panelPath))as GameObject;
            instPanel.transform.SetParent(CanvasTransform,false);
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return  instPanel.GetComponent<BasePanel>();
        }

        return panel;
    }

    /// <summary>
    /// 把某页面入栈并实例化显示
    /// </summary>
    /// <param name="panelType"></param>
    public void PanelPush(UIPanelType panelType)
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }

    /// <summary>
    /// 把某个页面出栈，不显示该页面
    /// </summary>
    /// <param 需要关闭的UI面板类型="panelType"></param>
    public void PanelPop()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack .Count<= 0)
        {
            return;
        }
        BasePanel panel = panelStack.Pop();
        panel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel nextPanel = panelStack.Peek();
        nextPanel.OnResume();
    }

    public void DarkLoad(string scene)
    {
        GameObject.Find("DarkEffect").GetComponent<DarkEffect>().DarkDisPlaySH(scene);
    }
    public void DarkEffect(TweenCallback callback )
    {
        GameObject.Find("DarkEffect").GetComponent<DarkEffect>().DarkDisPlay(callback);
    }
}
