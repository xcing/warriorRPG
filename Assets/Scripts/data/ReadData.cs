using UnityEngine;
using System;
using System.IO;
using System.Collections;
using LitJson;
using UnityEngine.UI;
using System.Collections.Generic;

public class ReadData : MonoBehaviour
{
    public string userId;
    public int languageId;
    public static string inp_ln;
    public FileData fileData;
    public DataUser dataUserModel;
    public JsonData jsonWarriorData;
    public JsonData jsonSkillData;
    public JsonData jsonLeaderSkillData;
    public JsonData jsonWordingData;
    public JsonData jsonConditionData;
    public JsonData jsonEquipmentData;
    public JsonData jsonGemData;
    public JsonData jsonGemSortData;
    public JsonData jsonElementData;
    public JsonData jsonExpData;
    public JsonData jsonTargetData;
    public JsonData jsonMoneyUpgradeSkillData;
    public JsonData jsonAreaData;
    public JsonData jsonStageData;
    public JsonData jsonStageNormalData;
    public JsonData jsonEnemyData;
    public JsonData jsonEnemySkillData;
    public JsonData jsonQuestData;

    public CanvasGroup loadingPanelCanvasGroup;
    public CanvasGroup loadingCanvasGroup;
    public CanvasGroup internetProbPanelCanvasGroup;
    int serviceNo;
    //MonsterController MonsterController;
    bool notPass = false;
    Encryption encryption;
    string signatureKey = "XW@rRl3Er";

    public float[] castTimeConstantValueArray = new float[5];

    void Awake()
    {
        userId = "20150504101235123451";
        languageId = 1;

        castTimeConstantValueArray[0] = 5.0f;
        castTimeConstantValueArray[1] = 4.0f;
        castTimeConstantValueArray[2] = 3.0f;
        castTimeConstantValueArray[3] = 2.0f;
        castTimeConstantValueArray[4] = 1.0f;

        jsonWarriorData = GetDataOnFile("warriors");
        jsonSkillData = GetDataOnFile("skill");
        jsonLeaderSkillData = GetDataOnFile("leaderskill");
        jsonWordingData = GetDataOnFile("wording");
        jsonConditionData = GetDataOnFile("condition");
        jsonEquipmentData = GetDataOnFile("equipment");
        jsonGemData = GetDataOnFile("gem");
        jsonGemSortData = GetDataOnFile("gemsort");
        jsonElementData = GetDataOnFile("element");
        jsonExpData = GetDataOnFile("exp");
        jsonTargetData = GetDataOnFile("target");
        jsonMoneyUpgradeSkillData = GetDataOnFile("moneyUpgradeSkill");
        jsonAreaData = GetDataOnFile("area");
        jsonStageData = GetDataOnFile("stage");
        jsonStageNormalData = GetDataOnFile("stageNormal");
        jsonEnemyData = GetDataOnFile("enemy");
        jsonEnemySkillData = GetDataOnFile("enemySkill");
        jsonQuestData = GetDataOnFile("quest");

        GameObject.Find("InternetProbTopic").GetComponent<Text>().text = (string)jsonWordingData["internetProb"][languageId];
        GameObject.Find("InternetProbTryAgainButton").GetComponentInChildren<Text>().text = (string)jsonWordingData["tryAgain"][languageId];
        GameObject.Find("InternetProbCancelButton").GetComponentInChildren<Text>().text = (string)jsonWordingData["cancel"][languageId];

        encryption = new Encryption();
        if (Application.loadedLevelName != "Login")
        {
            GetDataUserOnFile();
        }
    }

    public void OpenLoading()
    {
        loadingPanelCanvasGroup.alpha = 1;
        loadingPanelCanvasGroup.blocksRaycasts = true;
        loadingCanvasGroup.alpha = 1;
        loadingCanvasGroup.blocksRaycasts = true;
    }

    public void CloseLoading()
    {
        loadingPanelCanvasGroup.alpha = 0;
        loadingPanelCanvasGroup.blocksRaycasts = false;
        loadingCanvasGroup.alpha = 0;
        loadingCanvasGroup.blocksRaycasts = false;
    }

    public JsonData GetDataOnFile(string filename)
    {
        TextAsset ta = Resources.Load<TextAsset>("textFile/" + filename);
        JsonData jsonData = JsonMapper.ToObject(ta.text);

        return jsonData;
    }

    /*public JsonData GetDataOnFile(string filename)
    {
        TextAsset ta = Resources.Load<TextAsset>("textFile/" + filename);
        string[] lines = ta.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        JsonData jsonData = JsonMapper.ToObject(lines[0]);

        return jsonData;
    }*/

    public void GetDataUserOnFile()
    {
        if (File.Exists(Application.persistentDataPath + "/userData.dat"))
        {
            dataUserModel = fileData.LoadDataUser();
        }
    }

    public MoneyStore GetMoneyStoreOnFile()
    {
        MoneyStore moneyStore;
        if (File.Exists(Application.persistentDataPath + "/moneyStore.dat"))
        {
            moneyStore = fileData.LoadMoneyStore();
            if (moneyStore.lastRefreshDate != DateTime.Now.ToString("yyyy/MM/dd"))
            {
                moneyStore = new MoneyStore();
                moneyStore.RandomItem();
            }
        }
        else
        {
            moneyStore = new MoneyStore();
            moneyStore.RandomItem();
        }
        SaveMoneyStore(moneyStore);
        return moneyStore;
    }

    public void SaveMoneyStore(MoneyStore moneyStore)
    {
        fileData.SaveMoneyStore(moneyStore);
    }

