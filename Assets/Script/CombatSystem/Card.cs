using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public CardData data;
    public int index = -1;
    public UISprite card;

    void Awake()
    {
        card = GetComponent<UISprite>();
        gameObject.SetActive(false);
    }

    public void SetData(CardData _data)
    {
        data = _data;
        if (data.number == 0)
            card.spriteName = "back";
        else if (data.number == 18)
            card.spriteName = "black_Joker";
        else if (data.number == 19)
            card.spriteName = "red_Joker";
        else
            card.spriteName = data.color.ToString() + "_" + data.number;
        gameObject.SetActive(true);
    }
}
