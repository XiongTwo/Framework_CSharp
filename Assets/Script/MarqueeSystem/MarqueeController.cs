using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarqueeController {

    public MarqueeMode mMarqueeMode;
    public MarqueeView mMarqueeView;

    public MarqueeController()
    {
        mMarqueeMode = new MarqueeMode();
        AddListener();
    }
    private void AddListener()
    {
        EventManage.Instance.AddListener<string>(EventEnum.play_marquee, Marquee);
        EventManage.Instance.AddListener(EventEnum.start_game, StartGame);
    }
    private void StartGame()
    {
        TimeManage.Instance.AddTimerTask(Announcement,0);
    }
    private void Announcement()
    {
        string str = DesEnum.单机斗地主.ToString() + "，" + DesEnum.娱乐作品.ToString() + "，" + DesEnum.作者QQ419318386.ToString();
        EventManage.Instance.Broadcast(EventEnum.play_marquee, str);
        TimeManage.Instance.AddTimerTask(Announcement, Random.Range(0,60));
    }
    private void Marquee(string str)
    {
        if(mMarqueeView==null)
            mMarqueeView = ResourceManage.Instance.LoadPrefab("MarqueeView").transform.Find("hengfu/MarqueeView").gameObject.AddComponent<MarqueeView>();
        mMarqueeView.AddSystemMessage(str);
    }
    private void Processor(string type)
    {
        switch (type)
        {
            case "show":

                break;
        }
    }
}
