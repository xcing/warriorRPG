using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Globalization;
using System.Linq;


public class EquipmentList : MonoBehaviour
{
    public Home homeScript;
    public Text header;

    //equipment stock
    public List<Equipment> equipments;
    public int equipmentHave;
    public GameObject equipmentUnitBoxPrefab;
    public GameObject[] equipmentUnitBox;
    public Image equipmentBoxScrollRect;
    public Image equipmentStockBox;
    public Scrollbar equipmentScrollbar;
    public GameObject borderEquipStockPick;
    public GameObject borderPickPrefab;
    public int equipmentSize;

    //equipment detail
    public Equipment currentEquipment;
    public int currentEquipmentI;
    public int currentPositionUnlockGem;
    public int currentPositionPickGem;
    public Text equipmentName;
    public Image warriorIcon;
    public CanvasGroup warriorIconCanvaseGroup;
    public Image equipmentStatIcon1;
    public Text equipmentStatValue1;
    public Image equipmentStatIcon2;
    public CanvasGroup equipmentStatIconCanvasGroup2;
    public Text equipmentStatValue2;
    public Image equipmentStatIcon3;
    public CanvasGroup equipmentStatIconCanvasGroup3;
    public Text equipmentStatValue3;
    public Image equipmentStatIcon4;
    public CanvasGroup equipmentStatIconCanvasGroup4;
    public Text equipmentStatValue4;
    public Image gemSlot1;
    public CanvasGroup gemSlotCanvasGroup1;
    public CanvasGroup gemSlotLock1;
    public Image gemSlot2;
    public CanvasGroup gemSlotCanvasGroup2;
    public CanvasGroup gemSlotLock2;
    public Image gemSlot3;
    public CanvasGroup gemSlotCanvasGroup3;
    public CanvasGroup gemSlotLock3;
    public Text equipmentLimitLvText;
    public CanvasGroup equipmentLimitLvCanvasGroup;
    public Text amountOreEnhance;
    public Text enhanceButtonText;
    public Text amountOreFullEnhance;
    public Text amountOreFuse;
    public Text fuseButtonText;
    public Text amountMoneySell;
    public Text sellButtonText;
    public Text fullEnhanceButtonText;
    public Text amountOreHaveText;
    public Text amountOreHave;

    //popup unlock gem slot
    public CanvasGroup bgFade;
    public CanvasGroup unlockGemSlotPopup;
    public Image unlockEquipmentImage;
    public Text unlockEquipmentName;
    public Text unlockDetail;
    public Text unlockMoneyUseText;

    //popup upgrade equipment bag
    public CanvasGroup upgradeEquipmentBag;
    public Text upgradeEquipmentBagHeader;
    public Text upgradeEquipmentBagText;

    //gem stock
    public List<Gem> gems;
    public int gemHave;
    public GameObject gemUnitBoxPrefab;
    public GameObject[] gemUnitBox;
    public Image gemBoxScrollRect;
    public Image gemStockBox;
    public Scrollbar gemScrollbar;
    public GameObject borderGemStockPick;
    public GameObject borderPickGemPrefab;
    public int gemSize;

    //gem detail
    public Gem currentGemEquiped;
    public Gem currentGemStock;
    public int currentGemI;
    public CanvasGroup gemBoxScrollRectCanvasGroup;
    public CanvasGroup gemBoxScrollbarCanvasGroup;
    public CanvasGroup gemEquipedDetailCanvasGroup;
    public CanvasGroup gemStockDetailCanvasGroup;
    public CanvasGroup backToEquipmentCanvasGroup;
    public Text gemEquipedHeader;
    public Text gemEquipedName;
    public Image gemEquipedStatIcon;
    public Text gemEquipedStatValue;
    public Text removeMoneyUse;
    public Button gemEquipedRemoveButton;
    public Image gemEquipedMoneyIcon;
    public Text gemStockHeader;
    public Text gemStockName;
    public Image gemStockStatIcon;
    public Text gemStockStatValue;
    public Button gemStockEquipButton;
    public Image borderGemEquipedPick;
    public CanvasGroup borderGemEquipedPickCanvasGroup;
    public Text moneySellText;
    public Button gemStockSellButton;
    public Image gemStockMoneyIcon;

