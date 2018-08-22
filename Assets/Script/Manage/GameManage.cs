using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage {

    private static GameManage instance;
    public static GameManage Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManage();
            return instance;
        }
    }

    private bool isInitManages;

    private void InitManages()
    {
        ControllerManage.Instance.Init();
        TimeManage.Instance.Init();
        AudioManage.Instance.Init();

        Input.multiTouchEnabled = false;
        isInitManages = true;
    }

    public void StartGame()
    {
        if (!isInitManages)
            InitManages();
        EventManage.Instance.Broadcast(EventEnum.start_game);
    }
}
