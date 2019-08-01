using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class DarkEffect : MonoBehaviour
{
    TweenCallback tweenCallback;
    public Image dark;
    public int AnimSpeed;
    public bool isDark = false;
    public string sceneName;

    private void Update()
    {
        //TODO
        this.GetComponent<Transform>().SetAsLastSibling();
    }

    public void DarkDisPlaySH(string name)
    {
        sceneName = name;
        isDark = true;
        Tween tween= dark.DOColor(Color.black, AnimSpeed);
        tween.OnComplete(DarkHideSD);
    }
    public void DarkHideSD()
    {
        SceneManager.LoadScene(sceneName);
        isDark = false;
        Tween tween = dark.DOColor(Color.clear, AnimSpeed * 2);

    }
    public void DarkDisPlay(TweenCallback tween1)
    {
        tweenCallback = tween1;
        isDark = true;
        Tween tween = dark.DOColor(Color.black, AnimSpeed);
        tween.OnComplete(tween1);
    }
    public void DarkHide()
    {
        isDark = false;

        Tween tween = dark.DOColor(Color.clear, AnimSpeed * 3);
        tween.OnComplete(tweenCallback);
    
    }
}
