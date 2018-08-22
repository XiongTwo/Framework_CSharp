using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManage : MonoBehaviour {

    private static TimeManage instance;
    public static TimeManage Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("TimeManage").AddComponent<TimeManage>();
            return instance;
        }
    }

    [SerializeField]
    private float totalTime;
    [SerializeField]
    private float totalFrame;

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        totalTime = 0;
        totalFrame = 0;
    }
    public float GetTotalTime()
    {
        return totalTime;
    }
    public float GetTotalFrame()
    {
        return totalFrame;
    }
    void Update()
    {
        totalTime += Time.deltaTime;
        totalFrame++;

        if (mTimerTaskList.Count > 0)
        {
            var temp = mTimerTaskList.ToArray();
            for (int i = 0; i < temp.Length; i++)
            {
                if (TimeManage.Instance.GetTotalTime() >= temp[i].mTime)
                {
                    temp[i].mAc();
                    mTimerTaskList.Remove(temp[i]);
                }
            }
        }
    }

    List<TimerTask> mTimerTaskList = new List<TimerTask>();

    public TimerTask AddTimerTask(Action ac, float interval)
    {
        var task = new TimerTask(ac, interval);
        mTimerTaskList.Add(task);
        return task;
    }
    public void RemoveTimerTask(TimerTask task)
    {
        if(mTimerTaskList.Contains(task))
            mTimerTaskList.Remove(task);
    }
}
public class TimerTask
{
    public Action mAc;
    public float mTime;
    public TimerTask(Action ac, float interval)
    {
        mAc = ac;
        mTime = interval + TimeManage.Instance.GetTotalTime();
    }
}
