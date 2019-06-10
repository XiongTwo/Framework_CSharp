using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRedDot : MonoBehaviour {

	void Start () {
        RedDotManage.Register(RedDotEnum.FirstRedDot, (redDotVo) =>
        {
            redDotVo.state = true;
            redDotVo.num = 1;
        });

        RedDotManage.Register(RedDotEnum.SecondRedDot, (redDotVo) =>
        {
            redDotVo.state = true;
            redDotVo.num = 2;
        });

        RedDotManage.Register(RedDotEnum.ThirdRedDot, (redDotVo) =>
        {
            redDotVo.state = true;
            redDotVo.num = 3;
        });

        RedDotManage.Register(RedDotEnum.MultiRedDot, (redDotVo) =>
        {
            Debug.LogError("触发的多条件判断红点刷新:" + RedDotEnum.MultiRedDot.ToString());
            Debug.LogError(redDotVo.state);
            Debug.LogError(redDotVo.num);
            Debug.LogError("************************");
        }, RedDotEnum.FirstRedDot, RedDotEnum.SecondRedDot, RedDotEnum.ThirdRedDot);

        RefreshRedDot(RedDotEnum.FirstRedDot);
        RefreshRedDot(RedDotEnum.SecondRedDot);
        RefreshRedDot(RedDotEnum.ThirdRedDot);

        RefreshRedDot(RedDotEnum.MultiRedDot);
    }
	
    void RefreshRedDot(RedDotEnum e)
    {
        RedDotManage.Execute(e);
        RedDotVo vo = RedDotManage.GetRedDotVo(e);
        Debug.LogError("红点刷新:" + e.ToString());
        Debug.LogError(vo.state);
        Debug.LogError("************************");
    }

    void Update () {
		
	}
}
