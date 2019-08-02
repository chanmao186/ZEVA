using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;


    public float totalVolume = 30;
    public List < AudioSource > audioBGM;
    public float bgmVolume   =30;
    public List <AudioSource> audioEffect;
    public float effectVolume=30;


    #region Mono Function
    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            if (Instance !=this)
            {
                Destroy(transform.gameObject);
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 将需要进行管理的音乐初始化
    /// </summary>
    /// <param name="audio">音乐源文件</param>
    /// <param name="isAudioEft">是否为音效</param>
    public int Init(AudioSource audio,bool isAudioEft)
    {
        if (isAudioEft)
        {
            audioEffect.Add(audio);
            audio.volume = effectVolume/100 * totalVolume / 100;
            Debug.Log(audio + ":已加入音效池"+ audioEffect.Count);
            return audioEffect.Count-1;
            
        }else
        {
            audioBGM.Add(audio);
            audio.volume = bgmVolume/100 * totalVolume / 100;
            Debug.Log(audio + "：已加入背景音乐池"+audioBGM.Count);
            return audioBGM .Count-1;
        }
    }


    public void AdjustBgmVolume(float num )
    {
        foreach (AudioSource audio in audioBGM  )
        {
            audio.volume = num/100 * totalVolume/100;
            bgmVolume = num;
        }
       
    }
    public void AdjustEftVolume( float num)
    {
        foreach (AudioSource audio in audioEffect )
        {
            audio.volume = num/100 * totalVolume/100;
            effectVolume = num;
        }
    }
    public void PauseSound()
    {
        foreach (AudioSource audio in audioEffect)
        {
            audio.Pause ();
        }foreach (AudioSource audio in audioBGM)
        {
            audio.Pause ();
        }
    }
    public void ResumeSound()
    {
        foreach (AudioSource audio in audioEffect)
        {
            audio.Play();
        }
        foreach (AudioSource audio in audioBGM)
        {
            audio.Play();
        }
    }
    public void PlayEffect(int num )
    {
        if (!audioEffect[num].isPlaying)
        {
            audioEffect[num].Play();
        }
    }
    public void PlayBGM(int num)
    { 
        if (!audioBGM[num].isPlaying)
        {
            audioBGM[num].Play();
        }
    }
    public void StopEffect(int num)
    {
        if (audioEffect[num].isPlaying)
        {
            audioEffect[num].Stop();
        }
    }
    public void StopBGM(int num)
    {
        if (audioBGM[num].isPlaying)
        {
            audioBGM[num].Stop();
        }
    }

    
}
