using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManage {

    private static ControllerManage instance;
    public static ControllerManage Instance
    {
        get
        {
            if (instance == null)
                instance = new ControllerManage();
            return instance;
        }
    }

    public ExampleControll mExampleControll;

    public void Init()
    {
        mExampleControll = new ExampleControll();
    }
}
