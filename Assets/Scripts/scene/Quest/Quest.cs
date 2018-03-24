using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{

    public ReadData readData;
    public GameObject alertMessageBg;
    public float countdown;

    public Text header;
    public CanvasGroup bgFadeCanvasGroup;

    public Image borderPickQuestType;
    public CanvasGroup dailyQuestCanvasGroup;
    public CanvasGroup storyQuestCanvasGroup;
    public CanvasGroup warriorQuestCanvasGroup;

    // upgrade bag popup
    public CanvasGroup upgradeBagCanvasGroup;
    public Text upgradeBagHeader;
    public Text upgradeBagText;

    // dailyquest tab
    public Image questGuage;
    public int currentQuestPoint;
    public Image[] dailyQuestUnitBox;
    public int[] currentDailyQuestDone;

    // storyquest tab
    public int storyQuestHave;
    int heightStoryQuestPanelBox;
    public Image storyQuestScrollRect;
    public Image storyQuestBox;
    public GameObject storyQuestUnitBoxPrefab;
    public GameObject[] storyQuestUnitBox;
    public List<QuestData> quests;

    // warriorquest tab 1
    public int warriorQuestHave;
    int heightWarriorQuestPanelBox;
    public Image warriorQuestScrollRect;
    public Image warriorQuestBox;
    public GameObject warriorQuestUnitBoxPrefab;
    public GameObject[] warriorQuestUnitBox;
    public List<QuestData> warriorQuests;
    public List<int> warriorsId;
    public int currentWarriorQuestId;


    // warriorquest tab 2 (quest list)
    public CanvasGroup warriorQuestTopicCanvasGroup;
    public Image warriorTopicIcon;
    public Image[] warriorTopicStar;
    public Image warriorTopicWeaponIcon;
    public Button warriorTopicGoButton;
    public int warriorQuestListHave;
    int heightWarriorQuestListPanelBox;
    public Image warriorQuestListScrollRect;
    public Image warriorQuestListBox;
    public GameObject warriorQuestListUnitBoxPrefab;
    public GameObject[] warriorQuestListUnitBox;
    public List<QuestData> WarriorQuestLists;


    void Start()
    {
        header.text = readData.jsonWordingData["quest"][readData.languageId].ToString();

        currentDailyQuestDone = new int[10];
        currentDailyQuestDone[0] = 5;
        currentDailyQuestDone[1] = 1;
        currentDailyQuestDone[2] = 1;
        currentDailyQuestDone[3] = 2;
        currentDailyQuestDone[4] = 0;
        currentDailyQuestDone[5] = 1;
        currentDailyQuestDone[6] = 0;
        currentDailyQuestDone[7] = 1;
        currentDailyQuestDone[8] = 0;
        currentDailyQuestDone[9] = 1;

        currentQuestPoint = currentDailyQuestDone[0] * 3 + currentDailyQuestDone[1] * 20 + currentDailyQuestDone[2] * 5 + currentDailyQuestDone[3] * 5 + currentDailyQuestDone[4] * 10 + currentDailyQuestDone[5] * 10 + currentDailyQuestDone[6] * 10 + currentDailyQuestDone[7] * 5 + currentDailyQuestDone[8] * 10 + currentDailyQuestDone[9] * 10;
        questGuage.fillAmount = (float)currentQuestPoint / 100.0f;

        SetDataQuestUnitBox(0, readData.jsonWordingData["dailyquest1"][readData.languageId].ToString(), currentDailyQuestDone[0], 10, "+3/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(1, readData.jsonWordingData["dailyquest2"][readData.languageId].ToString(), currentDailyQuestDone[1], 1, "+20/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(2, readData.jsonWordingData["dailyquest3"][readData.languageId].ToString(), currentDailyQuestDone[2], 2, "+5/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(3, readData.jsonWordingData["dailyquest4"][readData.languageId].ToString(), currentDailyQuestDone[3], 2, "+5/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(4, readData.jsonWordingData["dailyquest5"][readData.languageId].ToString(), currentDailyQuestDone[4], 1, "+10/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(5, readData.jsonWordingData["dailyquest6"][readData.languageId].ToString(), currentDailyQuestDone[5], 1, "+10/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(6, readData.jsonWordingData["dailyquest7"][readData.languageId].ToString(), currentDailyQuestDone[6], 1, "+10/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(7, readData.jsonWordingData["dailyquest8"][readData.languageId].ToString(), currentDailyQuestDone[7], 2, "+5/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(8, readData.jsonWordingData["dailyquest9"][readData.languageId].ToString(), currentDailyQuestDone[8], 1, "+10/" + readData.jsonWordingData["time"][readData.languageId].ToString());
        SetDataQuestUnitBox(9, readData.jsonWordingData["dailyquest10"][readData.languageId].ToString(), currentDailyQuestDone[9], 1, "+10/" + readData.jsonWordingData["time"][readData.languageId].ToString());

        SetDataStoryQuestUnitBox();
        SetDataWarriorQuestUnitBox();
    }

    public void SetDataWarriorQuestUnitBox()
    {
        for (int i = warriorQuestBox.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(warriorQuestBox.transform.GetChild(i).gameObject);
        }
        int warriorIdTemp;
        foreach (Warrior warrior in readData.dataUserModel.warrior)
        {
            warriorIdTemp = 0;
            warriorIdTemp = warriorsId.Find(x => x == warrior.warriorId);
            if (warriorIdTemp == 0)
                warriorsId.Add(warrior.warriorId);
        }

        warriorQuestHave = warriorsId.Count;

        warriorQuestUnitBox = new GameObject[warriorQuestHave];
        warriorQuestUnitBoxPrefab = Resources.Load<GameObject>("Prefab/WarriorQuestUnitBox");
        if (warriorQuestHave > 5)
            heightWarriorQuestPanelBox = (warriorQuestHave * 160) + 20;
        else
            heightWarriorQuestPanelBox = Convert.ToInt32(warriorQuestScrollRect.rectTransform.rect.height);

        if (heightWarriorQuestPanelBox < warriorQuestScrollRect.rectTransform.rect.height)
        {
            warriorQuestBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            warriorQuestBox.rectTransform.anchoredPosition = new Vector3(0, (heightWarriorQuestPanelBox - warriorQuestScrollRect.rectTransform.rect.height) / -2);
        }
        warriorQuestBox.rectTransform.sizeDelta = new Vector2(warriorQuestBox.rectTransform.rect.width, heightWarriorQuestPanelBox);

        for (int i = 0; i < warriorQuestHave; i++)
        {
            warriorQuestUnitBox[i] = Instantiate(warriorQuestUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            warriorQuestUnitBox[i].transform.SetParent(warriorQuestBox.transform, true);
            warriorQuestUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            warriorQuestUnitBox[i].transform.position = warriorQuestBox.transform.position;
            warriorQuestUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(0, (heightWarriorQuestPanelBox / 2) - 105 - (i * 160));

            WarriorQuestTempData tempData = warriorQuestUnitBox[i].GetComponent<WarriorQuestTempData>();
            tempData.questScript = this;
            tempData.tempI = i;
            tempData.warriorId = warriorsId[i];
            tempData.SetData();
            if (readData.dataUserModel.warriorquest.Find(x => x.warriorId == warriorsId[i]).status < 10)
            {
                warriorQuestUnitBox[i].GetComponentsInChildren<Button>()[1].onClick.AddListener(() => tempData.GoButton());
            }

        }
    }

    public void SetDataWarriorQuestTopic()
    {
        warriorTopicIcon.sprite = Resources.Load<Sprite>("Warriors/" + readData.jsonWarriorData[currentWarriorQuestId.ToString()]["name"][0] + "/icon");

        for (int i = 0; i < 10; i++)
        {
            warriorTopicStar[i].sprite = Resources.Load<Sprite>("UI/starEmpty");
        }

        for (int i = 1; i <= readData.dataUserModel.warriorquest.Find(x => x.warriorId == currentWarriorQuestId).status; i++)
        {
            warriorTopicStar[i - 1].sprite = Resources.Load<Sprite>("UI/star");
        }
        warriorTopicWeaponIcon.sprite = Resources.Load<Sprite>("equipments/" + readData.jsonEquipmentData[readData.dataUserModel.warriorquest.Find(x => x.warriorId == currentWarriorQuestId).superEquipmentId.ToString()]["name"][0]);
        warriorTopicGoButton.GetComponentInChildren<Text>().text = readData.jsonWordingData["back"][readData.languageId].ToString();
        
        SetDataWarriorListQuestUnitBox();
        OpenWarriorQuestList();
    }

    public void SetDataWarriorListQuestUnitBox()
    {
        for (int i = warriorQuestListBox.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(warriorQuestListBox.transform.GetChild(i).gameObject);
        }

        warriorQuestListHave = 10;

        warriorQuestListUnitBox = new GameObject[warriorQuestListHave];
        warriorQuestListUnitBoxPrefab = Resources.Load<GameObject>("Prefab/WarriorQuestListUnitBox");
        if (warriorQuestListHave > 5)
            heightWarriorQuestListPanelBox = (warriorQuestListHave * 160) + 20;
        else
            heightWarriorQuestListPanelBox = Convert.ToInt32(warriorQuestListScrollRect.rectTransform.rect.height);

        if (heightWarriorQuestListPanelBox < warriorQuestListScrollRect.rectTransform.rect.height)
        {
            warriorQuestListBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            warriorQuestListBox.rectTransform.anchoredPosition = new Vector3(0, (heightWarriorQuestListPanelBox - warriorQuestListScrollRect.rectTransform.rect.height) / -2);
        }
        warriorQuestListBox.rectTransform.sizeDelta = new Vector2(warriorQuestListBox.rectTransform.rect.width, heightWarriorQuestListPanelBox);

        for (int i = 0; i < warriorQuestListHave; i++)
        {
            warriorQuestListUnitBox[i] = Instantiate(warriorQuestListUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            warriorQuestListUnitBox[i].transform.SetParent(warriorQuestListBox.transform, true);
            warriorQuestListUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            warriorQuestListUnitBox[i].transform.position = warriorQuestListBox.transform.position;
            warriorQuestListUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(0, (heightWarriorQuestListPanelBox / 2) - 105 - (i * 160));

            WarriorQuestListTempData tempData = warriorQuestListUnitBox[i].GetComponent<WarriorQuestListTempData>();
            tempData.questScript = this;
            tempData.tempI = i;
            //tempData.questId = quests[i].questId;
            tempData.SetData();


            if (readData.dataUserModel.warriorquest.Find(x => x.warriorId == currentWarriorQuestId).status <= i)
            {
                warriorQuestListUnitBox[i].GetComponent<Image>().color = new Color32(0, 0, 0, 80);
                warriorQuestListUnitBox[i].GetComponentsInChildren<Button>()[1].onClick.AddListener(() => tempData.GoButton());
            }
            else
            {
                warriorQuestListUnitBox[i].GetComponent<Image>().color = new Color32(73, 178, 57, 255);
            }

            /*if (quests[i].status == 1)
            {
                warriorQuestListUnitBox[i].GetComponent<Image>().color = new Color32(73, 178, 57, 255);
            }
            else
            {
                warriorQuestListUnitBox[i].GetComponent<Image>().color = new Color32(0, 0, 0, 80);
                warriorQuestListUnitBox[i].GetComponentsInChildren<Button>()[1].onClick.AddListener(() => tempData.GoButton());
            }*/
        }

        OpenWarriorQuestList();
    }

    public void SetDataStoryQuestUnitBox()
    {
        for (int i = storyQuestBox.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(storyQuestBox.transform.GetChild(i).gameObject);
        }

        int stageId;
        int areaId;
        int level;
        int ordinal;

        foreach (QuestData quest in readData.dataUserModel.quest)
        {
            if (quest.status == 1)
            {
                //Debug.Log("add 1");
                quests.Add(quest);
            }
            else if (quest.status == 0)
            {
                stageId = Convert.ToInt32(readData.jsonQuestData[quest.questId.ToString()]["stageId"].ToString());
                areaId = Convert.ToInt32(readData.jsonStageNormalData[stageId.ToString()]["areaId"].ToString());
                level = Convert.ToInt32(readData.jsonStageNormalData[stageId.ToString()]["level"].ToString());
                ordinal = Convert.ToInt32(readData.jsonStageNormalData[stageId.ToString()]["ordinal"].ToString());
                if (ordinal > 1)
                {
                    if (readData.dataUserModel.stage[areaId - 1][level - 1][ordinal - 2] == 1)
                    {
                        //Debug.Log(stageId + " " + areaId + " " + level + " " + ordinal + " add 2");
                        quests.Add(quest);
                    }
                }
                else if (ordinal == 1 && level == 1 && areaId == 1)
                {
                    //Debug.Log(stageId + " " + areaId + " " + level + " " + ordinal + " add 3");
                    quests.Add(quest);
                }
                else if (ordinal == 1 && level == 1)
                {
                    if (readData.dataUserModel.stage[areaId - 2][level - 1][5] == 1)
                    {
                        //Debug.Log(stageId + " " + areaId + " " + level + " " + ordinal + " add 4");
                        quests.Add(quest);
                    }
                }
                else if (ordinal == 1)
                {
                    if (readData.dataUserModel.stage[areaId - 1][level - 2][5] == 1)
                    {
                        //Debug.Log(stageId + " " + areaId + " " + level + " " + ordinal + " add 5");
                        quests.Add(quest);
                    }
                }
            }
        }
        //Debug.Log("total quest" + quests.Count);

        storyQuestHave = quests.Count;

        storyQuestUnitBox = new GameObject[storyQuestHave];
        storyQuestUnitBoxPrefab = Resources.Load<GameObject>("Prefab/StoryQuestUnitBox");
        if (storyQuestHave > 5)
            heightStoryQuestPanelBox = (storyQuestHave * 160) + 20;
        else
            heightStoryQuestPanelBox = Convert.ToInt32(storyQuestScrollRect.rectTransform.rect.height);

        if (heightStoryQuestPanelBox < storyQuestScrollRect.rectTransform.rect.height)
        {
            storyQuestBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            storyQuestBox.rectTransform.anchoredPosition = new Vector3(0, (heightStoryQuestPanelBox - storyQuestScrollRect.rectTransform.rect.height) / -2);
        }
        storyQuestBox.rectTransform.sizeDelta = new Vector2(storyQuestBox.rectTransform.rect.width, heightStoryQuestPanelBox);

        for (int i = 0; i < storyQuestHave; i++)
        {
            storyQuestUnitBox[i] = Instantiate(storyQuestUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            storyQuestUnitBox[i].transform.SetParent(storyQuestBox.transform, true);
            storyQuestUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            storyQuestUnitBox[i].transform.position = storyQuestBox.transform.position;
            storyQuestUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(0, (heightStoryQuestPanelBox / 2) - 105 - (i * 160));

            StoryQuestTempData tempData = storyQuestUnitBox[i].GetComponent<StoryQuestTempData>();
            tempData.questScript = this;
            tempData.tempI = i;
            tempData.questId = quests[i].questId;
            tempData.SetData();

            if (quests[i].status == 1)
            {
                storyQuestUnitBox[i].GetComponent<Image>().color = new Color32(73, 178, 57, 255);
            }
            else
            {
                storyQuestUnitBox[i].GetComponent<Image>().color = new Color32(0, 0, 0, 80);
                storyQuestUnitBox[i].GetComponentsInChildren<Button>()[1].onClick.AddListener(() => tempData.GoButton());
            }
        }
    }

    public void SetDataQuestUnitBox(int i, string questDetail, int questAmountDone, int questAmount, string questPoint)
    {
        dailyQuestUnitBox[i].GetComponentsInChildren<Text>()[0].text = questDetail;
        dailyQuestUnitBox[i].GetComponentsInChildren<Text>()[1].text = questAmountDone.ToString() + "/" + questAmount.ToString();
        dailyQuestUnitBox[i].GetComponentsInChildren<Text>()[2].text = questPoint;

        if (questAmountDone >= questAmount)
        {
            dailyQuestUnitBox[i].GetComponentsInChildren<Text>()[3].text = readData.jsonWordingData["complete"][readData.languageId].ToString();
            dailyQuestUnitBox[i].GetComponent<Image>().color = new Color32(73, 178, 57, 255);
            dailyQuestUnitBox[i].GetComponentInChildren<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        }
        else
        {
            dailyQuestUnitBox[i].GetComponentsInChildren<Text>()[3].text = readData.jsonWordingData["go"][readData.languageId].ToString();
            dailyQuestUnitBox[i].GetComponent<Image>().color = new Color32(0, 0, 0, 80);
        }
    }

    public void OpenWarriorQuestList()
    {
        warriorQuestScrollRect.GetComponent<CanvasGroup>().alpha = 0;
        warriorQuestScrollRect.GetComponent<CanvasGroup>().blocksRaycasts = false;
        warriorQuestTopicCanvasGroup.alpha = 1;
        warriorQuestTopicCanvasGroup.blocksRaycasts = true;
        warriorQuestListScrollRect.GetComponent<CanvasGroup>().alpha = 1;
        warriorQuestListScrollRect.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void CloseWarriorQuestList()
    {
        warriorQuestScrollRect.GetComponent<CanvasGroup>().alpha = 1;
        warriorQuestScrollRect.GetComponent<CanvasGroup>().blocksRaycasts = true;
        warriorQuestTopicCanvasGroup.alpha = 0;
        warriorQuestTopicCanvasGroup.blocksRaycasts = false;
        warriorQuestListScrollRect.GetComponent<CanvasGroup>().alpha = 0;
        warriorQuestListScrollRect.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void ChestRewardButton(int position)
    {
        switch (position)
        {
            case 1:
                if (currentQuestPoint >= 25)
                    OpenAlertMessage("get money", null);
                else
                    OpenAlertMessage(readData.jsonWordingData["notEnoughQuestPoint"][readData.languageId].ToString(), null);
                break;
            case 2:
                if (currentQuestPoint >= 50)
                    OpenAlertMessage("get ore", null);
                else
                    OpenAlertMessage(readData.jsonWordingData["notEnoughQuestPoint"][readData.languageId].ToString(), null);
                break;
            case 3:
                if (currentQuestPoint >= 75)
                    OpenAlertMessage("get warrior soul", null);
                else
                    OpenAlertMessage(readData.jsonWordingData["notEnoughQuestPoint"][readData.languageId].ToString(), null);
                break;
            case 4:
                if (currentQuestPoint >= 100)
                    OpenAlertMessage("get diamond", null);
                else
                    OpenAlertMessage(readData.jsonWordingData["notEnoughQuestPoint"][readData.languageId].ToString(), null);
                break;
        }
    }

    public void GoButton(int position)
    {
        OpenAlertMessage(position.ToString(), null);
    }

    public void QuestTypeButton(int position)
    {
        RectTransform currentPickSkillRectTransform;

        switch (position)
        {
            case 1:
                dailyQuestCanvasGroup.alpha = 1;
                dailyQuestCanvasGroup.blocksRaycasts = true;
                storyQuestCanvasGroup.alpha = 0;
                storyQuestCanvasGroup.blocksRaycasts = false;
                warriorQuestCanvasGroup.alpha = 0;
                warriorQuestCanvasGroup.blocksRaycasts = false;
                currentPickSkillRectTransform = GameObject.Find("DailyQuestButton").GetComponent<RectTransform>();
                break;
            case 2:
                dailyQuestCanvasGroup.alpha = 0;
                dailyQuestCanvasGroup.blocksRaycasts = false;
                storyQuestCanvasGroup.alpha = 1;
                storyQuestCanvasGroup.blocksRaycasts = true;
                warriorQuestCanvasGroup.alpha = 0;
                warriorQuestCanvasGroup.blocksRaycasts = false;
                currentPickSkillRectTransform = GameObject.Find("StoryQuestButton").GetComponent<RectTransform>();
                break;
            case 3:
                dailyQuestCanvasGroup.alpha = 0;
                dailyQuestCanvasGroup.blocksRaycasts = false;
                storyQuestCanvasGroup.alpha = 0;
                storyQuestCanvasGroup.blocksRaycasts = false;
                warriorQuestCanvasGroup.alpha = 1;
                warriorQuestCanvasGroup.blocksRaycasts = true;
                currentPickSkillRectTransform = GameObject.Find("WarriorQuestButton").GetComponent<RectTransform>();
                break;
            default:
                dailyQuestCanvasGroup.alpha = 1;
                dailyQuestCanvasGroup.blocksRaycasts = true;
                storyQuestCanvasGroup.alpha = 0;
                storyQuestCanvasGroup.blocksRaycasts = false;
                warriorQuestCanvasGroup.alpha = 0;
                warriorQuestCanvasGroup.blocksRaycasts = false;
                currentPickSkillRectTransform = GameObject.Find("DailyQuestButton").GetComponent<RectTransform>();
                break;
        }
        borderPickQuestType.rectTransform.anchoredPosition = new Vector2(currentPickSkillRectTransform.anchoredPosition.x, currentPickSkillRectTransform.anchoredPosition.y);
    }

    public void OpenPopupUpgradeBag()
    {
        bgFadeCanvasGroup.alpha = 1;
        bgFadeCanvasGroup.blocksRaycasts = true;
        upgradeBagCanvasGroup.alpha = 1;
        upgradeBagCanvasGroup.blocksRaycasts = true;
    }

    public void CloseUpgradeBag()
    {
        bgFadeCanvasGroup.alpha = 0;
        bgFadeCanvasGroup.blocksRaycasts = false;
        upgradeBagCanvasGroup.alpha = 0;
        upgradeBagCanvasGroup.blocksRaycasts = false;
    }

    public void UpgradeBag()
    {
        if (readData.dataUserModel.diamond < 5)
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughDiamond"][readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(readData.UpgradeWarriorBagFromOpenChestPage());
        }
    }

    public void OpenAlertMessage(string message, string image)
    {
        alertMessageBg.transform.position = new Vector3(0, 46, 0);
        alertMessageBg.GetComponent<CanvasGroup>().alpha = 1;
        alertMessageBg.GetComponent<CanvasGroup>().blocksRaycasts = true;
        alertMessageBg.GetComponentInChildren<Text>().text = message;
        if (image != null)
        {
            alertMessageBg.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(image);
            alertMessageBg.GetComponentsInChildren<Image>()[1].GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            alertMessageBg.GetComponentsInChildren<Image>()[1].sprite = null;
            alertMessageBg.GetComponentsInChildren<Image>()[1].GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void Update()
    {
        if (alertMessageBg.GetComponent<CanvasGroup>().alpha == 1 && alertMessageBg.transform.position.y <= 100)
        {
            countdown = 2;
            alertMessageBg.transform.Translate(new Vector3(0, 3, 0));
        }
        else if (alertMessageBg.GetComponent<CanvasGroup>().alpha == 1)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0.0f)
            {
                alertMessageBg.GetComponent<CanvasGroup>().alpha = 0;
                alertMessageBg.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void BackToHome()
    {
        Application.LoadLevel("Home");
    }
}
