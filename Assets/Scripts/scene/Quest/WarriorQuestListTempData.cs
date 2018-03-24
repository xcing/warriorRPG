using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WarriorQuestListTempData : MonoBehaviour
{
    public Quest questScript;
    public int tempI;
    public Text questDetail;
    public Image questReward;
    public Text rewardAmount;
    public Button goButton;
    public CanvasGroup bgFadeCanvasGroup;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData()
    {
        questDetail.text = "test" + tempI;
        questReward.sprite = Resources.Load<Sprite>("Warriors/" + questScript.readData.jsonWarriorData[questScript.currentWarriorQuestId.ToString()]["name"][0] + "/icon");
        if(tempI > 1){
            rewardAmount.text = "x"+tempI.ToString();
        }
        if (questScript.readData.dataUserModel.warriorquest.Find(x => x.warriorId == questScript.currentWarriorQuestId).status <= tempI)
        {
            goButton.GetComponentInChildren<Text>().text = questScript.readData.jsonWordingData["go"][questScript.readData.languageId].ToString();
            if(questScript.readData.dataUserModel.warriorquest.Find(x => x.warriorId == questScript.currentWarriorQuestId).status == tempI){
                bgFadeCanvasGroup.alpha = 0;
                bgFadeCanvasGroup.blocksRaycasts = false;
            }
            else
            {
                bgFadeCanvasGroup.alpha = 1;
                bgFadeCanvasGroup.blocksRaycasts = true;
            }
        }
        else
        {
            goButton.GetComponentInChildren<Text>().text = questScript.readData.jsonWordingData["complete"][questScript.readData.languageId].ToString();
        }
    }

    public void GoButton()
    {
        questScript.OpenAlertMessage(tempI.ToString(), null);
    }
}
