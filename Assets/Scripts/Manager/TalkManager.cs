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
        // 뒤에 숫자는 초상화 번호
        talkData.Add(1000, new string[] { "안녕.:0", "안녕2.:0" });

        portraitData.Add(1000 + 0, portraitArr[0]); // 디폴트
        portraitData.Add(1000 + 1, portraitArr[1]); // 반함
        portraitData.Add(1000 + 2, portraitArr[2]); // 정색
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
