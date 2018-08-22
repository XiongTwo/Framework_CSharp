using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginControll
{
    public LoginMode mLoginMode;
    public LoginView mLoginView;
    

    public LoginControll()
    {
        mLoginMode = new LoginMode();
        AddListener();
    }
    private void AddListener()
    {
        EventManage.Instance.AddListener(EventEnum.start_game, StartGame);
        EventManage.Instance.AddListener<string>(EventEnum.login, Processor);
    }
    private void StartGame()
    {
        mLoginView=ResourceManage.Instance.LoadPrefab("LoginView").AddComponent<LoginView>();
        AudioManage.Instance.PlayMusic(AudioClipEnum.main);
    }
    private void Processor(string type)
    {
        switch (type)
        {
            case "wx":
                Processor2wx();
                break;
            case "user":
                Processor2user();
                break;
            case "register":
                Processor2register();
                break;
            case "login":
                Processor2login();
                break;
        }
    }
    private void Processor2wx()
    {
        mLoginMode.loginUserData = mLoginMode.userDataCache.wx;
        EventManage.Instance.Broadcast(EventEnum.enter_lobby);
        GameObject.Destroy(mLoginView.gameObject);
    }
    private void Processor2user()
    {
        mLoginView.accountView.SetActive(true);
        if (mLoginMode.userDataCache.userListIndex != -1)
        {
            var userData = mLoginMode.userDataCache.userList[mLoginMode.userDataCache.userListIndex];
            mLoginView.accountInput.value = userData.account;
            mLoginView.passwordInput.value = userData.password;
        }
    }
    private void Processor2register()
    {
        if (string.IsNullOrEmpty(mLoginView.accountInput.value))
        {
            Common.CreateMessageBox(DesEnum.账号不能为空.ToString());
            return;
        }
        if (string.IsNullOrEmpty(mLoginView.passwordInput.value))
        {
            Common.CreateMessageBox(DesEnum.密码不能为空.ToString());
            return;
        }
        bool isUser = false;
        for (int i = 0; i < mLoginMode.userDataCache.userList.Count; i++)
        {
            if (mLoginMode.userDataCache.userList[i].account == mLoginView.accountInput.value)
                isUser = true;
        }
        if (isUser)
        {
            Common.CreateMessageBox(DesEnum.该账号已存在.ToString());
            return;
        }
        UserData newUserData = new UserData();
        newUserData.account = mLoginView.accountInput.value;
        newUserData.password = mLoginView.passwordInput.value;
        newUserData.nickname = mLoginView.accountInput.value;
        mLoginMode.userDataCache.userList.Add(newUserData);
        mLoginMode.userDataCache.userListIndex = mLoginMode.userDataCache.userList.Count - 1;
        mLoginMode.SaveUserDataCache();
        Common.CreateMessageBox(DesEnum.注册成功.ToString());
    }
    private void Processor2login()
    {
        if (string.IsNullOrEmpty(mLoginView.accountInput.value))
        {
            Common.CreateMessageBox(DesEnum.账号不能为空.ToString());
            return;
        }
        if (string.IsNullOrEmpty(mLoginView.passwordInput.value))
        {
            Common.CreateMessageBox(DesEnum.密码不能为空.ToString());
            return;
        }
        bool isUser = false;
        for (int i = 0; i < mLoginMode.userDataCache.userList.Count; i++)
        {
            if(mLoginMode.userDataCache.userList[i].account== mLoginView.accountInput.value)
            {
                if(mLoginMode.userDataCache.userList[i].password != mLoginView.passwordInput.value)
                {
                    Common.CreateMessageBox(DesEnum.密码错误.ToString());
                    return;
                }
                isUser = true;
                mLoginMode.userDataCache.userListIndex = i;
                mLoginMode.SaveUserDataCache();
                mLoginMode.loginUserData = mLoginMode.userDataCache.userList[i];
            }
        }
        if (!isUser)
        {
            Common.CreateMessageBox(DesEnum.该账号不存在.ToString());
            return;
        }
        EventManage.Instance.Broadcast(EventEnum.enter_lobby);
        GameObject.Destroy(mLoginView.gameObject);
    }
}
