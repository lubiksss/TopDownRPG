using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArray;
    

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        //Talk Data
        talkData.Add(100, new string[] { "우리집이다." });
        talkData.Add(200, new string[] { "큰 나무다. 흔들어 볼까?" });
        talkData.Add(300, new string[] { "아무것도 들어있지 않다." });
        talkData.Add(400, new string[] { "너무 무거워서 밀 수 없다." });
        talkData.Add(500, new string[] { "누군가 사용한 흔적이 있는 책상이다." });
        talkData.Add(600, new string[] { "호수에 전설이 있다던데?" });
        talkData.Add(1000, new string[] { "안녕?:2", "이 곳에 처음 왔구나?:3" });
        talkData.Add(2000, new string[] { "이 호수에 전설이 있다던데..:4", "아냐 혼잣말이야..:7" });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "우리마을 오른쪽에 있는 호수에는 전설이 있대:3", "궁금하지 않아?:2" });
        talkData.Add(11 + 2000, new string[] { "호수의 전설이 궁금해서 온거야?:7", "그러기 전에 일좀 해줬으면 좋겠는데....:6", "우리집 근처에 동전좀 주워줘:5" });

        talkData.Add(20 + 1000, new string[] { "루도의 동전?:1", "돈을 흘리고 다니면 안되지!:3", "나중에 루도에게 한마디 해야겠어:3" });
        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다 줘.:7" });
        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });
        talkData.Add(21 + 2000, new string[] { "엇, 찾아줘서 고마워.!:6" });


        //Portrait Data
        portraitData.Add(1000 + 0, portraitArray[0]);
        portraitData.Add(1000 + 1, portraitArray[1]);
        portraitData.Add(1000 + 2, portraitArray[2]);
        portraitData.Add(1000 + 3, portraitArray[3]);
        portraitData.Add(2000 + 4, portraitArray[4]);
        portraitData.Add(2000 + 5, portraitArray[5]);
        portraitData.Add(2000 + 6, portraitArray[6]);
        portraitData.Add(2000 + 7, portraitArray[7]);
    }
    public string GetTalk(int id, int talkIndex)
    {
        //해당 퀘스트 진행 순서 대사가 없을 때.
        //퀘스트 맨처음 대사 가져온다.
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10)) { return GetTalk(id - id % 100, talkIndex); }
            else { return GetTalk(id - id % 10, talkIndex); }

        }
        if (talkIndex == talkData[id].Length)
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
