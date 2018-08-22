using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAITime : MonoBehaviour {

    public PlayerAI mPlayerAI;
    int time = 30;
    void Awake()
    {
        mPlayerAI = transform.parent.GetComponent<PlayerAI>();
    }
    void OnEnable()
    {
        time = 30;
        mPlayerAI.SetTime(true, time);
        InvokeRepeating("ChangTime",1,1);
    }
    void ChangTime()
    {
        time--;
        mPlayerAI.SetTime(true, time);
    }
    void OnDisable()
    {
        CancelInvoke("ChangTime");
    }
}
