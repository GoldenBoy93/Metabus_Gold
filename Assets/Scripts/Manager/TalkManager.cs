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

        // Alice
        talkData.Add(1000, new string[] { "안녕?:0", "왜 스프라이트랑 대화초상화랑 다르냐고?:1", "그게 뭔데 씹덕아.:2" });

        // Eve
        talkData.Add(2000, new string[] { "안녕!:3", "너를 아냐고?:4", "당연히 모르지, 지금 처음 봤는데.:5" });
        
        // May
        talkData.Add(3000, new string[] { "묻겠다.:6", "당신이 나의 마스터인가?:6", "제 연기 어땠나요?:6" });

        // Alice Boyfriend
        talkData.Add(5000, new string[] { "넌 참견하지 말고 꺼져!" });

        // Dog
        talkData.Add(100, new string[] { "헥헥!!", "개가 다리에 붙어서 마운팅을 하고 있다." });

        // ATM123
        talkData.Add(200, new string[] { "ATM이다. 지금은 쓸 일이 없다." });

        // Dog Quest Talk
        talkData.Add(10 + 100, new string[] { "왈왈!!", "개가 저쪽을 향해 사납게 짖고 있다.", "무슨 일인지 확인해볼까?" });

        talkData.Add(11 + 1000, new string[] { "야! 거기 너!:2", "나 좀 도와줘!:2", "상황 딱보면 몰라? 저거 좀 어떻게 해보라고!:2" });

        talkData.Add(20 + 1000, new string[] { "야! 거기 너!:2", "나 좀 도와줘!:2", "상황 딱보면 몰라? 저거 좀 어떻게 해보라고!:2" });

        portraitData.Add(1000 + 0, portraitArr[0]); // Alice디폴트
        portraitData.Add(1000 + 1, portraitArr[1]); // Alice반함
        portraitData.Add(1000 + 2, portraitArr[2]); // Alice정색

        portraitData.Add(2000 + 3, portraitArr[3]); // Eve디폴트
        portraitData.Add(2000 + 4, portraitArr[4]); // Eve놀람
        portraitData.Add(2000 + 5, portraitArr[5]); // Eve정색

        portraitData.Add(3000 + 6, portraitArr[6]); // May디폴트
    }

    public string GetTalk(int id, int talkIndex)
    {
        // 예외처리
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
