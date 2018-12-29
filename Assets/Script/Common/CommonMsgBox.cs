using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommonMsgBox : MonoBehaviour
{
    public delegate void ButtonCallback();

    public ButtonCallback mRightButtonCallback;
    public ButtonCallback mLeftButtonCallback;

    public UILabel titleLabel;
    public UILabel buttonLabel;
    public UILabel confirmButtonLabel;

    public GameObject confirmButton;
    public GameObject cancelButton;

    public GameObject panel;

    private void Awake()
    {
        titleLabel = transform.Find("Background/Title").GetComponent<UILabel>();
        buttonLabel = transform.Find("Background/Button (1)/ButtonLabel").GetComponent<UILabel>();
        panel = transform.Find("Background").gameObject;
        confirmButton = transform.Find("Background/Button").gameObject;
        confirmButtonLabel = transform.Find("Background/Button/ButtonLabel").GetComponent<UILabel>();
        cancelButton = transform.Find("Background/Button (1)").gameObject;

        Common.AddClickListener(confirmButton, OnButtonClick);
        Common.AddClickListener(cancelButton, OnCancelButtonClick);

        NGUITools.AddWidgetCollider(transform.Find("Mask").gameObject);
        //Common.AddClickListener(transform.Find("Mask").gameObject, ()=> Common.PopupGameObject(panel, DestroySelf, false), false);
    }

    public void ShowOnlyConfirmButton()
    {
        confirmButton.transform.localPosition = new Vector3(0, -122, 0);
        cancelButton.SetActive(false);
    }

    public void DisableMaskEvent()
    {
        UIEventListener _UIEventListener = UIEventListener.Get(transform.Find("Mask").gameObject);
        _UIEventListener.onPress = null;
    }
    
    public void SetTitle( string title )
    {
        titleLabel.text = "[b]" + title;
    }

    public void SetButtonLabel(string text)
    {
        buttonLabel.text = "[b]" + text;
    }

    public void OnButtonClick()
    {
        Common.PopupGameObject(panel, () => { if(mRightButtonCallback!=null) mRightButtonCallback.Invoke(); DestroySelf(); }, false);
    }
    public void OnCancelButtonClick()
    {
        Common.PopupGameObject(panel, () => { if (mLeftButtonCallback != null) mLeftButtonCallback.Invoke(); DestroySelf(); }, false);
    }

    public void Close()
    {
        Common.PopupGameObject(panel, DestroySelf, false);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

	void OnEnable()
	{
        Common.PopupGameObject(panel);
    }

	void OnDisable()
	{
		
	}
}
