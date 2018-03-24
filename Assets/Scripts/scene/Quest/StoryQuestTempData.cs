using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StoryQuestTempData : MonoBehaviour
{
    public Quest questScript;
    public int tempI;
    public int questId;
    public Text questDetail;
    public Image questReward;
    public Button goButton;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData()
    {
        questDetail.text = questScript.readData.jsonQuestData[questId.ToString()]["detail"][questScript.readData.languageId].ToString();
        questReward.sprite = Resources.Load<Sprite>("UI/moneyIcon");
        if (questScript.quests[tempI].status == 1)
        {
            goButton.GetComponentInChildren<Text>().text = questScript.readData.jsonWordingData["complete"][questScript.readData.languageId].ToString();
        }
        else
        {
            goButton.GetComponentInChildren<Text>().text = questScript.readData.jsonWordingData["go"][questScript.readData.languageId].ToString();
        }
    }

    public void GoButton()
    {
        questScript.OpenAlertMessage(questId.ToString(), null);
    }
}
