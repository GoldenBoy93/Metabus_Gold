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

        // Alice
        talkData.Add(1000, new string[] { "�ȳ�?:0", "�� ��������Ʈ�� ��ȭ�ʻ�ȭ�� �ٸ��İ�?:1", "�װ� ���� �ô���.:2" });

        // Eve
        talkData.Add(2000, new string[] { "�ȳ�!:3", "�ʸ� �Ƴİ�?:4", "�翬�� ����, ���� ó�� �ôµ�.:5" });
        
        // May
        talkData.Add(3000, new string[] { "���ڴ�.:6", "����� ���� �������ΰ�?:6", "�� ���� �����?:6" });

        // Alice Boyfriend
        talkData.Add(5000, new string[] { "�� �������� ���� ����!" });

        // Dog
        talkData.Add(100, new string[] { "����!!", "���� �ٸ��� �پ �������� �ϰ� �ִ�." });

        // ATM123
        talkData.Add(200, new string[] { "ATM�̴�. ������ �� ���� ����." });

        // Dog Quest Talk
        talkData.Add(10 + 100, new string[] { "�п�!!", "���� ������ ���� �糳�� ¢�� �ִ�.", "���� ������ Ȯ���غ���?" });

        talkData.Add(11 + 1000, new string[] { "��! �ű� ��!:2", "�� �� ������!:2", "��Ȳ ������ ����? ���� �� ��� �غ����!:2" });

        talkData.Add(20 + 1000, new string[] { "��! �ű� ��!:2", "�� �� ������!:2", "��Ȳ ������ ����? ���� �� ��� �غ����!:2" });

        portraitData.Add(1000 + 0, portraitArr[0]); // Alice����Ʈ
        portraitData.Add(1000 + 1, portraitArr[1]); // Alice����
        portraitData.Add(1000 + 2, portraitArr[2]); // Alice����

        portraitData.Add(2000 + 3, portraitArr[3]); // Eve����Ʈ
        portraitData.Add(2000 + 4, portraitArr[4]); // Eve���
        portraitData.Add(2000 + 5, portraitArr[5]); // Eve����

        portraitData.Add(3000 + 6, portraitArr[6]); // May����Ʈ
    }

    public string GetTalk(int id, int talkIndex)
    {
        // ����ó��
        if(!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex); // Get First Talk
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex); // Get First Quest Talk
            }
        }

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
