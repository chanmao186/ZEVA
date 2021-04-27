using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasePanel : MonoBehaviour {

    /// <summary>
    /// 界面被显示
    /// </summary>
	public virtual void OnEnter()
    {

    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {

    }
    /// <summary>
    /// 界面继续交互
    /// </summary>
    public virtual void OnResume()
    {

    }
    /// <summary>
    /// 页面停止交互
    /// </summary>
    public  virtual  void OnExit()
    {

    }

}
