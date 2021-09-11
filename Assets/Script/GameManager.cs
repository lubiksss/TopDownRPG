using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject scanObject;
    public GameObject menuSet;
    public Animator talkPanel;
    public TypeEffect talk;
    public Image portraitImg;
    public Sprite prevPortrait;
    public Animator portraitAnim;
    public Text questText;
    public GameObject player;
    public int talkIndex;
    public bool isAction;

    void Start()
    {
        GameLoad();
        questText.text = questManager.checkQuest();
    }
    void Update()
    {
        //Sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            SubMenuActive();
        }
    }
    public void SubMenuActive()
    {
        if (menuSet.activeSelf) { menuSet.SetActive(false); }
        else { menuSet.SetActive(true); }
    }
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);
        // talkPanel.SetActive(isAction);
        talkPanel.SetBool("isShow", isAction);

    }
    void Talk(int id, bool isNPC)
    {
        //Set Talk Data
        int questTalkIndex = 0;
        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        //End Talk
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questText.text = questManager.checkQuest(id);
            return;
        }

        //Continue Tlak
        if (isNPC)
        {
            talk.SetMsg(talkData.Split(':')[0]);

            //Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
            if (prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QstId", questManager.questId);
        PlayerPrefs.SetInt("QstActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();
        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX")) { return; }
        else
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            int questId = PlayerPrefs.GetInt("QstId");
            int questActionIndex = PlayerPrefs.GetInt("QstActionIndex");

            player.transform.position = new Vector3(x, y, 0);
            questManager.questId = questId;
            questManager.questActionIndex = questActionIndex;
            questManager.ControlObject();
        }
    }
}