    public void TryAgain()
    {
        internetProbPanelCanvasGroup.alpha = 0;
        internetProbPanelCanvasGroup.blocksRaycasts = false;
        switch (serviceNo)
        {
            case 0:
                StartCoroutine(GetDataUserOnService(userId, true));
                break;
            case 1:
                StartCoroutine(UpgradeLvSkill());
                break;
            case 2:
                StartCoroutine(RemoveEquipment());
                break;
            case 3:
                StartCoroutine(EquipEquipment());
                break;
            case 4:
                StartCoroutine(RemoveAllEquipment());
                break;
            case 5:
                StartCoroutine(LockWarrior());
                break;
            case 6:
                StartCoroutine(UpgradeLvPlusWarrior());
                break;
            case 7:
                StartCoroutine(EnhanceEquipment());
                break;
            case 8:
                StartCoroutine(FullEnhanceEquipment());
                break;
            case 9:
                StartCoroutine(FuseEquipment());
                break;
            case 10:
                StartCoroutine(SellEquipment());
                break;
            case 11:
                StartCoroutine(UnlockGemSlot());
                break;
            case 12:
                StartCoroutine(UpgradeEquipmentBag());
                break;
            case 13:
                StartCoroutine(RemoveGem());
                break;
            case 14:
                StartCoroutine(EquipGem());
                break;
            case 15:
                StartCoroutine(SellGem());
                break;
            case 16:
                StartCoroutine(UpgradeWarriorBag());
                break;
            case 17:
                StartCoroutine(ChangePositionTeam());
                break;
            case 18:
                StartCoroutine(BuyItemFromMoneyStore());
                break;
            case 19:
                StartCoroutine(RefreshMoneyStore());
                break;
            case 20:
                StartCoroutine(UpgradeBagFromMoneyShop());
                break;
            case 21:
                StartCoroutine(RemoveAllEquipmentFromSacrificeWarrior());
                break;
            case 22:
                StartCoroutine(UpgradeEquipmentBagFromWarriorDetailPage());
                break;
            case 23:
                StartCoroutine(CombineGem());
                break;
            case 24:
                StartCoroutine(UpgradeBagFromInbox());
                break;
            case 25:
                StartCoroutine(ReceiptMail());
                break;
            case 26:
                StartCoroutine(UpgradeWarriorBagFromOpenChestPage());
                break;
            case 27:
                StartCoroutine(OpenChest());
                break;
            case 28:
                StartCoroutine(AddFriend());
                break;
            case 29:
                StartCoroutine(AcceptFriend());
                break;
            case 30:
                StartCoroutine(CancelFriend());
                break;
            case 31:
                StartCoroutine(SendHeartToFriend());
                break;
            case 32:
                StartCoroutine(SendHeartAllFriend());
                break;
            default:
                break;
        }
    }

    public void Cancel()
    {
        CloseLoading();
        internetProbPanelCanvasGroup.alpha = 0;
        internetProbPanelCanvasGroup.blocksRaycasts = false;
    }

    public IEnumerator GetDataUserOnService(string userId, bool isFirstScene)
    {
        if (!File.Exists(Application.persistentDataPath + "/userData.dat") || isFirstScene)
        {
            OpenLoading();

            string encryptData = encryption.EncryptRJ256("getdatauser" + "|" + signatureKey + "|" + userId);
            WWWForm form = new WWWForm();
            form.AddField("data", encryptData);
            string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
            WWW www = new WWW(url, form);

            yield return www;
            notPass = false;

            if (www.error == null)
            {
                JsonData jsonData = JsonMapper.ToObject(www.text);

                string signature = encryption.DecryptRJ256((string)jsonData["signature"]);
                string signatureValue = jsonData["userId"].ToString() + jsonData["level"].ToString() + jsonData["diamond"].ToString() + jsonData["money"].ToString() + jsonData["ore"].ToString() + jsonData["fame"].ToString() + jsonData["heart"].ToString() + signatureKey;
                if (signature.CompareTo(signatureValue) == 1)
                {
                    dataUserModel = JsonMapper.ToObject<DataUser>(www.text);
                    for (int i = 0; i < dataUserModel.warrior.Count; i++)
                    {
                        dataUserModel.warrior[i].SetupData(this);
                    }
                    for (int i = 0; i < dataUserModel.equipment.Count; i++)
                    {
                        dataUserModel.equipment[i].SetupData(jsonEquipmentData);
                    }
                    for (int i = 0; i < dataUserModel.gem.Count; i++)
                    {
                        dataUserModel.gem[i].SetupData(jsonGemData);
                    }
                    fileData.SaveDataUser(dataUserModel);

                    CloseLoading();
                }
                else
                {
                    notPass = true;
                }
            }
            else
            {
                notPass = true;
            }
            if (notPass)
            {
                this.userId = userId;
                serviceNo = 0;
                internetProbPanelCanvasGroup.alpha = 1;
                internetProbPanelCanvasGroup.blocksRaycasts = true;
            }
        }
        else
        {
            dataUserModel = fileData.LoadDataUser();
            yield return null;
        }

    }

