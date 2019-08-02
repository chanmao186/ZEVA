using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameRoot : MonoBehaviour {

    public static GameRoot instance;
    public static GameRoot Instance
    {
        get
        {
            if (instance ==null)
            {
                GameObject obj = new GameObject();
            }
            return instance;
        }
    }
	void Awake () {
        UIManager.Instance.PanelPush(UIPanelType.Main);
        DontDestroyOnLoad(this.gameObject);
        
    }

    private void Update()
    {

    }
}
