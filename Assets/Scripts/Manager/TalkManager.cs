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
        talkData.Add(1000, new string[] { "�ȳ�.", "�ȳ�2.", "�ȳ�3.", "�ȳ�4.", "�ȳ�5.", "�ȳ�6." });
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
