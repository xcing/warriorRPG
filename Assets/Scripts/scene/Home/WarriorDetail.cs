using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Globalization;
using System.Linq;

public class WarriorDetail : MonoBehaviour
{

    public Home homeScript;
    public Text header;
    public CanvasGroup warriorSkillTabCanvasGroup;
    public CanvasGroup warriorEquipmentTabCanvasGroup;
    public CanvasGroup warriorUpgradeTabCanvasGroup;
    WarriorList warriorList;

    public Image skillTabButton;
    public Image equipmentTabButton;
    public Image upgradeTabButton;

    public GameObject warriorBody;
    public Image lockWarrior;
    GameObject monsterFile;
    public GameObject monsterEntity;
    public Warrior warrior;
    public string warriorUserId;

    //status
    public Text warriorName;
    public Text lvText;
    public Text lvValue;
    public Image element;
    public Image expGuage;
    public Text expValue;
    public Text hpValue;
    public Text atkValue;
    public Text defValue;
    public Text spdValue;
    public Text luckValue;
    public Text leaderSkill;

    //skill tab
    public Image borderPickSkill;
    public Text skill1Text;
    public Text skill2Text;
    public Text skill3Text;
    public Image skill1Image;
    public Image skill2Image;
    public Image skill3Image;
    public Text skill1Lv;
    public Text skill2Lv;
    public Text skill3Lv;
    public CanvasGroup skill2Lock;
    public CanvasGroup skill3Lock;
    public Text skillDetailText;
    public Text upgradeButtonText;
    public Text upgradeMoney;
    public int currentPickSkill;

    //equipment tab
    public int amountEquipmentsStock;
    public List<Equipment> equipments;
    public int equipmentHave;
    public GameObject equipmentUnitBoxPrefab;
    public GameObject[] equipmentUnitBox;
    public Image equipment1;
    public Image equipment2;
    public Image equipment3;
    public Image equipment4;
    public Image equipment5;
    public Image equipment6;
    public Image equipmentStock;
    public Image equipmentStockBox;
    public Scrollbar scrollbar;
    public Button removeAllButton;
    //public Button autoEquipButton;
    public CanvasGroup equipedDetail;
    public Text equipedName;
    public Image equipedStatIcon1;
    public Text equipedStatValue1;
    public Image equipedStatIcon2;
    public CanvasGroup equipedStatIconCanvasGroup2;
    public Text equipedStatValue2;
    public Image equipedStatIcon3;
    public CanvasGroup equipedStatIconCanvasGroup3;
    public Text equipedStatValue3;
    public Image equipedStatIcon4;
    public CanvasGroup equipedStatIconCanvasGroup4;
    public Text equipedStatValue4;
    public Image equipedGemSlot1;
    public CanvasGroup equipedGemSlotCanvasGroup1;
    public CanvasGroup equipedGemSlotLock1;
    public Image equipedGemSlot2;
    public CanvasGroup equipedGemSlotCanvasGroup2;
    public CanvasGroup equipedGemSlotLock2;
    public Image equipedGemSlot3;
    public CanvasGroup equipedGemSlotCanvasGroup3;
    public CanvasGroup equipedGemSlotLock3;
    public Button equipedUpgradeButton;
    public Button equupedRemoveButton;
    public CanvasGroup equipStockDetail;
    public Text equipStockName;
    public Image equipStockStatIcon1;
    public Text equipStockStatValue1;
    public Image equipStockStatIcon2;
    public CanvasGroup equipStockStatIconCanvasGroup2;
    public Text equipStockStatValue2;
    public Image equipStockStatIcon3;
    public CanvasGroup equipStockStatIconCanvasGroup3;
    public Text equipStockStatValue3;
    public Image equipStockStatIcon4;
    public CanvasGroup equipStockStatIconCanvasGroup4;
    public Text equipStockStatValue4;
    public Image equipStockGemSlot1;
    public CanvasGroup equipStockGemSlotCanvasGroup1;
    public CanvasGroup equipStockGemSlotLock1;
    public Image equipStockGemSlot2;
    public CanvasGroup equipStockGemSlotCanvasGroup2;
    public CanvasGroup equipStockGemSlotLock2;
    public Image equipStockGemSlot3;
    public CanvasGroup equipStockGemSlotCanvasGroup3;
    public CanvasGroup equipStockGemSlotLock3;
    public Button equipStockUpgradeButton;
    public Button equupStockEquipButton;
    public Text equipStockLimitLv;
    public CanvasGroup equipStockLimitLvCanvasGroup;
    public GameObject borderPickPrefab;
    public Image borderEquipedPick;
    public GameObject borderEquipStockPick;
    public int currentPickEquiped;
    public Equipment equiped;
    public Equipment equipStock;
    public CanvasGroup bgFadeStockFullCanvasGroup;
    public CanvasGroup stockFullPopupCanvasGroup;
    public Text stockFullHeader;
    public Text stockFullDetail;
    public Button stockFullOpenStockButton;
    public Text stockFullOpenStockText;
    public Button stockFullUpgradeBagButton;
    public Text stockFullUpgradeBagText;

    //upgrade tab
    public Text lvPlusCurrent;
    public Text lvPlusNew;
    public Button upgradeHistoryButton;
    public Text hpPlusValue;
    public Text atkPlusValue;
    public Text defPlusValue;
    public Text spdPlusValue;
    public Text luckPlusValue;
    public Image skillPlusIcon;
    public Text skillPlusText;
    public Text upgradeWarriorMoneyText;
    public Image warriorSoulImage;
    public Image warriorSoulGuage;
    public Text warriorSoulValue;
    public Button upgradeBySoul;
    public Image warriorStockBox;
    public List<Warrior> warriorsUpgrade;
    public int warriorsUpgradeHave;
    public GameObject warriorStockPrefab;
    public GameObject[] warriorStockUnitBox;
    public GameObject borderWarriorStockPick;
    public string currentPickUpgradeWarrior;
    public Button upgradeByWarrior;
    public bool isUpgradeBySoul;

    //history upgrade
    public CanvasGroup historyUpgradePanel;
    public int historyUpgradeHave;
    public Image historyUpgradeBox;
    public GameObject historyUpgradePrefab;
    public GameObject[] historyUpgradeUnitBox;

