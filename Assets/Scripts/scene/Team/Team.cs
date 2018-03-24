using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Team : MonoBehaviour
{

    public ReadData readData;
    public GameObject alertMessageBg;
    public float countdown;

    public Text teamPower;
    public Text columnText1;
    public Text columnText2;
    public Text columnText3;

    public Image borderUnit;
    public int currentPositionEnter;
    public List<Image> unit;
    public Image[] unitLock;
    public List<Warrior> warriorInTeam;
    GameObject monsterFile;
    public GameObject monsterEntity;

    //scrroll rect
    public int warriorSize;
    public int warriorHave;
    public Image warriorBoxScrollRect;
    public Image warriorBox;
    public GameObject warriorUnitBoxPrefab;
    public GameObject[] warriorUnitBox;
    public List<Warrior> warriors;

    //popup upgrade warrior bag
    public CanvasGroup bgFade;
    public CanvasGroup upgradeWarriorBag;
    public Text upgradeWarriorBagHeader;
    public Text upgradeeWarriorBagText;

    void Awake()
    {
        if (GameObject.Find("BgMusic") != null)
        {
            Destroy(GameObject.Find("BgMusic"));
        }
    }

    void Start()
    {
        currentPositionEnter = 0;
        upgradeWarriorBagHeader.text = readData.jsonWordingData["upgradeBag"][readData.languageId].ToString();
        upgradeeWarriorBagText.text = readData.jsonWordingData["sizeBag"][readData.languageId].ToString() + "+5";

        teamPower.text = readData.jsonWordingData["teamPower"][readData.languageId].ToString() + ": " + "1431875";
        columnText1.text = readData.jsonWordingData["frontRow"][readData.languageId].ToString();
        columnText2.text = readData.jsonWordingData["backRow"][readData.languageId].ToString();
        columnText3.text = readData.jsonWordingData["reserveRow"][readData.languageId].ToString();

        SetDataTeam();
        SetDataWarriorStock();
    }

    public void SetDataTeam()
    {
        SetUnlockPositionFormation();

        for (int i = 0; i < 9; i++)
        {
            if (unit[i].transform.childCount > 2)
            {
                GameObject.Destroy(unit[i].transform.GetChild(2).gameObject);
            }
            unit[i].GetComponentsInChildren<Text>()[0].text = null;
            unit[i].GetComponentsInChildren<Text>()[1].text = null;
        }
        warriorInTeam = readData.dataUserModel.warrior.FindAll(x => x.position > 0);

        foreach (Warrior warrior in warriorInTeam)
        {
            monsterFile = (GameObject)Resources.Load("Warriors/" + warrior.name[0] + "/AnimateCharacter/entity_01", typeof(GameObject));
            monsterEntity = Instantiate(monsterFile, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            monsterEntity.transform.parent = unit[warrior.position - 1].transform;
            monsterEntity.transform.position = unit[warrior.position - 1].transform.position;
            monsterEntity.transform.Translate(0, -50, 0);
            monsterEntity.transform.localScale = new Vector3(70, 70, 1);
            monsterEntity.SetActive(true);

            if (warrior.lvPlus > 0)
            {
                unit[warrior.position - 1].GetComponentsInChildren<Text>()[0].text = "+" + warrior.lvPlus.ToString();
            }
            unit[warrior.position - 1].GetComponentsInChildren<Text>()[1].text = "Lv " + warrior.level.ToString();
        }

        currentPositionEnter = unit.FindIndex(x => x.GetComponentsInChildren<Text>()[1].text == "");
        if (currentPositionEnter >= 0)
        {
            if (unitLock[currentPositionEnter].enabled == false)
            {
                borderUnit.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(unit[currentPositionEnter].GetComponent<Image>().rectTransform.anchoredPosition.x, unit[currentPositionEnter].GetComponent<Image>().rectTransform.anchoredPosition.y);
            }
            else
            {
                currentPositionEnter = -1;
                borderUnit.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(0, 4000);
            }
        }
        else
        {
            borderUnit.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(0, 4000);
        }
    }

    public void SetDataWarriorStock()
    {
        for (int i = warriorBox.transform.childCount - 1; i > 0; i--)
        {
            GameObject.Destroy(warriorBox.transform.GetChild(i).gameObject);
        }
        warriors = readData.dataUserModel.warrior.FindAll(x => x.position == 0);
        warriors.Sort((x, y) => -1 * x.level.CompareTo(y.level));

        warriorSize = (readData.dataUserModel.bag_warrior + 1);
        warriorHave = warriors.Count;

        warriorUnitBox = new GameObject[warriorSize];
        warriorUnitBoxPrefab = Resources.Load<GameObject>("Prefab/WarriorTeamUnitBox");

        int heightWarriorPanelBox;
        if (warriorSize % 4 == 0)
            heightWarriorPanelBox = ((warriorSize / 4) * 185) + 30;
        else
            heightWarriorPanelBox = (((warriorSize / 4) + 1) * 185) + 30;
        if (heightWarriorPanelBox < warriorBoxScrollRect.rectTransform.rect.height)
        {
            heightWarriorPanelBox = Convert.ToInt32(warriorBoxScrollRect.rectTransform.rect.height);
            warriorBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            warriorBox.rectTransform.anchoredPosition = new Vector3(0, (heightWarriorPanelBox - warriorBoxScrollRect.rectTransform.rect.height) / -2);
        }

        warriorBox.rectTransform.sizeDelta = new Vector2(warriorBox.rectTransform.rect.width, heightWarriorPanelBox);

        for (int i = 0; i < warriorSize; i++)
        {
            if (i == (warriorSize - 1))
            {
                warriorUnitBoxPrefab = Resources.Load<GameObject>("Prefab/PlusBagWarriorTeamUnitBox");
            }
            warriorUnitBox[i] = Instantiate(warriorUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            warriorUnitBox[i].transform.SetParent(warriorBox.transform, true);
            warriorUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            warriorUnitBox[i].transform.position = warriorBox.transform.position;
            warriorUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(((i % 4) * 145) - 219, (-185 * (i / 4)) + (heightWarriorPanelBox / 2) - 100);
            if (i < warriorHave)
            {
                warriorUnitBox[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("Warriors/" + warriors[i].name[0] + "/icon");
                warriorUnitBox[i].GetComponentInChildren<Text>().text = "Lv" + warriors[i].level;
            }
            if (i < warriorHave || i == (warriorSize - 1))
            {
                WarriorListTeamTempData tempData = warriorUnitBox[i].GetComponent<WarriorListTeamTempData>();
                tempData.warriorSize = (warriorSize - 1);
                tempData.tempI = i;
                tempData.teamScript = this;
                if (i != (warriorSize - 1))
                {
                    tempData.warriorUserId = warriors[i].warriorUserId;
                }
                warriorUnitBox[i].GetComponent<Button>().onClick.AddListener(() => tempData.MoveWarriorInTeam());
            }
            else
            {
                warriorUnitBox[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("UI/empty");
            }
        }
    }

    public void SetUnlockPositionFormation()
    {
        unitLock[0].enabled = false;
        unitLock[1].enabled = false;
        if (readData.dataUserModel.level >= 5)
        {
            unitLock[2].enabled = false;
        }
        if (readData.dataUserModel.level >= 10)
        {
            unitLock[3].enabled = false;
        }
        if (readData.dataUserModel.level >= 15)
        {
            unitLock[4].enabled = false;
        }
        if (readData.dataUserModel.level >= 20)
        {
            unitLock[5].enabled = false;
        }
        if (readData.dataUserModel.level >= 25)
        {
            unitLock[6].enabled = false;
        }
        if (readData.dataUserModel.level >= 30)
        {
            unitLock[7].enabled = false;
        }
        if (readData.dataUserModel.level >= 35)
        {
            unitLock[8].enabled = false;
        }
    }

    public void OpenPopupUpgradeWarriorBag()
    {
        bgFade.alpha = 1;
        bgFade.blocksRaycasts = true;
        upgradeWarriorBag.alpha = 1;
        upgradeWarriorBag.blocksRaycasts = true;
    }

    public void CloseUpgradeWarriorBag()
    {
        bgFade.alpha = 0;
        bgFade.blocksRaycasts = false;
        upgradeWarriorBag.alpha = 0;
        upgradeWarriorBag.blocksRaycasts = false;
    }

    public void UpgradeWarriorBag()
    {
        if (readData.dataUserModel.diamond < 5)
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughDiamond"][readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(readData.UpgradeWarriorBagFromTeamPage());
        }
    }

    public void RemoveFromTeam(int position)
    {
        if (unit[position - 1].GetComponentsInChildren<Text>()[1].text != "")
        {
            readData.dataUserModel.warrior.Find(x => x.position == position).position = 0;
            SetDataTeam();
            SetDataWarriorStock();
        }
    }

    public void LockButton(int position)
    {
        switch (position)
        {
            case 3:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenLv"][readData.languageId].ToString() + " 5", null);
                break;
            case 4:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenLv"][readData.languageId].ToString() + " 10", null);
                break;
            case 5:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenLv"][readData.languageId].ToString() + " 15", null);
                break;
            case 6:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenLv"][readData.languageId].ToString() + " 20", null);
                break;
            case 7:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenLv"][readData.languageId].ToString() + " 25", null);
                break;
            case 8:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenLv"][readData.languageId].ToString() + " 30", null);
                break;
            case 9:
                OpenAlertMessage(readData.jsonWordingData["unlockWhenLv"][readData.languageId].ToString() + " 35", null);
                break;
        }
    }

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
        StartCoroutine(readData.ChangePositionTeam());
    }
}
