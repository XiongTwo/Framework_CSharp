using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class LoginMode
{
    public UserDataCache userDataCache;
    public UserData loginUserData;

    private string cachePath = Application.persistentDataPath + "/Cache/";
    private string cacheFile = "UserDataCache.txt";

    public LoginMode()
    {
        Debug.Log(Application.persistentDataPath);
        string data = ResourceManage.Instance.LoadTextFile(cachePath, cacheFile);
        if (data != null)
        {
            try
            {
                userDataCache = JsonMapper.ToObject<UserDataCache>(data);
            }
            catch
            {
                data = null;
            }
        }
        if (data == null)
        {
            userDataCache = new UserDataCache();
            UserData wx = new UserData();
            wx.account = "WX";
            wx.password = "WX";
            wx.nickname = "WX";
            userDataCache.wx = wx;
            SaveUserDataCache();
        }
    }
    public void SaveUserDataCache()
    {
        ResourceManage.Instance.SaveTextFile(cachePath, cacheFile, JsonMapper.ToJson(userDataCache));
    }
    public void SetLoginUserScore(int score)
    {
        if (score == loginUserData.score)
            return;
        loginUserData.score = score;
        SaveUserDataCache();
        EventManage.Instance.Broadcast(EventEnum.chang_score, loginUserData.score);
    }
}
public class UserData
{
    public string account;
    public string password;
    public string nickname;
    public int score;
}
public class UserDataCache
{
    public UserData wx;
    public int userListIndex = -1;
    public List<UserData> userList=new List<UserData>();
}
