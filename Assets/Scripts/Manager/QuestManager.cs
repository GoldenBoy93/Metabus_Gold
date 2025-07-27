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
        questList.Add(10, new QuestData("상황 보러가기", new int[] { 100, 1000 }));
        questList.Add(20, new QuestData("괴한 물리치기", new int[] { 1000 }));
        questList.Add(30, new QuestData("퀘스트 클리어", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // 말을 건 NPC의 ID == 퀘스트 리스트의 현재 퀘스트 ID . 현재 '퀘스트액션인덱스'번째의 NPC ID랑 일치하는지?
        // 즉, 말을 건 NPC가 현재 진행중인 퀘스트의 순서에 맞는 NPC인지 확인
        // 맞으면 인덱스가 1올라서 다음 NPC 순서가 됨
        if (id == questList[questId].npcId[questActionIndex])
        questActionIndex++;

        // 현재 '퀘스트액션인덱스' == 퀘스트 리스트의 현재 퀘스트 ID . npcID 전체 열의 갯수와 일치하는지?
        // 즉, 모든 관련 NPC와 순서대로 대화를 끝냈는지?
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
