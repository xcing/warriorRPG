using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    public ReadData readData;
    public GameObject alertMessageBg;
    public float countdown;

    public Text header;
    public Text moneyText;
    public Text refreshText;

    public List<Image> itemBox;
    public List<CanvasGroup> itemSoldOut;
    public List<CanvasGroup> itemLock;
    public MoneyStore moneyStore;
    public CanvasGroup bgFadeCanvasGroup;
    public CanvasGroup bgFade2CanvasGroup;

    // confirm buy popup
    public CanvasGroup confirmBuyPopupCanvasGroup;
    public int currentItemPick;
    public Image pickItemImage;
    public Text pickItemName;
    public Text pickAmountInStock;
    public Image pickStatIcon1;
    public Text pickStatValue1;
    public Image pickStatIcon2;
    public Text pickStatValue2;
    public Image pickStatIcon3;
    public Text pickStatValue3;
    public Image pickStatIcon4;
    public Text pickStatValue4;
    public Image pickGemSlotLock1;
    public Image pickGemSlotLock2;
    public Image pickGemSlotLock3;
    public Text pickMoneyText;
    public Button pickBuyButton;

    // refresh popup
    public CanvasGroup refreshPopupCanvasGroup;
    public Text refreshPopupHeader;
    //public Text refreshPopupText;

    // upgrade bag popup
    public CanvasGroup upgradeBagCanvasGroup;
    public Text upgradeBagHeader;
    public Text upgradeBagText;

    void Start()
    {
        header.text = readData.jsonWordingData["store"][readData.languageId].ToString();
        refreshText.text = "14:31:20";
        refreshPopupHeader.text = readData.jsonWordingData["confirmRefreshMoneyStore"][readData.languageId].ToString();

        moneyStore = readData.GetMoneyStoreOnFile();

        pickBuyButton.GetComponentInChildren<Text>().text = readData.jsonWordingData["buy"][readData.languageId].ToString();

        SetItemData();
        SetUnlockItem();
    }

    public void SetItemData()
    {
        moneyText.text = readData.dataUserModel.money.ToString("#,#");
        for (int i = 0; i < 12; i++)
        {
            switch (moneyStore.itemType[i])
            {
                case 1: //warrior
                    itemBox[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("Warriors/" + readData.jsonWarriorData[moneyStore.itemId[i].ToString()]["name"][0].ToString() + "/icon");
                    itemBox[i].GetComponentsInChildren<Text>()[0].text = readData.jsonWordingData["soul"][readData.languageId].ToString() + readData.jsonWarriorData[moneyStore.itemId[i].ToString()]["name"][readData.languageId].ToString();
                    itemBox[i].GetComponentsInChildren<Text>()[1].text = CalculatePrice(moneyStore.itemType[i], moneyStore.itemId[i]).ToString("#,#");
                    itemBox[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = readData.jsonWordingData["buy"][readData.languageId].ToString();
                    break;
                case 2: // warrior soul
                    itemBox[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("Warriors/" + readData.jsonWarriorData[moneyStore.itemId[i].ToString()]["name"][0].ToString() + "/icon");
                    itemBox[i].GetComponentsInChildren<Text>()[0].text = readData.jsonWarriorData[moneyStore.itemId[i].ToString()]["name"][readData.languageId].ToString();
                    itemBox[i].GetComponentsInChildren<Text>()[1].text = CalculatePrice(moneyStore.itemType[i], moneyStore.itemId[i]).ToString("#,#");
                    itemBox[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = readData.jsonWordingData["buy"][readData.languageId].ToString();
                    break;
                case 3: // equipment
                    itemBox[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("equipments/" + readData.jsonEquipmentData[moneyStore.itemId[i].ToString()]["name"][0].ToString());
                    itemBox[i].GetComponentsInChildren<Text>()[0].text = readData.jsonEquipmentData[moneyStore.itemId[i].ToString()]["name"][readData.languageId].ToString();
                    itemBox[i].GetComponentsInChildren<Text>()[1].text = CalculatePrice(moneyStore.itemType[i], moneyStore.itemId[i]).ToString("#,#");
                    itemBox[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = readData.jsonWordingData["buy"][readData.languageId].ToString();
                    break;
                case 4: // gem
                    itemBox[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("gems/" + readData.jsonGemData[moneyStore.itemId[i].ToString()]["name"][0].ToString());
                    itemBox[i].GetComponentsInChildren<Text>()[0].text = readData.jsonGemData[moneyStore.itemId[i].ToString()]["name"][readData.languageId].ToString();
                    itemBox[i].GetComponentsInChildren<Text>()[1].text = CalculatePrice(moneyStore.itemType[i], moneyStore.itemId[i]).ToString("#,#");
                    itemBox[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = readData.jsonWordingData["buy"][readData.languageId].ToString();
                    break;
            }
        }
    }

    public void SetUnlockItem()
    {
        if (readData.dataUserModel.vip >= 1)
        {
            itemLock[0].alpha = 0;
            itemLock[0].blocksRaycasts = false;
            itemLock[1].alpha = 0;
            itemLock[1].blocksRaycasts = false;
        }
        if (readData.dataUserModel.vip >= 3)
        {
            itemLock[2].alpha = 0;
            itemLock[2].blocksRaycasts = false;
            itemLock[3].alpha = 0;
            itemLock[3].blocksRaycasts = false;
        }
        if (readData.dataUserModel.vip >= 5)
        {
            itemLock[4].alpha = 0;
            itemLock[4].blocksRaycasts = false;
            itemLock[5].alpha = 0;
            itemLock[5].blocksRaycasts = false;
        }
        if (readData.dataUserModel.vip >= 7)
        {
            itemLock[6].alpha = 0;
            itemLock[6].blocksRaycasts = false;
            itemLock[7].alpha = 0;
            itemLock[7].blocksRaycasts = false;
        }
    }

    public void SetWordingUpgradePopup()
    {
        switch (moneyStore.itemType[currentItemPick])
        {
            case 2:
                upgradeBagHeader.text = readData.jsonWordingData["upgradeBag"][readData.languageId].ToString();
                upgradeBagText.text = readData.jsonWordingData["sizeBag"][readData.languageId].ToString() + "+5";
                break;
            case 3:
                upgradeBagHeader.text = readData.jsonWordingData["upgradeBag"][readData.languageId].ToString();
                upgradeBagText.text = readData.jsonWordingData["sizeBag"][readData.languageId].ToString() + "+5";
                break;
            case 4:
                upgradeBagHeader.text = readData.jsonWordingData["upgradeBag"][readData.languageId].ToString();
                upgradeBagText.text = readData.jsonWordingData["sizeBag"][readData.languageId].ToString() + "+5";
                break;
        }

    }

    // Update is called once per frame
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

    public void ItemLock(int position)
    {
        switch (position)
        {
            case 5:
            case 6:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenVIP"][readData.languageId].ToString() + " 1", null);
                break;
            case 7:
            case 8:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenVIP"][readData.languageId].ToString() + " 3", null);
                break;
            case 9:
            case 10:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenVIP"][readData.languageId].ToString() + " 5", null);
                break;
            case 11:
            case 12:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenVIP"][readData.languageId].ToString() + " 7", null);
                break;
        }
    }

    public void BuyButton(int position)
    {
        currentItemPick = position - 1;
        if (moneyStore.itemType[currentItemPick] != 0)
        {
            bgFadeCanvasGroup.alpha = 1;
            bgFadeCanvasGroup.blocksRaycasts = true;
            confirmBuyPopupCanvasGroup.alpha = 1;
            confirmBuyPopupCanvasGroup.blocksRaycasts = true;

            SetDataConfirmBuyPopup();
        }
    }

    public void SetDataConfirmBuyPopup()
    {
        switch (moneyStore.itemType[currentItemPick])
        {
            case 1: //warrior
                pickItemImage.sprite = Resources.Load<Sprite>("Warriors/" + readData.jsonWarriorData[moneyStore.itemId[currentItemPick].ToString()]["name"][0].ToString() + "/icon");
                pickItemName.text = readData.jsonWordingData["soul"][readData.languageId].ToString() + readData.jsonWarriorData[moneyStore.itemId[currentItemPick].ToString()]["name"][readData.languageId].ToString();
                int amount = 0;
                if (readData.dataUserModel.warriorSoul.Find(x => x.warriorId == moneyStore.itemId[currentItemPick]) != null)
                {
                    amount = readData.dataUserModel.warriorSoul.Find(x => x.warriorId == moneyStore.itemId[currentItemPick]).amount;
                }
                pickAmountInStock.text = readData.jsonWordingData["haveAmount"][readData.languageId].ToString() + " " + amount.ToString();
                pickMoneyText.text = CalculatePrice(moneyStore.itemType[currentItemPick], moneyStore.itemId[currentItemPick]).ToString("#,#");
                HiddenStatPickItem();
                HiddenGemSlot();
                break;
            case 2: // warrior soul
                pickItemImage.sprite = Resources.Load<Sprite>("Warriors/" + readData.jsonWarriorData[moneyStore.itemId[currentItemPick].ToString()]["name"][0].ToString() + "/icon");
                pickItemName.text = readData.jsonWarriorData[moneyStore.itemId[currentItemPick].ToString()]["name"][readData.languageId].ToString();
                pickAmountInStock.text = null;
                pickMoneyText.text = CalculatePrice(moneyStore.itemType[currentItemPick], moneyStore.itemId[currentItemPick]).ToString("#,#");
                HiddenStatPickItem();
                HiddenGemSlot();
                break;
            case 3: // equipment
                pickItemImage.sprite = Resources.Load<Sprite>("equipments/" + readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["name"][0].ToString());
                pickItemName.text = readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["name"][readData.languageId].ToString();
                pickAmountInStock.text = null;
                pickMoneyText.text = CalculatePrice(moneyStore.itemType[currentItemPick], moneyStore.itemId[currentItemPick]).ToString("#,#");
                ShowStatEquipment();
                ShowGemSlot();
                break;
            case 4: // gem
                pickItemImage.sprite = Resources.Load<Sprite>("gems/" + readData.jsonGemData[moneyStore.itemId[currentItemPick].ToString()]["name"][0].ToString());
                pickItemName.text = readData.jsonGemData[moneyStore.itemId[currentItemPick].ToString()]["name"][readData.languageId].ToString();
                pickAmountInStock.text = null;
                pickMoneyText.text = CalculatePrice(moneyStore.itemType[currentItemPick], moneyStore.itemId[currentItemPick]).ToString("#,#");
                ShowStatGem();
                HiddenGemSlot();
                break;
        }

    }

    public int CalculatePrice(int type, int id)
    {
        int price = 0;
        switch (type)
        {
            case 1:
                price = Convert.ToInt32(readData.jsonWarriorData[id.ToString()]["rare"].ToString()) * Convert.ToInt32(readData.jsonWarriorData[id.ToString()]["rare"].ToString()) * Convert.ToInt32(readData.jsonWarriorData[id.ToString()]["rare"].ToString()) * 10000;
                break;
            case 2:
                price = Convert.ToInt32(readData.jsonWarriorData[id.ToString()]["rare"].ToString()) * Convert.ToInt32(readData.jsonWarriorData[id.ToString()]["rare"].ToString()) * Convert.ToInt32(readData.jsonWarriorData[id.ToString()]["rare"].ToString()) * 150000;
                break;
            case 3:
                price = Convert.ToInt32(readData.jsonEquipmentData[id.ToString()]["rare"].ToString()) * Convert.ToInt32(readData.jsonEquipmentData[id.ToString()]["rare"].ToString()) * Convert.ToInt32(readData.jsonWarriorData[id.ToString()]["rare"].ToString()) * 1000;
                break;
            case 4:
                price = Convert.ToInt32(readData.jsonGemData[id.ToString()]["rare"].ToString()) * Convert.ToInt32(readData.jsonGemData[id.ToString()]["rare"].ToString()) * Convert.ToInt32(readData.jsonWarriorData[id.ToString()]["rare"].ToString()) * 1000;
                break;
        }
        return price;
    }

    public void ShowStatEquipment()
    {
        HiddenStatPickItem();
        int i = 1;
        if (readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["hp"].ToString() != "0")
        {
            SetPickItemStatEquipment(i, "hp");
            i++;
        }
        if (readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["atk"].ToString() != "0")
        {
            SetPickItemStatEquipment(i, "atk");
            i++;
        }
        if (readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["def"].ToString() != "0")
        {
            SetPickItemStatEquipment(i, "def");
            i++;
        }
        if (readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["spd"].ToString() != "0")
        {
            SetPickItemStatEquipment(i, "spd");
            i++;
        }
        if (readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["luck"].ToString() != "0")
        {
            SetPickItemStatEquipment(i, "luck");
            i++;
        }
    }

    public void SetPickItemStatEquipment(int i, string statTemp) //stat 1 = hp, 2 = atk, 3 = def, 4 = spd. 5 = luck
    {
        Sprite spriteTemp;
        switch (statTemp)
        {
            case "hp":
                spriteTemp = Resources.Load<Sprite>("UI/hpIcon");
                break;
            case "atk":
                spriteTemp = Resources.Load<Sprite>("UI/atkIcon");
                break;
            case "def":
                spriteTemp = Resources.Load<Sprite>("UI/defIcon");
                break;
            case "spd":
                spriteTemp = Resources.Load<Sprite>("UI/spdIcon");
                break;
            case "luck":
                spriteTemp = Resources.Load<Sprite>("UI/luckIcon");
                break;
            default:
                spriteTemp = Resources.Load<Sprite>("UI/hpIcon");
                break;
        }
        switch (i)
        {
            case 1:
                pickStatIcon1.sprite = spriteTemp;
                pickStatValue1.text = "+" + Convert.ToInt32(readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()][statTemp].ToString()).ToString("#,#");
                pickStatIcon1.GetComponent<CanvasRenderer>().SetAlpha(1);
                break;
            case 2:
                pickStatIcon2.sprite = spriteTemp;
                pickStatValue2.text = "+" + Convert.ToInt32(readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()][statTemp].ToString()).ToString("#,#");
                pickStatIcon2.GetComponent<CanvasRenderer>().SetAlpha(1);
                break;
            case 3:
                pickStatIcon3.sprite = spriteTemp;
                pickStatValue3.text = "+" + Convert.ToInt32(readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()][statTemp].ToString()).ToString("#,#");
                pickStatIcon3.GetComponent<CanvasRenderer>().SetAlpha(1);
                break;
            case 4:
                pickStatIcon4.sprite = spriteTemp;
                pickStatValue4.text = "+" + Convert.ToInt32(readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()][statTemp].ToString()).ToString("#,#");
                pickStatIcon4.GetComponent<CanvasRenderer>().SetAlpha(1);
                break;
        }
    }

    public void ShowStatGem()
    {
        HiddenStatPickItem();
        Sprite spriteTemp;
        switch (Convert.ToInt32(readData.jsonGemData[moneyStore.itemId[currentItemPick].ToString()]["buffType"].ToString()))
        {
            case 1:
                spriteTemp = Resources.Load<Sprite>("UI/hpIcon");
                break;
            case 2:
                spriteTemp = Resources.Load<Sprite>("UI/atkIcon");
                break;
            case 3:
                spriteTemp = Resources.Load<Sprite>("UI/defIcon");
                break;
            case 4:
                spriteTemp = Resources.Load<Sprite>("UI/spdIcon");
                break;
            case 5:
                spriteTemp = Resources.Load<Sprite>("UI/luckIcon");
                break;
            default:
                spriteTemp = Resources.Load<Sprite>("UI/hpIcon");
                break;
        }
        pickStatIcon1.sprite = spriteTemp;
        pickStatValue1.text = "+" + Convert.ToInt32(readData.jsonGemData[moneyStore.itemId[currentItemPick].ToString()]["buffValue"].ToString()).ToString() + "%";
        pickStatIcon1.GetComponent<CanvasRenderer>().SetAlpha(1);
    }

    public void ShowGemSlot()
    {
        HiddenGemSlot();
        if (Convert.ToInt32(readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["rare"].ToString()) >= 2)
        {
            pickGemSlotLock1.GetComponent<CanvasRenderer>().SetAlpha(1);
        }
        if (Convert.ToInt32(readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["rare"].ToString()) >= 4)
        {
            pickGemSlotLock2.GetComponent<CanvasRenderer>().SetAlpha(1);
        }
        if (Convert.ToInt32(readData.jsonEquipmentData[moneyStore.itemId[currentItemPick].ToString()]["rare"].ToString()) >= 6)
        {
            pickGemSlotLock3.GetComponent<CanvasRenderer>().SetAlpha(1);
        }
    }

    public void HiddenStatPickItem()
    {
        pickStatIcon1.GetComponent<CanvasRenderer>().SetAlpha(0);
        pickStatIcon2.GetComponent<CanvasRenderer>().SetAlpha(0);
        pickStatIcon3.GetComponent<CanvasRenderer>().SetAlpha(0);
        pickStatIcon4.GetComponent<CanvasRenderer>().SetAlpha(0);
        pickStatValue1.text = null;
        pickStatValue2.text = null;
        pickStatValue3.text = null;
        pickStatValue4.text = null;
    }

    public void HiddenGemSlot()
    {
        pickGemSlotLock1.GetComponent<CanvasRenderer>().SetAlpha(0);
        pickGemSlotLock2.GetComponent<CanvasRenderer>().SetAlpha(0);
        pickGemSlotLock3.GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    public void ConfirmBuyButton()
    {
        if (readData.dataUserModel.money < CalculatePrice(moneyStore.itemType[currentItemPick], moneyStore.itemId[currentItemPick]))
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughMoney"][readData.languageId].ToString(), null);
        }
        else if (moneyStore.itemType[currentItemPick] == 2 && readData.dataUserModel.warrior.Count >= readData.dataUserModel.bag_warrior)
        {
            SetWordingUpgradePopup();
            OpenPopupUpgradeBag();
        }
        else if (moneyStore.itemType[currentItemPick] == 3 && readData.dataUserModel.equipment.Count >= readData.dataUserModel.bag_equipment)
        {
            SetWordingUpgradePopup();
            OpenPopupUpgradeBag();
        }
        else if (moneyStore.itemType[currentItemPick] == 4 && readData.dataUserModel.gem.Count >= readData.dataUserModel.bag_gem)
        {
            SetWordingUpgradePopup();
            OpenPopupUpgradeBag();
        }
        else
        {
            StartCoroutine(readData.BuyItemFromMoneyStore());
        }
    }

    public void ClosePopupConfirmBuy()
    {
        bgFadeCanvasGroup.alpha = 0;
        bgFadeCanvasGroup.blocksRaycasts = false;
        confirmBuyPopupCanvasGroup.alpha = 0;
        confirmBuyPopupCanvasGroup.blocksRaycasts = false;
    }

    public void OpenRefreshPopup()
    {
        bgFadeCanvasGroup.alpha = 1;
        bgFadeCanvasGroup.blocksRaycasts = true;
        refreshPopupCanvasGroup.alpha = 1;
        refreshPopupCanvasGroup.blocksRaycasts = true;
    }

    public void CloseRefreshPopup()
    {
        bgFadeCanvasGroup.alpha = 0;
        bgFadeCanvasGroup.blocksRaycasts = false;
        refreshPopupCanvasGroup.alpha = 0;
        refreshPopupCanvasGroup.blocksRaycasts = false;
    }

    public void RefreshButton()
    {
        if (readData.dataUserModel.diamond < 5)
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughDiamond"][readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(readData.RefreshMoneyStore());
        }
    }

    public void OpenPopupUpgradeBag()
    {
        bgFade2CanvasGroup.alpha = 1;
        bgFade2CanvasGroup.blocksRaycasts = true;
        upgradeBagCanvasGroup.alpha = 1;
        upgradeBagCanvasGroup.blocksRaycasts = true;
    }

    public void CloseUpgradeBag()
    {
        bgFade2CanvasGroup.alpha = 0;
        bgFade2CanvasGroup.blocksRaycasts = false;
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
            StartCoroutine(readData.UpgradeBagFromMoneyShop());
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

    public void BackToHome()
    {
        Application.LoadLevel("Home");
    }
}
