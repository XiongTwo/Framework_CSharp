using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBillboard : MonoBehaviour {

    public BoxCollider dragBG;
    public UIWrapContent uiWrapContent;
    public UICenterOnChild uiCenterOnChild;
    public List<Texture> TexList;

    private List<GameObject> itemList = new List<GameObject>();

    private Dictionary<GameObject, GameObject> itemToDot = new Dictionary<GameObject, GameObject>();
    public UIGrid dotGrid;
    public GameObject dotObj;
    private GameObject markDot;

    string current = "current";
    float delayed = 4f;

    private void Awake()
    {
        transform.DestroyChildren();
        dotGrid.transform.DestroyChildren();
        uiCenterOnChild.onFinished = OnFinished;
        uiCenterOnChild.springStrength = 18f;
        Show();
    }
    private void OnEnable()
    {
        InvokeRepeating("Carousel", delayed, delayed);
    }
    private void OnDisable()
    {
        CancelInvoke("Carousel");
    }
    private void Show()
    {
        for (int i = 0; i < TexList.Count; i++)
        {
            Add(TexList[i]);
        }
        itemList[0].name = current;
        if (itemList.Count <= 1)
            dragBG.enabled = false;
        else
            dragBG.enabled = true;

        markDot = NGUITools.AddChild(dotGrid.transform.parent.gameObject, dotObj);
        markDot.GetComponent<UISprite>().spriteName = "HallDotBright";
        markDot.GetComponent<UISprite>().MakePixelPerfect();
        markDot.SetActive(true);
    }
    void Add(Texture _tex)
    {
        var go = NGUITools.AddChild(gameObject);
        itemList.Add(go);
        go.transform.localPosition = new Vector3(itemList.Count* uiWrapContent.itemSize, 0,0);
        var tex = go.AddComponent<UITexture>();
        tex.mainTexture = _tex;
        tex.MakePixelPerfect();

        var goo = NGUITools.AddChild(dotGrid.gameObject,dotObj);
        itemToDot.Add(go,goo);
        goo.SetActive(true);
        dotGrid.Reposition();
    }

    void Carousel()
    {
        if (itemList.Count <= 1)
            return;
        GameObject item= uiCenterOnChild.centeredObject;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == item)
            {
                int index = 0;
                if ((i + 1) < itemList.Count)
                {
                    index = i + 1;
                }
                item = itemList[index];
                break;
            }
        }
        uiCenterOnChild.CenterOn(item.transform);
    }
    void OnFinished()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].name = "null";
        }
        GameObject item = uiCenterOnChild.centeredObject;
        item.name = current;
        markDot.transform.position = itemToDot[item].transform.position;
    }
}
