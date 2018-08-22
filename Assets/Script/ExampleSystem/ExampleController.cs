using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleControll {

    public ExampleMode mExampleMode;
    public ExampleView mExampleView;

    public ExampleControll()
    {
        mExampleMode =new ExampleMode();
        AddListener();
    }
    private void AddListener()
    {
        EventManage.Instance.AddListener<string>(EventEnum.example, Processor);
    }
    
    private void Processor(string type)
    {
        switch (type)
        {
            case "show":
                
                break;
        }
    }
}
