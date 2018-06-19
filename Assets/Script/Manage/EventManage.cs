using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManage {

    private static EventManage instance;
    public static EventManage Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManage();
            return instance;
        }
    }

    public void AddListener(EventEnum e,Callback handler)
    {
        Messenger.AddListener(e.ToString(), handler);
    }
    public void AddListener<T>(EventEnum e, Callback<T> handler)
    {
        Messenger.AddListener(e.ToString(), handler);
    }
    public void AddListener<T,U>(EventEnum e, Callback<T,U> handler)
    {
        Messenger.AddListener(e.ToString(), handler);
    }
    public void AddListener<T,U,V>(EventEnum e, Callback<T,U,V> handler)
    {
        Messenger.AddListener(e.ToString(), handler);
    }

    public void RemoveListener(EventEnum e, Callback handler)
    {
        Messenger.RemoveListener(e.ToString(), handler);
    }
    public void RemoveListener<T>(EventEnum e, Callback<T> handler)
    {
        Messenger.RemoveListener(e.ToString(), handler);
    }
    public void RemoveListener<T, U>(EventEnum e, Callback<T, U> handler)
    {
        Messenger.RemoveListener(e.ToString(), handler);
    }
    public void RemoveListener<T, U, V>(EventEnum e, Callback<T, U, V> handler)
    {
        Messenger.RemoveListener(e.ToString(), handler);
    }

    public void Broadcast(EventEnum e)
    {
        Messenger.Broadcast(e.ToString());
    }
    public void Broadcast<T>(EventEnum e, T arg1)
    {
        Messenger.Broadcast(e.ToString(), arg1);
    }
    public void Broadcast<T, U>(EventEnum e, T arg1, U arg2)
    {
        Messenger.Broadcast(e.ToString(), arg1, arg2);
    }
    public void Broadcast<T, U, V>(EventEnum e, T arg1, U arg2, V arg3)
    {
        Messenger.Broadcast(e.ToString(), arg1, arg2, arg3);
    }
}
