using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetControll
{
    public SetMode mSetMode;
    public SetView mSetView;

    public SetControll()
    {
        mSetMode =new SetMode();
        AddListener();
    }
    private void AddListener()
    {
        EventManage.Instance.AddListener<string>(EventEnum.set, Processor);
        EventManage.Instance.AddListener(EventEnum.enter_set, Enter);
    }
    private void Enter()
    {
        if (mSetView == null)
            mSetView = ResourceManage.Instance.LoadPrefab("SetView").AddComponent<SetView>();
        else
            mSetView.gameObject.SetActive(true);
    }
    private void Processor(string type)
    {
        switch (type)
        {
            case "save":
                mSetMode.SaveSetDataCache();
                break;
        }
    }
}