    public IEnumerator UpgradeLvSkill()
    {
        WarriorDetail controller = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradelvskill" + "|" + signatureKey + "|" + userId + "|" + controller.warriorUserId + "|" + controller.currentPickSkill);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                Warrior warrior = dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId);
                dataUserModel.money -= Convert.ToInt32(jsonMoneyUpgradeSkillData[warrior.rare.ToString()][controller.currentPickSkill.ToString()][warrior.skillLv[controller.currentPickSkill - 1].ToString()].ToString());
                warrior.skillLv[controller.currentPickSkill - 1]++;
                fileData.SaveDataUser(dataUserModel);

                controller.warrior.skill[controller.currentPickSkill - 1].calculateStatSKill(warrior.skillLv[controller.currentPickSkill - 1], warrior.skillLvPlus[controller.currentPickSkill - 1]);
                controller.SetLvSkill();
                controller.homeScript.OpenAlertMessage(jsonWordingData["completeUpgradeSkill"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 1;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator RemoveEquipment()
    {
        WarriorDetail controller = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("removeequipment" + "|" + signatureKey + "|" + userId + "|" + controller.warriorUserId + "|" + controller.currentPickEquiped);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).dataEquipmentUserId[controller.currentPickEquiped - 1] = "0";
                dataUserModel.equipment.Find(x => x.equipmentUserId == controller.equiped.equipmentUserId).warriorUserIdEquiped = "0";
                dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).SetDataFromLevelAndEquipment(this);
                fileData.SaveDataUser(dataUserModel);

                controller.SetValueWarrior();

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 2;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator RemoveAllEquipment()
    {
        WarriorDetail controller = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("removeallequipment" + "|" + signatureKey + "|" + userId + "|" + controller.warriorUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                foreach (string equipmentUserId in controller.warrior.dataEquipmentUserId)
                {
                    if (equipmentUserId != "0")
                    {
                        dataUserModel.equipment.Find(x => x.equipmentUserId == equipmentUserId).warriorUserIdEquiped = "0";
                    }
                }
                for (int i = 0; i < 6; i++)
                {
                    dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).dataEquipmentUserId[i] = "0";
                }

                dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).SetDataFromLevelAndEquipment(this);
                fileData.SaveDataUser(dataUserModel);

                controller.SetValueWarrior();

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 4;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator EquipEquipment()
    {
        WarriorDetail controller = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("equipequipment" + "|" + signatureKey + "|" + userId + "|" + controller.warriorUserId + "|" + controller.currentPickEquiped + "|" + controller.equipStock.equipmentUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.equipment.Find(x => x.equipmentUserId == controller.equipStock.equipmentUserId).warriorUserIdEquiped = controller.warriorUserId;
                if (dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).dataEquipmentUserId[controller.currentPickEquiped - 1] != "0")
                {
                    dataUserModel.equipment.Find(x => x.equipmentUserId == controller.equiped.equipmentUserId).warriorUserIdEquiped = "0";
                }
                dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).dataEquipmentUserId[controller.currentPickEquiped - 1] = controller.equipStock.equipmentUserId;
                dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).SetDataFromLevelAndEquipment(this);
                fileData.SaveDataUser(dataUserModel);

                controller.SetValueWarrior();

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 3;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator LockWarrior()
    {
        WarriorDetail controller = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("lockwarrior" + "|" + signatureKey + "|" + userId + "|" + controller.warriorUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                if (controller.warrior.isLock)
                {
                    dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).isLock = false;
                    controller.lockWarrior.sprite = Resources.Load<Sprite>("UI/unlock");
                    controller.warrior.isLock = false;
                    controller.homeScript.OpenAlertMessage(jsonWordingData["completeUnlock"][languageId].ToString(), null);
                }
                else
                {
                    dataUserModel.warrior.Find(x => x.warriorUserId == controller.warriorUserId).isLock = true;
                    controller.lockWarrior.sprite = Resources.Load<Sprite>("UI/lock");
                    controller.warrior.isLock = true;
                    controller.homeScript.OpenAlertMessage(jsonWordingData["completeLock"][languageId].ToString(), null);
                }
                fileData.SaveDataUser(dataUserModel);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 5;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UpgradeLvPlusWarrior()
    {
        WarriorDetail controller = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradelvpluswarrior" + "|" + signatureKey + "|" + userId + "|" + controller.warriorUserId + "|" + controller.isUpgradeBySoul + "|" + controller.currentPickUpgradeWarrior);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                controller.warrior.lvPlus++;
                if (controller.warrior.lvPlus == 3)
                {
                    controller.warrior.skillLv[1] = 1;
                }
                else if (controller.warrior.lvPlus == 6)
                {
                    controller.warrior.skillLv[2] = 1;
                }
                else if (controller.warrior.lvPlus > 7 && controller.warrior.lvPlus % 10 == 0)
                {
                    controller.warrior.skillLvPlus[0]++;
                }
                else if (controller.warrior.lvPlus > 7 && controller.warrior.lvPlus % 10 == 3)
                {
                    controller.warrior.skillLvPlus[1]++;
                }
                else if (controller.warrior.lvPlus > 7 && controller.warrior.lvPlus % 10 == 6)
                {
                    controller.warrior.skillLvPlus[2]++;
                }
                dataUserModel.money -= controller.warrior.rare * 1000 * controller.warrior.lvPlus;

                if (controller.isUpgradeBySoul)
                {
                    dataUserModel.warriorSoul.Find(x => x.warriorId == controller.warrior.warriorId).amount -= controller.warrior.rare * 10;
                }
                else
                {
                    dataUserModel.warrior.Remove(dataUserModel.warrior.Find(x => x.warriorUserId == controller.currentPickUpgradeWarrior));
                    controller.SetDataWarriorStock();
                }

                fileData.SaveDataUser(dataUserModel);
                controller.warrior.SetSkillDataFromUserData(this);
                controller.warrior.SetDataFromLevelAndEquipment(this);
                controller.SetValueWarrior();
                controller.homeScript.OpenAlertMessage(jsonWordingData["completeUpgradeWarrior"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 6;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator EnhanceEquipment()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("enhanceequipment" + "|" + signatureKey + "|" + userId + "|" + controller.currentEquipment.equipmentUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.ore -= Convert.ToInt32(controller.amountOreEnhance.text.ToString().Replace(",", ""));
                dataUserModel.equipment.Find(x => x.equipmentUserId == controller.currentEquipment.equipmentUserId).lvPlus++;

                fileData.SaveDataUser(dataUserModel);
                controller.equipmentUnitBox[controller.currentEquipmentI].GetComponent<EquipmentListTempData>().PickEquipmentStockDetail();
                controller.homeScript.OpenAlertMessage(jsonWordingData["completeEnhance"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 7;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator FullEnhanceEquipment()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("fullenhanceequipment" + "|" + signatureKey + "|" + userId + "|" + controller.currentEquipment.equipmentUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.ore -= Convert.ToInt32(controller.amountOreFullEnhance.text.ToString().Replace(",", ""));
                dataUserModel.equipment.Find(x => x.equipmentUserId == controller.currentEquipment.equipmentUserId).lvPlus = dataUserModel.level;

                fileData.SaveDataUser(dataUserModel);
                controller.equipmentUnitBox[controller.currentEquipmentI].GetComponent<EquipmentListTempData>().PickEquipmentStockDetail();
                controller.homeScript.OpenAlertMessage(jsonWordingData["completeEnhance"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 8;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator FuseEquipment()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("fuseequipment" + "|" + signatureKey + "|" + userId + "|" + controller.currentEquipment.equipmentUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.ore += Convert.ToInt32(controller.amountOreFuse.text.ToString().Replace(",", ""));
                dataUserModel.equipment.Remove(dataUserModel.equipment.Find(x => x.equipmentUserId == controller.currentEquipment.equipmentUserId));

                fileData.SaveDataUser(dataUserModel);
                if (controller.currentEquipmentI == (controller.equipmentHave - 1))
                {
                    controller.currentEquipmentI--;
                }
                controller.homeScript.OpenAlertMessage(jsonWordingData["get"][languageId].ToString() + jsonWordingData["ore"][languageId].ToString() + " " + controller.amountOreFuse.text, null);
                controller.SetDataEquipmentStock(controller.currentEquipmentI);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 9;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator SellEquipment()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("sellequipment" + "|" + signatureKey + "|" + userId + "|" + controller.currentEquipment.equipmentUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.money += Convert.ToInt32(controller.amountMoneySell.text.ToString().Replace(",", ""));
                dataUserModel.equipment.Remove(dataUserModel.equipment.Find(x => x.equipmentUserId == controller.currentEquipment.equipmentUserId));

                fileData.SaveDataUser(dataUserModel);
                if (controller.currentEquipmentI == (controller.equipmentHave - 1))
                {
                    controller.currentEquipmentI--;
                }
                controller.homeScript.OpenAlertMessage(jsonWordingData["get"][languageId].ToString() + jsonWordingData["money"][languageId].ToString() + " " + controller.amountMoneySell.text, null);
                controller.SetDataEquipmentStock(controller.currentEquipmentI);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 10;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UnlockGemSlot()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("unlockgemslot" + "|" + signatureKey + "|" + userId + "|" + controller.currentEquipment.equipmentUserId + "|" + controller.currentPositionUnlockGem);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.money -= controller.currentEquipment.rare * controller.currentEquipment.rare * 1000 * controller.currentPositionUnlockGem;
                dataUserModel.equipment.Find(x => x.equipmentUserId == controller.currentEquipment.equipmentUserId).dataGemUserId[controller.currentPositionUnlockGem - 1] = "0";

                fileData.SaveDataUser(dataUserModel);
                controller.equipmentUnitBox[controller.currentEquipmentI].GetComponent<EquipmentListTempData>().PickEquipmentStockDetail();
                controller.CloseUnlockGemSlot();
                controller.homeScript.OpenAlertMessage(jsonWordingData["completeUnlockGemslot"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 11;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UpgradeEquipmentBag()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradeequipmentbag" + "|" + signatureKey + "|" + userId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.bag_equipment += 5;
                dataUserModel.diamond -= 5;

                fileData.SaveDataUser(dataUserModel);
                controller.SetDataEquipmentStock(0);
                controller.CloseUpgradeEquipmentBag();
                controller.homeScript.OpenAlertMessage(jsonWordingData["completeUpgradeEquipmentBag"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 12;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UpgradeWarriorBag()
    {
        WarriorList controller = GameObject.Find("WarriorListScript").GetComponent<WarriorList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradewarriorbag" + "|" + signatureKey + "|" + userId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.bag_warrior += 5;
                dataUserModel.diamond -= 5;

                fileData.SaveDataUser(dataUserModel);
                controller.SetDataWarriorStock();
                controller.CloseUpgradeWarriorBag();
                controller.homeScript.OpenAlertMessage(jsonWordingData["completeUpgradeWarriorBag"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 16;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UpgradeWarriorBagFromTeamPage()
    {
        Team controller = GameObject.Find("TeamScript").GetComponent<Team>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradewarriorbag" + "|" + signatureKey + "|" + userId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.bag_warrior += 5;
                dataUserModel.diamond -= 5;

                fileData.SaveDataUser(dataUserModel);
                controller.SetDataWarriorStock();
                controller.CloseUpgradeWarriorBag();
                controller.OpenAlertMessage(jsonWordingData["completeUpgradeWarriorBag"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 16;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator RemoveGem()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("removegem" + "|" + signatureKey + "|" + userId + "|" + controller.currentEquipment.equipmentUserId + "|" + controller.currentPositionPickGem);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                controller.currentGemEquiped.equipmentUserIdEquiped = "0";
                controller.currentEquipment.dataGemUserId[controller.currentPositionPickGem - 1] = "0";
                WarriorDetail warriorDetail = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();
                dataUserModel.warrior.Find(x => x.warriorUserId == warriorDetail.warriorUserId).SetDataFromLevelAndEquipment(this);
                dataUserModel.money -= controller.currentGemEquiped.rare * controller.currentGemEquiped.rare * 1000;
                fileData.SaveDataUser(dataUserModel);

                controller.SetEquipmentGem(controller.currentPositionPickGem, "0");
                controller.SetDataGemStock(controller.currentGemI);
                controller.GemEquipedDetailisEmpty();
                controller.GemStockDetailisNotEmpty();
                warriorDetail.SetValueWarrior();

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 13;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator EquipGem()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("equipgem" + "|" + signatureKey + "|" + userId + "|" + controller.currentEquipment.equipmentUserId + "|" + controller.currentPositionPickGem + "|" + controller.currentGemStock.gemUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                controller.currentGemStock.equipmentUserIdEquiped = controller.currentEquipment.equipmentUserId;
                controller.currentEquipment.dataGemUserId[controller.currentPositionPickGem - 1] = controller.currentGemStock.gemUserId;
                WarriorDetail warriorDetail = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();
                dataUserModel.warrior.Find(x => x.warriorUserId == warriorDetail.warriorUserId).SetDataFromLevelAndEquipment(this);
                fileData.SaveDataUser(dataUserModel);

                controller.SetEquipmentGem(controller.currentPositionPickGem, controller.currentGemStock.gemUserId);
                if (controller.currentGemI == (controller.gemHave - 1))
                {
                    controller.currentGemI--;
                }
                controller.SetDataGemStock(controller.currentGemI);
                controller.PickGemSlot(controller.currentPositionPickGem);
                warriorDetail.SetValueWarrior();

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 14;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator SellGem()
    {
        EquipmentList controller = GameObject.Find("EquipmentListScript").GetComponent<EquipmentList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("sellgem" + "|" + signatureKey + "|" + userId + "|" + controller.currentGemStock.gemUserId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.money += Convert.ToInt32(controller.moneySellText.text.ToString().Replace(",", ""));
                dataUserModel.gem.Remove(dataUserModel.gem.Find(x => x.gemUserId == controller.currentGemStock.gemUserId));

                fileData.SaveDataUser(dataUserModel);
                if (controller.currentGemI == (controller.gemHave - 1))
                {
                    controller.currentGemI--;
                }
                controller.homeScript.OpenAlertMessage(jsonWordingData["get"][languageId].ToString() + jsonWordingData["money"][languageId].ToString() + " " + controller.moneySellText.text, null);
                controller.SetDataGemStock(controller.currentGemI);
                controller.removeMoneyUse.text = (controller.currentGemEquiped.rare * controller.currentGemEquiped.rare * 1000).ToString("#,#") + " / " + controller.homeScript.readData.dataUserModel.money.ToString("#,#");

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 15;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator ChangePositionTeam()
    {
        //Team controller = GameObject.Find("TeamScript").GetComponent<Team>();

        OpenLoading();

        List<WarriorPosition> warriorsPosition = new List<WarriorPosition>();
        foreach (Warrior warrior in dataUserModel.warrior)
        {
            WarriorPosition warriorPosition = new WarriorPosition();
            warriorPosition.warriorUserId = warrior.warriorUserId;
            warriorPosition.position = warrior.position;
            warriorsPosition.Add(warriorPosition);
        }
        JsonData jsonWarriorsPositionData = JsonMapper.ToJson(warriorsPosition);
        //Debug.Log(jsonWarriorsPositionData);

        string encryptData = encryption.EncryptRJ256("changepositionteam" + "|" + signatureKey + "|" + userId + "|" + jsonWarriorsPositionData);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                fileData.SaveDataUser(dataUserModel);

                CloseLoading();

                Application.LoadLevel("Home");
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 17;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator BuyItemFromMoneyStore()
    {
        Shop controller = GameObject.Find("ShopScript").GetComponent<Shop>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("buyitemfrommoneystore" + "|" + signatureKey + "|" + userId + "|" + controller.moneyStore.itemType[controller.currentItemPick] + "|" + controller.moneyStore.itemId[controller.currentItemPick]);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            string message = null;
            string imagePath = null;
            string[] result = www.text.Split(',');
            if (result[0] == "success")
            {
                switch (controller.moneyStore.itemType[controller.currentItemPick])
                {
                    case 1:
                        WarriorSoul warriorSoul = dataUserModel.warriorSoul.Find(x => x.warriorId == controller.moneyStore.itemId[controller.currentItemPick]);
                        if (warriorSoul != null)
                        {
                            warriorSoul.amount++;
                        }
                        else
                        {
                            warriorSoul = new WarriorSoul();
                            warriorSoul.warriorId = controller.moneyStore.itemId[controller.currentItemPick];
                            warriorSoul.amount = 1;
                            dataUserModel.warriorSoul.Add(warriorSoul);
                        }
                        message = jsonWordingData["get"][languageId].ToString() + jsonWordingData["soul"][languageId].ToString() + jsonWarriorData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["name"][languageId].ToString();
                        imagePath = "Warriors/" + jsonWarriorData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["name"][0].ToString() + "/icon";
                        break;
                    case 2:
                        Warrior warrior = new Warrior();
                        warrior.SetupNewData(controller.moneyStore.itemId[controller.currentItemPick], result[1]);
                        warrior.SetupData(this);
                        dataUserModel.warrior.Add(warrior);
                        message = jsonWordingData["get"][languageId].ToString() + jsonWarriorData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["name"][languageId].ToString();
                        imagePath = "Warriors/" + jsonWarriorData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["name"][0].ToString() + "/icon";
                        break;
                    case 3:
                        Equipment equipment = new Equipment();
                        equipment.SetupNewData(controller.moneyStore.itemId[controller.currentItemPick], result[1], Convert.ToInt32(jsonEquipmentData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["rare"].ToString()));
                        equipment.SetupData(jsonEquipmentData);
                        dataUserModel.equipment.Add(equipment);
                        message = jsonWordingData["get"][languageId].ToString() + jsonEquipmentData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["name"][languageId].ToString();
                        imagePath = "equipments/" + jsonEquipmentData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["name"][0].ToString();
                        break;
                    case 4:
                        Gem gem = new Gem();
                        gem.SetupNewData(controller.moneyStore.itemId[controller.currentItemPick], result[1]);
                        gem.SetupData(jsonGemData);
                        dataUserModel.gem.Add(gem);
                        message = jsonWordingData["get"][languageId].ToString() + jsonGemData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["name"][languageId].ToString();
                        imagePath = "gems/" + jsonGemData[controller.moneyStore.itemId[controller.currentItemPick].ToString()]["name"][0].ToString();
                        break;
                }
                dataUserModel.money -= controller.CalculatePrice(controller.moneyStore.itemType[controller.currentItemPick], controller.moneyStore.itemId[controller.currentItemPick]);

                fileData.SaveDataUser(dataUserModel);
                controller.itemSoldOut[controller.currentItemPick].alpha = 1;
                controller.itemSoldOut[controller.currentItemPick].blocksRaycasts = true;
                controller.SetItemData();
                controller.ClosePopupConfirmBuy();
                controller.OpenAlertMessage(message, imagePath);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 18;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator RefreshMoneyStore()
    {
        Shop controller = GameObject.Find("ShopScript").GetComponent<Shop>();

        OpenLoading();

        //Debug.Log(jsonWarriorsPositionData);

        string encryptData = encryption.EncryptRJ256("refreshmoneystore" + "|" + signatureKey + "|" + userId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            string[] result = www.text.Split(',');
            if (result[0] == "success")
            {
                dataUserModel.diamond -= 5;

                fileData.SaveDataUser(dataUserModel);
                controller.moneyStore.RandomItem();
                controller.SetItemData();
                SaveMoneyStore(controller.moneyStore);
                controller.CloseRefreshPopup();
                controller.OpenAlertMessage(jsonWordingData["completeRefreshMoneyStore"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 19;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UpgradeBagFromMoneyShop()
    {
        Shop controller = GameObject.Find("ShopScript").GetComponent<Shop>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradebag" + "|" + signatureKey + "|" + userId + "|" + controller.moneyStore.itemType[controller.currentItemPick]);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                if (controller.moneyStore.itemType[controller.currentItemPick] == 2)
                {
                    dataUserModel.bag_warrior += 5;
                    controller.OpenAlertMessage(jsonWordingData["completeUpgradeWarriorBag"][languageId].ToString(), null);

                }
                else if (controller.moneyStore.itemType[controller.currentItemPick] == 3)
                {
                    dataUserModel.bag_equipment += 5;
                    controller.OpenAlertMessage(jsonWordingData["completeUpgradeEquipmentBag"][languageId].ToString(), null);
                }
                else if (controller.moneyStore.itemType[controller.currentItemPick] == 4)
                {
                    dataUserModel.bag_gem += 5;
                    controller.OpenAlertMessage(jsonWordingData["completeUpgradeGemBag"][languageId].ToString(), null);
                }
                dataUserModel.diamond -= 5;

                fileData.SaveDataUser(dataUserModel);
                controller.CloseUpgradeBag();

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 20;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator RemoveAllEquipmentFromSacrificeWarrior()
    {
        WarriorDetail controller = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("removeallequipment" + "|" + signatureKey + "|" + userId + "|" + controller.currentPickUpgradeWarrior);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                foreach (string equipmentUserId in controller.warriorsUpgrade.Find(x => x.warriorUserId == controller.currentPickUpgradeWarrior).dataEquipmentUserId)
                {
                    if (equipmentUserId != "0")
                    {
                        dataUserModel.equipment.Find(x => x.equipmentUserId == equipmentUserId).warriorUserIdEquiped = "0";
                    }
                }
                for (int i = 0; i < 6; i++)
                {
                    dataUserModel.warrior.Find(x => x.warriorUserId == controller.currentPickUpgradeWarrior).dataEquipmentUserId[i] = "0";
                }

                dataUserModel.warrior.Find(x => x.warriorUserId == controller.currentPickUpgradeWarrior).SetDataFromLevelAndEquipment(this);
                fileData.SaveDataUser(dataUserModel);

                StartCoroutine(UpgradeLvPlusWarrior());

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 21;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UpgradeEquipmentBagFromWarriorDetailPage()
    {
        WarriorDetail controller = GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradeequipmentbag" + "|" + signatureKey + "|" + userId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.bag_equipment += 5;
                dataUserModel.diamond -= 5;

                fileData.SaveDataUser(dataUserModel);
                controller.SetDataEquipmentStock(0);
                controller.CloseStockFullPopup();
                controller.homeScript.OpenAlertMessage(jsonWordingData["completeUpgradeEquipmentBag"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 22;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator CombineGem()
    {
        CombineGem controller = GameObject.Find("CombineGemScript").GetComponent<CombineGem>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("combinegem" + "|" + signatureKey + "|" + userId + "|" + jsonGemSortData[controller.currentPickGemType.ToString()][(controller.currentPickGem + 1).ToString()]["gemId"] + "|" + jsonGemSortData[controller.currentPickGemType.ToString()][controller.currentPickGem.ToString()]["gemId"]);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;
        if (www.error == null)
        {
            string[] result = www.text.Split(',');
            if (result[0] == "success")
            {
                for (int i = 0; i < controller.amountRequire; i++)
                {
                    dataUserModel.gem.Remove(dataUserModel.gem.Find(x => x.gemId == Convert.ToInt32(jsonGemSortData[controller.currentPickGemType.ToString()][controller.currentPickGem.ToString()]["gemId"].ToString())));
                }
                Gem gem = new Gem();
                gem.SetupNewData(Convert.ToInt32(jsonGemSortData[controller.currentPickGemType.ToString()][(controller.currentPickGem + 1).ToString()]["gemId"].ToString()), result[1]);
                gem.SetupData(jsonGemData);
                dataUserModel.gem.Add(gem);

                dataUserModel.ore -= (controller.currentRare * controller.currentRare * 500);
                fileData.SaveDataUser(dataUserModel);
                controller.SetOreHave();
                controller.SetDataGemDetail();
                controller.OpenAlertMessage(jsonWordingData["get"][languageId].ToString() + jsonGemSortData[controller.currentPickGemType.ToString()][(controller.currentPickGem + 1).ToString()]["name"][0].ToString(), "gems/" + jsonGemSortData[controller.currentPickGemType.ToString()][(controller.currentPickGem + 1).ToString()]["name"][0].ToString());

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 23;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UpgradeBagFromInbox()
    {
        Inbox controller = GameObject.Find("InboxScript").GetComponent<Inbox>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradebag" + "|" + signatureKey + "|" + userId + "|" + controller.mails[controller.currentPickMail].itemType);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                if (controller.mails[controller.currentPickMail].itemType == "2")
                {
                    dataUserModel.bag_warrior += 5;
                    controller.OpenAlertMessage(jsonWordingData["completeUpgradeWarriorBag"][languageId].ToString(), null);

                }
                else if (controller.mails[controller.currentPickMail].itemType == "3")
                {
                    dataUserModel.bag_equipment += 5;
                    controller.OpenAlertMessage(jsonWordingData["completeUpgradeEquipmentBag"][languageId].ToString(), null);
                }
                else if (controller.mails[controller.currentPickMail].itemType == "4")
                {
                    dataUserModel.bag_gem += 5;
                    controller.OpenAlertMessage(jsonWordingData["completeUpgradeGemBag"][languageId].ToString(), null);
                }
                dataUserModel.diamond -= 5;

                fileData.SaveDataUser(dataUserModel);
                controller.CloseUpgradeBag();

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 24;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator ReceiptMail()
    {
        Inbox controller = GameObject.Find("InboxScript").GetComponent<Inbox>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("receiptmail" + "|" + signatureKey + "|" + userId + "|" + controller.mails[controller.currentPickMail].itemType + "|" + controller.mails[controller.currentPickMail].itemId + "|" + controller.mails[controller.currentPickMail].itemAmount + "|" + controller.mails[controller.currentPickMail].mailId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            string message = null;
            string imagePath = null;
            string[] result = www.text.Split(',');
            if (result[0] == "success")
            {
                switch (controller.mails[controller.currentPickMail].itemType)
                {
                    case "1":
                        WarriorSoul warriorSoul = dataUserModel.warriorSoul.Find(x => x.warriorId == Convert.ToInt32(controller.mails[controller.currentPickMail].itemId));
                        if (warriorSoul != null)
                        {
                            warriorSoul.amount += controller.mails[controller.currentPickMail].itemAmount;
                        }
                        else
                        {
                            warriorSoul = new WarriorSoul();
                            warriorSoul.warriorId = Convert.ToInt32(controller.mails[controller.currentPickMail].itemId);
                            warriorSoul.amount = controller.mails[controller.currentPickMail].itemAmount;
                            dataUserModel.warriorSoul.Add(warriorSoul);
                        }
                        message = jsonWordingData["get"][languageId].ToString() + jsonWordingData["soul"][languageId].ToString() + jsonWarriorData[controller.mails[controller.currentPickMail].itemId]["name"][languageId].ToString();
                        if (controller.mails[controller.currentPickMail].itemAmount > 1)
                            message = message + " x" + controller.mails[controller.currentPickMail].itemAmount.ToString();
                        imagePath = "Warriors/" + jsonWarriorData[controller.mails[controller.currentPickMail].itemId]["name"][0].ToString() + "/icon";
                        break;
                    case "2":
                        Warrior warrior = new Warrior();
                        warrior.SetupNewData(Convert.ToInt32(controller.mails[controller.currentPickMail].itemId), result[1]);
                        warrior.SetupData(this);
                        dataUserModel.warrior.Add(warrior);
                        message = jsonWordingData["get"][languageId].ToString() + jsonWarriorData[controller.mails[controller.currentPickMail].itemId]["name"][languageId].ToString();
                        imagePath = "Warriors/" + jsonWarriorData[controller.mails[controller.currentPickMail].itemId]["name"][0].ToString() + "/icon";
                        break;
                    case "3":
                        Equipment equipment = new Equipment();
                        equipment.SetupNewData(Convert.ToInt32(controller.mails[controller.currentPickMail].itemId), result[1], Convert.ToInt32(jsonEquipmentData[controller.mails[controller.currentPickMail].itemId]["rare"].ToString()));
                        equipment.SetupData(jsonEquipmentData);
                        dataUserModel.equipment.Add(equipment);
                        message = jsonWordingData["get"][languageId].ToString() + jsonEquipmentData[controller.mails[controller.currentPickMail].itemId]["name"][languageId].ToString();
                        imagePath = "equipments/" + jsonEquipmentData[controller.mails[controller.currentPickMail].itemId]["name"][0].ToString();
                        break;
                    case "4":
                        Gem gem = new Gem();
                        gem.SetupNewData(Convert.ToInt32(controller.mails[controller.currentPickMail].itemId), result[1]);
                        gem.SetupData(jsonGemData);
                        dataUserModel.gem.Add(gem);
                        message = jsonWordingData["get"][languageId].ToString() + jsonGemData[controller.mails[controller.currentPickMail].itemId]["name"][languageId].ToString();
                        imagePath = "gems/" + jsonGemData[controller.mails[controller.currentPickMail].itemId]["name"][0].ToString();
                        break;
                    case "5":
                        dataUserModel.money += controller.mails[controller.currentPickMail].itemAmount;
                        message = jsonWordingData["get"][languageId].ToString() + jsonWordingData["money"][languageId].ToString() + controller.mails[controller.currentPickMail].itemAmount;
                        imagePath = "UI/moneyIcon";
                        break;
                    case "6":
                        dataUserModel.ore += controller.mails[controller.currentPickMail].itemAmount;
                        message = jsonWordingData["get"][languageId].ToString() + jsonWordingData["ore"][languageId].ToString() + controller.mails[controller.currentPickMail].itemAmount;
                        imagePath = "UI/oreIcon";
                        break;
                }
                dataUserModel.mail.Remove(controller.mails[controller.currentPickMail]);
                controller.currentPickMail = 0;

                fileData.SaveDataUser(dataUserModel);
                controller.SetDataMailList();
                controller.CloseMailDetailPopup();
                if (message != null)
                {
                    controller.OpenAlertMessage(message, imagePath);
                }


                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 25;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator UpgradeWarriorBagFromOpenChestPage()
    {
        OpenChest controller = GameObject.Find("OpenChestScript").GetComponent<OpenChest>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("upgradewarriorbag" + "|" + signatureKey + "|" + userId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "success")
            {
                dataUserModel.bag_warrior += 5;
                dataUserModel.diamond -= 5;

                fileData.SaveDataUser(dataUserModel);
                controller.CloseUpgradeBag();
                controller.OpenAlertMessage(jsonWordingData["completeUpgradeWarriorBag"][languageId].ToString(), null);

                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 26;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator OpenChest()
    {
        OpenChest controller = GameObject.Find("OpenChestScript").GetComponent<OpenChest>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("openchest" + "|" + signatureKey + "|" + userId + "|" + controller.currentPositionBuyChest);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            string message = null;
            string imagePath = null;
            JsonData jsonData = JsonMapper.ToObject(www.text);
            if (jsonData["result"].ToString() == "fail")
            {
                notPass = true;
            }
            string signature;
            string signatureValue;

            if (!notPass)
            {
                switch (controller.currentPositionBuyChest)
                {
                    case 1:
                    case 2:
                        signature = encryption.DecryptRJ256((string)jsonData["signature"]);
                        signatureValue = jsonData["isSoul"].ToString() + jsonData["warriorId"].ToString() + jsonData["amount"].ToString() + jsonData["warriorUserId"].ToString() + signatureKey;
                        if (signature.CompareTo(signatureValue) == 1)
                        {
                            if (jsonData["isSoul"].ToString() == "1")
                            {
                                WarriorSoul warriorSoul = dataUserModel.warriorSoul.Find(x => x.warriorId == Convert.ToInt32(jsonData["warriorId"].ToString()));
                                if (warriorSoul != null)
                                {
                                    warriorSoul.amount += Convert.ToInt32(jsonData["amount"].ToString());
                                }
                                else
                                {
                                    warriorSoul = new WarriorSoul();
                                    warriorSoul.warriorId = Convert.ToInt32(jsonData["warriorId"].ToString());
                                    warriorSoul.amount = Convert.ToInt32(jsonData["amount"].ToString());
                                    dataUserModel.warriorSoul.Add(warriorSoul);
                                }
                                message = jsonWordingData["get"][languageId].ToString() + jsonWordingData["soul"][languageId].ToString() + jsonWarriorData[jsonData["warriorId"].ToString()]["name"][languageId].ToString();
                                if (Convert.ToInt32(jsonData["amount"].ToString()) > 1)
                                    message = message + " x" + jsonData["amount"].ToString();
                                imagePath = "Warriors/" + jsonWarriorData[jsonData["warriorId"].ToString()]["name"][0].ToString() + "/icon";
                            }
                            else if (jsonData["isSoul"].ToString() == "2")
                            {
                                Warrior warrior = new Warrior();
                                warrior.SetupNewData(Convert.ToInt32(jsonData["warriorId"].ToString()), jsonData["warriorUserId"].ToString());
                                warrior.SetupData(this);
                                dataUserModel.warrior.Add(warrior);
                                message = jsonWordingData["get"][languageId].ToString() + jsonWarriorData[jsonData["warriorId"].ToString()]["name"][languageId].ToString();
                                imagePath = "Warriors/" + jsonWarriorData[jsonData["warriorId"].ToString()]["name"][0].ToString() + "/icon";
                            }

                            if (controller.currentPositionBuyChest == 1)
                                dataUserModel.heart -= 300;
                            else if (controller.currentPositionBuyChest == 2)
                                dataUserModel.diamond -= 100;

                            fileData.SaveDataUser(dataUserModel);
                            controller.SetDataOpenChest();
                            controller.OpenAlertMessage(message, imagePath);

                            CloseLoading();
                        }
                        else
                        {
                            notPass = true;
                        }
                        break;
                    case 3:
                        signature = encryption.DecryptRJ256((string)jsonData["signature"]);
                        signatureValue = jsonData["isSoul"][0].ToString() + jsonData["warriorId"][0].ToString() + jsonData["amount"][0].ToString() + jsonData["warriorUserId"][0].ToString() + signatureKey;
                        if (signature.CompareTo(signatureValue) == 1)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (jsonData["isSoul"][i].ToString() == "1")
                                {
                                    WarriorSoul warriorSoul = dataUserModel.warriorSoul.Find(x => x.warriorId == Convert.ToInt32(jsonData["warriorId"][i].ToString()));
                                    if (warriorSoul != null)
                                    {
                                        warriorSoul.amount += Convert.ToInt32(jsonData["amount"][i].ToString());
                                    }
                                    else
                                    {
                                        warriorSoul = new WarriorSoul();
                                        warriorSoul.warriorId = Convert.ToInt32(jsonData["warriorId"][i].ToString());
                                        warriorSoul.amount = Convert.ToInt32(jsonData["amount"][i].ToString());
                                        dataUserModel.warriorSoul.Add(warriorSoul);
                                    }
                                    controller.warriorImage[i].sprite = Resources.Load<Sprite>("Warriors/" + jsonWarriorData[jsonData["warriorId"][i].ToString()]["name"][0].ToString() + "/icon");
                                    if (Convert.ToInt32(jsonData["amount"][i].ToString()) > 1)
                                    {
                                        controller.warriorImage[i].GetComponentInChildren<Text>().text = "x" + jsonData["amount"][i].ToString();
                                    }
                                    else
                                    {
                                        controller.warriorImage[i].GetComponentInChildren<Text>().text = "";
                                    }
                                }
                                else if (jsonData["isSoul"][i].ToString() == "2")
                                {
                                    Warrior warrior = new Warrior();
                                    warrior.SetupNewData(Convert.ToInt32(jsonData["warriorId"][i].ToString()), jsonData["warriorUserId"][i].ToString());
                                    warrior.SetupData(this);
                                    dataUserModel.warrior.Add(warrior);
                                    controller.warriorImage[i].sprite = Resources.Load<Sprite>("Warriors/" + jsonWarriorData[jsonData["warriorId"][i].ToString()]["name"][0].ToString() + "/icon");
                                    controller.warriorImage[i].GetComponentInChildren<Text>().text = "";
                                }
                            }

                            dataUserModel.diamond -= 900;

                            fileData.SaveDataUser(dataUserModel);
                            controller.SetDataOpenChest();
                            controller.OpenResult10ChestPopup();

                            CloseLoading();
                        }
                        else
                        {
                            notPass = true;
                        }
                        break;
                }
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 27;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator AddFriend()
    {
        FriendList controller = GameObject.Find("FriendListScript").GetComponent<FriendList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("addfriend" + "|" + signatureKey + "|" + userId + "|" + controller.addFriendInputField.text);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "adddone")
            {
                controller.OpenAlertMessage(jsonWordingData["addFriendDone"][languageId].ToString(), null);
                CloseLoading();
            }
            else if ((string)www.text == "notfoundaccount")
            {
                controller.OpenAlertMessage(jsonWordingData["notFoundAccount"][languageId].ToString(), null);
                CloseLoading();
            }
            else if ((string)www.text == "alreadyadd")
            {
                controller.OpenAlertMessage(jsonWordingData["alreadyAdd"][languageId].ToString(), null);
                CloseLoading();
            }
            else if ((string)www.text == "alreadyfriend")
            {
                controller.OpenAlertMessage(jsonWordingData["alreadyFriend"][languageId].ToString(), null);
                CloseLoading();
            }
            else if ((string)www.text == "pendingyou")
            {
                controller.OpenAlertMessage(jsonWordingData["pendingYou"][languageId].ToString(), null);
                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 28;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator AcceptFriend()
    {
        FriendList controller = GameObject.Find("FriendListScript").GetComponent<FriendList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("acceptfriend" + "|" + signatureKey + "|" + userId + "|" + controller.currentDataUserId2);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "acceptdone")
            {
                controller.OpenAlertMessage(jsonWordingData["acceptDone"][languageId].ToString(), null);
                controller.readData.dataUserModel.friend.Find(x => x.data_userId2 == controller.currentDataUserId2).status = 1;
                controller.SetDataFriendListUnitBox();
                fileData.SaveDataUser(dataUserModel);
                CloseLoading();
            }
            else if ((string)www.text == "notfoundaccount")
            {
                controller.OpenAlertMessage(jsonWordingData["notFoundAccount"][languageId].ToString(), null);
                controller.readData.dataUserModel.friend.Remove(controller.readData.dataUserModel.friend.Find(x => x.data_userId2 == controller.currentDataUserId2));
                controller.SetDataFriendListUnitBox();
                fileData.SaveDataUser(dataUserModel);
                CloseLoading();
            }
            else if ((string)www.text == "alreadyfriend")
            {
                controller.OpenAlertMessage(jsonWordingData["alreadyFriend"][languageId].ToString(), null);
                controller.readData.dataUserModel.friend.Find(x => x.data_userId2 == controller.currentDataUserId2).status = 1;
                controller.SetDataFriendListUnitBox();
                fileData.SaveDataUser(dataUserModel);
                CloseLoading();
            }
            else if ((string)www.text == "cancelfirst")
            {
                controller.OpenAlertMessage(jsonWordingData["cancelFirst"][languageId].ToString(), null);
                controller.readData.dataUserModel.friend.Remove(controller.readData.dataUserModel.friend.Find(x => x.data_userId2 == controller.currentDataUserId2));
                controller.SetDataFriendListUnitBox();
                fileData.SaveDataUser(dataUserModel);
                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 29;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator CancelFriend()
    {
        FriendList controller = GameObject.Find("FriendListScript").GetComponent<FriendList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("cancelfriend" + "|" + signatureKey + "|" + userId + "|" + controller.currentDataUserId2);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "canceldone")
            {
                controller.OpenAlertMessage(jsonWordingData["cancelDone"][languageId].ToString(), null);
                controller.readData.dataUserModel.friend.Remove(controller.readData.dataUserModel.friend.Find(x => x.data_userId2 == controller.currentDataUserId2));
                controller.SetDataFriendListUnitBox();
                fileData.SaveDataUser(dataUserModel);
                CloseLoading();
            }
            else if ((string)www.text == "notfoundaccount")
            {
                controller.OpenAlertMessage(jsonWordingData["notFoundAccount"][languageId].ToString(), null);
                controller.readData.dataUserModel.friend.Remove(controller.readData.dataUserModel.friend.Find(x => x.data_userId2 == controller.currentDataUserId2));
                controller.SetDataFriendListUnitBox();
                fileData.SaveDataUser(dataUserModel);
                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 30;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator SendHeartToFriend()
    {
        FriendList controller = GameObject.Find("FriendListScript").GetComponent<FriendList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("sendhearttofriend" + "|" + signatureKey + "|" + userId + "|" + controller.currentDataUserId2);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "sendheartdone")
            {
                controller.OpenAlertMessage(jsonWordingData["sendHeartDone"][languageId].ToString(), null);
                controller.readData.dataUserModel.friend.Find(x => x.data_userId2 == controller.currentDataUserId2).sendHeartLastedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                controller.SetDataFriendListUnitBox();
                fileData.SaveDataUser(dataUserModel);
                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 31;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }

    public IEnumerator SendHeartAllFriend()
    {
        FriendList controller = GameObject.Find("FriendListScript").GetComponent<FriendList>();

        OpenLoading();

        string encryptData = encryption.EncryptRJ256("sendheartallfriend" + "|" + signatureKey + "|" + userId);
        WWWForm form = new WWWForm();
        form.AddField("data", encryptData);
        string url = "http://warrior.gurugames.in.th/index.php?r=service/update";
        WWW www = new WWW(url, form);

        yield return www;
        notPass = false;

        if (www.error == null)
        {
            if ((string)www.text == "sendheartdone")
            {
                controller.OpenAlertMessage(jsonWordingData["sendHeartDone"][languageId].ToString(), null);
                List<Friend> friends = controller.readData.dataUserModel.friend.FindAll(x => x.sendHeartLastedDate != System.DateTime.Now.ToString("yyyy-MM-dd"));
                foreach(Friend friend in friends){
                    friend.sendHeartLastedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                }
                controller.SetDataFriendListUnitBox();
                fileData.SaveDataUser(dataUserModel);
                CloseLoading();
            }
            else
            {
                notPass = true;
            }
        }
        else
        {
            notPass = true;
        }
        if (notPass)
        {
            serviceNo = 32;
            internetProbPanelCanvasGroup.alpha = 1;
            internetProbPanelCanvasGroup.blocksRaycasts = true;
        }
    }
}
