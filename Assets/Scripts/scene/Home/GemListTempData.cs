using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GemListTempData : MonoBehaviour
{
    public string gemUserId;
    public int tempI;
    public EquipmentList equipmentList;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PickGemStockDetail()
    {
        equipmentList.currentGemI = tempI;
        //equipmentList.gemStockName.text = null;
        //equipmentList.gemStockStatIcon.sprite = null;
        //equipmentList.gemStockStatValue.text = null;
        equipmentList.borderGemStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(equipmentList.gemUnitBox[tempI].GetComponent<Image>().rectTransform.anchoredPosition.x, equipmentList.gemUnitBox[tempI].GetComponent<Image>().rectTransform.anchoredPosition.y);

        equipmentList.currentGemStock = equipmentList.homeScript.readData.dataUserModel.gem.Find(x => x.gemUserId == equipmentList.gems[tempI].gemUserId);
        equipmentList.gemStockName.text = equipmentList.homeScript.readData.jsonGemData[equipmentList.currentGemStock.gemId.ToString()]["name"][equipmentList.homeScript.readData.languageId].ToString();

        equipmentList.gemStockStatIcon.sprite = equipmentList.GetStatIcon(equipmentList.homeScript.readData.jsonGemData[equipmentList.currentGemStock.gemId.ToString()]["buffType"].ToString());
        equipmentList.gemStockStatValue.text = "+" + equipmentList.homeScript.readData.jsonGemData[equipmentList.currentGemStock.gemId.ToString()]["buffValue"].ToString() + "%";

        equipmentList.moneySellText.text = (equipmentList.currentGemStock.rare * equipmentList.currentGemStock.rare * 500).ToString("#,#");
    }

    public void AskForUpgradeGemBag()
    {
        equipmentList.OpenPopupUpgradeEquipmentBag();
    }
}
