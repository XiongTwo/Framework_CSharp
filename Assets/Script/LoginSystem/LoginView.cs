using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginView : MonoBehaviour {

    public GameObject accountView;
    public UIInput accountInput;
    public UIInput passwordInput;

    void Awake()
    {
        accountView = transform.Find("Account").gameObject;
        Common.AddClickListener(transform.Find("BG/WX").gameObject,
            ()=>EventManage.Instance.Broadcast<string>(EventEnum.login, "wx"));
        Common.AddClickListener(transform.Find("BG/Test").gameObject,
            () => EventManage.Instance.Broadcast<string>(EventEnum.login, "user"));

        NGUITools.AddWidgetCollider(transform.Find("Account/Mask").gameObject);
        Common.AddClickListener(transform.Find("Account/BG/Close").gameObject,
            () => accountView.SetActive(false));
        Common.AddClickListener(transform.Find("Account/BG/RegisterBtn").gameObject,
            () => EventManage.Instance.Broadcast<string>(EventEnum.login, "register"));
        Common.AddClickListener(transform.Find("Account/BG/LoginBtn").gameObject,
            () => EventManage.Instance.Broadcast<string>(EventEnum.login, "login"));

        accountInput = transform.Find("Account/BG/AccountInput/Text").GetComponent<UIInput>();
        passwordInput = transform.Find("Account/BG/PasswordInput/Text").GetComponent<UIInput>();
    }
    void Start()
    {
        accountView.SetActive(false);
    }

}
