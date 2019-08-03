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
        float sub = node.color.a / time*Time.fixedDeltaTime;
        return StartCoroutine(FadeIn1(sub, c, node, CallBack));
    }

    /// <summary>
    /// 渐隐效果
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="node">node节点</param>
    /// <param name="CallBack">回调函数</param>
    /// <returns></returns>
    public Coroutine FadeIn(float time, GameObject node, Func CallBack)
    {
        if (node.GetComponent<SpriteRenderer>())
        {
            return FadeIn(time, node.GetComponent<SpriteRenderer>(), CallBack);
        }
        else
        {
            Debug.Log("该节点没有SpriteRender属性");
        }
        return null;
    }

    private IEnumerator FadeIn1(float sub, Color c, SpriteRenderer node, Func CallBack)
    {              
        while (node.color.a > 0)
        {
            //Debug.Log(c);
            c.a -= sub ;
            node.color = c;
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
        float add = (1 - node.color.a) / time *Time.fixedDeltaTime;
        return StartCoroutine(FadeOut1(add, c, node, CallBack));
    }

    /// <summary>
    /// 渐显示效果
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="node">node节点</param>
    /// <param name="CallBack">回调函数</param>
    /// <returns></returns>
    public Coroutine FadeOut(float time, GameObject node, Func CallBack)
    {
        if (node.GetComponent<SpriteRenderer>())
        {
            return FadeOut(time, node.GetComponent<SpriteRenderer>(), CallBack);
        }
        else
        {
            Debug.Log("该节点没有SpriteRender属性");
        }
        return null;
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
        while (node.color.a < 1)
        {
            Debug.Log(c);
            c.a += add ;
            node.color = c;
            yield return 0;
        }
        CallBack?.Invoke();
    }

    /// <summary>
    /// 颜色淡出
    /// </summary>
    /// <param name="node"></param>
    /// <param name="time"></param>
    /// <param name="Target"></param>
    /// <param name="CallBack"></param>
    /// <returns></returns>
    public Coroutine ColorOut(SpriteRenderer node, float time, Color Target, Func CallBack)
    {
        Color value = (Target-node.color  )/time * Time.deltaTime;
        return StartCoroutine(ColorOut1(node,value,Target,CallBack));
    }
    private IEnumerator ColorOut1(SpriteRenderer node,Color Value,Color Target,Func CallBack)
    {
        while (node.color.r<Target.r)
        {
            //Debug.Log(node.name+Value);

            node.color += Value;
            yield return 0;
        }
        CallBack?.Invoke();
    }
}
