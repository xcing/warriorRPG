using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CombineGem : MonoBehaviour
{

    public ReadData readData;
    public GameObject alertMessageBg;
    public float countdown;

    public Text header;
    public Text oreHaveText;

    //gemList
    public Image borderPickGemType;
    public Image borderPickGem;
    public int currentPickGemType;
    public int currentPickGem;
    public Image[] gemIcon;
    public Text[] gemName;
    public Image[] statIcon;
    public Text[] statValue;

    //gemDetail
    public Image materialGemIcon;
    public Text materialGemAmount;
    public Text oreText;
    public Text combineButtonText;
    public int amountHave;
    public int amountRequire;
    public int currentRare;

    void Start()
    {
        header.text = readData.jsonWordingData["combineStore"][readData.languageId].ToString();
        combineButtonText.text = readData.jsonWordingData["combine"][readData.languageId].ToString();

        currentPickGemType = 1;
        currentPickGem = 1;

        SetOreHave();
        SetDataGemList();
        PickGemType(currentPickGemType);
        //gemIcon = new Image[4];
        //gemName = new Text[4];
    }

    public void SetOreHave()
    {
        oreHaveText.text = readData.dataUserModel.ore.ToString("#,#");
    }

    public void SetDataGemList()
    {
        for (int i = 0; i < 4; i++)
        {
            gemIcon[i].sprite = Resources.Load<Sprite>("Gems/" + readData.jsonGemSortData[currentPickGemType.ToString()][(i + 2).ToString()]["name"][0]);
            gemName[i].text = readData.jsonGemSortData[currentPickGemType.ToString()][(i + 2).ToString()]["name"][readData.languageId].ToString();
            statIcon[i].sprite = GetStatIcon(readData.jsonGemSortData[currentPickGemType.ToString()][(i + 2).ToString()]["buffType"].ToString());
            statValue[i].text = "+" + readData.jsonGemSortData[currentPickGemType.ToString()][(i + 2).ToString()]["buffValue"].ToString() + "%";
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

    public void SetDataGemDetail()
    {
        currentRare = Convert.ToInt32(readData.jsonGemSortData[currentPickGemType.ToString()][(currentPickGem + 1).ToString()]["rare"].ToString());
        amountHave = readData.dataUserModel.gem.FindAll(x => (x.buffType == currentPickGemType) && (x.rare == Convert.ToInt32(readData.jsonGemSortData[currentPickGemType.ToString()][currentPickGem.ToString()]["rare"].ToString())) && (x.equipmentUserIdEquiped == "0")).Count;
        if (currentRare < 4)
            amountRequire = 5;
        else
            amountRequire = 10;

        materialGemIcon.sprite = Resources.Load<Sprite>("Gems/" + readData.jsonGemSortData[currentPickGemType.ToString()][currentPickGem.ToString()]["name"][0]);
        materialGemAmount.text = amountHave.ToString() + " / " + amountRequire.ToString();
        oreText.text = (currentRare * currentRare * 500).ToString("#,#");
    }

    public void PickGemType(int position)
    {
        currentPickGemType = position;
        PickGem(currentPickGem);
        Vector2 borderGemTypePosition;

        switch (currentPickGemType)
        {
            case 1:
                borderGemTypePosition = new Vector2(-700, 389);
                break;
            case 2:
                borderGemTypePosition = new Vector2(-560, 389);
                break;
            case 3:
                borderGemTypePosition = new Vector2(-420, 389);
                break;
            case 4:
                borderGemTypePosition = new Vector2(-280, 389);
                break;
            case 5:
                borderGemTypePosition = new Vector2(-140, 389);
                break;
            default:
                borderGemTypePosition = new Vector2(-700, 389);
                break;
        }
        borderPickGemType.rectTransform.anchoredPosition = borderGemTypePosition;
        SetDataGemList();
    }

    public void PickGem(int position)
    {
        currentPickGem = position;
        Vector2 borderGemPosition;

        switch (currentPickGem)
        {
            case 1:
                borderGemPosition = new Vector2(0, 280);
                break;
            case 2:
                borderGemPosition = new Vector2(0, 95);
                break;
            case 3:
                borderGemPosition = new Vector2(0, -90);
                break;
            case 4:
                borderGemPosition = new Vector2(0, -275);
                break;
            default:
                borderGemPosition = new Vector2(0, 280);
                break;
        }
        borderPickGem.rectTransform.anchoredPosition = borderGemPosition;
        SetDataGemDetail();
    }

    public void CombineButton()
    {
        if (amountHave < amountRequire)
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughGem"][readData.languageId].ToString(), null);
        }
        else if (readData.dataUserModel.ore < (currentRare * currentRare * 500))
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughMoney"][readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(readData.CombineGem());
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
}
