using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EquipmentListTempData : MonoBehaviour
{
    public string equipmentUserId;
    public int tempI;
    public EquipmentList equipmentList;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PickEquipmentStockDetail()
    {
        equipmentList.currentEquipmentI = tempI;
        equipmentList.equipmentStatValue1.text = null;
        equipmentList.equipmentStatValue2.text = null;
        equipmentList.equipmentStatValue3.text = null;
        equipmentList.equipmentStatValue4.text = null;
        equipmentList.equipmentStatIconCanvasGroup2.alpha = 0;
        equipmentList.equipmentStatIconCanvasGroup3.alpha = 0;
        equipmentList.equipmentStatIconCanvasGroup4.alpha = 0;
        equipmentList.gemSlotCanvasGroup1.alpha = 0;
        equipmentList.gemSlotCanvasGroup2.alpha = 0;
        equipmentList.gemSlotCanvasGroup3.alpha = 0;
        equipmentList.gemSlotCanvasGroup1.blocksRaycasts = false;
        equipmentList.gemSlotCanvasGroup2.blocksRaycasts = false;
        equipmentList.gemSlotCanvasGroup3.blocksRaycasts = false;
        equipmentList.gemSlotLock1.alpha = 0;
        equipmentList.gemSlotLock2.alpha = 0;
        equipmentList.gemSlotLock3.alpha = 0;
        equipmentList.gemSlotLock1.blocksRaycasts = false;
        equipmentList.gemSlotLock2.blocksRaycasts = false;
        equipmentList.gemSlotLock3.blocksRaycasts = false;
        equipmentList.borderEquipStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(equipmentList.equipmentUnitBox[tempI].GetComponent<Image>().rectTransform.anchoredPosition.x, equipmentList.equipmentUnitBox[tempI].GetComponent<Image>().rectTransform.anchoredPosition.y);

        equipmentList.currentEquipment = equipmentList.homeScript.readData.dataUserModel.equipment.Find(x => x.equipmentUserId == equipmentList.equipments[tempI].equipmentUserId);
        equipmentList.equipmentName.text = equipmentList.homeScript.readData.jsonEquipmentData[equipmentList.currentEquipment.equipmentId.ToString()]["name"][equipmentList.homeScript.readData.languageId].ToString();
        if (equipmentList.currentEquipment.lvPlus > 0)
        {
            equipmentList.equipmentName.text += "+" + equipmentList.currentEquipment.lvPlus;
        }
        
        if(equipmentList.currentEquipment.warriorId != 0){
            equipmentList.warriorIcon.sprite = Resources.Load<Sprite>("Warriors/" + equipmentList.homeScript.readData.jsonWarriorData[equipmentList.currentEquipment.warriorId.ToString()]["name"][0].ToString() + "/icon");
            equipmentList.warriorIconCanvaseGroup.alpha = 1;
        }
        else
        {
            equipmentList.warriorIconCanvaseGroup.alpha = 0;
        }
        
        int i = 1;
        if (equipmentList.homeScript.readData.jsonEquipmentData[equipmentList.currentEquipment.equipmentId.ToString()]["hp"].ToString() != "0")
        {
            equipmentList.SetEquipmentStatDetail(i, "hp");
            i++;
        }
        if (equipmentList.homeScript.readData.jsonEquipmentData[equipmentList.currentEquipment.equipmentId.ToString()]["atk"].ToString() != "0")
        {
            equipmentList.SetEquipmentStatDetail(i, "atk");
            i++;
        }
        if (equipmentList.homeScript.readData.jsonEquipmentData[equipmentList.currentEquipment.equipmentId.ToString()]["def"].ToString() != "0")
        {
            equipmentList.SetEquipmentStatDetail(i, "def");
            i++;
        }
        if (equipmentList.homeScript.readData.jsonEquipmentData[equipmentList.currentEquipment.equipmentId.ToString()]["spd"].ToString() != "0")
        {
            equipmentList.SetEquipmentStatDetail(i, "spd");
            i++;
        }
        if (equipmentList.homeScript.readData.jsonEquipmentData[equipmentList.currentEquipment.equipmentId.ToString()]["luck"].ToString() != "0")
        {
            equipmentList.SetEquipmentStatDetail(i, "luck");
        }

        i = 1;
        foreach (string gemId in equipmentList.currentEquipment.dataGemUserId)
        {
            equipmentList.SetEquipmentGem(i, gemId);
            i++;
        }
        if (equipmentList.currentEquipment.warriorUserIdEquiped != "0")
        {
            equipmentList.equipmentLimitLvCanvasGroup.alpha = 1;
            equipmentList.equipmentLimitLvText.color = new Color32(54, 236, 63, 255);
            Warrior warrior = equipmentList.homeScript.readData.dataUserModel.warrior.Find(x => x.warriorUserId == equipmentList.currentEquipment.warriorUserIdEquiped);
            equipmentList.equipmentLimitLvText.text = warrior.name[equipmentList.homeScript.readData.languageId];
            if (warrior.lvPlus > 0)
            {
                equipmentList.equipmentLimitLvText.text += "+" + warrior.lvPlus.ToString();
            }
            equipmentList.equipmentLimitLvText.text += " " + equipmentList.homeScript.readData.jsonWordingData["equiped"][equipmentList.homeScript.readData.languageId].ToString();
        }
        else if (Convert.ToInt32(equipmentList.homeScript.readData.jsonEquipmentData[equipmentList.currentEquipment.equipmentId.ToString()]["limitLv"].ToString()) > 1)
        {
            equipmentList.equipmentLimitLvCanvasGroup.alpha = 1;
            equipmentList.equipmentLimitLvText.color = new Color32(225, 89, 89, 255);
            equipmentList.equipmentLimitLvText.text = equipmentList.homeScript.readData.jsonWordingData["limitLv"][equipmentList.homeScript.readData.languageId].ToString() + " " + equipmentList.homeScript.readData.jsonEquipmentData[equipmentList.currentEquipment.equipmentId.ToString()]["limitLv"].ToString();
        }
        else
        {
            equipmentList.equipmentLimitLvText.text = null;
            equipmentList.equipmentLimitLvCanvasGroup.alpha = 0;
        }

        int amount = 0;
        amount = (equipmentList.currentEquipment.lvPlus + 1) * 10 * equipmentList.currentEquipment.rare;
        equipmentList.amountOreEnhance.text = amount.ToString("#,#");
        amount = 0;
        if (equipmentList.currentEquipment.lvPlus < equipmentList.homeScript.readData.dataUserModel.level)
        {
            for (int j = (equipmentList.currentEquipment.lvPlus + 1); j <= equipmentList.homeScript.readData.dataUserModel.level; j++)
            {
                amount += j * 10 * equipmentList.currentEquipment.rare;
            }
            equipmentList.amountOreFullEnhance.text = amount.ToString("#,#");
        }
        else
        {
            equipmentList.amountOreFullEnhance.text = "-";
        }

        if (equipmentList.currentEquipment.warriorId == 0)
        {
            amount = 0;
            if (equipmentList.currentEquipment.lvPlus > 0)
            {
                for (int j = 0; j < equipmentList.currentEquipment.lvPlus; j++)
                {
                    amount += (j + 1) * 10 * equipmentList.currentEquipment.rare;
                }
                amount += equipmentList.currentEquipment.rare * equipmentList.currentEquipment.rare * 10;
            }
            else
            {
                amount = equipmentList.currentEquipment.rare * equipmentList.currentEquipment.rare * 10;
            }
            equipmentList.amountOreFuse.text = (amount / 2).ToString("#,#");
            amount = (equipmentList.currentEquipment.rare * equipmentList.currentEquipment.rare * equipmentList.currentEquipment.rare * 1000) / 2;
            equipmentList.amountMoneySell.text = amount.ToString("#,#");
        }
        else
        {
            equipmentList.amountOreFuse.text = "-";
            equipmentList.amountMoneySell.text = "-";
        }

        equipmentList.amountOreHave.text = equipmentList.homeScript.readData.dataUserModel.ore.ToString("#,#");
    }

    public void AskForUpgradeEquipmentBag()
    {
        equipmentList.OpenPopupUpgradeEquipmentBag();
    }
}
