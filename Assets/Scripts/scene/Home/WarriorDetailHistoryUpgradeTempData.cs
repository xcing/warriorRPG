using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WarriorDetailHistoryUpgradeTempData : MonoBehaviour
{
    public WarriorDetail warriorDetail;
    public Text lvPlus;
    public Text hpPlusValue;
    public Text atkPlusValue;
    public Text defPlusValue;
    public Text spdPlusValue;
    public Text luckPlusValue;
    public Image skillPlusIcon;
    public Text skillPlusText;

    void Start()
    {

    }

    void Update()
    {

    }

    public void setData(int lvPlus)
    {
        this.lvPlus.text = "+" + lvPlus.ToString();
        int multiple;
        if (lvPlus % 10 == 9 || lvPlus == 100)
            multiple = 2;
        else
            multiple = 1;
        this.hpPlusValue.text = "+" + Convert.ToInt32(warriorDetail.warrior.defaultStatWhenPlusLvUpArray[0]) * multiple;
        this.atkPlusValue.text = "+" + Convert.ToInt32(warriorDetail.warrior.defaultStatWhenPlusLvUpArray[1]) * multiple;
        this.defPlusValue.text = "+" + Convert.ToInt32(warriorDetail.warrior.defaultStatWhenPlusLvUpArray[2]) * multiple;
        this.spdPlusValue.text = "+" + Convert.ToInt32(warriorDetail.warrior.defaultStatWhenPlusLvUpArray[3]) * multiple;
        this.luckPlusValue.text = "+" + Convert.ToInt32(warriorDetail.warrior.defaultStatWhenPlusLvUpArray[4]) * multiple;

        if (lvPlus % 10 == 9 || (warriorDetail.warrior.lvPlus + 1) == 100)
        {
            skillPlusIcon.sprite = Resources.Load<Sprite>("UI/statupIcon");
            skillPlusText.text = warriorDetail.homeScript.readData.jsonWordingData["increseAllStatus2x"][warriorDetail.homeScript.readData.languageId].ToString();
        }
        else if (lvPlus % 10 == 0)
        {
            skillPlusIcon.sprite = Resources.Load<Sprite>("Warriors/" + warriorDetail.warrior.name[0] + "/skill1");
            skillPlusText.text = warriorDetail.homeScript.readData.jsonWordingData["increseUseChance"][warriorDetail.homeScript.readData.languageId].ToString() + " " + warriorDetail.warrior.skill[0].name[warriorDetail.homeScript.readData.languageId];
        }
        else if (lvPlus % 10 == 3)
        {
            skillPlusIcon.sprite = Resources.Load<Sprite>("Warriors/" + warriorDetail.warrior.name[0] + "/skill2");
            if (lvPlus < 10)
            {
                skillPlusText.text = warriorDetail.homeScript.readData.jsonWordingData["learn"][warriorDetail.homeScript.readData.languageId].ToString() + " " + warriorDetail.warrior.skill[1].name[warriorDetail.homeScript.readData.languageId];
            }
            else
            {
                skillPlusText.text = warriorDetail.homeScript.readData.jsonWordingData["increseUseChance"][warriorDetail.homeScript.readData.languageId].ToString() + " " + warriorDetail.warrior.skill[1].name[warriorDetail.homeScript.readData.languageId];
            }
        }
        else if (lvPlus % 10 == 6)
        {
            skillPlusIcon.sprite = Resources.Load<Sprite>("Warriors/" + warriorDetail.warrior.name[0] + "/skill3");
            if (lvPlus < 10)
            {
                skillPlusText.text = warriorDetail.homeScript.readData.jsonWordingData["learn"][warriorDetail.homeScript.readData.languageId].ToString() + " " + warriorDetail.warrior.skill[2].name[warriorDetail.homeScript.readData.languageId];
            }
            else
            {
                skillPlusText.text = warriorDetail.homeScript.readData.jsonWordingData["increseUseChance"][warriorDetail.homeScript.readData.languageId].ToString() + " " + warriorDetail.warrior.skill[2].name[warriorDetail.homeScript.readData.languageId];
            }
        }
        else
        {
            skillPlusIcon.sprite = Resources.Load<Sprite>("UI/empty");
            skillPlusText.text = warriorDetail.homeScript.readData.jsonWordingData["none"][warriorDetail.homeScript.readData.languageId].ToString();
        }
    }
}
