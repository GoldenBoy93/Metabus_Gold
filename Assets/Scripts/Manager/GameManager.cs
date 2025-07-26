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
        // ��ũ�׼��� false
        if (isTalkAction)
        {
            isTalkAction = false;
            talkPanel.SetActive(false);
        }
        // ��ũ�׼��� true
        else
        {
            isTalkAction = true;
            talkPanel.SetActive(true);
            scanObject = scanObj;
            talkText.text = $"�̰��� �̸��� {scanObject.name}�̶�� �Ѵ�";
        }
        // ��ũ�׼��� true��� ��ȭâ�� �Ҵ�
        talkPanel.SetActive(isTalkAction);
    }
}