using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArithmetic {


    public enum CardType
    {
        Nnll,

        单张,

        对子,
        王炸,

        三条,

        三带一,
        炸弹,

        三带二,
        单顺,

        四带二,
        双顺,
        三顺,

        四带对,
        小飞机,

        大飞机,
    }
    /// <summary>
    /// 检查出牌
    /// </summary>
    /// <returns></returns>
    static public CardType Check(List<CardData> datas)
    {
        datas.Sort();//从大到小排列

        string log = "";
        for (int i = 0; i < datas.Count; i++)
            log += datas[i].number + ",";
        Debug.LogError(log);

        if (datas.Count == 1)//单张
            return CardType.单张;
        if (datas.Count == 2)//对子，王炸
        {
            if(datas[0].number== datas[1].number)
                return CardType.对子;
            if (datas[0].number==19&& datas[1].number == 18)
                return CardType.王炸;
        }
        if (datas.Count == 3)//三条
        {
            if (datas[0].number == datas[1].number&& datas[0].number== datas[2].number)
                return CardType.三条;
        }
        if (datas.Count == 4)//三带一,炸弹
        {
            if (datas[0].number == datas[1].number && datas[0].number == datas[2].number && datas[0].number == datas[3].number)
                return CardType.炸弹;
            if (datas[0].number == datas[1].number && datas[0].number == datas[2].number)
                return CardType.三带一;
            if (datas[3].number == datas[2].number && datas[3].number == datas[1].number)
                return CardType.三带一;
        }
        if (datas.Count == 5)//三带二,(单顺)
        {
            if (datas[0].number == datas[1].number && datas[0].number == datas[2].number)
            {
                if (datas[3].number == datas[4].number)
                    return CardType.三带二;
            }
            if (datas[4].number == datas[3].number && datas[4].number == datas[2].number)
            {
                if (datas[0].number == datas[1].number)
                    return CardType.三带二;
            }
        }
        if (datas.Count == 6)//四带二，(单顺,双顺,三顺)
        {
            if (datas[0].number == datas[1].number && datas[0].number == datas[2].number && datas[0].number == datas[3].number)
                return CardType.四带二;
            if (datas[5].number == datas[4].number && datas[5].number == datas[3].number && datas[5].number == datas[2].number)
                return CardType.四带二;
            if (datas[4].number == datas[3].number && datas[4].number == datas[2].number && datas[4].number == datas[1].number)
                return CardType.四带二;
        }
        if (datas.Count == 8)//四带二对，(小飞机,单顺,双顺）
        {
            if (datas[0].number == datas[1].number && datas[0].number == datas[2].number && datas[0].number == datas[3].number)
            {
                if (datas[4].number == datas[5].number&& datas[6].number == datas[7].number)
                    return CardType.四带对;
            }
            if (datas[7].number == datas[6].number && datas[7].number == datas[5].number && datas[7].number == datas[4].number)
            {
                if (datas[3].number == datas[2].number && datas[1].number == datas[0].number)
                    return CardType.四带对;
            }
            if (datas[5].number == datas[4].number && datas[5].number == datas[3].number && datas[5].number == datas[2].number)
            {
                if (datas[0].number == datas[1].number && datas[7].number == datas[6].number)
                    return CardType.四带对;
            }
        }

        CardType type = CardType.Nnll;
        if (datas.Count >= 5) {
            type = IsContinuousSingle(datas);
            if (type != CardType.Nnll)
                return type; //单顺
        }
        
        if (datas.Count >= 6 && datas.Count % 2 == 0)
        {
            type = IsContinuousTwo(datas);
            if (type != CardType.Nnll)
                return type; //双顺
        }

        if (datas.Count >= 6 && datas.Count % 3 == 0)
        {
            type = IsContinuousThree(datas);
            if (type != CardType.Nnll)
                return type; //三顺
        }

        if (datas.Count >= 8 && datas.Count % 4 == 0)
        {
            type = IsSmallFly(datas);
            if (type != CardType.Nnll)
                return type; //小飞机
        }
        
        if (datas.Count >= 10 && datas.Count % 5 == 0)
        {
            type = IsBigFly(datas);
            if (type != CardType.Nnll)
                return type; //大飞机
        }

        return CardType.Nnll;
    }
    static private CardType IsContinuousSingle(List<CardData> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            if (i == datas.Count - 1)
                break;
            int next = datas[i].number - 1;
            if (next != datas[i + 1].number)
                return CardType.Nnll;
        }
        return CardType.单顺;
    }
    static private CardType IsContinuousTwo(List<CardData> datas)
    {
        List<CardData> temp = new List<CardData>();
        int beishu = datas.Count / 2;
        for (int i = 0; i < beishu; i++)
        {
            int index = i * 2;
            if (datas[index].number != datas[index + 1].number)
                return CardType.Nnll;
            temp.Add(datas[index]);
        }
        if(IsContinuousSingle(temp)== CardType.Nnll)
            return CardType.Nnll;
        return CardType.双顺;
    }
    static private CardType IsContinuousThree(List<CardData> datas)
    {
        List<CardData> temp = new List<CardData>();
        int beishu = datas.Count / 3;
        for (int i = 0; i < beishu; i++)
        {
            int index = i * 3;
            if (datas[index].number != datas[index + 1].number)
                return CardType.Nnll;
            if (datas[index].number != datas[index + 2].number)
                return CardType.Nnll;
            temp.Add(datas[index]);
        }
        if (IsContinuousSingle(temp) == CardType.Nnll)
            return CardType.Nnll;
        return CardType.三顺; 
    }
    static private CardType IsSmallFly(List<CardData> datas)
    {
        var temp= GetCard(datas,3);
        if (temp.Count < 2)
            return CardType.Nnll;
        if(IsContinuousSingle(temp)== CardType.Nnll)
            return CardType.Nnll;
        return CardType.小飞机;
    }
    static private CardType IsBigFly(List<CardData> datas)
    {
        var temp = GetCard(datas,3);
        if (temp.Count < 2)
            return CardType.Nnll;
        if (IsContinuousSingle(temp) == CardType.Nnll)
            return CardType.Nnll;

        List<CardData> dd = new List<CardData>();
        List<int> ii = new List<int>();
        for (int i = 0; i < temp.Count; i++)
            ii.Add(temp[i].number);
        for (int i = 0; i < datas.Count; i++)
        {
            if(!ii.Contains(datas[i].number))
                dd.Add(datas[i]);
        }
        dd.Sort();

        int beishu = dd.Count / 2;
        if(beishu!= temp.Count)
            return CardType.Nnll;
        for (int i = 0; i < beishu; i++)
        {
            int index = i * 2;
            if (datas[index].number != datas[index + 1].number)
                return CardType.Nnll;
        }

        return CardType.大飞机;
    }
    /// <summary>
    /// 获取牌中N张相同的牌的号码
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    static private List<CardData> GetCard(List<CardData> datas,int N)
    {
        List<CardData> temp = new List<CardData>();
        List<int> number = new List<int>();
        for (int i = 0; i < datas.Count; i++)
        {
            if (number.Contains(datas[i].number))
                continue;
            int count = 0;
            for (int j = 0; j < datas.Count; j++)
            {
                if (datas[i].number == datas[j].number)
                    count++;
                if (count == N)
                {
                    temp.Add(datas[i]);
                    number.Add(datas[i].number);
                    break;
                }
            }
        }
        temp.Sort();
        return temp;
    }

    /// <summary>
    /// 出牌和打牌提示
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    static public List<CardData> AutoPlay(List<CardData> selfData, PlayCard data=null)
    {
        //出牌
        if (data == null)
            return SeparateCard(selfData);
        //打牌
        return HitCard(selfData, data.cardDataList);
    }
    /// <summary>
    /// 拆牌（智能出牌）
    /// </summary>
    /// <param name="selfData"></param>
    /// <returns></returns>
    static private List<CardData> SeparateCard(List<CardData> selfData)
    {
        List<CardData> data = new List<CardData>();
        selfData.Sort();
        data.Add(selfData[selfData.Count-1]);
        return data;
    }
    /// <summary>
    /// 打牌（智能打牌）
    /// </summary>
    /// <param name="selfData"></param>
    /// <returns></returns>
    static private List<CardData> HitCard(List<CardData> separateCard, List<CardData> data)
    {
        separateCard.Sort();
        List<CardData> tipsCard = new List<CardData>();
        CardType type = Check(data);

        if (separateCard.Count >= data.Count)
        {
            if (type == CardType.单张)
            {
                for (int i = separateCard.Count - 1; i >= 0; i--)
                {
                    if (separateCard[i].number > data[0].number)
                    {
                        tipsCard.Add(separateCard[i]);
                        return tipsCard;
                    }
                }
            }
            else if (type == CardType.对子)
            {
                var temp = GetCard(separateCard, 2);
                temp.Sort();
                int number = -1;
                for (int i = temp.Count - 1; i >= 0; i--)
                {
                    if (temp[i].number > data[0].number)
                    {
                        number = temp[i].number;
                        break;
                    }
                }
                if (number != -1)
                {
                    for (int i = separateCard.Count - 1; i >= 0; i--)
                    {
                        if (tipsCard.Count == 2)
                            return tipsCard;
                        if (separateCard[i].number == number)
                            tipsCard.Add(separateCard[i]);
                    }
                }
            }
            else if (type == CardType.王炸)
            {
                return null;
            }
            else if (type == CardType.三条)
            {
                var temp = GetCard(separateCard, 3);
                temp.Sort();
                int number = -1;
                for (int i = temp.Count - 1; i >= 0; i--)
                {
                    if (temp[i].number > data[0].number)
                    {
                        number = temp[i].number;
                        break;
                    }
                }
                if (number != -1)
                {
                    for (int i = separateCard.Count - 1; i >= 0; i--)
                    {
                        if (tipsCard.Count == 3)
                            return tipsCard;
                        if (separateCard[i].number == number)
                            tipsCard.Add(separateCard[i]);
                    }
                }
            }
            else if (type == CardType.炸弹)
            {

            }
            else if (type == CardType.三带一)
            {
                var temp = GetCard(separateCard, 3);
                temp.Sort();
                int number = -1;
                for (int i = temp.Count - 1; i >= 0; i--)
                {
                    if (temp[i].number > data[0].number)
                    {
                        number = temp[i].number;
                        break;
                    }
                }
                if (number != -1)
                {
                    for (int i = separateCard.Count - 1; i >= 0; i--)
                    {
                        if (tipsCard.Count == 3)
                            break;
                        if (separateCard[i].number == number)
                            tipsCard.Add(separateCard[i]);
                    }
                    for (int i = separateCard.Count - 1; i >= 0; i--)
                    {
                        if (tipsCard.Count == 4)
                            return tipsCard;
                        if (separateCard[i].number != number)
                            tipsCard.Add(separateCard[i]);
                    }
                }
            }
            else if (type == CardType.三带二)
            {
                var temp = GetCard(separateCard, 3);
                temp.Sort();
                int number = -1;
                for (int i = temp.Count - 1; i >= 0; i--)
                {
                    if (temp[i].number > data[0].number)
                    {
                        number = temp[i].number;
                        break;
                    }
                }
                if (number != -1)
                {
                    for (int i = separateCard.Count - 1; i >= 0; i--)
                    {
                        if (tipsCard.Count == 3)
                            break;
                        if (separateCard[i].number == number)
                            tipsCard.Add(separateCard[i]);
                    }

                    var temp2 = GetCard(separateCard, 2);
                    temp2.Sort();
                    int number2 = -1;
                    for (int i = temp2.Count - 1; i >= 0; i--)
                    {
                        if (temp2[i].number != number)
                        {
                            number2 = temp2[i].number;
                            break;
                        }
                    }
                    if (number2 != -1)
                    {
                        for (int i = separateCard.Count - 1; i >= 0; i--)
                        {
                            if (tipsCard.Count == 5)
                                return tipsCard;
                            if (separateCard[i].number == number2)
                                tipsCard.Add(separateCard[i]);
                        }
                    }
                }
            }
            else if (type == CardType.四带二)
            {

            }
            else if (type == CardType.四带对)
            {

            }
            else if (type == CardType.单顺)
            {
                data.Sort();

            }
            else if (type == CardType.双顺)
            {
                data.Sort();
            }
            else if (type == CardType.三顺)
            {
                data.Sort();
            }
            else if (type == CardType.小飞机)
            {

            }
            else if (type == CardType.大飞机)
            {

            }
        }
        if (tipsCard.Count == 0)
        {
            var temp = GetCard(separateCard,4);
            if (temp.Count > 0)
            {
                int number = -1;
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].number > data[0].number)
                    {
                        number = temp[i].number;
                        break;
                    }
                }
                if (number != -1)
                {
                    for (int i = 0; i < separateCard.Count; i++)
                    {
                        if (separateCard[i].number == number)
                            tipsCard.Add(separateCard[i]);
                    }
                    return tipsCard;
                }
            }
            if (separateCard.Count >= 2 && separateCard[0].number == 19 && separateCard[1].number == 18)
            {
                tipsCard.Add(separateCard[0]); tipsCard.Add(separateCard[1]);
                return tipsCard;
            }
            return null;
        }

        return null;
    }
}
