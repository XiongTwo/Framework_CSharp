using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController
{
    public CombatMode mCombatMode;
    public CombatView mCombatView;

    private List<TimerTask> mTimerTask = new List<TimerTask>();

    public PlayCard mPlayCard;

    public CombatController()
    {
        mCombatMode = new CombatMode();
        AddListener();
    }
    private void AddListener()
    {
        EventManage.Instance.AddListener(EventEnum.enter_combat, Enter);
        EventManage.Instance.AddListener<int>(EventEnum.chang_score, ChangScore);
        EventManage.Instance.AddListener<string>(EventEnum.combat, Processor);
    }
    private void ChangScore(int score)
    {
        if (mCombatView == null)
            return;
        mCombatView.mPlayer.mScore.text = "[b]" + score;
    }
    private void Enter()
    {
        for (int i = 0; i < mTimerTask.Count; i++)
            TimeManage.Instance.RemoveTimerTask(mTimerTask[i]);

        mPlayCard = null;
        mTimerTask.Clear();
        mCombatMode.Init();
        if (mCombatView == null)
            mCombatView = ResourceManage.Instance.LoadPrefab("CombatView").AddComponent<CombatView>();
        mCombatView.Init();
    }
    private void Processor(string type)
    {
        switch (type)
        {
            case "start":
                Processor2Start();
                Processor2StartAnimation();
                break;
            case "exit":
                Processor2Exit();
                break;
            case "landlord":
                Processor2Landlord();
                break;
            case "unlandlord":
                Processor2Unlandlord();
                break;
            case "playCard":
                Processor2PlayCard();
                break;
            case "over":
                Processor2Over();
                break;
        }
    }
    private void Processor2Start()
    {
        mCombatView.mStartGame.SetActive(false);
        for (int i = 0; i < mCombatView.mCard3Data.Length; i++)
        {
            mCombatView.mCard3Data[i]=GetSingleCard();
        }
        for (int i = 0; i < 17; i++)
        {
            mCombatView.mPlayer.mCardData.Add(GetSingleCard());
            mCombatView.mPlayerAI1.mCardData.Add(GetSingleCard());
            mCombatView.mPlayerAI2.mCardData.Add(GetSingleCard());
        }
        mCombatView.mPlayer.mCardData.Sort();
        mCombatView.mPlayerAI1.mCardData.Sort();
        mCombatView.mPlayerAI2.mCardData.Sort();
    }
    private void Processor2StartAnimation()
    {
        for (int i = 0; i < mCombatView.mCard3.Length; i++)
            mCombatView.mCard3[i].SetData(new CardData(0));

        for (int i = 0; i < mCombatView.mPlayer.mCardData.Count; i++)
        {
            int index = i;
            var task = TimeManage.Instance.AddTimerTask(() =>
                {
                    mCombatView.mPlayer.mCardList[index].SetData(mCombatView.mPlayer.mCardData[index]);
                    if (index == 0 || index == 1)
                        mCombatView.mPlayer.mCardList[index].gameObject.transform.localPosition = new Vector3(0, 0, 0);
                    else
                        mCombatView.mPlayer.mCardList[index].gameObject.transform.localPosition = new Vector3((mCombatView.mPlayer.mCard.cellWidth / 2) * (index - 1), 0, 0);

                    if (index == 1)
                    {
                        var spp = mCombatView.mPlayer.mCardList[0].gameObject.AddComponent<SpringPosition>();
                        spp.ignoreTimeScale = true;
                        spp.updateScrollView = true;
                        spp.strength = 15f;
                        spp.target = new Vector3((mCombatView.mPlayer.mCard.cellWidth / 2) * -1, 0, 0);
                    }
                    mCombatView.mPlayer.mCard.enabled = true;

                    mCombatView.mPlayerAI1.SetCount(index + 1);
                    mCombatView.mPlayerAI2.SetCount(index + 1);
                }, index * 0.4f);
            mTimerTask.Add(task);
        }
        mTimerTask.Add(TimeManage.Instance.AddTimerTask(() =>
       {
           mCombatView.mPlayer.mActLandlord.SetActive(true);
           for (int i = 0; i < mCombatView.mPlayer.mCardList.Count; i++)
           {
               var spp = mCombatView.mPlayer.mCardList[i].GetComponent<SpringPosition>();
               if (spp != null)
                   GameObject.Destroy(spp);
           }
       }, mCombatView.mPlayer.mCardData.Count * 0.4f));
    }
    private void Processor2Exit()
    {
        EventManage.Instance.Broadcast(EventEnum.enter_lobby);
        mCombatView.gameObject.SetActive(false);
        for (int i = 0; i < mTimerTask.Count; i++)
            TimeManage.Instance.RemoveTimerTask(mTimerTask[i]);
    }
    private void Processor2Landlord()
    {
        mCombatView.mPlayer.mActLandlord.SetActive(false);
        mCombatView.mPlayer.mLandlord.SetActive(true);
        mCombatView.SetCard3();
        for (int i = 0; i < mCombatView.mCard3Data.Length; i++)
            mCombatView.mPlayer.mCardData.Add(mCombatView.mCard3Data[i]);
        mCombatView.mPlayer.mCardData.Sort();
        for (int i = 0; i < mCombatView.mPlayer.mCardData.Count; i++)
            mCombatView.mPlayer.mCardList[i].SetData(mCombatView.mPlayer.mCardData[i]);
        mCombatView.mPlayer.mCard.animateSmoothly = false;
        mCombatView.mPlayer.mCard.Reposition();
        mCombatView.mPlayer.CardAddAction();
        EventManage.Instance.Broadcast(EventEnum.combat, "playCard");
    }
    private void Processor2Unlandlord()
    {
        mCombatView.mPlayer.mActLandlord.SetActive(false);
        if (Random.Range(0, 2) == 0)
        {
            mCombatView.mPlayerAI1.mLandlord.SetActive(true);
            for (int i = 0; i < mCombatView.mCard3Data.Length; i++)
                mCombatView.mPlayerAI1.mCardData.Add(mCombatView.mCard3Data[i]);
            mCombatView.mPlayerAI1.mCardData.Sort();
            mCombatView.mPlayerAI1.SetCount(mCombatView.mPlayerAI1.mCardData.Count);
        }
        else
        {
            mCombatView.mPlayerAI2.mLandlord.SetActive(true);
            for (int i = 0; i < mCombatView.mCard3Data.Length; i++)
                mCombatView.mPlayerAI2.mCardData.Add(mCombatView.mCard3Data[i]);
            mCombatView.mPlayerAI2.mCardData.Sort();
            mCombatView.mPlayerAI2.SetCount(mCombatView.mPlayerAI2.mCardData.Count);
        }
        mCombatView.SetCard3();
        mCombatView.mPlayer.mCard.animateSmoothly = false;
        mCombatView.mPlayer.CardAddAction();
        EventManage.Instance.Broadcast(EventEnum.combat, "playCard");
    }
    private void Processor2PlayCard()
    {
        if (mPlayCard == null)
            mPlayCard = new PlayCard();
        Player playPlayer= mPlayCard.player.GetComponent<Player>(); ;
        PlayerAI playPlayerAI = mPlayCard.player.GetComponent<PlayerAI>();
        
        //出牌
        if (mPlayCard.cardDataListToPlayer == null|| mPlayCard.player== mPlayCard.cardDataListToPlayer)
        {
            if (playPlayer != null)
            {
                Debug.LogError("出牌:"+playPlayer.gameObject);
                playPlayer.PlayCardInit();
                playPlayer.mPlay.SetActive(true);
            }
            else if (playPlayerAI != null)
            {
                Debug.LogError("出牌:" + playPlayerAI.gameObject);
                playPlayerAI.mPass.SetActive(false);
                for (int i = 0; i < playPlayerAI.mCardList.Count; i++)
                    playPlayerAI.mCardList[i].gameObject.SetActive(false);
                playPlayerAI.SetTime(true);
                mTimerTask.Add(TimeManage.Instance.AddTimerTask(playPlayerAI.Play,Random.Range(1.4f,3.4f))); 
            }
        }
        else//打牌
        {
            if (playPlayer != null)
            {
                Debug.LogError("打牌:" + playPlayer.gameObject);
                playPlayer.PlayCardInit();
                if (CardArithmetic.AutoPlay(mCombatView.mPlayer.mCardData, mPlayCard) == null)
                    playPlayer.mPass.SetActive(true);
                else
                    playPlayer.mAct.SetActive(true);
            }
            else if (playPlayerAI != null)
            {
                Debug.LogError("打牌:" + playPlayerAI.gameObject);
                playPlayerAI.mPass.SetActive(false);
                for (int i = 0; i < playPlayerAI.mCardList.Count; i++)
                    playPlayerAI.mCardList[i].gameObject.SetActive(false);
                playPlayerAI.SetTime(true);
                mTimerTask.Add(TimeManage.Instance.AddTimerTask(playPlayerAI.HitPlay, Random.Range(1.4f, 3.4f)));
            }
        }
    }
    private void Processor2Over()
    {
        Debug.LogError("Over");
        string str = "";
        if (mCombatView.mPlayer.mCardData.Count == 0)
        {
            str = mCombatView.mPlayer.mName.text + DesEnum.胜利 + "+1" + DesEnum.钻石+"\n";
            str += mCombatView.mPlayerAI1.mName.text + DesEnum.失败 + "-1" + DesEnum.钻石 + "\n";
            str += mCombatView.mPlayerAI2.mName.text + DesEnum.失败 + "-1" + DesEnum.钻石;
            ControllerManage.Instance.mLoginControll.mLoginMode.SetLoginUserScore(ControllerManage.Instance.mLoginControll.mLoginMode.loginUserData.score + 1);
            mCombatView.mPlayerAI1.mScore.text = "[b]" + (int.Parse(mCombatView.mPlayerAI1.mScore.text.Replace("[b]", "")) - 1);
            mCombatView.mPlayerAI2.mScore.text = "[b]" + (int.Parse(mCombatView.mPlayerAI2.mScore.text.Replace("[b]", "")) - 1);
        }
        if (mCombatView.mPlayerAI1.mCardData.Count == 0)
        {
            str = mCombatView.mPlayerAI1.mName.text + DesEnum.胜利 + "+1" + DesEnum.钻石 + "\n";
            str += mCombatView.mPlayer.mName.text + DesEnum.失败 + "-1" + DesEnum.钻石 + "\n";
            str += mCombatView.mPlayerAI2.mName.text + DesEnum.失败 + "-1" + DesEnum.钻石;
            mCombatView.mPlayerAI1.mScore.text="[b]"+(int.Parse(mCombatView.mPlayerAI1.mScore.text.Replace("[b]", ""))+1);
            ControllerManage.Instance.mLoginControll.mLoginMode.SetLoginUserScore(ControllerManage.Instance.mLoginControll.mLoginMode.loginUserData.score - 1);
            mCombatView.mPlayerAI2.mScore.text = "[b]" + (int.Parse(mCombatView.mPlayerAI2.mScore.text.Replace("[b]", "")) - 1);
        }
        if (mCombatView.mPlayerAI2.mCardData.Count == 0)
        {
            str = mCombatView.mPlayerAI2.mName.text + DesEnum.胜利 + "+1" + DesEnum.钻石 + "\n";
            str += mCombatView.mPlayer.mName.text + DesEnum.失败 + "-1" + DesEnum.钻石 + "\n";
            str += mCombatView.mPlayerAI1.mName.text + DesEnum.失败 + "-1" + DesEnum.钻石;
            mCombatView.mPlayerAI2.mScore.text = "[b]" + (int.Parse(mCombatView.mPlayerAI2.mScore.text.Replace("[b]", "")) + 1);
            ControllerManage.Instance.mLoginControll.mLoginMode.SetLoginUserScore(ControllerManage.Instance.mLoginControll.mLoginMode.loginUserData.score - 1);
            mCombatView.mPlayerAI1.mScore.text = "[b]" + (int.Parse(mCombatView.mPlayerAI1.mScore.text.Replace("[b]", "")) - 1);
        }
        str ="[b]" + str.Replace("[b]", "");
        Common.CreateMessageBox(str, DesEnum.返回大厅.ToString(), () => Enter(), 
            () => EventManage.Instance.Broadcast(EventEnum.combat,"exit")).confirmButtonLabel.text="[b]"+ DesEnum.再来一局.ToString();
    }
    private CardData GetSingleCard()
    {
        CardData single = null;
        if (mCombatMode.allCardData.Count>0)
        {
            int index = Random.Range(0, mCombatMode.allCardData.Count);
            single = mCombatMode.allCardData[index];
            mCombatMode.allCardData.Remove(single);
        }
        return single;
    }
}
