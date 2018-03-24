using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WarriorDetailEquipmentTempData : MonoBehaviour
{
    public string equipmentUserId;
    public int tempI;
    public WarriorDetail warriorDetail;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PickEquipmentStockDetail()
    {
        warriorDetail.equipStockStatValue1.text = null;
        warriorDetail.equipStockStatValue2.text = null;
        warriorDetail.equipStockStatValue3.text = null;
        warriorDetail.equipStockStatValue4.text = null;
        warriorDetail.equipStockStatIconCanvasGroup2.alpha = 0;
        warriorDetail.equipStockStatIconCanvasGroup3.alpha = 0;
        warriorDetail.equipStockStatIconCanvasGroup4.alpha = 0;
        warriorDetail.equipStockGemSlotCanvasGroup1.alpha = 0;
        warriorDetail.equipStockGemSlotCanvasGroup2.alpha = 0;
        warriorDetail.equipStockGemSlotCanvasGroup3.alpha = 0;
        warriorDetail.equipStockGemSlotLock1.alpha = 0;
        warriorDetail.equipStockGemSlotLock2.alpha = 0;
        warriorDetail.equipStockGemSlotLock3.alpha = 0;
        warriorDetail.equipStockDetail.alpha = 1;
        warriorDetail.equipStockDetail.blocksRaycasts = true;
        warriorDetail.borderEquipStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(warriorDetail.equipmentUnitBox[tempI].GetComponent<Image>().rectTransform.anchoredPosition.x, warriorDetail.equipmentUnitBox[tempI].GetComponent<Image>().rectTransform.anchoredPosition.y);

        warriorDetail.equipStockDetail.alpha = 1;
        warriorDetail.equipStockDetail.blocksRaycasts = true;
        warriorDetail.equipStock = warriorDetail.homeScript.readData.dataUserModel.equipment.Find(x => x.equipmentUserId == warriorDetail.equipments[tempI].equipmentUserId);
        warriorDetail.equipStockName.text = warriorDetail.homeScript.readData.jsonEquipmentData[warriorDetail.equipStock.equipmentId.ToString()]["name"][warriorDetail.homeScript.readData.languageId].ToString();
        if (warriorDetail.equipStock.lvPlus > 0)
        {
            warriorDetail.equipStockName.text += "+" + warriorDetail.equipStock.lvPlus;
        }
        int i = 1;
        if (warriorDetail.homeScript.readData.jsonEquipmentData[warriorDetail.equipStock.equipmentId.ToString()]["hp"].ToString() != "0")
        {
            warriorDetail.SetEquipmentStatDetail(i, "hp", false);
            i++;
        }
        if (warriorDetail.homeScript.readData.jsonEquipmentData[warriorDetail.equipStock.equipmentId.ToString()]["atk"].ToString() != "0")
        {
            warriorDetail.SetEquipmentStatDetail(i, "atk", false);
            i++;
        }
        if (warriorDetail.homeScript.readData.jsonEquipmentData[warriorDetail.equipStock.equipmentId.ToString()]["def"].ToString() != "0")
        {
            warriorDetail.SetEquipmentStatDetail(i, "def", false);
            i++;
        }
        if (warriorDetail.homeScript.readData.jsonEquipmentData[warriorDetail.equipStock.equipmentId.ToString()]["spd"].ToString() != "0")
        {
            warriorDetail.SetEquipmentStatDetail(i, "spd", false);
            i++;
        }
        if (warriorDetail.homeScript.readData.jsonEquipmentData[warriorDetail.equipStock.equipmentId.ToString()]["luck"].ToString() != "0")
        {
            warriorDetail.SetEquipmentStatDetail(i, "luck", false);
        }

        i = 1;
        if (warriorDetail.equipStock.dataGemUserId != null)
        {
            foreach (string gemId in warriorDetail.equipStock.dataGemUserId)
            {
                warriorDetail.SetEquipmentGem(i, gemId, false);
                i++;
            }
        }

        if (Convert.ToInt32(warriorDetail.homeScript.readData.jsonEquipmentData[warriorDetail.equipStock.equipmentId.ToString()]["limitLv"].ToString()) > 1)
        {
            warriorDetail.equipStockLimitLvCanvasGroup.alpha = 1;
            warriorDetail.equipStockLimitLv.text = warriorDetail.homeScript.readData.jsonWordingData["limitLv"][warriorDetail.homeScript.readData.languageId].ToString() + " " + warriorDetail.homeScript.readData.jsonEquipmentData[warriorDetail.equipStock.equipmentId.ToString()]["limitLv"].ToString();
        }
        else
        {
            warriorDetail.equipStockLimitLvCanvasGroup.alpha = 0;
        }
    }
}
