using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    GameManager gameManager;
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    
    void GenerateData()
    {
        questList.Add(10, new QuestData("��Ȳ ��������", new int[] { 100, 1000 }));
        questList.Add(20, new QuestData("���� ����ġ��", new int[] { 1000 }));
        questList.Add(30, new QuestData("����Ʈ Ŭ����", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // ���� �� NPC�� ID == ����Ʈ ����Ʈ�� ���� ����Ʈ ID . ���� '����Ʈ�׼��ε���'��°�� NPC ID�� ��ġ�ϴ���?
        // ��, ���� �� NPC�� ���� �������� ����Ʈ�� ������ �´� NPC���� Ȯ��
        // ������ �ε����� 1�ö� ���� NPC ������ ��
        if (id == questList[questId].npcId[questActionIndex])
        questActionIndex++;

        // ���� '����Ʈ�׼��ε���' == ����Ʈ ����Ʈ�� ���� ����Ʈ ID . npcID ��ü ���� ������ ��ġ�ϴ���?
        // ��, ��� ���� NPC�� ������� ��ȭ�� ���´���?
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // Control Quest Object
        ControlObject();

        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 1)
                {
                    questObject[0].SetActive(true);
                }
                    break;
            case 20:
                if (questActionIndex == 0)
                {
                    questObject[0].SetActive(false);
                    gameManager.StartBattle();
                }
                    break;

        }
    }
}