    void Start()
    {
        //warriorUserId = "120150504113631"; //guanyu 
        //warriorUserId = "220150504113632"; //zhang fri
        //warriorUserId = "120150504113642"; //guan yu low lv

        header.text = homeScript.readData.jsonWordingData["headerWarriorDetail"][homeScript.readData.languageId].ToString();

        upgradeHistoryButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["upgradeHistory"][homeScript.readData.languageId].ToString();
        upgradeBySoul.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["upgradeBySoul"][homeScript.readData.languageId].ToString();
        upgradeByWarrior.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["upgradeByWarrior"][homeScript.readData.languageId].ToString();

        lvText.text = homeScript.readData.jsonWordingData["lv"][homeScript.readData.languageId].ToString();
        upgradeButtonText.text = homeScript.readData.jsonWordingData["enhance"][homeScript.readData.languageId].ToString();
        equipStockLimitLv.text = homeScript.readData.jsonWordingData["limitLv"][homeScript.readData.languageId].ToString();

        stockFullHeader.text = homeScript.readData.jsonWordingData["yourBagIsFull"][homeScript.readData.languageId].ToString();
        stockFullDetail.text = homeScript.readData.jsonWordingData["stockFullDetail"][homeScript.readData.languageId].ToString();
        stockFullOpenStockText.text = homeScript.readData.jsonWordingData["stockItem"][homeScript.readData.languageId].ToString();
        stockFullUpgradeBagText.text = homeScript.readData.jsonWordingData["upgradeBag"][homeScript.readData.languageId].ToString();

        currentPickEquiped = 1;

        /*monsterFile = (GameObject)Resources.Load("Warriors/Inorog/entity1", typeof(GameObject));
        monsterEntity = Instantiate(monsterFile, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        monsterEntity.transform.parent = warriorBody.transform;
        monsterEntity.transform.position = warriorBody.transform.position;
        monsterEntity.transform.Translate(0, -70, 0);*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetValueWarrior()
    {
        warriorList = GameObject.Find("WarriorListScript").GetComponent<WarriorList>();
        currentPickSkill = 1;
        warrior = homeScript.readData.dataUserModel.warrior.Find(x => x.warriorUserId == warriorUserId);
        if (warrior.isLock)
            lockWarrior.sprite = Resources.Load<Sprite>("UI/lock");
        else
            lockWarrior.sprite = Resources.Load<Sprite>("UI/unlock");
        warriorName.text = warrior.name[homeScript.readData.languageId];
        if (warrior.lvPlus > 0)
        {
            warriorName.text += "+" + warrior.lvPlus.ToString();
        }

        foreach (Transform child in warriorBody.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        monsterFile = (GameObject)Resources.Load("Warriors/" + warrior.name[0] + "/AnimateCharacter/entity_01", typeof(GameObject));
        monsterEntity = Instantiate(monsterFile, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        monsterEntity.transform.parent = warriorBody.transform;
        monsterEntity.transform.position = warriorBody.transform.position;
        monsterEntity.transform.Translate(0, -70, 1);
        monsterEntity.SetActive(true);

        // ----------------------- status ---------------------------
        lvValue.text = warrior.level.ToString();
        element.sprite = Resources.Load<Sprite>("UI/" + homeScript.readData.jsonElementData[warrior.elementId.ToString()]["name"][0] + "Icon");
        expValue.text = warrior.exp.ToString("#,#") + " / " + Convert.ToInt32(homeScript.readData.jsonExpData[warrior.rare.ToString()][warrior.level.ToString()].ToString()).ToString("#,#");
        expGuage.fillAmount = warrior.exp / float.Parse(homeScript.readData.jsonExpData[warrior.rare.ToString()][warrior.level.ToString()].ToString(), CultureInfo.InvariantCulture.NumberFormat);
        hpValue.text = warrior.hp.ToString("#,#");
        atkValue.text = warrior.atk.ToString("#,#");
        defValue.text = warrior.def.ToString("#,#");
        spdValue.text = warrior.spd.ToString("#,#");
        luckValue.text = warrior.luck.ToString("#,#");
        //leaderSkill.text = homeScript.readData.jsonWordingData["leaderSkill"][homeScript.readData.languageId].ToString() + ": " + homeScript.readData.jsonLeaderSkillData[warrior.leaderSkillId.ToString()]["detail"][homeScript.readData.languageId].ToString();

        // ------------------------ skill ---------------------------
        skill1Text.text = warrior.skill[0].name[homeScript.readData.languageId];
        skill2Text.text = warrior.skill[1].name[homeScript.readData.languageId];
        skill3Text.text = warrior.skill[2].name[homeScript.readData.languageId];
        skill1Image.sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/skill1");
        skill2Image.sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/skill2");
        skill3Image.sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/skill3");
        SetLvSkill();

        if (warrior.lvPlus > 2)
        {
            skill2Lock.alpha = 0;
            skill2Lock.blocksRaycasts = false;
        }
        if (warrior.lvPlus > 5)
        {
            skill3Lock.alpha = 0;
            skill3Lock.blocksRaycasts = false;
        }

        amountEquipmentsStock = homeScript.readData.dataUserModel.equipment.FindAll(x => x.warriorUserIdEquiped == "0").Count;
        int i = 0;
        equipment1.sprite = Resources.Load<Sprite>("UI/weaponIcon");
        equipment2.sprite = Resources.Load<Sprite>("UI/armorIcon");
        equipment3.sprite = Resources.Load<Sprite>("UI/helmIcon");
        equipment4.sprite = Resources.Load<Sprite>("UI/bootIcon");
        equipment5.sprite = Resources.Load<Sprite>("UI/accessoryIcon");
        equipment6.sprite = Resources.Load<Sprite>("UI/accessoryIcon");
        foreach (string equipmentUserId in warrior.dataEquipmentUserId)
        {
            if (equipmentUserId != "0")
            {
                equiped = homeScript.readData.dataUserModel.equipment.Find(x => x.equipmentUserId == equipmentUserId);
                switch (i)
                {
                    case 0:
                        equipment1.sprite = Resources.Load<Sprite>("equipments/" + homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["name"][0].ToString());
                        break;
                    case 1:
                        equipment2.sprite = Resources.Load<Sprite>("equipments/" + homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["name"][0].ToString());
                        break;
                    case 2:
                        equipment3.sprite = Resources.Load<Sprite>("equipments/" + homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["name"][0].ToString());
                        break;
                    case 3:
                        equipment4.sprite = Resources.Load<Sprite>("equipments/" + homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["name"][0].ToString());
                        break;
                    case 4:
                        equipment5.sprite = Resources.Load<Sprite>("equipments/" + homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["name"][0].ToString());
                        break;
                    case 5:
                        equipment6.sprite = Resources.Load<Sprite>("equipments/" + homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["name"][0].ToString());
                        break;
                    default:
                        equipment1.sprite = Resources.Load<Sprite>("equipments/" + homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["name"][0].ToString());
                        break;
                }
            }
            i++;
        }
        PickEquiped(currentPickEquiped);

        lvPlusCurrent.text = "+" + warrior.lvPlus.ToString();
        if (warrior.lvPlus < 99)
        {
            lvPlusNew.text = "+" + (warrior.lvPlus + 1).ToString();
            int multiple;
            if ((warrior.lvPlus + 1) % 10 == 9 || (warrior.lvPlus + 1) == 100)
                multiple = 2;
            else
                multiple = 1;
            hpPlusValue.text = "+" + (Convert.ToInt32(warrior.defaultStatWhenPlusLvUpArray[0]) * multiple);
            atkPlusValue.text = "+" + (Convert.ToInt32(warrior.defaultStatWhenPlusLvUpArray[1]) * multiple);
            defPlusValue.text = "+" + (Convert.ToInt32(warrior.defaultStatWhenPlusLvUpArray[2]) * multiple);
            spdPlusValue.text = "+" + (Convert.ToInt32(warrior.defaultStatWhenPlusLvUpArray[3]) * multiple);
            luckPlusValue.text = "+" + (Convert.ToInt32(warrior.defaultStatWhenPlusLvUpArray[4]) * multiple);

            if ((warrior.lvPlus + 1) % 10 == 9 || (warrior.lvPlus + 1) == 100)
            {
                skillPlusIcon.sprite = Resources.Load<Sprite>("UI/statupIcon");
                skillPlusText.text = homeScript.readData.jsonWordingData["increseAllStatus2x"][homeScript.readData.languageId].ToString();
            }
            else if ((warrior.lvPlus + 1) % 10 == 0)
            {
                skillPlusIcon.sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/skill1");
                skillPlusText.text = homeScript.readData.jsonWordingData["increseUseChance"][homeScript.readData.languageId].ToString() + " " + warrior.skill[0].name[homeScript.readData.languageId];
            }
            else if ((warrior.lvPlus + 1) % 10 == 3)
            {
                skillPlusIcon.sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/skill2");
                if (warrior.lvPlus < 10)
                {
                    skillPlusText.text = homeScript.readData.jsonWordingData["learn"][homeScript.readData.languageId].ToString() + " " + warrior.skill[1].name[homeScript.readData.languageId];
                }
                else
                {
                    skillPlusText.text = homeScript.readData.jsonWordingData["increseUseChance"][homeScript.readData.languageId].ToString() + " " + warrior.skill[1].name[homeScript.readData.languageId];
                }
            }
            else if ((warrior.lvPlus + 1) % 10 == 6)
            {
                skillPlusIcon.sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/skill3");
                if (warrior.lvPlus < 10)
                {
                    skillPlusText.text = homeScript.readData.jsonWordingData["learn"][homeScript.readData.languageId].ToString() + " " + warrior.skill[2].name[homeScript.readData.languageId];
                }
                else
                {
                    skillPlusText.text = homeScript.readData.jsonWordingData["increseUseChance"][homeScript.readData.languageId].ToString() + " " + warrior.skill[2].name[homeScript.readData.languageId];
                }
            }
            else
            {
                skillPlusIcon.sprite = Resources.Load<Sprite>("UI/empty");
                skillPlusText.text = homeScript.readData.jsonWordingData["none"][homeScript.readData.languageId].ToString();
            }
            upgradeWarriorMoneyText.text = (warrior.rare * 1000 * (warrior.lvPlus + 1)).ToString("#,#") + " / " + homeScript.readData.dataUserModel.money.ToString("#,#");
        }
        else
        {
            lvPlusNew.text = "-";
            hpPlusValue.text = "-";
            atkPlusValue.text = "-";
            defPlusValue.text = "-";
            spdPlusValue.text = "-";
            luckPlusValue.text = "-";
            skillPlusIcon.sprite = Resources.Load<Sprite>("UI/empty");
            skillPlusText.text = homeScript.readData.jsonWordingData["none"][homeScript.readData.languageId].ToString();
            upgradeWarriorMoneyText.text = "-";
        }

        warriorSoulImage.sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/icon");
        if (homeScript.readData.dataUserModel.warriorSoul.Find(x => x.warriorId == warrior.warriorId) != null)
        {
            warriorSoulValue.text = homeScript.readData.dataUserModel.warriorSoul.Find(x => x.warriorId == warrior.warriorId).amount.ToString() + "/" + (warrior.rare * 10).ToString();
            warriorSoulGuage.fillAmount = (float)homeScript.readData.dataUserModel.warriorSoul.Find(x => x.warriorId == warrior.warriorId).amount / (float)(warrior.rare * 10);
        }
        else
        {
            warriorSoulValue.text = "0/" + (warrior.rare * 10).ToString();
            warriorSoulGuage.fillAmount = 0;
        }

        SetDataWarriorStock();

        SetDataHistoryUpgrade();
    }

    public void SetLvSkill()
    {
        skill1Lv.text = "Lv. " + warrior.skillLv[0];
        skill2Lv.text = "Lv. " + warrior.skillLv[1];
        skill3Lv.text = "Lv. " + warrior.skillLv[2];
        PickSkill(currentPickSkill);
    }

    public void LockWarrior()
    {
        StartCoroutine(homeScript.readData.LockWarrior());
    }

    public void SkillTabButton()
    {
        skillTabButton.sprite = Resources.Load<Sprite>("UI/skill_icon");
        equipmentTabButton.sprite = Resources.Load<Sprite>("UI/equipment_icon_gray");
        upgradeTabButton.sprite = Resources.Load<Sprite>("UI/upgrade_icon_gray");

        warriorSkillTabCanvasGroup.alpha = 1;
        warriorSkillTabCanvasGroup.blocksRaycasts = true;
        warriorEquipmentTabCanvasGroup.alpha = 0;
        warriorEquipmentTabCanvasGroup.blocksRaycasts = false;
        warriorUpgradeTabCanvasGroup.alpha = 0;
        warriorUpgradeTabCanvasGroup.blocksRaycasts = false;
    }

    public void EquipmentTabButton()
    {
        skillTabButton.sprite = Resources.Load<Sprite>("UI/skill_icon_gray");
        equipmentTabButton.sprite = Resources.Load<Sprite>("UI/equipment_icon");
        upgradeTabButton.sprite = Resources.Load<Sprite>("UI/upgrade_icon_gray");

        warriorSkillTabCanvasGroup.alpha = 0;
        warriorSkillTabCanvasGroup.blocksRaycasts = false;
        warriorEquipmentTabCanvasGroup.alpha = 1;
        warriorEquipmentTabCanvasGroup.blocksRaycasts = true;
        warriorUpgradeTabCanvasGroup.alpha = 0;
        warriorUpgradeTabCanvasGroup.blocksRaycasts = false;
    }

    public void UpgradeTabButton()
    {
        skillTabButton.sprite = Resources.Load<Sprite>("UI/skill_icon_gray");
        equipmentTabButton.sprite = Resources.Load<Sprite>("UI/equipment_icon_gray");
        upgradeTabButton.sprite = Resources.Load<Sprite>("UI/upgrade_icon");

        warriorSkillTabCanvasGroup.alpha = 0;
        warriorSkillTabCanvasGroup.blocksRaycasts = false;
        warriorEquipmentTabCanvasGroup.alpha = 0;
        warriorEquipmentTabCanvasGroup.blocksRaycasts = false;
        warriorUpgradeTabCanvasGroup.alpha = 1;
        warriorUpgradeTabCanvasGroup.blocksRaycasts = true;
    }

    public void SetDataEquipmentStock(int equipmentType)
    {
        for (int i = equipmentStockBox.transform.childCount - 1; i > 0; i--)
        {
            GameObject.Destroy(equipmentStockBox.transform.GetChild(i).gameObject);
        }
        Destroy(borderEquipStockPick);

        //equipmentSize = 39;
        equipments = homeScript.readData.dataUserModel.equipment.FindAll(x => (x.equipmentType == equipmentType) && (x.warriorUserIdEquiped == "0") && ((x.warriorId == 0) || (x.warriorId == warrior.warriorId)));
        equipments = equipments.OrderByDescending(x => x.rare).ThenByDescending(y => y.lvPlus).ThenBy(z => z.equipmentId).ToList();
        equipmentHave = equipments.Count;

        equipmentUnitBox = new GameObject[equipmentHave];
        equipmentUnitBoxPrefab = Resources.Load<GameObject>("Prefab/EquipmentUnitBox");
        borderPickPrefab = Resources.Load<GameObject>("Prefab/BorderPick110");
        int heightEquipmentPanelBox;
        if (equipmentHave % 4 == 0)
            heightEquipmentPanelBox = ((equipmentHave / 4) * 120) + 30;
        else
            heightEquipmentPanelBox = (((equipmentHave / 4) + 1) * 120) + 30;
        if (heightEquipmentPanelBox < equipmentStock.rectTransform.rect.height)
        {
            heightEquipmentPanelBox = Convert.ToInt32(equipmentStock.rectTransform.rect.height);
            equipmentStockBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            equipmentStockBox.rectTransform.anchoredPosition = new Vector3(0, (heightEquipmentPanelBox - equipmentStock.rectTransform.rect.height) / -2);
        }
        equipmentStockBox.rectTransform.sizeDelta = new Vector2(equipmentStockBox.rectTransform.rect.width, heightEquipmentPanelBox);

        borderEquipStockPick = Instantiate(borderPickPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        borderEquipStockPick.transform.SetParent(equipmentStockBox.transform, true);
        borderEquipStockPick.transform.localScale = new Vector3(1, 1, 1);
        borderEquipStockPick.transform.position = equipmentStockBox.transform.position;
        borderEquipStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-198, (heightEquipmentPanelBox / 2) - 67);

        removeAllButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["removeAll"][homeScript.readData.languageId].ToString();
        equipedUpgradeButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["enhance"][homeScript.readData.languageId].ToString();
        equupedRemoveButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["remove"][homeScript.readData.languageId].ToString();
        equipStockUpgradeButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["enhance"][homeScript.readData.languageId].ToString();
        equupStockEquipButton.GetComponentInChildren<Text>().text = homeScript.readData.jsonWordingData["equip"][homeScript.readData.languageId].ToString();

        for (int i = 0; i < equipmentHave; i++)
        {
            //if (i < equipmentHave)
            //{
            equipmentUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Equipments/" + homeScript.readData.jsonEquipmentData[equipments[i].equipmentId.ToString()]["name"][0].ToString());
            //}
            //else
            //{
            //    equipmentUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/empty");
            //}
            equipmentUnitBox[i] = Instantiate(equipmentUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            equipmentUnitBox[i].transform.SetParent(equipmentStockBox.transform, true);
            equipmentUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            equipmentUnitBox[i].transform.position = equipmentStockBox.transform.position;
            equipmentUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(((i % 4) * 120) - 198, (-120 * (i / 4)) + (heightEquipmentPanelBox / 2) - 67);
            //if (i < equipmentHave)
            //{
            WarriorDetailEquipmentTempData tempData = equipmentUnitBox[i].GetComponent<WarriorDetailEquipmentTempData>();
            //tempData.equipmentHave = equipmentHave;
            tempData.tempI = i;
            tempData.warriorDetail = this;
            tempData.equipmentUserId = equipments[i].equipmentUserId;
            equipmentUnitBox[i].GetComponent<Button>().onClick.AddListener(() => tempData.PickEquipmentStockDetail());
            if (i == 0)
            {
                tempData.PickEquipmentStockDetail();
            }
            //}
        }
        if (equipmentHave == 0)
        {
            equipStockDetail.alpha = 0;
            equipStockDetail.blocksRaycasts = false;
            borderEquipStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-400, 0);
        }
    }

    public void SetDataWarriorStock()
    {
        for (int i = warriorStockBox.transform.childCount - 1; i > 0; i--)
        {
            GameObject.Destroy(warriorStockBox.transform.GetChild(i).gameObject);
        }
        Destroy(borderWarriorStockPick);

        warriorsUpgrade = homeScript.readData.dataUserModel.warrior.FindAll(x => (x.warriorId == warrior.warriorId) && (x.position == 0) && (x.isLock == false) && (x.warriorUserId != warrior.warriorUserId));
        warriorsUpgrade = warriorsUpgrade.OrderBy(x => x.level).ToList();
        warriorsUpgradeHave = warriorsUpgrade.Count;

        warriorStockUnitBox = new GameObject[warriorsUpgradeHave];
        warriorStockPrefab = Resources.Load<GameObject>("Prefab/WarriorStockPrefab");
        borderWarriorStockPick = Resources.Load<GameObject>("Prefab/BorderPick160");
        //borderWarriorStockPick.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(160, 160);
        int widthWarriorUpgradePanelBox;
        if (warriorsUpgradeHave > 3)
        {
            widthWarriorUpgradePanelBox = (warriorsUpgradeHave * 170);
            warriorStockBox.rectTransform.anchoredPosition = new Vector3((widthWarriorUpgradePanelBox - 520) / 2, 0);
        }
        else
        {
            widthWarriorUpgradePanelBox = 520;
            warriorStockBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        warriorStockBox.rectTransform.sizeDelta = new Vector2(widthWarriorUpgradePanelBox, warriorStockBox.rectTransform.rect.height);

        borderWarriorStockPick = Instantiate(borderWarriorStockPick, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        borderWarriorStockPick.transform.SetParent(warriorStockBox.transform, true);
        borderWarriorStockPick.transform.localScale = new Vector3(1, 1, 1);
        borderWarriorStockPick.transform.position = warriorStockBox.transform.position;
        borderWarriorStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3((widthWarriorUpgradePanelBox / 2) + 90, 30);

        for (int i = 0; i < warriorsUpgradeHave; i++)
        {
            warriorStockPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/icon");

            warriorStockUnitBox[i] = Instantiate(warriorStockPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            warriorStockUnitBox[i].transform.SetParent(warriorStockBox.transform, true);
            warriorStockUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            warriorStockUnitBox[i].transform.position = warriorStockBox.transform.position;
            warriorStockUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3((i * 170) - (widthWarriorUpgradePanelBox / 2) + 90, 30);
            warriorStockUnitBox[i].GetComponentInChildren<Text>().text = "Lv " + warriorsUpgrade[i].level;

            WarriorDetailUpgradeTempData tempData = warriorStockUnitBox[i].GetComponent<WarriorDetailUpgradeTempData>();
            tempData.tempI = i;
            tempData.warriorDetail = this;
            tempData.warriorUserId = warriorsUpgrade[i].warriorUserId;
            warriorStockUnitBox[i].GetComponent<Button>().onClick.AddListener(() => tempData.PickWarriorStockDetail());
            if (i == 0)
            {
                tempData.PickWarriorStockDetail();
            }
        }
        if (warriorsUpgradeHave == 0)
        {
            borderWarriorStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-400, 0);
        }
    }

    public void SetDataHistoryUpgrade()
    {
        for (int i = historyUpgradeBox.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(historyUpgradeBox.transform.GetChild(i).gameObject);
        }

        historyUpgradeHave = warrior.lvPlus + 3;

        historyUpgradeUnitBox = new GameObject[historyUpgradeHave];
        historyUpgradePrefab = Resources.Load<GameObject>("Prefab/HistoryUpgradeUnitBox");
        int heightHistoryUpgradePanelBox;
        if (historyUpgradeHave > 4)
        {
            heightHistoryUpgradePanelBox = historyUpgradeHave * 220;
            historyUpgradeBox.rectTransform.anchoredPosition = new Vector3(0, (heightHistoryUpgradePanelBox - 875) / 2);
        }
        else
        {
            heightHistoryUpgradePanelBox = 875;
            historyUpgradeBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        historyUpgradeBox.rectTransform.sizeDelta = new Vector2(historyUpgradeBox.rectTransform.rect.width, heightHistoryUpgradePanelBox);

        for (int i = 0; i < historyUpgradeHave; i++)
        {
            warriorStockPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Warriors/" + warrior.name[0] + "/icon");

            historyUpgradeUnitBox[i] = Instantiate(historyUpgradePrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            historyUpgradeUnitBox[i].transform.SetParent(historyUpgradeBox.transform, true);
            historyUpgradeUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            historyUpgradeUnitBox[i].transform.position = historyUpgradeBox.transform.position;
            historyUpgradeUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(0, (heightHistoryUpgradePanelBox / 2) - 110 - (i * 220));
            if (i < warrior.lvPlus)
                historyUpgradeUnitBox[i].GetComponent<Image>().color = new Color32(73, 178, 57, 255);
            else
                historyUpgradeUnitBox[i].GetComponent<Image>().color = new Color32(0, 0, 0, 80);

            WarriorDetailHistoryUpgradeTempData tempData = historyUpgradeUnitBox[i].GetComponent<WarriorDetailHistoryUpgradeTempData>();
            tempData.warriorDetail = this;
            tempData.setData((i + 1));
        }
    }

    public void PickSkillLock(int position)
    {
        Debug.Log("Must Upgrade your warrior");
    }

    public void PickSkill(int position)
    {
        currentPickSkill = position;
        RectTransform currentPickSkillRectTransform;

        switch (currentPickSkill)
        {
            case 1:
                currentPickSkillRectTransform = skill1Image.GetComponent<RectTransform>();
                break;
            case 2:
                currentPickSkillRectTransform = skill2Image.GetComponent<RectTransform>();
                break;
            case 3:
                currentPickSkillRectTransform = skill3Image.GetComponent<RectTransform>();
                break;
            default:
                currentPickSkillRectTransform = skill1Image.GetComponent<RectTransform>();
                break;
        }
        borderPickSkill.rectTransform.anchoredPosition = new Vector2(currentPickSkillRectTransform.anchoredPosition.x, currentPickSkillRectTransform.anchoredPosition.y);

        skillDetailText.text = warrior.skill[currentPickSkill - 1].detail[homeScript.readData.languageId] + "\n";
        if (warrior.skill[currentPickSkill - 1].dmg != 0)
        {
            skillDetailText.text += "\n " + homeScript.readData.jsonWordingData["dmg"][homeScript.readData.languageId] + ": " + warrior.skill[currentPickSkill - 1].dmg + "%";
        }
        if (warrior.skill[currentPickSkill - 1].target != 0)
        {
            skillDetailText.text += "\n " + homeScript.readData.jsonWordingData["target"][homeScript.readData.languageId] + ": " + homeScript.readData.jsonTargetData["enemyTarget"][warrior.skill[currentPickSkill - 1].target.ToString()][homeScript.readData.languageId];
        }
        if (warrior.skill[currentPickSkill - 1].conditionId != 0)
        {
            skillDetailText.text += "\n " + homeScript.readData.jsonWordingData["cond"][homeScript.readData.languageId] + ": " + homeScript.readData.jsonConditionData[warrior.skill[currentPickSkill - 1].conditionId.ToString()]["name"][homeScript.readData.languageId];
            skillDetailText.text += "\v " + homeScript.readData.jsonWordingData["chance"][homeScript.readData.languageId] + ": " + warrior.skill[currentPickSkill - 1].condAcc;
        }
        if (warrior.skill[currentPickSkill - 1].selfCondId != 0)
        {
            skillDetailText.text += "\n " + homeScript.readData.jsonWordingData["selfCond"][homeScript.readData.languageId] + ": " + homeScript.readData.jsonConditionData[warrior.skill[currentPickSkill - 1].selfCondId]["name"][homeScript.readData.languageId];
            //skillDetailText.text += "\v " + homeScript.readData.jsonWordingData["selfTarget"][homeScript.readData.languageId] + ": " + homeScript.readData.jsonTargetData["selfTarget"][warrior.skill[currentPickSkill - 1].selfCondTarget.ToString()][homeScript.readData.languageId]; ;
        }
        if (warrior.skill[currentPickSkill - 1].buffType != 0)
        {
            string buffTypeText = "";
            switch (warrior.skill[currentPickSkill - 1].buffType)
            {
                case 1:
                    buffTypeText = homeScript.readData.jsonWordingData["increse"][homeScript.readData.languageId].ToString() + homeScript.readData.jsonWordingData["atk"][homeScript.readData.languageId].ToString();
                    break;
                case 2:
                    buffTypeText = homeScript.readData.jsonWordingData["increse"][homeScript.readData.languageId].ToString() + homeScript.readData.jsonWordingData["def"][homeScript.readData.languageId].ToString();
                    break;
                case 3:
                    buffTypeText = homeScript.readData.jsonWordingData["increse"][homeScript.readData.languageId].ToString() + homeScript.readData.jsonWordingData["spd"][homeScript.readData.languageId].ToString();
                    break;
                case 4:
                    buffTypeText = homeScript.readData.jsonWordingData["increse"][homeScript.readData.languageId].ToString() + homeScript.readData.jsonWordingData["luck"][homeScript.readData.languageId].ToString();
                    break;
                case 5:
                    buffTypeText = homeScript.readData.jsonWordingData["increse"][homeScript.readData.languageId].ToString() + homeScript.readData.jsonWordingData["allStat"][homeScript.readData.languageId].ToString();
                    break;
            }
            if (warrior.skill[currentPickSkill - 1].buffTarget == 1)
            {
                buffTypeText += " " + homeScript.readData.jsonWordingData["forSelf"][homeScript.readData.languageId].ToString();
            }
            else
            {
                buffTypeText += " " + homeScript.readData.jsonWordingData["forTeam"][homeScript.readData.languageId].ToString();
            }
            skillDetailText.text += "\n " + buffTypeText + ": " + warrior.skill[currentPickSkill - 1].buffValue + "%";
            //skillDetailText.text += "\v " + homeScript.readData.jsonWordingData["selfTarget"][homeScript.readData.languageId] + ": " + homeScript.readData.jsonTargetData["selfTarget"][warrior.skill[currentPickSkill - 1].buffTarget.ToString()][homeScript.readData.languageId]; ;
        }
        if (warrior.skill[currentPickSkill - 1].decreseDmgReceive != 0 && warrior.skill[currentPickSkill - 1].decreseDmgReceive != 100)
        {
            skillDetailText.text += "\n " + homeScript.readData.jsonWordingData["blockDmg"][homeScript.readData.languageId].ToString() + ": " + warrior.skill[currentPickSkill - 1].decreseDmgReceive + "%";
        }
        skillDetailText.text += "\n " + homeScript.readData.jsonWordingData["useChance"][homeScript.readData.languageId] + ": " + warrior.skill[currentPickSkill - 1].percentUse + "%";
        upgradeMoney.text = Convert.ToInt32(homeScript.readData.jsonMoneyUpgradeSkillData[warrior.rare.ToString()][currentPickSkill.ToString()][warrior.skillLv[currentPickSkill - 1].ToString()].ToString()).ToString("#,#") + " / " + homeScript.readData.dataUserModel.money.ToString("#,#");
    }

    public void UpradeSkill()
    {
        if (Convert.ToInt32(homeScript.readData.jsonMoneyUpgradeSkillData[warrior.rare.ToString()][currentPickSkill.ToString()][warrior.skillLv[currentPickSkill - 1].ToString()].ToString()) > homeScript.readData.dataUserModel.money)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughMoney"][homeScript.readData.languageId].ToString(), null);
        }
        else if (warrior.skillLv[currentPickSkill - 1] >= warrior.level)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughLevel"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.UpgradeLvSkill());
        }
    }

    public void PickEquiped(int position)
    {
        currentPickEquiped = position;
        int equipmentType;
        if (position == 6)
            equipmentType = 5;
        else
            equipmentType = position;
        SetDataEquipmentStock(equipmentType);

        RectTransform currentPickEquipedRectTransform;
        equipedStatValue1.text = null;
        equipedStatValue2.text = null;
        equipedStatValue3.text = null;
        equipedStatValue4.text = null;
        equipedStatIconCanvasGroup2.alpha = 0;
        equipedStatIconCanvasGroup3.alpha = 0;
        equipedStatIconCanvasGroup4.alpha = 0;
        equipedGemSlotCanvasGroup1.alpha = 0;
        equipedGemSlotCanvasGroup2.alpha = 0;
        equipedGemSlotCanvasGroup3.alpha = 0;
        equipedGemSlotLock1.alpha = 0;
        equipedGemSlotLock2.alpha = 0;
        equipedGemSlotLock3.alpha = 0;

        switch (currentPickEquiped)
        {
            case 1:
                currentPickEquipedRectTransform = equipment1.GetComponent<RectTransform>();
                break;
            case 2:
                currentPickEquipedRectTransform = equipment2.GetComponent<RectTransform>();
                break;
            case 3:
                currentPickEquipedRectTransform = equipment3.GetComponent<RectTransform>();
                break;
            case 4:
                currentPickEquipedRectTransform = equipment4.GetComponent<RectTransform>();
                break;
            case 5:
                currentPickEquipedRectTransform = equipment5.GetComponent<RectTransform>();
                break;
            case 6:
                currentPickEquipedRectTransform = equipment6.GetComponent<RectTransform>();
                break;
            default:
                currentPickEquipedRectTransform = equipment1.GetComponent<RectTransform>();
                break;
        }
        borderEquipedPick.rectTransform.anchoredPosition = new Vector2(currentPickEquipedRectTransform.anchoredPosition.x, currentPickEquipedRectTransform.anchoredPosition.y);

        if (warrior.dataEquipmentUserId[currentPickEquiped - 1] != "0")
        {
            equipedDetail.alpha = 1;
            equipedDetail.blocksRaycasts = true;
            equiped = homeScript.readData.dataUserModel.equipment.Find(x => x.equipmentUserId == warrior.dataEquipmentUserId[currentPickEquiped - 1]);
            equipedName.text = homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["name"][homeScript.readData.languageId].ToString();
            if (equiped.lvPlus > 0)
            {
                equipedName.text += "+" + equiped.lvPlus;
            }
            int i = 1;
            if (homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["hp"].ToString() != "0")
            {
                SetEquipmentStatDetail(i, "hp", true);
                i++;
            }
            if (homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["atk"].ToString() != "0")
            {
                SetEquipmentStatDetail(i, "atk", true);
                i++;
            }
            if (homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["def"].ToString() != "0")
            {
                SetEquipmentStatDetail(i, "def", true);
                i++;
            }
            if (homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["spd"].ToString() != "0")
            {
                SetEquipmentStatDetail(i, "spd", true);
                i++;
            }
            if (homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()]["luck"].ToString() != "0")
            {
                SetEquipmentStatDetail(i, "luck", true);
            }

            i = 1;
            foreach (string gemId in equiped.dataGemUserId)
            {
                SetEquipmentGem(i, gemId, true);
                i++;
            }
        }
        else
        {
            equipedDetail.alpha = 0;
            equipedDetail.blocksRaycasts = false;
        }

    }

    public void SetEquipmentGem(int i, string gemId, bool isEquiped)
    {
        Gem gemTemp = homeScript.readData.dataUserModel.gem.Find(x => x.gemUserId == gemId);
        if (isEquiped)
        {
            switch (i)
            {
                case 1:
                    equipedGemSlotCanvasGroup1.alpha = 1;
                    if (gemId == "-1")
                    {
                        equipedGemSlotLock1.alpha = 1;
                        equipedGemSlot1.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else if (gemId == "0")
                    {
                        equipedGemSlot1.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else
                    {
                        equipedGemSlot1.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                    }
                    break;
                case 2:
                    equipedGemSlotCanvasGroup2.alpha = 1;
                    if (gemId == "-1")
                    {
                        equipedGemSlotLock2.alpha = 1;
                        equipedGemSlot2.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else if (gemId == "0")
                    {
                        equipedGemSlot2.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else
                    {
                        equipedGemSlot2.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                    }
                    break;
                case 3:
                    equipedGemSlotCanvasGroup3.alpha = 1;
                    if (gemId == "-1")
                    {
                        equipedGemSlotLock3.alpha = 1;
                        equipedGemSlot3.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else if (gemId == "0")
                    {
                        equipedGemSlot3.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else
                    {
                        equipedGemSlot3.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                    }
                    break;
            }
        }
        else
        {
            switch (i)
            {
                case 1:
                    equipStockGemSlotCanvasGroup1.alpha = 1;
                    if (gemId == "-1")
                    {
                        equipStockGemSlotLock1.alpha = 1;
                        equipStockGemSlot1.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else if (gemId == "0")
                    {
                        equipStockGemSlot1.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else
                    {
                        equipStockGemSlotLock1.alpha = 0;
                        equipStockGemSlot1.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                    }
                    break;
                case 2:
                    equipStockGemSlotCanvasGroup2.alpha = 1;
                    if (gemId == "-1")
                    {
                        equipStockGemSlotLock2.alpha = 1;
                        equipStockGemSlot2.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else if (gemId == "0")
                    {
                        equipStockGemSlot2.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else
                    {
                        equipStockGemSlot2.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                    }
                    break;
                case 3:
                    equipStockGemSlotCanvasGroup3.alpha = 1;
                    if (gemId == "-1")
                    {
                        equipStockGemSlotLock3.alpha = 1;
                        equipStockGemSlot3.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else if (gemId == "0")
                    {
                        equipStockGemSlot3.sprite = Resources.Load<Sprite>("UI/empty");
                    }
                    else
                    {
                        equipStockGemSlot3.sprite = Resources.Load<Sprite>("Gems/" + homeScript.readData.jsonGemData[gemTemp.gemId.ToString()]["name"][0].ToString());
                    }
                    break;
            }
        }
    }

    public void SetEquipmentStatDetail(int i, string statTemp, bool isEquiped) //stat 1 = hp, 2 = atk, 3 = def, 4 = spd. 5 = luck
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
        if (isEquiped)
        {
            switch (i)
            {
                case 1:
                    equipedStatIcon1.sprite = spriteTemp;
                    equipedStatValue1.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()][statTemp].ToString()) + equiped.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                    break;
                case 2:
                    equipedStatIcon2.sprite = spriteTemp;
                    equipedStatValue2.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()][statTemp].ToString()) + equiped.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                    equipedStatIconCanvasGroup2.alpha = 1;
                    break;
                case 3:
                    equipedStatIcon3.sprite = spriteTemp;
                    equipedStatValue3.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()][statTemp].ToString()) + equiped.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                    equipedStatIconCanvasGroup3.alpha = 1;
                    break;
                case 4:
                    equipedStatIcon4.sprite = spriteTemp;
                    equipedStatValue4.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()][statTemp].ToString()) + equiped.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[equiped.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                    equipedStatIconCanvasGroup3.alpha = 1;
                    break;
            }
        }
        else
        {
            switch (i)
            {
                case 1:
                    equipStockStatIcon1.sprite = spriteTemp;
                    equipStockStatValue1.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()][statTemp].ToString()) + equipStock.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                    break;
                case 2:
                    equipStockStatIcon2.sprite = spriteTemp;
                    equipStockStatValue2.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()][statTemp].ToString()) + equipStock.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                    equipStockStatIconCanvasGroup2.alpha = 1;
                    break;
                case 3:
                    equipStockStatIcon3.sprite = spriteTemp;
                    equipStockStatValue3.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()][statTemp].ToString()) + equipStock.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                    equipStockStatIconCanvasGroup3.alpha = 1;
                    break;
                case 4:
                    equipStockStatIcon4.sprite = spriteTemp;
                    equipStockStatValue4.text = (Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()][statTemp].ToString()) + equipStock.lvPlus * Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()][statPlusTemp].ToString())).ToString("#,#");
                    equipStockStatIconCanvasGroup4.alpha = 1;
                    break;
            }
        }
    }

    public void UpgradeEquipedButton()
    {
        EquipmentList equipmentList = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();
        equipmentList.SetDataEquipmentStock(equipmentList.equipments.IndexOf(equipmentList.equipments.Find(x => x.equipmentUserId == equiped.equipmentUserId)));
        homeScript.OpenEquipmentList();
    }

    public void RemoveEquipedButton()
    {
        if (homeScript.readData.dataUserModel.bag_equipment <= amountEquipmentsStock)
        {
            bgFadeStockFullCanvasGroup.alpha = 1;
            bgFadeStockFullCanvasGroup.blocksRaycasts = true;
            stockFullPopupCanvasGroup.alpha = 1;
            stockFullPopupCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            StartCoroutine(homeScript.readData.RemoveEquipment());
        }
    }

    public void UpgradeEquipStockButton()
    {
        EquipmentList equipmentList = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();
        equipmentList.SetDataEquipmentStock(equipmentList.equipments.IndexOf(equipmentList.equipments.Find(x => x.equipmentUserId == equipStock.equipmentUserId)));
        homeScript.OpenEquipmentList();
    }

    public void EquipEquipStockButton()
    {
        if (warrior.level < Convert.ToInt32(homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()]["limitLv"].ToString()))
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["warriorLimitLv"][homeScript.readData.languageId].ToString() + " " + homeScript.readData.jsonEquipmentData[equipStock.equipmentId.ToString()]["limitLv"].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.EquipEquipment());
        }
    }

    public void RemoveAllEquipedButton()
    {
        int amountEquiped = 0;
        foreach (string equipmentUserId in warrior.dataEquipmentUserId)
        {
            if (equipmentUserId != "0")
            {
                amountEquiped++;
            }
        }
        if (homeScript.readData.dataUserModel.bag_equipment < amountEquipmentsStock + amountEquiped)
        {
            bgFadeStockFullCanvasGroup.alpha = 1;
            bgFadeStockFullCanvasGroup.blocksRaycasts = true;
            stockFullPopupCanvasGroup.alpha = 1;
            stockFullPopupCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            StartCoroutine(homeScript.readData.RemoveAllEquipment());
        }
    }

    public void RemoveAllEquipedSacrificeWarrior()
    {
        int amountEquiped = 0;
        foreach (string equipmentUserId in warriorsUpgrade.Find(x => x.warriorUserId == currentPickUpgradeWarrior).dataEquipmentUserId)
        {
            if (equipmentUserId != "0")
            {
                amountEquiped++;
            }
        }
        if (homeScript.readData.dataUserModel.bag_equipment < amountEquipmentsStock + amountEquiped)
        {
            bgFadeStockFullCanvasGroup.alpha = 1;
            bgFadeStockFullCanvasGroup.blocksRaycasts = true;
            stockFullPopupCanvasGroup.alpha = 1;
            stockFullPopupCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            StartCoroutine(homeScript.readData.RemoveAllEquipmentFromSacrificeWarrior());
        }
    }

    public void OpenStock()
    {
        EquipmentList equipmentList = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();
        equipmentList.SetDataEquipmentStock(0);
        homeScript.OpenEquipmentList();
    }

    public void UpgradeStock()
    {
        if (homeScript.readData.dataUserModel.diamond < 5)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughDiamond"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.UpgradeEquipmentBagFromWarriorDetailPage());
        }
    }

    public void CloseStockFullPopup()
    {
        bgFadeStockFullCanvasGroup.alpha = 0;
        bgFadeStockFullCanvasGroup.blocksRaycasts = false;
        stockFullPopupCanvasGroup.alpha = 0;
        stockFullPopupCanvasGroup.blocksRaycasts = false;
    }

    public void ShowHistoryUpgradeWarrior()
    {
        bgFadeStockFullCanvasGroup.alpha = 1;
        bgFadeStockFullCanvasGroup.blocksRaycasts = true;
        historyUpgradePanel.alpha = 1;
        historyUpgradePanel.blocksRaycasts = true;
        monsterEntity.SetActive(false);
    }

    public void CloseHistoryUpgrade()
    {
        bgFadeStockFullCanvasGroup.alpha = 0;
        bgFadeStockFullCanvasGroup.blocksRaycasts = false;
        historyUpgradePanel.alpha = 0;
        historyUpgradePanel.blocksRaycasts = false;
        monsterEntity.SetActive(true);
    }

    public void UpgradeBySoul()
    {
        if (homeScript.readData.dataUserModel.warriorSoul.Find(x => x.warriorId == warrior.warriorId).amount < warrior.rare * 10)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughSoul"][homeScript.readData.languageId].ToString(), null);
        }
        else if (warrior.rare * 1000 * (warrior.lvPlus + 1) > homeScript.readData.dataUserModel.money)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughMoney"][homeScript.readData.languageId].ToString(), null);
        }
        else if (warrior.lvPlus >= warrior.level)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughLevel"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            isUpgradeBySoul = true;
            StartCoroutine(homeScript.readData.UpgradeLvPlusWarrior());
        }
    }

    public void UpgradeByWarrior()
    {
        if (warriorsUpgradeHave == 0)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notHaveWarrior"][homeScript.readData.languageId].ToString(), null);
        }
        else if (warriorsUpgrade.Find(x => x.warriorUserId == currentPickUpgradeWarrior).lvPlus > warrior.lvPlus)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["cannotSacrificeHigherWarrior"][homeScript.readData.languageId].ToString(), null);
        }
        else if (warriorsUpgrade.Find(x => x.warriorUserId == currentPickUpgradeWarrior).lvPlus == warrior.lvPlus && warriorsUpgrade.Find(x => x.warriorUserId == currentPickUpgradeWarrior).level > warrior.level)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["cannotSacrificeHigherWarrior"][homeScript.readData.languageId].ToString(), null);
        }
        else if (warrior.rare * 1000 * (warrior.lvPlus + 1) > homeScript.readData.dataUserModel.money)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughMoney"][homeScript.readData.languageId].ToString(), null);
        }
        else if (warrior.lvPlus >= warrior.level)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughLevel"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            isUpgradeBySoul = false;
            RemoveAllEquipedSacrificeWarrior();
            //StartCoroutine(homeScript.readData.UpgradeLvPlusWarrior());
        }
    }

    public void ChangeWarrior(bool isLeft)
    {
        if (isLeft)
        {
            if (warriorList.warriorI > 0)
                warriorList.warriorI--;
            else
                warriorList.warriorI = warriorList.warriorHave - 1;
        }
        else
        {
            if (warriorList.warriorI == (warriorList.warriorHave-1))
                warriorList.warriorI = 0;
            else
                warriorList.warriorI++;
        }
        warriorUserId = warriorList.warriors[warriorList.warriorI].warriorUserId;
        SetValueWarrior();
    }
}
