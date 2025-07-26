using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 게임 전체를 관리하는 메인 매니저 클래스
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player { get; private set; } // 플레이어 컨트롤러 (읽기 전용 프로퍼티)
    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0; // 현재 웨이브 번호

    private EnemyManager enemyManager; // 적 생성 및 관리하는 매니저


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
        // 싱글톤 할당
        instance = this;

        // 플레이어 찾고 초기화
        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        // 적 매니저 초기화
        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
    }

    public void StartGame()
    {
        StartNextWave(); // 첫 웨이브 시작
    }

    void StartNextWave()
    {
        currentWaveIndex += 1; // 웨이브 인덱스 증가
        // 5웨이브마다 난이도 증가 (예: 1~4 → 레벨 1, 5~9 → 레벨 2 ...)
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    // 웨이브 종료 후 다음 웨이브 시작
    public void EndOfWave()
    {
        StartNextWave();
    }

    // 플레이어가 죽었을 때 게임 오버 처리
    public void GameOver()
    {
        enemyManager.StopWave(); // 적 스폰 중지
    }

    // 개발용 테스트: Space 키로 게임 시작
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
        //talkText.text = $"이것의 이름은 {scanObject.name}이라고 한다";
        ObjectData objectData = scanObj.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNPC);
        
        // 토크액션이 true라면 대화창을 켠다
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
            
            // 함수종료
            return;
        }

        // Continue Talk
        if(isNPC)
        {
            // ':'를 기준으로 대사와 초상화 번호 분리
            string[] splitData = talkData.Split(':');

            // 순수 대화만 텍스트 UI에 할당
            talkText.text = splitData[0];

            // 초상화 번호를 가져와서 Sprite 할당
            int portraitIndex = int.Parse(splitData[1]);
            portraitImg.sprite = talkManager.GetPortrait(id, portraitIndex);

            // 초상화가 보이도록 색상 설정
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            // 초상화가 없는 not NPC 일경우는 이미지의 a값을 0 (투명)
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isTalkAction = true;
        talkIndex++;
    }
}