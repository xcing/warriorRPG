using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Inbox : MonoBehaviour
{

    public ReadData readData;
    public GameObject alertMessageBg;
    public float countdown;

    public Text header;
    public CanvasGroup bgFadeCanvasGroup;
    public CanvasGroup bgFade2CanvasGroup;

    //scrroll rect
    public int mailHave;
    public Image mailListBoxScrollRect;
    public Image mailListBox;
    public GameObject mailListUnitBoxPrefab;
    public GameObject[] mailListUnitBox;
    public List<Mail> mails;

    //popup
    public CanvasGroup mailDetailPopupCanvasGroup;
    public Text senderName;
    public Text mailSubject;
    public Text mailDetail;
    public Image itemImage;
    public Text itemName;
    public Button receiptButton;
    public int currentPickMail;

    // upgrade bag popup
    public CanvasGroup upgradeBagCanvasGroup;
    public Text upgradeBagHeader;
    public Text upgradeBagText;

    void Start()
    {
        header.text = readData.jsonWordingData["inbox"][readData.languageId].ToString();

        SetDataMailList();
    }

    public void SetDataMailList()
    {
        for (int i = mailListBox.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(mailListBox.transform.GetChild(i).gameObject);
        }
        mails = readData.dataUserModel.mail;

        mailHave = mails.Count;

        mailListUnitBox = new GameObject[mailHave];
        mailListUnitBoxPrefab = Resources.Load<GameObject>("Prefab/MailListUnitBox");

        int heightMailListPanelBox;
        if (mailHave > 3)
        {
            heightMailListPanelBox = mailHave * 240;
            mailListBox.rectTransform.anchoredPosition = new Vector3(0, (heightMailListPanelBox - 920) / 2);
        }
        else
        {
            heightMailListPanelBox = 920;
            mailListBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        if (heightMailListPanelBox < mailListBoxScrollRect.rectTransform.rect.height)
        {
            heightMailListPanelBox = Convert.ToInt32(mailListBoxScrollRect.rectTransform.rect.height);
            mailListBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            mailListBox.rectTransform.anchoredPosition = new Vector3(0, (heightMailListPanelBox - mailListBoxScrollRect.rectTransform.rect.height) / -2);
        }
        mailListBox.rectTransform.sizeDelta = new Vector2(mailListBox.rectTransform.rect.width, heightMailListPanelBox);

        for (int i = 0; i < mailHave; i++)
        {
            mailListUnitBox[i] = Instantiate(mailListUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            mailListUnitBox[i].transform.SetParent(mailListBox.transform, true);
            mailListUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            mailListUnitBox[i].transform.position = mailListBox.transform.position;
            mailListUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(0, (heightMailListPanelBox / 2) - 130 - (i * 240));

            mailListUnitBox[i].GetComponentInChildren<Text>().text = mails[i].subject[readData.languageId];

            InboxTempData tempData = mailListUnitBox[i].GetComponent<InboxTempData>();
            tempData.tempI = i;
            mailListUnitBox[i].GetComponentsInChildren<Button>()[1].onClick.AddListener(() => tempData.ViewMail());
            mailListUnitBox[i].GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = readData.jsonWordingData["view"][readData.languageId].ToString();
        }
    }

    public void OpenMailDetailPopup(int position)
    {
        currentPickMail = position;
        bgFadeCanvasGroup.alpha = 1;
        bgFadeCanvasGroup.blocksRaycasts = true;
        mailDetailPopupCanvasGroup.alpha = 1;
        mailDetailPopupCanvasGroup.blocksRaycasts = true;

        senderName.text = readData.jsonWordingData["systemSender"][readData.languageId].ToString();
        mailSubject.text = mails[position].subject[readData.languageId];
        mailDetail.text = mails[position].detail[readData.languageId];
        switch(mails[position].itemType){
            case "0":
                itemImage.sprite = null;
                itemImage.GetComponent<CanvasRenderer>().SetAlpha(0);
                itemName.text = "";
                break;
            case "1":
                itemImage.sprite = Resources.Load<Sprite>("Warriors/" + readData.jsonWarriorData[mails[position].itemId]["name"][0].ToString() + "/icon");
                itemImage.GetComponent<CanvasRenderer>().SetAlpha(1);
                itemName.text = readData.jsonWordingData["soul"][readData.languageId].ToString() + readData.jsonWarriorData[mails[position].itemId]["name"][readData.languageId].ToString();
                if (mails[position].itemAmount > 1)
                    itemName.text = itemName.text + " x" + mails[position].itemAmount.ToString();
                break;
            case "2":
                itemImage.sprite = Resources.Load<Sprite>("Warriors/" + readData.jsonWarriorData[mails[position].itemId]["name"][0].ToString() + "/icon");
                itemImage.GetComponent<CanvasRenderer>().SetAlpha(1);
                itemName.text = readData.jsonWarriorData[mails[position].itemId]["name"][readData.languageId].ToString();
                break;
            case "3":
                itemImage.sprite = Resources.Load<Sprite>("equipments/" + readData.jsonEquipmentData[mails[position].itemId]["name"][0].ToString());
                itemImage.GetComponent<CanvasRenderer>().SetAlpha(1);
                itemName.text = readData.jsonEquipmentData[mails[position].itemId]["name"][readData.languageId].ToString();
                break;
            case "4":
                itemImage.sprite = Resources.Load<Sprite>("gems/" + readData.jsonGemData[mails[position].itemId]["name"][0].ToString());
                itemImage.GetComponent<CanvasRenderer>().SetAlpha(1);
                itemName.text = readData.jsonGemData[mails[position].itemId]["name"][readData.languageId].ToString();
                break;
            case "5":
                itemImage.sprite = Resources.Load<Sprite>("UI/moneyIcon");
                itemImage.GetComponent<CanvasRenderer>().SetAlpha(1);
                itemName.text = mails[position].itemAmount.ToString("#,#");
                break;
            case "6":
                itemImage.sprite = Resources.Load<Sprite>("UI/oreIcon");
                itemImage.GetComponent<CanvasRenderer>().SetAlpha(1);
                itemName.text = mails[position].itemAmount.ToString("#,#");
                break;
        }
        receiptButton.GetComponentInChildren<Text>().text = readData.jsonWordingData["receipt"][readData.languageId].ToString();
    }

    public void CloseMailDetailPopup()
    {
        bgFadeCanvasGroup.alpha = 0;
        bgFadeCanvasGroup.blocksRaycasts = false;
        mailDetailPopupCanvasGroup.alpha = 0;
        mailDetailPopupCanvasGroup.blocksRaycasts = false;
    }

    public void ReceiptButton()
    {
        if (mails[currentPickMail].itemType == "2" && readData.dataUserModel.warrior.Count >= readData.dataUserModel.bag_warrior)
        {
            SetWordingUpgradePopup();
            OpenPopupUpgradeBag();
        }
        else if (mails[currentPickMail].itemType == "3" && readData.dataUserModel.equipment.Count >= readData.dataUserModel.bag_equipment)
        {
            SetWordingUpgradePopup();
            OpenPopupUpgradeBag();
        }
        else if (mails[currentPickMail].itemType == "4" && readData.dataUserModel.gem.Count >= readData.dataUserModel.bag_gem)
        {
            SetWordingUpgradePopup();
            OpenPopupUpgradeBag();
        }
        else
        {
            StartCoroutine(readData.ReceiptMail());
            CloseMailDetailPopup();
        }
        
    }

    public void SetWordingUpgradePopup()
    {
        switch (mails[currentPickMail].itemType)
        {
            case "2":
                upgradeBagHeader.text = readData.jsonWordingData["upgradeBag"][readData.languageId].ToString();
                upgradeBagText.text = readData.jsonWordingData["sizeBag"][readData.languageId].ToString() + "+5";
                break;
            case "3":
                upgradeBagHeader.text = readData.jsonWordingData["upgradeBag"][readData.languageId].ToString();
                upgradeBagText.text = readData.jsonWordingData["sizeBag"][readData.languageId].ToString() + "+5";
                break;
            case "4":
                upgradeBagHeader.text = readData.jsonWordingData["upgradeBag"][readData.languageId].ToString();
                upgradeBagText.text = readData.jsonWordingData["sizeBag"][readData.languageId].ToString() + "+5";
                break;
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
            StartCoroutine(readData.UpgradeBagFromInbox());
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
