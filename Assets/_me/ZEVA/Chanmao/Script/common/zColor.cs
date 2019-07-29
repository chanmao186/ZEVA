using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zColor : MonoBehaviour
{
    // Start is called before the first frame update


    /// <summary>
    /// 渐隐效果
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="node">node节点</param>
    /// <returns></returns>
    public Coroutine FadeIn(float time, SpriteRenderer node)
    {
        return FadeIn(time, node, null);
    }
    /// <summary>
    /// 渐隐效果
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="node">node节点</param>
    /// <param name="CallBack">回调函数</param>
    /// <returns></returns>
    public Coroutine FadeIn(float time, SpriteRenderer node, Func CallBack)
    {
        Color c = node.color;
        //每秒要减少的量
        time = time <= 0 ? 0.1f : time;
        float sub = node.color.a / time;
        return StartCoroutine(FadeIn1(sub, c, node, CallBack));
    }

    private IEnumerator FadeIn1(float sub, Color c, SpriteRenderer node, Func CallBack)
    {

        c.a -= sub * Time.fixedDeltaTime;
        node.color = c;

        while (node.color.a > 0)
        {
            yield return 0;
        }
        CallBack?.Invoke();
    }

    /// <summary>
    /// 渐显示效果
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="node">node节点</param>
    /// <returns></returns>
    public Coroutine FadeOut(float time, SpriteRenderer node)
    {
        return FadeOut(time, node, null);
    }
    /// <summary>
    /// 渐显示效果
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="node">node节点</param>
    /// <param name="CallBack">回调函数</param>
    /// <returns></returns>
    public Coroutine FadeOut(float time, SpriteRenderer node, Func CallBack)
    {
        Color c = node.color;
        //每秒要减少的量
        time = time <= 0 ? 0.1f : time;
        float add = (255 - node.color.a) / time;
        return StartCoroutine(FadeOut1(add, c, node, CallBack));
    }

    /// <summary>
    /// 将节点立即显示出来
    /// </summary>
    /// <param name="node">要显示的节点</param>
    public void ShowIn( SpriteRenderer node)
    {
        Color c = node.color;
        c.a = 255;
        node.color = c;
    }

    /// <summary>
    /// 将节点立即隐藏起来
    /// </summary>
    /// <param name="node">要隐藏的节点</param>
    public void HideIn(SpriteRenderer node)
    {
        Color c = node.color;
        c.a = 0;
        node.color = c;
    }

    private IEnumerator FadeOut1(float add, Color c, SpriteRenderer node, Func CallBack)
    {

        c.a += add * Time.fixedDeltaTime;
        node.color = c;
        while (node.color.a < 255)
        {
            yield return 0;
        }
        CallBack?.Invoke();
    }
}
