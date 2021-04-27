using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;
//using UnityEditor;
using System.Text;

public class FileData : MonoBehaviour
{
    public List <FileInfo> fileInfo;
    public GameObject audioManager;
    public string fileData;
    public int health;
    private void Awake()
    {
        ParseFileJson();
        audioManager = GameObject.Find("AudioManager");
    }
    private void Update()
    {

    }

    public void ParseFileJson()
    {
        fileInfo = new List<FileInfo>();
        TextAsset textAsset = Resources.Load<TextAsset>("FileData");
        fileData = textAsset.text;
        fileInfo = JsonMapper.ToObject<List<FileInfo>>(fileData);
    }

    public void WriteFileJson()
    {
        string json = JsonMapper.ToJson(fileInfo );
        string filePath = Application.dataPath + "/QingFeng/QingFeng/Resources/FileData.json";
        StreamWriter sw = File.CreateText(filePath);
        sw.Close();
        File.WriteAllText(filePath, json, Encoding.UTF8);
        
    }

    public void SafeFile( int num)
    {

        if (num < 0)
        {
            return;
        }
        FileInfo info = fileInfo[num];
        info.isSafe = true ;
        info.totalVolume = (int)audioManager.GetComponent<AudioManager>().totalVolume;
        info.bgmVolume = (int)audioManager.GetComponent<AudioManager>().bgmVolume;
        info.effectVolume = (int)audioManager.GetComponent<AudioManager>().effectVolume;
        info.scene = SceneManager.GetActiveScene().name;
        info.health = 4;
        health = 4;
        WriteFileJson();
        GameObject.Find ("FilePoint").GetComponent <FilePoint >(). SafeComp();
    }

    public void ReadFile(int num )
    {
        if (num < 0)
        {
            return;
        }
        FileInfo info = fileInfo[num];
        if (info.isSafe && num >= 0)
        {
            audioManager.GetComponent<AudioManager>().totalVolume= info.totalVolume ;
            audioManager.GetComponent<AudioManager>().bgmVolume=info.bgmVolume ;
            audioManager.GetComponent<AudioManager>().effectVolume=info.effectVolume ;
            health = info.health;
            UIManager.Instance.DarkLoad(info.scene);
        }
    }

    public void ClearFile(int num )
    {
        if (num < 0)
        {
            return;
        }
        FileInfo info = fileInfo[num];
        if (info.isSafe&&num >=0)
        {
            info.isSafe = false;
            info.totalVolume = 30;
            info.bgmVolume = 30;
            info.effectVolume = 30;
            info.scene = "Init";
            info.health = 4;
            WriteFileJson();
            Debug.Log("恢复默认" + num);
        }
    }

}