    void Start()
    {
        header.text = homeScript.readData.jsonWordingData["headerEquipmentList"][homeScript.readData.languageId].ToString();
        enhanceButtonText.text = homeScript.readData.jsonWordingData["enhance"][homeScript.readData.languageId].ToString();
        fullEnhanceButtonText.text = homeScript.readData.jsonWordingData["fullEnhance"][homeScript.readData.languageId].ToString();
        amountOreHaveText.text = homeScript.readData.jsonWordingData["have"][homeScript.readData.languageId].ToString();
        fuseButtonText.text = homeScript.readData.jsonWordingData["fuse"][homeScript.readData.languageId].ToString();
        sellButtonText.text = homeScript.readData.jsonWordingData["sell"][homeScript.readData.languageId].ToString();

        upgradeEquipmentBagHeader.text = homeScript.readData.jsonWordingData["upgradeBag"][homeScript.readData.languageId].ToString();
        upgradeEquipmentBagText.text = homeScript.readData.jsonWordingData["sizeBag"][homeScript.readData.languageId].ToString() + "+5";

        gemEquipedHeader.text = homeScript.readData.jsonWordingData["equiped"][homeScript.readData.languageId].ToString();
        gemStockHeader.text = homeScript.readData.jsonWordingData["picked"][homeScript.readData.languageId].ToString();
        gemEquipedRemoveButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["remove"][homeScript.readData.languageId].ToString();
        gemStockEquipButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["equip"][homeScript.readData.languageId].ToString();
        gemStockSellButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["sell"][homeScript.readData.languageId].ToString();
        currentEquipmentI = 0;
        currentGemI = 0;
        SetDataEquipmentStock(currentEquipmentI);
        SetDataGemStock(currentGemI);
    }

    public void SetDataEquipmentStock(int currentPickEquipment)
    {
        for (int i = equipmentStockBox.transform.childCount - 1; i > 0; i--)
        {
            GameObject.Destroy(equipmentStockBox.transform.GetChild(i).gameObject);
        }
        Destroy(borderEquipStockPick);

        equipmentSize = (homeScript.readData.dataUserModel.bag_equipment + 1);
        equipments = homeScript.readData.dataUserModel.equipment;
        equipments = equipments.OrderByDescending(x => x.rare).ThenByDescending(y => y.lvPlus).ThenBy(z => z.equipmentId).ToList();
        equipmentHave = equipments.Count;

        equipmentUnitBox = new GameObject[equipmentSize];
        equipmentUnitBoxPrefab = Resources.Load<GameObject>("Prefab/EquipmentListUnitBox");
        borderPickPrefab = Resources.Load<GameObject>("Prefab/BorderPick140");
        int heightEquipmentPanelBox;
        if (equipmentSize % 4 == 0)
            heightEquipmentPanelBox = ((equipmentSize / 4) * 150) + 30;
        else
            heightEquipmentPanelBox = (((equipmentSize / 4) + 1) * 150) + 30;
        if (heightEquipmentPanelBox < equipmentBoxScrollRect.rectTransform.rect.height)
        {
            heightEquipmentPanelBox = Convert.ToInt32(equipmentBoxScrollRect.rectTransform.rect.height);
            equipmentStockBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            equipmentStockBox.rectTransform.anchoredPosition = new Vector3(0, (heightEquipmentPanelBox - equipmentBoxScrollRect.rectTransform.rect.height) / -2);
        }
        equipmentStockBox.rectTransform.sizeDelta = new Vector2(equipmentStockBox.rectTransform.rect.width, heightEquipmentPanelBox);

        borderEquipStockPick = Instantiate(borderPickPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        borderEquipStockPick.transform.SetParent(equipmentStockBox.transform, true);
        borderEquipStockPick.transform.localScale = new Vector3(1, 1, 1);
        borderEquipStockPick.transform.position = equipmentStockBox.transform.position;
        borderEquipStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-240, (heightEquipmentPanelBox / 2) - 87);

        for (int i = 0; i < equipmentSize; i++)
        {
            if (i < equipmentHave)
            {
                equipmentUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Equipments/" + homeScript.readData.jsonEquipmentData[equipments[i].equipmentId.ToString()]["name"][0].ToString());
            }
            else if (i == (equipmentSize - 1))
            {
                equipmentUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/close");
            }
            else
            {
                equipmentUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/empty");
            }
            equipmentUnitBox[i] = Instantiate(equipmentUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            equipmentUnitBox[i].transform.SetParent(equipmentStockBox.transform, true);
            equipmentUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            equipmentUnitBox[i].transform.position = equipmentStockBox.transform.position;
            equipmentUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(((i % 4) * 150) - 240, (-150 * (i / 4)) + (heightEquipmentPanelBox / 2) - 87);
            if (i < equipmentHave)
            {
                EquipmentListTempData tempData = equipmentUnitBox[i].GetComponent<EquipmentListTempData>();
                tempData.tempI = i;
                tempData.equipmentList = this;
                tempData.equipmentUserId = equipments[i].equipmentUserId;
                equipmentUnitBox[i].GetComponent<Button>().onClick.AddListener(() => tempData.PickEquipmentStockDetail());
                if (i == currentPickEquipment)
                {
                    tempData.PickEquipmentStockDetail();
                }
            }
            else if (i == (equipmentSize - 1))
            {
                EquipmentListTempData tempData = equipmentUnitBox[i].GetComponent<EquipmentListTempData>();
                tempData.equipmentList = this;
                equipmentUnitBox[i].GetComponent<Button>().onClick.AddListener(() => tempData.AskForUpgradeEquipmentBag());
            }
        }
        if (equipmentHave == 0)
        {
            borderEquipStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-400, 0);
        }
    }

