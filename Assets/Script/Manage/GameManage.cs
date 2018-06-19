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

        isInitManages = true;
    }

    public void StartGame()
    {
        if (!isInitManages)
            InitManages();
        Debug.Log("Event.example");
        EventManage.Instance.Broadcast(EventEnum.example);
    }
}
