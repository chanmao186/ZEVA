using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PictureBookPanel : BasePanel
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
    public int count = 0;
    public int index = 0;
    public bool isTextComp=false ;      //字是否播放完成
    public int AnimSpeed;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        audioNum = AudioManager.Instance.Init(audio, false);
        AudioManager.Instance.PlayBGM(audioNum );
        texts = new string[] { "那座山是Zoe ", "人们称呼她脚下的森林为欲望森林", "森林的名字源于它深处的Kappmu墓穴", "Kappmu里有着人们所渴望的一切\n许多的人追逐着自己的念想进入森林，进入Kappmu ", "但是人们在索求愿望的过程中", "往往付出的都是自己", "数不尽的有来无回为欲望森林的Kappmu盖上了神秘的面纱\n也让后人心生畏惧", "这个村落是Zandra", "这天，一对平凡的夫妇的家中，有位新的生命到来\n不平凡的是，他们的孩子天生失明,两人四处奔波寻医，结果都只有叹息", "那天，看着带着泪渍睡着的女子，男子不告而别", "几日后，一位老人来到颓废的女子面前\n说是受人所托，治好了孩子的眼睛", "在母亲一人的呵护下，孩子长大了\n他发现自己的眼睛与众不同，时不时能看见一些与自己相关的过去与未来", "十六年，他在每晚的梦中都依稀地放映着一位女子在织着围巾\n男子轻轻地摇晃着婴孩，两人讨论着婴孩的姓名的画面", "每回临近梦的结尾，都是这位男子将一张纸条放在熟睡着的女子的枕边", "挎上一把短剑，然后消散在欲望森林的黑影里", "最后伴随着婴孩的哭闹和女子的啜泣，已经不再是小孩的少年总能看见\n那被泪水湿润的纸片上写着自己的名字--ZEVA" };
        TextAnim();
    }
    public void PicOver()
    {
        image1.color = Color.clear;
        text.color = Color.clear;
        OnClosePanel();
        AudioManager.Instance.StopBGM(audioNum);
    }
    public void Skip()
    {
        index = 0;
        count = 0;
        Tween tween = dark.DOColor(Color.black, AnimSpeed);
        tween.OnComplete(PicOver);
    }
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
        if (index <= 13&&isTextComp)
        {
            if (count != 6&&count !=11)
            {
                index++;
            }
            image2.enabled = false;
            if (index == 1)
            {
                image2.enabled = true;
                Tween tween = image2.DOColor(Color.white, AnimSpeed);
                image2.sprite = gameCG[index];
            }
            else if (index == 5 )
            {
                image2.enabled = true;
                Tween tween = image2.DOColor(Color.white, AnimSpeed);
                image2.sprite = gameCG[index];
            }
            else if (index != 1 && index != 5)
            {
                image1.color = Color.clear;
                image2.color = Color.clear;
                image1.enabled = true;
                Tween tween = image1.DOColor(Color.white, AnimSpeed);
                image1.sprite = gameCG[index];
            }
        }
        else if (index == 14 && isTextComp)
        {
            Skip();
        }
    }

    /// <summary>
    /// 文字动画
    /// </summary>
    public void TextAnim()
    {
        if(isTextComp)
        {
            isTextComp = false;
            text.color = Color.clear;
            text.text = texts[count];
            Tween tween = text.DOColor(Color.white , 1.5f);
            tween.OnComplete(TextChange);
        }
    }
    /// <summary>
    /// 当前面板关闭
    /// </summary>
    public void OnClosePanel()
    {
        UIManager.Instance.PanelPop();
        SceneManager.LoadScene("Card_1_Map_1");
        OnPushPanel("Game");
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

}
