using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public Dictionary<int, string[]> talkData;
    private Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    protected virtual void GenerateData()
    {
        // �ڿ� ���ڴ� �ʻ�ȭ ��ȣ
        talkData.Add(1000, new string[] { "�ȳ�.:0", "�ȳ�2.:0" });

        portraitData.Add(1000 + 0, portraitArr[0]); // ����Ʈ
        portraitData.Add(1000 + 1, portraitArr[1]); // ����
        portraitData.Add(1000 + 2, portraitArr[2]); // ����
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex >= talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

}
