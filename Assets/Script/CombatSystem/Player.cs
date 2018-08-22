using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public UIGrid mActCard;
    public UIGrid mCard;

    public List<Card> mActCardList=new List<Card>();
    public List<Card> mCardList=new List<Card>();

    public List<CardData> mCardData = new List<CardData>();

    public GameObject mActLandlord;
    public GameObject mAct;
    public GameObject mPass;
    public GameObject mPassLabel;
    public GameObject mPlay;

    public UILabel mName;
    public UILabel mScore;
    public GameObject mLandlord;

    public float mBeginDot;
    public bool isSelectCard;
    public Card mFirstSelectCard;

    public GameObject mTime;
    public UILabel mTimeLabel;

    void Awake()
    {
        mActCard= transform.parent.parent.Find("ActCard").GetComponent<UIGrid>();
        mCard = transform.parent.parent.Find("Card").GetComponent<UIGrid>();
        mActLandlord = transform.parent.parent.Find("Buttons/Landlord").gameObject;
        mAct = transform.parent.parent.Find("Buttons/Act/Act").gameObject;
        mPass = transform.parent.parent.Find("Buttons/Act/Pass/Pass").gameObject;
        mPlay = transform.parent.parent.Find("Buttons/Act/play").gameObject;
        mPassLabel = transform.parent.parent.Find("Buttons/Act/Pass/Label").gameObject;

        mName = transform.Find("Name").GetComponent<UILabel>();
        mScore = transform.Find("Score").GetComponent<UILabel>();
        mLandlord = transform.Find("DealerText").gameObject;

        mTime = transform.Find("Time").gameObject;
        mTimeLabel = mTime.GetComponentInChildren<UILabel>();

        Common.AddClickListener(mActLandlord.transform.Find("yes").gameObject,()=> EventManage.Instance.Broadcast(EventEnum.combat, "landlord"));
        Common.AddClickListener(mActLandlord.transform.Find("no").gameObject, () => EventManage.Instance.Broadcast(EventEnum.combat, "unlandlord"));
        Common.AddClickListener(mPlay, Play);
        Common.AddClickListener(mPass, PassPlay);
        Common.AddClickListener(mAct.transform.Find("pass").gameObject, PassPlay);
        Common.AddClickListener(mAct.transform.Find("tips").gameObject, TipsPlay);
        Common.AddClickListener(mAct.transform.Find("play").gameObject, HitPlay);
    }
    public void SetTime(bool b, int count = 0)
    {
        mTime.SetActive(b);
        mTimeLabel.text = "[b]" + count;
    }
    public void Init()
    {
        mCardData.Clear();
        mCard.animateSmoothly = true;
        isSelectCard = false;
        SetTime(false);
        for (int i = 0; i < mCardList.Count; i++)
        {
            mCardList[i].card.color = Color.white;
            mCardList[i].transform.localPosition = Vector3.zero;
            mCardList[i].gameObject.SetActive(false);
            var collider = mCardList[i].GetComponent<BoxCollider>();
            if (collider != null)
                Destroy(collider);
            var spp = mCardList[i].GetComponent<SpringPosition>();
            if (spp != null)
                Destroy(spp);
        }

        var mActCardChildList = mActCard.GetChildList();
        for (int i = 0; i < mActCardChildList.Count; i++)
        {
            mActCardChildList[i].transform.localPosition = Vector3.zero;
            mActCardList.Add(mActCardChildList[i].gameObject.AddComponent<Card>());
        }
        
        var mCardChildList = mCard.GetChildList();
        for (int i = 0; i < mCardChildList.Count; i++)
        {
            var temp = mCardChildList[i].gameObject.AddComponent<Card>();
            temp.index = i;
            mCardList.Add(temp);
            mCardChildList[i].gameObject.transform.localScale = Vector3.one;
        }

        mActLandlord.SetActive(false);
        mAct.SetActive(false);
        mPass.SetActive(false);
        mPlay.SetActive(false);
        mPassLabel.SetActive(false);

        mName.text = "[b]"+ControllerManage.Instance.mLoginControll.mLoginMode.loginUserData.nickname;
        mScore.text = "[b]" + ControllerManage.Instance.mLoginControll.mLoginMode.loginUserData.score;
        mLandlord.SetActive(false);
    }
    public void CardAddAction()
    {
        for (int i = 0; i < mCardList.Count; i++)
        {
            NGUITools.AddWidgetCollider(mCardList[i].gameObject);
            UIEventListener.Get(mCardList[i].gameObject).onPress = (go, b) =>
            {
                isSelectCard = b;
                if (b)
                    mFirstSelectCard = go.GetComponent<Card>();
                else
                {
                    for (int j = 0; j < mCardList.Count; j++)
                    {
                        if (mCardList[j].gameObject.activeSelf && mCardList[j].card.color == Color.gray)
                        {
                            mCardList[j].card.color = Color.white;
                            int y = mCardList[j].transform.localPosition.y > 20 ? 0 : 24;
                            mCardList[j].transform.localPosition = new Vector3(mCardList[j].transform.localPosition.x, y,0);
                        }
                    }
                }
            };
        }
    }
    public void PlayCardInit()
    {
        for (int i = 0; i < mActCardList.Count; i++)
        {
            mActCardList[i].gameObject.SetActive(false);
            mPassLabel.SetActive(false);
        }
    }
    private void Update()
    {
        if (isSelectCard)
        {
            var ray = UICamera.currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var temp = hitInfo.collider.GetComponent<Card>();
                if (temp != null&&mCardList.Contains(temp))
                {
                    int beginIndex = mFirstSelectCard.index <= temp.index ? mFirstSelectCard.index : temp.index;
                    int endIndex = mFirstSelectCard.index < temp.index ? temp.index : mFirstSelectCard.index;
                    for (int i = 0; i < mCardList.Count; i++)
                        mCardList[i].card.color = Color.white;
                    for (int i= beginIndex; i <= endIndex; i++)
                        mCardList[i].card.color = Color.gray;
                }
            }
        }
    }
    /// <summary>
    /// 获取要出的牌列表
    /// </summary>
    /// <returns></returns>
    private List<CardData> GetPlayCardData()
    {
        List<CardData> temp = new List<CardData>();
        for (int i = 0; i < mCardList.Count; i++)
        {
            if (mCardList[i].gameObject.activeSelf && mCardList[i].transform.localPosition.y > 20)
                temp.Add(mCardList[i].data);
        }
        return temp;
    }
    /// <summary>
    /// 出牌
    /// </summary>
    private void Play()
    {
        List<CardData> temp=GetPlayCardData();
        var type = CardArithmetic.Check(temp);
        Debug.LogError(type);
        if (type == CardArithmetic.CardType.Nnll)
            return;
            
        for (int i = 0; i < temp.Count; i++)
        {
            mCardData.Remove(temp[i]);
            for (int j = 0; j < mCardList.Count; j++)
            {
                if(mCardList[j].data == temp[i])
                {
                    mCardList[j].gameObject.SetActive(false);
                    break;
                }
            }
            mActCardList[i].SetData(temp[i]);
        }
        mActCard.enabled = true;
        mCard.enabled = true;
        mPlay.SetActive(false);

        if (mCardData.Count == 0)
        {
            EventManage.Instance.Broadcast(EventEnum.combat, "over");
            return;
        }
        ControllerManage.Instance.mCombatController.mPlayCard.Play(temp);
    }
    private void HitPlay()
    {
        List<CardData> temp = GetPlayCardData();
        var type = CardArithmetic.Check(temp);
        Debug.LogError(type);
        if (type == CardArithmetic.CardType.Nnll)
            return;
        mAct.SetActive(false);
        Play();
    }
    private void TipsPlay()
    {
        for (int i = 0; i < mCardList.Count; i++)
            mCardList[i].transform.localPosition = new Vector3(mCardList[i].transform.localPosition.x, 0, 0);
        var tips = CardArithmetic.AutoPlay(mCardData, ControllerManage.Instance.mCombatController.mPlayCard);
        for (int i = 0; i < mCardList.Count; i++)
        {
            for (int j = 0; j < tips.Count; j++)
            {
                if (tips[j] == mCardList[i].data)
                    mCardList[i].transform.localPosition = new Vector3(mCardList[i].transform.localPosition.x, 24, 0);
            }
        }
    }
    private void PassPlay()
    {
        mAct.SetActive(false);
        mPass.SetActive(false);
        mPassLabel.SetActive(true);
        for (int i = 0; i < mCardList.Count; i++)
            mCardList[i].transform.localPosition = new Vector3(mCardList[i].transform.localPosition.x, 0,0);
        ControllerManage.Instance.mCombatController.mPlayCard.Play();
    }
}
