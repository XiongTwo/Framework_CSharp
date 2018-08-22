using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatView : MonoBehaviour {

    public GameObject mDetail;
    public GameObject mMenu;

    public Card[] mCard3 = new Card[3];
    public CardData[] mCard3Data = new CardData[3];

    public Player mPlayer;
    public PlayerAI mPlayerAI1;
    public PlayerAI mPlayerAI2;

    public GameObject mStartGame;

    void Awake()
    {
        mDetail = transform.Find("Detail").gameObject;
        Common.AddClickListener(mDetail, ()=>IsMenu(true));
        mMenu = transform.Find("Menu").gameObject;
        Common.AddClickListener(mMenu.transform.Find("Setting").gameObject, Setting);
        Common.AddClickListener(mMenu.transform.Find("Leave").gameObject, Leave);
        NGUITools.AddWidgetCollider(mMenu.transform.Find("Mask (1)").gameObject);
        Common.AddClickListener(mMenu.transform.Find("Mask (1)").gameObject, () => IsMenu(false));

        

        mCard3[0]= transform.Find("Card3").GetChild(0).gameObject.AddComponent<Card>();
        mCard3[1] = transform.Find("Card3").GetChild(1).gameObject.AddComponent<Card>();
        mCard3[2] = transform.Find("Card3").GetChild(2).gameObject.AddComponent<Card>();

        mPlayer = transform.Find("Players/Player").gameObject.AddComponent<Player>();
        mPlayerAI1 = transform.Find("Players/Player2").gameObject.AddComponent<PlayerAI>();
        mPlayerAI2 = transform.Find("Players/Player1").gameObject.AddComponent<PlayerAI>();

        mStartGame = transform.Find("Buttons/StartGame").gameObject;

        Common.AddClickListener(mStartGame, StartGame);
    }
    public void Init()
    {
        IsMenu(false);
        
        mPlayer.Init();
        mPlayerAI1.Init();
        mPlayerAI2.Init();

        mStartGame.SetActive(true);
        gameObject.SetActive(true);
        for (int i = 0; i < mCard3.Length; i++)
            mCard3[i].gameObject.SetActive(false);
    }
    private void IsMenu(bool b)
    {
        mMenu.SetActive(b);
        if (b)
            mDetail.GetComponent<UISprite>().spriteName = "Arrow";
        else
            mDetail.GetComponent<UISprite>().spriteName = "ArrowFrame";
    }
    private void Setting()
    {
        IsMenu(false);
        EventManage.Instance.Broadcast(EventEnum.enter_set);
    }
    private void Leave()
    {
        EventManage.Instance.Broadcast(EventEnum.combat,"exit");
    }

    private void StartGame()
    {
        EventManage.Instance.Broadcast(EventEnum.combat, "start");
    }
    public void SetCard3()
    {
        for (int i = 0; i < mCard3.Length; i++)
            mCard3[i].SetData(mCard3Data[i]);
    }
}
