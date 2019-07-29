using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[SerializeField]
public class UIPanelInfo {
    [NonSerialized]
    public UIPanelType panelType;
    public string panelTypeString
    {
        get {
            return panelType.ToString();
        }
        set {
            UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), value);
            panelType = type;
        }
    }
    public string path;

}
