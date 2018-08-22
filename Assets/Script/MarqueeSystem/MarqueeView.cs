using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MarqueeView : MonoBehaviour {

    private UILabel item;
    private UIPanel panel;

    string content;
    int count;
    float speed = 120;

    static private List<string> contentList = new List<string>();

    Tweener tweener = null;

    private void Awake()
    {
        item = transform.parent.Find("txt").GetComponent<UILabel>();
        item.transform.SetParent(transform);
        item.gameObject.SetActive(false);

        panel = GetComponent<UIPanel>();
    }

    private void Start()
    {
        Play();
    }

    public void AddSystemMessage(string _content)
    {
        contentList.Add(_content);
        Play();
    }

    private void Play()
    {
        if (contentList.Count > 0 && !item.gameObject.activeSelf)
        {
            transform.parent.parent.gameObject.SetActive(true);
            Play(contentList[0]);
            contentList.RemoveAt(0);
        }
    }
    private void Play(string _content)
    {
        item.transform.localPosition = new Vector3(panel.width / 2,0,0);
        item.text = _content;
        item.gameObject.SetActive(true);
        float x = item.width + panel.width / 2;

        if (tweener != null)
            tweener.Kill();
        tweener = item.transform.DOLocalMoveX (-x, speed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(()=> { transform.parent.parent.gameObject.SetActive(false); item.gameObject.SetActive(false); Play(); });
    }
}
