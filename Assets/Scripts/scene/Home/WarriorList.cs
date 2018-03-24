using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class WarriorList : MonoBehaviour
{
    public Home homeScript;
    public Text header;
    public int warriorSize;
    public int warriorHave;
    public Image warriorBoxScrollRect;
    public Image warriorBox;
    public GameObject warriorUnitBoxPrefab;
    public GameObject[] warriorUnitBox;
    public Scrollbar scrollbar;
    public List<Warrior> warriors;
    public int warriorI;

    //popup upgrade warrior bag
    public CanvasGroup bgFade;
    public CanvasGroup upgradeWarriorBag;
    public Text upgradeWarriorBagHeader;
    public Text upgradeeWarriorBagText;
    
    void Start()
    {
        header.text = homeScript.readData.jsonWordingData["headerWarriorList"][0].ToString();

        upgradeWarriorBagHeader.text = homeScript.readData.jsonWordingData["upgradeBag"][homeScript.readData.languageId].ToString();
        upgradeeWarriorBagText.text = homeScript.readData.jsonWordingData["sizeBag"][homeScript.readData.languageId].ToString() + "+5";

        SetDataWarriorStock();
    }

    public void SetDataWarriorStock()
    {
        for (int i = warriorBox.transform.childCount - 1; i > 0; i--)
        {
            GameObject.Destroy(warriorBox.transform.GetChild(i).gameObject);
        }

        warriorSize = (homeScript.readData.dataUserModel.bag_warrior+1);
        warriorHave = homeScript.readData.dataUserModel.warrior.Count;

        warriorUnitBox = new GameObject[warriorSize];
        warriorUnitBoxPrefab = Resources.Load<GameObject>("Prefab/WarriorUnitBox");

        int heightWarriorPanelBox;
        if (warriorSize % 6 == 0)
            heightWarriorPanelBox = ((warriorSize / 6) * 235) + 30;
        else
            heightWarriorPanelBox = (((warriorSize / 6) + 1) * 235) + 30;
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

        warriors = homeScript.readData.dataUserModel.warrior;
        warriors.Sort((x, y) => -1 * x.level.CompareTo(y.level));

        for (int i = 0; i < warriorSize; i++)
        {
            if (i == (warriorSize-1))
            {
                warriorUnitBoxPrefab = Resources.Load<GameObject>("Prefab/PlusBagWarriorUnitBox");
            }
            if (i < warriorHave)
            {
                warriorUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Warriors/" + warriors[i].name[0] + "/icon");
            }
            else if (i != (warriorSize - 1))
            {
                warriorUnitBoxPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/empty");
            }
            warriorUnitBox[i] = Instantiate(warriorUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            warriorUnitBox[i].transform.SetParent(warriorBox.transform, true);
            warriorUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            warriorUnitBox[i].transform.position = warriorBox.transform.position;
            warriorUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(((i % 6) * 235) - 608, (-235 * (i / 6)) + (heightWarriorPanelBox / 2) - 127);
            if (i < warriorHave)
            {
                warriorUnitBox[i].GetComponentInChildren<Text>().text = "Lv" + warriors[i].level;
            }
            if (i < warriorHave || i == (warriorSize - 1))
            {
                WarriorListTempData tempData = warriorUnitBox[i].GetComponent<WarriorListTempData>();
                tempData.warriorSize = (warriorSize - 1);
                tempData.tempI = i;
                if (i != (warriorSize - 1))
                {
                    tempData.warriorUserId = warriors[i].warriorUserId;
                }
                warriorUnitBox[i].GetComponent<Button>().onClick.AddListener(() => tempData.OpenWarriorDetail());
            }
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
        if (homeScript.readData.dataUserModel.diamond < 5)
        {
            homeScript.OpenAlertMessage(homeScript.readData.jsonWordingData["notEnoughDiamond"][homeScript.readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(homeScript.readData.UpgradeWarriorBag());
        }
    }
    
    void Update()
    {
        
    }
}
