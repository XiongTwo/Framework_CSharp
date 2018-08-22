using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetView : MonoBehaviour {

    private GameObject panel;
    private Vector3 panelScale;

    private UISlider musicSlide;
    private GameObject musicOff;
    private GameObject musicOn;

    private UISlider soundEffectSlide;
    private GameObject soundEffectOff;
    private GameObject soundEffectOn;

    private GameObject shakeOff;
    private GameObject shakeOn;

    void Awake()
    {
        panel = transform.Find("BG").gameObject;
        panelScale = panel.transform.localScale;
        NGUITools.AddWidgetCollider(transform.Find("Mask").gameObject);

        Common.AddClickListener(transform.Find("BG/GameObject").gameObject, Close);
        Common.AddClickListener(transform.Find("BG/Btn").gameObject, Close);

        NGUITools.AddWidgetCollider(transform.Find("BG/Slide/GameObject/GameObject").gameObject);
        NGUITools.AddWidgetCollider(transform.Find("BG/Slide/GameObject/GameObject/GameObject (1)/GameObject/GameObject (1)").gameObject);
        musicSlide =transform.Find("BG/Slide/GameObject").GetComponent<UISlider>();
        musicOff=transform.Find("BG/Slide/GameObject (2)").gameObject;
        musicOn=transform.Find("BG/Slide/GameObject (1)").gameObject;

        NGUITools.AddWidgetCollider(transform.Find("BG/Slide (1)/GameObject/GameObject").gameObject);
        NGUITools.AddWidgetCollider(transform.Find("BG/Slide (1)/GameObject/GameObject/GameObject (1)/GameObject/GameObject (1)").gameObject);
        soundEffectSlide = transform.Find("BG/Slide (1)/GameObject").GetComponent<UISlider>();
        soundEffectOff = transform.Find("BG/Slide (1)/GameObject (2)").gameObject;
        soundEffectOn = transform.Find("BG/Slide (1)/GameObject (1)").gameObject;

        shakeOff = transform.Find("BG/Slide (2)/GameObject/GameObject (1)").gameObject;
        shakeOn = transform.Find("BG/Slide (2)/GameObject/GameObject (2)").gameObject;

        Common.AddClickListener(transform.Find("BG/Slide (2)/GameObject/GameObject").gameObject, ShakeOff, false);
        Common.AddClickListener(transform.Find("BG/Slide (2)/GameObject/GameObject (3)").gameObject, ShakeOn, false);

    }
    void Start()
    {
        musicSlide.value = (float)ControllerManage.Instance.mSetControll.mSetMode.mSetData.musicSize;
        MusicSlideOnChange();
        soundEffectSlide.value = (float)ControllerManage.Instance.mSetControll.mSetMode.mSetData.soundEffectSize;
        SoundEffectSlideOnChange();
        SetShake(ControllerManage.Instance.mSetControll.mSetMode.mSetData.isShake);

        musicSlide.onChange.Add(new EventDelegate(MusicSlideOnChange));
        soundEffectSlide.onChange.Add(new EventDelegate(SoundEffectSlideOnChange));
    }
    void OnEnable()
    {
        Common.PopupGameObject(panel);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    void Close()
    {
        Common.PopupGameObject(panel, () => { gameObject.SetActive(false); panel.transform.localScale = panelScale; }, false);
        EventManage.Instance.Broadcast(EventEnum.set,"save");
    }
    void MusicSlideOnChange()
    {
        ControllerManage.Instance.mSetControll.mSetMode.mSetData.musicSize = musicSlide.value;
        AudioManage.Instance.UpdateMusicSize();
        musicOff.gameObject.SetActive(false);
        musicOn.gameObject.SetActive(true);
        if (musicSlide.value == 0)
        {
            musicOff.gameObject.SetActive(true);
            musicOn.gameObject.SetActive(false);
        }
    }
    void SoundEffectSlideOnChange()
    {
        ControllerManage.Instance.mSetControll.mSetMode.mSetData.soundEffectSize = soundEffectSlide.value;
        soundEffectOff.gameObject.SetActive(false);
        soundEffectOn.gameObject.SetActive(true);
        if (soundEffectSlide.value == 0)
        {
            soundEffectOff.gameObject.SetActive(true);
            soundEffectOn.gameObject.SetActive(false);
        }
    }
    void ShakeOff()
    {
        SetShake(false);
    }
    void ShakeOn()
    {
        SetShake(true);
    }
    void SetShake(bool b)
    {
        ControllerManage.Instance.mSetControll.mSetMode.mSetData.isShake = b;
        if (b)
        {
            shakeOff.SetActive(false);
            shakeOn.SetActive(true);
        }
        else
        {
            shakeOff.SetActive(true);
            shakeOn.SetActive(false);
        }
    }
    
}
