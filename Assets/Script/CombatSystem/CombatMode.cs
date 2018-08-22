using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMode
{
    public CombatMode()
    {
        
    }
    public List<CardData> allCardData = new List<CardData>();
    public void Init()
    {
        allCardData.Clear();
        allCardData.Add(new CardData(18));//小王
        allCardData.Add(new CardData(19));//大王
        for (int i = 3; i <= 14; i++)
        {
            allCardData.Add(new CardData(i, CardData.CardColor.FK));
            allCardData.Add(new CardData(i, CardData.CardColor.MH));
            allCardData.Add(new CardData(i, CardData.CardColor.HX));
            allCardData.Add(new CardData(i, CardData.CardColor.HT));
        }
        allCardData.Add(new CardData(16, CardData.CardColor.FK));
        allCardData.Add(new CardData(16, CardData.CardColor.MH));
        allCardData.Add(new CardData(16, CardData.CardColor.HX));
        allCardData.Add(new CardData(16, CardData.CardColor.HT));
    }
}
public class CardData : IComparable<CardData>
{
    public int number;
    public enum CardColor
    {
        FK,
        MH,
        HX,
        HT,
    }
    public CardColor color;
    public CardData(int _number, CardColor _color= CardColor.FK)
    {
        number = _number;
        color = _color;
    }

    public int CompareTo(CardData other)
    {
        if (number > other.number)
            return -1;
        if (number < other.number)
            return 1;
        if (number == other.number)
        {
            if (color > other.color)
                return -1;
            if (color < other.color)
                return 1;
        }
        return 0;
    }
}


