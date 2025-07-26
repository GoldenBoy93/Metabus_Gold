using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ���� ��ü�� �����ϴ� ���� �Ŵ��� Ŭ����
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player { get; private set; } // �÷��̾� ��Ʈ�ѷ� (�б� ���� ������Ƽ)
    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0; // ���� ���̺� ��ȣ

    private EnemyManager enemyManager; // �� ���� �� �����ϴ� �Ŵ���


    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isTalkAction;
    public TalkManager talkManager;
    public QuestManager questManager;
    public int talkIndex;
    public Image portraitImg;

    private void Awake()
    {
        // �̱��� �Ҵ�
        instance = this;

        // �÷��̾� ã�� �ʱ�ȭ
        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        // �� �Ŵ��� �ʱ�ȭ
        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
    }

    public void StartGame()
    {
        StartNextWave(); // ù ���̺� ����
    }

    void StartNextWave()
    {
        currentWaveIndex += 1; // ���̺� �ε��� ����
        // 5���̺긶�� ���̵� ���� (��: 1~4 �� ���� 1, 5~9 �� ���� 2 ...)
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    // ���̺� ���� �� ���� ���̺� ����
    public void EndOfWave()
    {
        StartNextWave();
    }

    // �÷��̾ �׾��� �� ���� ���� ó��
    public void GameOver()
    {
        enemyManager.StopWave(); // �� ���� ����
    }

    // ���߿� �׽�Ʈ: Space Ű�� ���� ����
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void TalkAction(GameObject scanObj)
    {
        scanObject = scanObj;
        //talkText.text = $"�̰��� �̸��� {scanObject.name}�̶�� �Ѵ�";
        ObjectData objectData = scanObj.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNPC);
        
        // ��ũ�׼��� true��� ��ȭâ�� �Ҵ�
        talkPanel.SetActive(isTalkAction);
    }

    void Talk(int id, bool isNPC)
    {
        // Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        
        // Talk End
        if (talkData == null)
        {
            isTalkAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            
            // �Լ�����
            return;
        }

        // Continue Talk
        if(isNPC)
        {
            // ':'�� �������� ���� �ʻ�ȭ ��ȣ �и�
            string[] splitData = talkData.Split(':');

            // ���� ��ȭ�� �ؽ�Ʈ UI�� �Ҵ�
            talkText.text = splitData[0];

            // �ʻ�ȭ ��ȣ�� �����ͼ� Sprite �Ҵ�
            int portraitIndex = int.Parse(splitData[1]);
            portraitImg.sprite = talkManager.GetPortrait(id, portraitIndex);

            // �ʻ�ȭ�� ���̵��� ���� ����
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            // �ʻ�ȭ�� ���� not NPC �ϰ��� �̹����� a���� 0 (����)
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isTalkAction = true;
        talkIndex++;
    }
}