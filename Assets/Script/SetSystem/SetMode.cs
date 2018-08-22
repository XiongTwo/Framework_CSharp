using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMode {

    public SetData mSetData;
    private string cachePath = Application.persistentDataPath + "/Cache/";
    private string cacheFile = "SetDataCache.txt";

    public SetMode()
    {
        string data = ResourceManage.Instance.LoadTextFile(cachePath, cacheFile);
        if (data != null)
        {
            try
            {
                mSetData = JsonMapper.ToObject<SetData>(data);
            }
            catch
            {
                data = null;
            }
        }
        if (data == null)
        {
            AudioListener.volume = 1;
            mSetData = new SetData();
            SaveSetDataCache();
        }
    }
    
    public void SaveSetDataCache()
    {
        ResourceManage.Instance.SaveTextFile(cachePath, cacheFile, JsonMapper.ToJson(mSetData));
    }
}
public class SetData
{
    public double musicSize = 1f;
    public double soundEffectSize
    {
        get
        {
            return AudioListener.volume;
        }
        set
        {
            AudioListener.volume = (float)value;
        }
    }
    public bool isShake;
}
