using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

static public class Common {

    static public void PopupGameObject(GameObject go, TweenCallback OnComplete = null, bool b = true, float d = 0)
    {
        if (b)
            go.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutBack).SetDelay(d).From().OnStart(() => go.SetActive(true)).OnComplete(OnComplete);
        else
            go.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).SetDelay(d).OnStart(() => go.SetActive(true)).OnComplete(OnComplete);
    }
    static public GameObject AddChild(GameObject parent, GameObject prefab)
    {
        return NGUITools.AddChild(parent, prefab,32);
    }
    static public void AddClickListener(GameObject go, EventDelegate.Callback e, bool isPlayAnimation = true)
    {
        if (go.GetComponent<Collider>() == null)
            NGUITools.AddWidgetCollider(go);
        UIEventListener _UIEventListener = UIEventListener.Get(go);
        if (!isPlayAnimation)
        {
            _UIEventListener.onPress = (_go, _b) =>
            {
                if (_b)
                {
                    e();
                    //播放音效
                    AudioManage.Instance.Play(AudioClipEnum.button);
                }
            };
            return;
        }
        var _Scale = go.transform.localScale;
        _UIEventListener.onPress = (_go, _b) =>
        {
            if (_b)
            {
                TweenScale.Begin(go, 0.2f, _Scale * 0.8f).method = UITweener.Method.EaseInOut;
            }
            else
            {
                var _tw = TweenScale.Begin(go, 0.2f, _Scale);
                _tw.method = UITweener.Method.EaseInOut;

                if (UICamera.IsHighlighted(go))
                {
                    _tw.SetOnFinished(() => e());
                    //播放音效
                    AudioManage.Instance.Play(AudioClipEnum.button);
                }
            }
        };
    }
    static public CommonMsgBox CreateMessageBox(string arg1, string arg2 = null, CommonMsgBox.ButtonCallback rightButtonCallback = null, CommonMsgBox.ButtonCallback leftButtonCallback = null)
    {
        GameObject go = ResourceManage.Instance.LoadPrefab("CommonMsgBox");
        CommonMsgBox box = go.AddComponent<CommonMsgBox>();

        box.SetTitle(arg1);
        if (String.IsNullOrEmpty(arg2))
            arg2 = "取消";
        box.SetButtonLabel(arg2);
        box.mRightButtonCallback = rightButtonCallback;
        box.mLeftButtonCallback = leftButtonCallback;

        if (rightButtonCallback == null && leftButtonCallback == null)
            box.ShowOnlyConfirmButton();
        return box;
    }
    static public void CreateMessageBox()
    {
        CreateMessageBox(DesEnum.暂未开放.ToString());
    }
}
