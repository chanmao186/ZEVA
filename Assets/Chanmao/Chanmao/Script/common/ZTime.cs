using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//定义公共的委托函数
public delegate void Func();

public class ZTime : MonoBehaviour
{
    // Start is called before the first frame update

    /// <summary>
    /// 延时函数
    /// </summary>
    /// <param name="function">要调用的函数</param>
    /// <param name="time">延迟的时间</param>
    /// <param name="Repeat">重复的次数,如果输入0，则表示一直循环</param>
    /// <param name="Callback">执行完该计时器后的回调函数</param>
    /// <returns></returns>
    private IEnumerator Schedule_(Func function, float time, int Repeat,Func Callback)
    {
        
        for (int i = 0; Repeat == 0||i < Repeat; i++)
        {
            yield return new WaitForSeconds(time);
            function?.Invoke();
        }
        Callback?.Invoke();
    }

    /// <summary>
    /// 延时函数
    /// </summary>
    /// <param name="function">要调用的函数</param>
    /// <param name="time">延迟的时间</param>
    /// <returns>事情本身</returns>
    public Coroutine ScheduleOnce(Func function, float time)
    {
        return Schedule(function, time, 1,null);
    }

    /// <summary>
    /// 延时函数
    /// </summary>
    /// <param name="function">要调用的函数</param>
    /// <param name="time">延迟的时间</param>
    /// <param name="Repeat">重复的次数,如果输入0，则表示一直循环</param>
    /// <param name="Callback">执行完该计时器后的回调函数</param>
    /// <returns></returns>
    public Coroutine Schedule(Func function, float time, int Repeat,Func Callback)
    {
        return StartCoroutine(Schedule_(function, time, Repeat,Callback));
    }

    /// <summary>
    /// 延时函数
    /// </summary>
    /// <param name="function">要调用的函数</param>
    /// <param name="time">延迟的时间</param>
    /// <param name="Repeat">重复的次数,如果输入0，则表示一直循环</param>
    /// <returns></returns>
    public Coroutine Schedule(Func function, float time, int Repeat)
    {
        return Schedule(function, time, Repeat, null);
    }
    // Update is called once per frame

    /// <summary>
    /// 停止计时器
    /// </summary>
    /// <param name="schedule">停止指定的计时器</param>
    public void StopSchedule(Coroutine schedule)
    {
        if (schedule != null)
            StopCoroutine(schedule);
    }
    /// <summary>
    /// 时间更新
    /// </summary>
    /// <param name="time">要进行更新的时间</param>
    /// <param name="CallBack">完成后的回调函数</param>
    public void TimeUpdate(ref float time, Func CallBack)
    {
        if (time > 0)
        {
            time -= Time.fixedDeltaTime;
            if (time <= 0)
            {
                time = 0;
                CallBack();
            }
        }
    }

    /// <summary>
    /// 时间更新
    /// </summary>
    /// <param name="time">要进行更新的时间</param>
    public void TimeUpdate(ref float time)
    {
        TimeUpdate(ref time, () => { });
    }

}
