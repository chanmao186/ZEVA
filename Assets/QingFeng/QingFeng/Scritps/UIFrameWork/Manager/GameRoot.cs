using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameRoot : MonoBehaviour {

    public FileData file;
    public static GameRoot Instance;
    public int health;
    public AudioSource mouseMove;
    public int moveNum;
    public AudioSource mouseCilck;
    public int cilckNum;

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
    public void Start()
    {
        moveNum = AudioManager.Instance.Init(mouseMove, true);
        cilckNum = AudioManager.Instance.Init(mouseCilck, true);
    }

    private void Update()
    {

    }
    public void MouseEnter()
    {
        AudioManager.Instance.PlayEffect(moveNum);
    }
    public void MouseExit()
    {
        AudioManager.Instance.StopEffect(moveNum);
        AudioManager.Instance.StopEffect(cilckNum);
    }
    public void MouseDown()
    {
        AudioManager.Instance.PlayEffect(cilckNum );
    }
}
