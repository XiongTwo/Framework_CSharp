using System.Collections.Generic;
using UnityEngine;

public class PlayCard
{
    public GameObject player;

    public GameObject cardDataListToPlayer;
    public List<CardData> cardDataList;

    private CombatController mCombatController;
    private CombatView mCombatView;

    GameObject[] playersOrder = new GameObject[3];

    public PlayCard()
    {
        mCombatController = ControllerManage.Instance.mCombatController;
        mCombatView = mCombatController.mCombatView;

        if (mCombatView.mPlayer.mLandlord.activeSelf)
            player = mCombatView.mPlayer.gameObject;
        else if (mCombatView.mPlayerAI1.mLandlord.activeSelf)
            player = mCombatView.mPlayerAI1.gameObject;
        else if (mCombatView.mPlayerAI2.mLandlord.activeSelf)
            player = mCombatView.mPlayerAI2.gameObject;

        playersOrder[0] = mCombatView.mPlayer.gameObject;
        playersOrder[1] = mCombatView.mPlayerAI1.gameObject;
        playersOrder[2] = mCombatView.mPlayerAI2.gameObject;
    }
    public void Play(List<CardData> _cardDataList)
    {
        cardDataListToPlayer = player;
        cardDataList = _cardDataList;
        PlayerChang();
    }
    public void Play()
    {
        PlayerChang();
    }
    private void PlayerChang()
    {
        for (int i = 0; i < playersOrder.Length; i++)
        {
            if (playersOrder[i] == player)
            {
                if ((i + 1) < playersOrder.Length)
                {
                    player = playersOrder[i + 1];
                    break;
                }
                else
                {
                    player = playersOrder[0];
                    break;
                }
            }
        }
        EventManage.Instance.Broadcast(EventEnum.combat, "playCard");
    }
}
