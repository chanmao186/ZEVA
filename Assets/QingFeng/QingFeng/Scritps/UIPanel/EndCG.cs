using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndCG : BasePanel
{
    public AudioSource audio;
    public int audioNum;

    private CanvasGroup canvasGroup;
    public Image dark;
    public List<Sprite> gameCG;
    public Text text;
    public string[] texts;
    public Image image1;
    public Image image2;
    public Image bg;
    public int count = 0;
    public int index = -1;
    public bool isTextComp = false  ;      //字是否播放完成
    public int AnimSpeed;
    public Text info;
    public Text thanks;
    public Text end;


    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Start()
    {
        PanelDisplay();
    }

    public void EndProducers ()
    {
        info.color = Color.clear;
        thanks.DOColor(Color.white, AnimSpeed*2);
        end.DOColor(Color.white ,AnimSpeed*2);
    }

    public void PicOver()
    {
        image1.color = Color.clear;
        text.color = Color.clear;
        index = -1;
        count = 0;
        Tween tween = info.DOColor(Color.white , AnimSpeed*3);
        tween.OnComplete(EndProducers );
    }
    public void Skip()
    {
        index = -1;
        count = 0;
        Tween tween = dark.DOColor(Color.black, AnimSpeed);
        tween.OnComplete(PicOver);
    }
    /// <summary>
    /// 点击字体
    /// </summary>
    public void TextChange()
    {
        count++;
        isTextComp = true;
    }
    /// <summary>
    /// 点击按钮，绘本向前翻页
    /// </summary>
    public void pictureForward()
    {
        if (index <= 10&&isTextComp)
        {
            isTextComp = false;
            index++;
            image1.color = Color.clear;
            image1.enabled = true;
            Tween tween = image1.DOColor(Color.white, AnimSpeed);
            tween.OnComplete(PictureComp);
            image1.sprite = gameCG[index];
        }
        else if (index == 11 )
        {
            Skip();
        }
    }
    public void PictureComp()
    {
        isTextComp = true;
    }
    /// <summary>
    /// 文字动画
    /// </summary>
    public void TextAnim()
    {
        if (isTextComp && count <= 3) 
        {
            text.color = Color.clear;
            if (count == 3)
            {
                bg.color = Color.white;
                return;
            }
            index++;
            isTextComp = false;
            text.text = texts[count];
            Tween tween = text.DOColor(Color.white, 1.5f);
            tween.OnComplete(TextChange);

        }
    }
    /// <summary>
    /// 当前面板显示
    /// </summary>
    private void PanelDisplay()
    {
        bg.color = Color.black;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        image1.color = Color.white;
        text.color = Color.white;
        audioNum = AudioManager.Instance.Init(audio, false);
        AudioManager.Instance.PlayBGM(audioNum);
        texts = new string[] { "恭喜你，抵达了终点。那么请说出你来到kappmu的愿望是......", "没问题，你的愿望可以达成，但是这样请将‘他’的愿望留下，这样你愿意吗？", "好的"};

        TextAnim();
        
    }
}
