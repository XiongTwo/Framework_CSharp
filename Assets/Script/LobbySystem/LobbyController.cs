using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyControll
{
    public LobbyMode mLobbyMode;
    public LobbyView mLobbyView;
    public LobbyBillboard mLobbyBillboard;

    public LobbyControll()
    {
        mLobbyMode = new LobbyMode();
        AddListener();
    }
    private void AddListener()
    {
        EventManage.Instance.AddListener(EventEnum.enter_lobby, Enter);
        EventManage.Instance.AddListener<int>(EventEnum.chang_score, ChangScore);
        EventManage.Instance.AddListener<string>(EventEnum.lobby, Processor);
    }
    private void ChangScore(int score)
    {
        if (mLobbyView == null)
            return;
        mLobbyView.score.text = score.ToString();
    }
    private void Enter()
    {
        if (mLobbyView == null)
        {
            mLobbyView = ResourceManage.Instance.LoadPrefab("LobbyView").AddComponent<LobbyView>();
            ResourceManage.Instance.LoadPrefab("LobbyBillboard", mLobbyView.gameObject);
        }
        else
        {
            mLobbyView.gameObject.SetActive(true);
        }
    }

    private void Processor(string type)
    {
        switch (type)
        {
            case "set":
                Processor2Set();
                break;
            case "combat":
                Processor2Combat();
                break;
        }
    }
    private void Processor2Set()
    {
        EventManage.Instance.Broadcast(EventEnum.enter_set);
    }
    private void Processor2Combat()
    {
        mLobbyView.gameObject.SetActive(false);
        EventManage.Instance.Broadcast(EventEnum.enter_combat);
    }
}
