using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditorWindow : EditorWindow {


    [MenuItem("Window/TestEditorWindow")]
    static public void CreateMainGameUI()
    {
        var window = EditorWindow.CreateInstance<TestEditorWindow>();
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Test Button"))
        {
            Debug.LogError("Click Test Button");
        }
        if (GUILayout.Button("跑马灯"))
        {
            string str = DesEnum.单机斗地主.ToString() +"，"+ DesEnum.娱乐作品.ToString() +"，"+ DesEnum.作者QQ419318386.ToString();
            EventManage.Instance.Broadcast(EventEnum.play_marquee,str);
        }
        if (GUILayout.Button("小飞机"))
        {
            List<CardData> data = new List<CardData>();
            data.Add(new CardData(3,CardData.CardColor.FK));
            data.Add(new CardData(3, CardData.CardColor.HT));
            data.Add(new CardData(3, CardData.CardColor.HX));
            data.Add(new CardData(4, CardData.CardColor.FK));
            data.Add(new CardData(4, CardData.CardColor.HT));
            data.Add(new CardData(4, CardData.CardColor.HX));
            data.Add(new CardData(5, CardData.CardColor.FK));
            data.Add(new CardData(5, CardData.CardColor.HT));
            data.Add(new CardData(5, CardData.CardColor.HX));

            data.Add(new CardData(10, CardData.CardColor.MH));
            data.Add(new CardData(10, CardData.CardColor.HX));
            data.Add(new CardData(11, CardData.CardColor.MH));
            data.Add(new CardData(11, CardData.CardColor.HX));
            data.Add(new CardData(12, CardData.CardColor.MH));
            data.Add(new CardData(12, CardData.CardColor.HX));

            Debug.LogError(CardArithmetic.Check(data).ToString());
        }
    }
}
