using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WarriorQuestTempData : MonoBehaviour
{
    public Quest questScript;
    public int tempI;
    public int warriorId;
    public Image warriorIcon;
    public Image[] star;
    public Image weaponIcon;
    public Button goButton;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData()
    {
        warriorIcon.sprite = Resources.Load<Sprite>("Warriors/" + questScript.readData.jsonWarriorData[warriorId.ToString()]["name"][0] + "/icon");
        for (int i = 1; i <= questScript.readData.dataUserModel.warriorquest.Find(x => x.warriorId == warriorId).status; i++)
        {
            star[i-1].sprite = Resources.Load<Sprite>("UI/star");
        }
        weaponIcon.sprite = Resources.Load<Sprite>("equipments/" + questScript.readData.jsonEquipmentData[questScript.readData.dataUserModel.warriorquest.Find(x => x.warriorId == warriorId).superEquipmentId.ToString()]["name"][0]);
        if (questScript.readData.dataUserModel.warriorquest.Find(x => x.warriorId == warriorId).status == 10)
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
        questScript.currentWarriorQuestId = warriorId;
        questScript.SetDataWarriorQuestTopic();
        //questScript.OpenAlertMessage(warriorId.ToString(), null);
    }
}
