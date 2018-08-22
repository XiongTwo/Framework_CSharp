using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour {

    public UILabel mName;
    public UILabel mScore;
    public GameObject mLandlord;

    public UILabel mCount;
    public GameObject mPass;
    public List<Card> mCardList = new List<Card>();

    public List<CardData> mCardData = new List<CardData>();

    public GameObject mTime;
    public UILabel mTimeLabel;

    void Awake()
    {
        mName = transform.Find("Name").GetComponent<UILabel>();
        mScore = transform.Find("Score").GetComponent<UILabel>();
        mLandlord = transform.Find("DealerText").gameObject;

        mCount= transform.Find("CardCount/count").GetComponent<UILabel>();
        mPass = transform.Find("ACT/Pass").gameObject;
        mTime = transform.Find("Time").gameObject;
        mTimeLabel = mTime.GetComponentInChildren<UILabel>();
        mTime.AddComponent<PlayerAITime>();

        Transform card1 = transform.Find("ACT/Card1");
        Transform card2 = transform.Find("ACT/Card2");
        for (int i = 0; i < card1.childCount; i++)
            mCardList.Add(card1.GetChild(i).gameObject.AddComponent<Card>());
        for (int i = 0; i < card2.childCount; i++)
            mCardList.Add(card2.GetChild(i).gameObject.AddComponent<Card>());
    }
    public void Init()
    {
        mCardData.Clear();
        SetTime(false);
        mName.text = "[b]AI_" + Random.Range(1000, 99999);
        mScore.text = "[b]" + Random.Range(1000,99999);
        mLandlord.SetActive(false);

        mCount.text = "[b]0";
        mPass.SetActive(false);
        for (int i = 0; i < mCardList.Count; i++)
            mCardList[i].gameObject.SetActive(false);
    }
    public void SetCount(int count)
    {
        mCount.text = "[b]"+ count;
    }
    public void SetTime(bool b,int count=30)
    {
        mTime.SetActive(b);
        mTimeLabel.text = "[b]" + count;
    }
    public void Play()
    {
        SetTime(false);

        var temp= CardArithmetic.AutoPlay(mCardData);
        for (int i = 0; i < temp.Count; i++)
        {
            mCardData.Remove(temp[i]);
            mCardList[i].SetData(temp[i]);
        }
        SetCount(mCardData.Count);
        if (mCardData.Count == 0)
        {
            EventManage.Instance.Broadcast(EventEnum.combat, "over");
            return;
        }
        ControllerManage.Instance.mCombatController.mPlayCard.Play(temp);
    }
    public void HitPlay()
    {
        SetTime(false);

        var temp = CardArithmetic.AutoPlay(mCardData, ControllerManage.Instance.mCombatController.mPlayCard);
        if (temp == null)
        {
            mPass.SetActive(true);
            ControllerManage.Instance.mCombatController.mPlayCard.Play();
            return;
        }
        for (int i = 0; i < temp.Count; i++)
        {
            mCardData.Remove(temp[i]);
            mCardList[i].SetData(temp[i]);
        }
        SetCount(mCardData.Count);
        if (mCardData.Count == 0)
        {
            EventManage.Instance.Broadcast(EventEnum.combat, "over");
            return;
        }
        ControllerManage.Instance.mCombatController.mPlayCard.Play(temp);
    }
}
