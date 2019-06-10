using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RedDotVo
{
    public bool state = false;
    public int num = 0;
    public List<RedDotEnum> byCiteList = new List<RedDotEnum>();
}

public class RedDotManage
{
    private static Dictionary<RedDotEnum, Action<RedDotVo>> redDotFunDic = new Dictionary<RedDotEnum, Action<RedDotVo>>();
    private static Dictionary<RedDotEnum, RedDotVo> redDotVoDic = new Dictionary<RedDotEnum, RedDotVo>();
    private static Dictionary<RedDotEnum, RedDotEnum[]> multiModeKeyList = new Dictionary<RedDotEnum, RedDotEnum[]>();

    public static void Register(RedDotEnum key, Action<RedDotVo> fun , params RedDotEnum[] array)
    {
        redDotFunDic.Add(key, fun);
        RedDotVo redDotVo = new RedDotVo();
        redDotVoDic.Add(key, redDotVo);
        if (array.Length>0)
        {
            multiModeKeyList.Add(key, array);
        }
        for (int i = 0; i < array.Length; i++)
        {
            redDotVoDic[array[i]].byCiteList.Add(key);
        }
    }

    public static void Execute(RedDotEnum key)
    {
        if (multiModeKeyList.ContainsKey(key))
        {
            RedDotEnum[] array = multiModeKeyList[key];
            for (int i = 0; i < array.Length; i++)
            {
                Execute(array[i]);
            }
        }
        else
        {
            Action<RedDotVo> fun = redDotFunDic[key];
            RedDotVo redDotVo = redDotVoDic[key];
            fun.Invoke(redDotVo);
            SetMultiModeRedDotVo(redDotVo);
        }
    }

    //设置多个模式的RedDotVo
    private static void SetMultiModeRedDotVo(RedDotVo redDotVo)
    {
        for (int i = 0; i < redDotVo.byCiteList.Count; i++)
        {
            RedDotEnum multiModeKey = redDotVo.byCiteList[i];
            Action<RedDotVo> multiModeFun = redDotFunDic[multiModeKey];
            RedDotVo multiModeRedDotVo = redDotVoDic[multiModeKey];

            RedDotEnum[] array = multiModeKeyList[multiModeKey];
            bool state = false;
            int num = 0;
            for (int j = 0; j < array.Length; j++)
            {
                RedDotVo vo = redDotVoDic[array[j]];
                num += vo.num;
                if (j == 0)
                    state = vo.state;
                else
                    state = state && vo.state;
            }
            multiModeRedDotVo.num = num;
            multiModeRedDotVo.state = state;
            multiModeFun.Invoke(multiModeRedDotVo);
        }
    }

    public static RedDotVo GetRedDotVo(RedDotEnum key)
    {
        return redDotVoDic[key];
    }
}
