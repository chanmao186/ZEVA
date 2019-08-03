using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameRoot : MonoBehaviour {

    public FileData file;
    public static GameRoot Instance;
	void Awake () {
        file = GetComponent<FileData>();
        if (SceneManager.GetActiveScene ().name == "Init")
        {
            UIManager.Instance.PanelPush(UIPanelType.Main);
        }
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(transform.gameObject);
            }
        }
    }

    private void Update()
    {

    }
}
