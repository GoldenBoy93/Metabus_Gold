using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    protected virtual void GenerateData()
    {
        talkData.Add(1000, new string[] { "¾È³ç.", "¾È³ç2.", "¾È³ç3.", "¾È³ç4.", "¾È³ç5.", "¾È³ç6." });
    }

    public string GetTalk(int id, int  talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
}
