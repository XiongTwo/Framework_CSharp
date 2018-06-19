using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleControll {

    private ExampleMode mExampleMode;
    private ExampleView mExampleView;

    public ExampleControll()
    {
        mExampleMode =new ExampleMode();
        AddListener();
    }
    private void AddListener()
    {
        EventManage.Instance.AddListener(EventEnum.example, Processor);
    }
    private void Processor()
    {
        Processor("show");
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
