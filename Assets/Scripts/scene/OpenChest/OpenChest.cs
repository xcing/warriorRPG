using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class OpenChest : MonoBehaviour {

    public ReadData readData;
    public GameObject alertMessageBg;
    public float countdown;

    public Text header;
    public CanvasGroup bgFadeCanvasGroup;

    public Text heartText;
    public Text diamondText;
    public Text detail1;
    public Text detail2;
    public Text detail3;
    public Text sourceValue1;
    public Text sourceValue2;
    public Text sourceValue3;
    public Button buyButton1;
    public Button buyButton2;
    public Button buyButton3;
    public int currentPositionBuyChest;

    // upgrade bag popup
    public CanvasGroup upgradeBagCanvasGroup;
    public Text upgradeBagHeader;
    public Text upgradeBagText;

    // result 10 chest popup
    public CanvasGroup result10ChestCanvasGroup;
    public Text resultHeader;
    public Image[] warriorImage;
    public Button submitButton;

	void Start () {
        header.text = readData.jsonWordingData["registerWarrior"][readData.languageId].ToString();

        upgradeBagHeader.text = readData.jsonWordingData["upgradeBag"][readData.languageId].ToString();
        upgradeBagText.text = readData.jsonWordingData["sizeBag"][readData.languageId].ToString() + "+5";

        resultHeader.text = readData.jsonWordingData["get"][readData.languageId].ToString();
        submitButton.GetComponentInChildren<Text>().text = readData.jsonWordingData["submit"][readData.languageId].ToString();

        sourceValue1.text = "300";
        sourceValue2.text = "100";
        sourceValue3.text = "900";

        detail1.text = readData.jsonWordingData["oneWarrior"][readData.languageId].ToString();
        detail2.text = readData.jsonWordingData["oneGreatWarrior"][readData.languageId].ToString();
        detail3.text = readData.jsonWordingData["tenGreatWarrior"][readData.languageId].ToString();

        buyButton1.GetComponentInChildren<Text>().text = readData.jsonWordingData["buy"][readData.languageId].ToString();
        buyButton2.GetComponentInChildren<Text>().text = readData.jsonWordingData["buy"][readData.languageId].ToString();
        buyButton3.GetComponentInChildren<Text>().text = readData.jsonWordingData["buy"][readData.languageId].ToString();

        SetDataOpenChest();
	}

    public void SetDataOpenChest()
    {
        heartText.text = readData.dataUserModel.heart.ToString("#,#");
        diamondText.text = readData.dataUserModel.diamond.ToString("#,#");
    }

    public void BuyButton(int position)
    {
        if (position == 1 && readData.dataUserModel.heart < 300)
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughHeart"][readData.languageId].ToString(), null);
        }
        else if (position == 2 && readData.dataUserModel.diamond < 100)
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughDiamond"][readData.languageId].ToString(), null);
        }
        else if (position == 3 && readData.dataUserModel.diamond < 900)
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughDiamond"][readData.languageId].ToString(), null);
        }
        else if (readData.dataUserModel.warrior.Count >= readData.dataUserModel.bag_warrior)
        {
            OpenPopupUpgradeBag();
        }
        else if (position == 3 && (readData.dataUserModel.warrior.Count + 9) >= readData.dataUserModel.bag_warrior)
        {
            OpenPopupUpgradeBag();
        }
        else
        {
            currentPositionBuyChest = position;   
            StartCoroutine(readData.OpenChest());
        }
    }

    public void OpenPopupUpgradeBag()
    {
        bgFadeCanvasGroup.alpha = 1;
        bgFadeCanvasGroup.blocksRaycasts = true;
        upgradeBagCanvasGroup.alpha = 1;
        upgradeBagCanvasGroup.blocksRaycasts = true;
    }

    public void CloseUpgradeBag()
    {
        bgFadeCanvasGroup.alpha = 0;
        bgFadeCanvasGroup.blocksRaycasts = false;
        upgradeBagCanvasGroup.alpha = 0;
        upgradeBagCanvasGroup.blocksRaycasts = false;
    }

    public void OpenResult10ChestPopup()
    {
        bgFadeCanvasGroup.alpha = 1;
        bgFadeCanvasGroup.blocksRaycasts = true;
        result10ChestCanvasGroup.alpha = 1;
        result10ChestCanvasGroup.blocksRaycasts = true;
    }

    public void CloseResult10ChestPopup()
    {
        bgFadeCanvasGroup.alpha = 0;
        bgFadeCanvasGroup.blocksRaycasts = false;
        result10ChestCanvasGroup.alpha = 0;
        result10ChestCanvasGroup.blocksRaycasts = false;
    }

    public void UpgradeBag()
    {
        if (readData.dataUserModel.diamond < 5)
        {
            OpenAlertMessage(readData.jsonWordingData["notEnoughDiamond"][readData.languageId].ToString(), null);
        }
        else
        {
            StartCoroutine(readData.UpgradeWarriorBagFromOpenChestPage());
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

    public void BackToHome()
    {
        Application.LoadLevel("Home");
    }
}
