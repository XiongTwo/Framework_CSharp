using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManage : MonoBehaviour
{

    private static AudioManage instance;

    public static AudioManage Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("AudioManage").AddComponent<AudioManage>();
            return instance;
        }
    }

    private AudioSource musicAudioSource;
    private List<AudioSource> audioSourceList=new List<AudioSource>();
    private AudioClipResource audioClipResource;

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.AddComponent<AudioListener>();
        musicAudioSource=gameObject.AddComponent<AudioSource>();
        musicAudioSource.loop = true;
        musicAudioSource.ignoreListenerVolume = true;
        UpdateMusicSize();

        audioClipResource =ResourceManage.Instance.LoadPrefab("AudioClipResource", gameObject).GetComponent<AudioClipResource>();
    }

    //播放背景音乐，只能有一个背景音乐在播放
    public void PlayMusic(AudioClipEnum path)
    {
        musicAudioSource.clip = LoadAudioClip(path);
        musicAudioSource.Play();
    }
    //播放音效
    public void Play(AudioClipEnum path)
    {
        AudioSource _AudioSource = null;
        for (int i = 0; i < audioSourceList.Count; i++)
        {
            if (!audioSourceList[i].isPlaying)
                _AudioSource = audioSourceList[i];
        }
        if (_AudioSource == null)
        {
            _AudioSource = gameObject.AddComponent<AudioSource>();
            audioSourceList.Add(_AudioSource);
        }
        _AudioSource.clip = LoadAudioClip(path);
        _AudioSource.Play();
    }
    //加载声音剪辑
    private AudioClip LoadAudioClip(AudioClipEnum path)
    {
        for (int i = 0; i < audioClipResource.audioClipResource.Count; i++)
        {
            if (audioClipResource.audioClipResource[i].name == path.ToString())
                return audioClipResource.audioClipResource[i];
        }
        return null;
    }
    //更新背景音乐的音量
    public void UpdateMusicSize()
    {
        musicAudioSource.volume = (float)ControllerManage.Instance.mSetControll.mSetMode.mSetData.musicSize;
    }
}
