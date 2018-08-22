using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyView : MonoBehaviour {

    public UILabel nickname;
    public UILabel score;

    void Awake()
    {
        nickname = transform.Find("BG/TopLeft/bg/name").GetComponent<UILabel>();
        score = transform.Find("BG/TopLeft/bg/zuanshi").GetComponent<UILabel>();

        Common.AddClickListener(transform.Find("BG/TopRight/bg/caidanBtn").gameObject,()=>EventManage.Instance.Broadcast(EventEnum.lobby,"set"));
        Common.AddClickListener(transform.Find("BG/Right/jiafangBtn").gameObject, () => EventManage.Instance.Broadcast(EventEnum.lobby, "combat"));

        Common.AddClickListener(transform.Find("BG/TopRight/bg/fenxiangBtn").gameObject, Common.CreateMessageBox);
        Common.AddClickListener(transform.Find("BG/TopRight/bg/zhanjiBtn").gameObject, Common.CreateMessageBox);
        Common.AddClickListener(transform.Find("BG/TopRight/bg/yaoqingmaBtn").gameObject, Common.CreateMessageBox);
        Common.AddClickListener(transform.Find("BG/Right/jianfangBtn").gameObject, Common.CreateMessageBox);
        Common.AddClickListener(transform.Find("BG/Right/xinxiBtn").gameObject, Common.CreateMessageBox);
        Common.AddClickListener(transform.Find("BG/Right/dailiBtn").gameObject, Common.CreateMessageBox);
    }
    void Start()
    {
        nickname.text = ControllerManage.Instance.mLoginControll.mLoginMode.loginUserData.nickname;
        score.text = ControllerManage.Instance.mLoginControll.mLoginMode.loginUserData.score.ToString();
    }
}