    public void SetDataGemStock(int currentPickGem)
    {
        for (int i = gemStockBox.transform.childCount - 1; i > 0; i--)
        {
            GameObject.Destroy(gemStockBox.transform.GetChild(i).gameObject);
        }
        Destroy(borderGemStockPick);

        gemSize = (homeScript.readData.dataUserModel.bag_gem + 1);
        gems = homeScript.readData.dataUserModel.gem.FindAll(x => x.equipmentUserIdEquiped == "0");
        gems = gems.OrderByDescending(x => x.rare).ThenBy(z => z.gemId).ToList();
        gemHave = gems.Count;
        gemUnitBox = new GameObject[gemSize];
        gemUnitBoxPrefab = Resources.Load<GameObject>("Prefab/GemListUnitBox");
        borderPickGemPrefab = Resources.Load<GameObject>("Prefab/BorderPick140");
        int heightGemPanelBox;
        if (gemSize % 4 == 0)
            heightGemPanelBox = ((gemSize / 4) * 150) + 30;
        else
            heightGemPanelBox = (((gemSize / 4) + 1) * 150) + 30;
        if (heightGemPanelBox < gemBoxScrollRect.rectTransform.rect.height)
        {
            heightGemPanelBox = Convert.ToInt32(gemBoxScrollRect.rectTransform.rect.height);
            gemStockBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            gemStockBox.rectTransform.anchoredPosition = new Vector3(0, (heightGemPanelBox - gemBoxScrollRect.rectTransform.rect.height) / -2);
        }
        gemStockBox.rectTransform.sizeDelta = new Vector2(gemStockBox.rectTransform.rect.width, heightGemPanelBox);

        borderGemStockPick = Instantiate(borderPickGemPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        borderGemStockPick.transform.SetParent(gemStockBox.transform, true);
        borderGemStockPick.transform.localScale = new Vector3(1, 1, 1);
        borderGemStockPick.transform.position = gemStockBox.transform.position;
        borderGemStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-240, (heightGemPanelBox / 2) - 87);

        for (int i = 0; i < gemSize; i++)
        {
            if (i < gemHave)
            {
                gemUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gems[i].gemId.ToString()]["name"][0].ToString());
            }
            else if (i == (gemSize - 1))
            {
                gemUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/close");
            }
            else
            {
                gemUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/empty");
            }
            gemUnitBox[i] = Instantiate(gemUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            gemUnitBox[i].transform.SetParent(gemStockBox.transform, true);
            gemUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            gemUnitBox[i].transform.position = gemStockBox.transform.position;
            gemUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(((i % 4) * 150) - 240, (-150 * (i / 4)) + (heightGemPanelBox / 2) - 87);
            if (i < gemHave)
            {
                GemListTempData tempData = gemUnitBox[i].GetComponent<GemListTempData>();
                tempData.tempI = i;
                tempData.equipmentList = this;
                tempData.gemUserId = gems[i].gemUserId;
                gemUnitBox[i].GetComponent<Button>().onClick.AddListener(() => tempData.PickGemStockDetail());
                if (i == currentPickGem)
                {
                    tempData.PickGemStockDetail();
                }
            }
            else if (i == (gemSize - 1))
            {
                GemListTempData tempData = gemUnitBox[i].GetComponent<GemListTempData>();
                tempData.equipmentList = this;
                gemUnitBox[i].GetComponent<Button>().onClick.AddListener(() => tempData.AskForUpgradeGemBag());
            }
        }
        if (gemHave == 0)
        {
            GemStockDetailisEmpty();
        }
    }

    public Sprite GetStatIcon(string statId)
    {
        switch (statId)
        {
            case "1":
                return Resources.Load<Sprite>("UI/hpIcon");
            case "2":
                return Resources.Load<Sprite>("UI/atkIcon");
            case "3":
                return Resources.Load<Sprite>("UI/defIcon");
            case "4":
                return Resources.Load<Sprite>("UI/spdIcon");
            case "5":
                return Resources.Load<Sprite>("UI/luckIcon");
            default:
                return Resources.Load<Sprite>("UI/hpIcon");
        }
    }

    public void SetEquipmentStatDetail(int i, string statTemp) //stat 1 = hp, 2 = atk, 3 = def, 4 = spd. 5 = luck
    {
        Sprite spriteTemp;
        string statPlusTemp;
        switch (statTemp)
        {
            case "hp":
                spriteTemp = Resources.Load<Sprite>("UI/hpIcon");
                statPlusTemp = "hpPlusPerLv";
                break;
            case "atk":
                spriteTemp = Resources.Load<Sprite>("UI/atkIcon");
                statPlusTemp = "atkPlusPerLv";
                break;
            case "def":
                spriteTemp = Resources.Load<Sprite>("UI/defIcon");
                statPlusTemp = "defPlusPerLv";
                break;
            case "spd":
                spriteTemp = Resources.Load<Sprite>("UI/spdIcon");
                statPlusTemp = "spdPlusPerLv";
                break;
            case "luck":
                spriteTemp = Resources.Load<Sprite>("UI/luckIcon");
                statPlusTemp = "luckPlusPerLv";
                break;
            default:
                spriteTemp = Resources.Load<Sprite>("UI/hpIcon");
                statPlusTemp = "hpPlusPerLv";
                break;
        }
        switch (i)
        {
            case 1:
                equipmentStatIcon1.sprite = spriteTemp;
                equipmentStatValue1.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()][statTemp].ToString()) + currentEquipment.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                break;
            case 2:
                equipmentStatIcon2.sprite = spriteTemp;
                equipmentStatValue2.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()][statTemp].ToString()) + currentEquipment.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                equipmentStatIconCanvasGroup2.alpha = 1;
                break;
            case 3:
                equipmentStatIcon3.sprite = spriteTemp;
                equipmentStatValue3.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()][statTemp].ToString()) + currentEquipment.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                equipmentStatIconCanvasGroup3.alpha = 1;
                break;
            case 4:
                equipmentStatIcon4.sprite = spriteTemp;
                equipmentStatValue4.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()][statTemp].ToString()) + currentEquipment.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                equipmentStatIconCanvasGroup4.alpha = 1;
                break;
        }

    }

    public void SetEquipmentGem(int i, string gemId)
    {
        Gem gemTemp = homeScript.readData.dataUserModel.gem.Find(x => x.gemUserId == gemId);

        switch (i)
        {
            case 1:
                gemSlotCanvasGroup1.alpha = 1;
                gemSlotCanvasGroup1.blocksRaycasts = true;
                if (gemId == "-1")
                {
                    gemSlotLock1.alpha = 1;
                    gemSlotLock1.blocksRaycasts = true;
                    gemSlot1.sprite = Resources.Load<Sprite>("UI/empty");
                }
                else if (gemId == "0")
                {
                    gemSlot1.sprite = Resources.Load<Sprite>("UI/empty");
                }
                else
                {
                    gemSlot1.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                }
                break;
            case 2:
                gemSlotCanvasGroup2.alpha = 1;
                gemSlotCanvasGroup2.blocksRaycasts = true;
                if (gemId == "-1")
                {
                    gemSlotLock2.alpha = 1;
                    gemSlotLock2.blocksRaycasts = true;
                    gemSlot2.sprite = Resources.Load<Sprite>("UI/empty");
                }
                else if (gemId == "0")
                {
                    gemSlot2.sprite = Resources.Load<Sprite>("UI/empty");
                }
                else
                {
                    gemSlot2.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                }
                break;
            case 3:
                gemSlotCanvasGroup3.alpha = 1;
                gemSlotCanvasGroup3.blocksRaycasts = true;
                if (gemId == "-1")
                {
                    gemSlotLock3.alpha = 1;
                    gemSlotLock3.blocksRaycasts = true;
                    gemSlot3.sprite = Resources.Load<Sprite>("UI/empty");
                }
                else if (gemId == "0")
                {
                    gemSlot3.sprite = Resources.Load<Sprite>("UI/empty");
                }
                else
                {
                    gemSlot3.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                }
                break;
        }
    }

    public void Enhance()
    {
        if (currentEquipment.lvPlus >= homeScript.readData.dataUserModel.level)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughUserLevel"][homeScript.readData.languageId].ToString(), null);
        }
        else if (homeScript.readData.dataUserModel.ore < Convert.ToInt32(amountOreEnhance.text))
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughOre"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.EnhanceEquipment());
        }
    }

    public void FullEnhance()
    {
        if (currentEquipment.lvPlus >= homeScript.readData.dataUserModel.level)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughUserLevel"][homeScript.readData.languageId].ToString(), null);
        }
        else if (homeScript.readData.dataUserModel.ore < Convert.ToInt32(amountOreFullEnhance.text))
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughOre"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.FullEnhanceEquipment());
        }
    }

    public void Fuse()
    {
        if (currentEquipment.warriorId != 0)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["cannotFuseLegendaryWeapon"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.FuseEquipment());
        }

    }

    public void Sell()
    {
        if (currentEquipment.warriorId != 0)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["cannotSellLegendaryWeapon"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.SellEquipment());
        }
    }

    public void OpenPopupUpgradeEquipmentBag()
    {
        bgFade.alpha = 1;
        bgFade.blocksRaycasts = true;
        upgradeEquipmentBag.alpha = 1;
        upgradeEquipmentBag.blocksRaycasts = true;
    }

    public void CloseUpgradeEquipmentBag()
    {
        bgFade.alpha = 0;
        bgFade.blocksRaycasts = false;
        upgradeEquipmentBag.alpha = 0;
        upgradeEquipmentBag.blocksRaycasts = false;
    }

    public void UpgradeEquipmentBag()
    {
        if (homeScript.readData.dataUserModel.diamond < 5)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughDiamond"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.UpgradeEquipmentBag());
        }
    }

    public void UnlockGemSlot(int position)
    {
        currentPositionUnlockGem = position;

        unlockEquipmentImage.sprite = Resources.Load<Sprite>("Equipments/" + homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()]["name"][0].ToString());
        unlockEquipmentName.text = homeScript.readData.jsonEquipmentData[currentEquipment.equipmentId.ToString()]["name"][homeScript.readData.languageId].ToString();
        if (currentEquipment.lvPlus > 0)
        {
            unlockEquipmentName.text += "+" + currentEquipment.lvPlus;
        }
        unlockDetail.text = homeScript.readData.jsonWordingData["unlockGemSlot"][homeScript.readData.languageId].ToString() + " " + currentPositionUnlockGem;
        int amount = 0;
        amount = currentEquipment.rare * currentEquipment.rare * 1000 * currentPositionUnlockGem;
        unlockMoneyUseText.text = amount + " / " + homeScript.readData.dataUserModel.money;

        bgFade.alpha = 1;
        bgFade.blocksRaycasts = true;
        unlockGemSlotPopup.alpha = 1;
        unlockGemSlotPopup.blocksRaycasts = true;
    }

    public void CloseUnlockGemSlot()
    {
        bgFade.alpha = 0;
        bgFade.blocksRaycasts = false;
        unlockGemSlotPopup.alpha = 0;
        unlockGemSlotPopup.blocksRaycasts = false;
    }

    public void UnlockGemSlotButton()
    {
        if (currentEquipment.rare * currentEquipment.rare * 1000 * currentPositionUnlockGem > homeScript.readData.dataUserModel.money)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughMoney"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.UnlockGemSlot());
        }
    }

    public void PickGemSlot(int position)
    {
        currentPositionPickGem = position;

        if (currentEquipment.dataGemUserId[currentPositionPickGem - 1] != "-1")
        {
            RectTransform currentPickEquipedRectTransform;
            switch (currentPositionPickGem)
            {
                case 1:
                    currentPickEquipedRectTransform = gemSlot1.GetComponent<RectTransform>();
                    break;
                case 2:
                    currentPickEquipedRectTransform = gemSlot2.GetComponent<RectTransform>();
                    break;
                case 3:
                    currentPickEquipedRectTransform = gemSlot3.GetComponent<RectTransform>();
                    break;
                default:
                    currentPickEquipedRectTransform = gemSlot1.GetComponent<RectTransform>();
                    break;
            }

            borderGemEquipedPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(currentPickEquipedRectTransform.anchoredPosition.x, currentPickEquipedRectTransform.anchoredPosition.y);
            borderGemEquipedPickCanvasGroup.alpha = 1;
        }
        gemEquipedDetailCanvasGroup.alpha = 1;
        gemEquipedDetailCanvasGroup.blocksRaycasts = true;
        if (currentEquipment.dataGemUserId[currentPositionPickGem - 1] != "0")
        {
            currentGemEquiped = homeScript.readData.dataUserModel.gem.Find(x => x.gemUserId == currentEquipment.dataGemUserId[currentPositionPickGem - 1]);
            gemEquipedName.text = homeScript.readData.jsonGemData[currentGemEquiped.gemId.ToString()]["name"][homeScript.readData.languageId].ToString();

            gemEquipedStatIcon.sprite = GetStatIcon(homeScript.readData.jsonGemData[currentGemEquiped.gemId.ToString()]["buffType"].ToString());
            gemEquipedStatValue.text = "+" + homeScript.readData.jsonGemData[currentGemEquiped.gemId.ToString()]["buffValue"].ToString() + "%";
            removeMoneyUse.text = (currentGemEquiped.rare * currentGemEquiped.rare * 1000).ToString("#,#") + " / " + homeScript.readData.dataUserModel.money.ToString("#,#");

            GemEquipedDetailisNotEmpty();
        }
        else
        {
            GemEquipedDetailisEmpty();
        }


        gemBoxScrollRectCanvasGroup.alpha = 1;
        gemBoxScrollbarCanvasGroup.alpha = 1;
        gemStockDetailCanvasGroup.alpha = 1;
        backToEquipmentCanvasGroup.alpha = 1;
        gemBoxScrollRectCanvasGroup.blocksRaycasts = true;
        gemBoxScrollbarCanvasGroup.blocksRaycasts = true;
        gemStockDetailCanvasGroup.blocksRaycasts = true;
        backToEquipmentCanvasGroup.blocksRaycasts = true;
        if(gemHave != 0){
            GemStockDetailisNotEmpty();
        }
    }

    public void GemEquipedDetailisNotEmpty()
    {
        gemEquipedName.GetComponent<CanvasRenderer>().SetAlpha(1);
        gemEquipedStatIcon.GetComponent<CanvasRenderer>().SetAlpha(1);
        gemEquipedStatValue.GetComponent<CanvasRenderer>().SetAlpha(1);
        gemEquipedMoneyIcon.GetComponent<CanvasRenderer>().SetAlpha(1);
        removeMoneyUse.GetComponent<CanvasRenderer>().SetAlpha(1);
        gemEquipedRemoveButton.GetComponent<CanvasGroup>().alpha = 1;
        gemEquipedRemoveButton.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void GemEquipedDetailisEmpty()
    {
        gemEquipedName.GetComponent<CanvasRenderer>().SetAlpha(0);
        gemEquipedStatIcon.GetComponent<CanvasRenderer>().SetAlpha(0);
        gemEquipedStatValue.GetComponent<CanvasRenderer>().SetAlpha(0);
        gemEquipedMoneyIcon.GetComponent<CanvasRenderer>().SetAlpha(0);
        removeMoneyUse.GetComponent<CanvasRenderer>().SetAlpha(0);
        gemEquipedRemoveButton.GetComponent<CanvasGroup>().alpha = 0;
        gemEquipedRemoveButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void GemStockDetailisNotEmpty()
    {
        gemStockName.GetComponent<CanvasRenderer>().SetAlpha(1);
        gemStockStatIcon.GetComponent<CanvasRenderer>().SetAlpha(1);
        gemStockStatValue.GetComponent<CanvasRenderer>().SetAlpha(1);
        gemStockMoneyIcon.GetComponent<CanvasRenderer>().SetAlpha(1);
        moneySellText.GetComponent<CanvasRenderer>().SetAlpha(1);
        gemStockEquipButton.GetComponent<CanvasGroup>().alpha = 1;
        gemStockEquipButton.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gemStockSellButton.GetComponent<CanvasGroup>().alpha = 1;
        gemStockSellButton.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void GemStockDetailisEmpty()
    {
        gemStockName.GetComponent<CanvasRenderer>().SetAlpha(0);
        gemStockStatIcon.GetComponent<CanvasRenderer>().SetAlpha(0);
        gemStockStatValue.GetComponent<CanvasRenderer>().SetAlpha(0);
        gemStockMoneyIcon.GetComponent<CanvasRenderer>().SetAlpha(0);
        moneySellText.GetComponent<CanvasRenderer>().SetAlpha(0);
        gemStockEquipButton.GetComponent<CanvasGroup>().alpha = 0;
        gemStockEquipButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gemStockSellButton.GetComponent<CanvasGroup>().alpha = 0;
        gemStockSellButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //borderGemStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-400, 0);
        borderGemStockPick.GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    public void CloseEquipedGemDetail()
    {
        gemEquipedDetailCanvasGroup.alpha = 0;
        gemEquipedDetailCanvasGroup.blocksRaycasts = false;
    }

    public void BackToEquipment()
    {
        gemBoxScrollRectCanvasGroup.alpha = 0;
        gemBoxScrollbarCanvasGroup.alpha = 0;
        gemStockDetailCanvasGroup.alpha = 0;
        backToEquipmentCanvasGroup.alpha = 0;
        borderGemEquipedPickCanvasGroup.alpha = 0;
        gemBoxScrollRectCanvasGroup.blocksRaycasts = false;
        gemBoxScrollbarCanvasGroup.blocksRaycasts = false;
        gemStockDetailCanvasGroup.blocksRaycasts = false;
        backToEquipmentCanvasGroup.blocksRaycasts = false;
        CloseEquipedGemDetail();
    }

    public void RemoveGem()
    {
        if (currentGemEquiped.rare * currentGemEquiped.rare * 1000 > homeScript.readData.dataUserModel.money)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughMoney"][homeScript.readData.languageId].ToString(), null);
        }
        else if (homeScript.readData.dataUserModel.bag_gem <= gemHave)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["yourBagIsFull"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.RemoveGem());
        }
    }

    public void EquipGem()
    {
        if (currentEquipment.dataGemUserId[currentPositionPickGem - 1] != "0")
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["pleaseRemoveEquipedGemFirst"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.EquipGem());
        }
    }

    public void SellGem()
    {
        StartCoroutine(homeScript.readData.SellGem());
    }

    void Update()
    {

    }
}
