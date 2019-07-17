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
    /// <returns></returns>
    private IEnumerator Schedule_(Func function, float time, int Repeat)
    {
        for (int i = 0; i < Repeat; i++)
        {
            yield return new WaitForSeconds(time);
            function();
        }
    }
    public void ScheduleOnce(Func function, float time)
    {
        Schedule(function, time, 1);
    }

    public void Schedule(Func function, float time, int Repeat)
    {
        StartCoroutine(Schedule_(function, time, Repeat));
    }
    // Update is called once per frame

    public void StopAllSchedule()
    {
        StopAllCoroutine();
    }

    private void StopAllCoroutine()
    {
        throw new NotImplementedException();
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
