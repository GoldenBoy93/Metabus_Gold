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
        questList.Add(20, new QuestData("���� ����ġ��", new int[] { 100, 1000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
        questActionIndex++;
        
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // Control Quest Object
        ControlObject();
        
        // ���� ����Ʈ Ȯ��
        Debug.Log($"���� ����Ʈ ID : {questId}");
        Debug.Log($"���� ����Ʈ Action Index : {questActionIndex}");

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
