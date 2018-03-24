using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FriendList : MonoBehaviour
{
    public ReadData readData;
    public GameObject alertMessageBg;
    public float countdown;

    public Text header;
    public CanvasGroup bgFadeCanvasGroup;
    public InputField addFriendInputField;
    public Text friendTabButtonText;
    public Text acceptTabButtonText;
    public Text pendingTabButtonText;
    public int currentTab;
    public string currentDataUserId2;
    public Button sendHeartAllFriendButton;

    // upgrade bag popup
    public CanvasGroup upgradeBagCanvasGroup;
    public Text upgradeBagHeader;
    public Text upgradeBagText;

    // friend list
    public int friendHave;
    int heightFriendPanelBox;
    public Image friendScrollRect;
    public Image friendBox;
    public GameObject friendUnitBoxPrefab;
    public GameObject[] friendUnitBox;
    public List<Friend> friends;

    // infomation popup
    public CanvasGroup informationPopupCanvasGroup;

    void Start()
    {
        currentTab = 1;
        header.text = readData.jsonWordingData["friendList"][readData.languageId].ToString();
        friendTabButtonText.text = readData.jsonWordingData["friendList"][readData.languageId].ToString();
        acceptTabButtonText.text = readData.jsonWordingData["accepting"][readData.languageId].ToString();
        pendingTabButtonText.text = readData.jsonWordingData["pending"][readData.languageId].ToString();
        SetDataFriendListUnitBox();
    }

    public void SetDataFriendListUnitBox()
    {
        for (int i = friendBox.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(friendBox.transform.GetChild(i).gameObject);
        }
        friends = readData.dataUserModel.friend.FindAll(x => x.status == currentTab);

        friendHave = friends.Count;

        friendUnitBox = new GameObject[friendHave];
        friendUnitBoxPrefab = Resources.Load<GameObject>("Prefab/FriendListUnitBox");
        if (friendHave > 5)
            heightFriendPanelBox = (friendHave * 160) + 40;
        else
            heightFriendPanelBox = Convert.ToInt32(friendScrollRect.rectTransform.rect.height);

        if (heightFriendPanelBox < friendScrollRect.rectTransform.rect.height)
        {
            friendBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            friendBox.rectTransform.anchoredPosition = new Vector3(0, (heightFriendPanelBox - friendScrollRect.rectTransform.rect.height) / -2);
        }
        friendBox.rectTransform.sizeDelta = new Vector2(friendBox.rectTransform.rect.width, heightFriendPanelBox);

        for (int i = 0; i < friendHave; i++)
        {
            friendUnitBox[i] = Instantiate(friendUnitBoxPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            friendUnitBox[i].transform.SetParent(friendBox.transform, true);
            friendUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            friendUnitBox[i].transform.position = friendBox.transform.position;
            friendUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(0, (heightFriendPanelBox / 2) - 95 - (i * 160));

            FriendListTempData tempData = friendUnitBox[i].GetComponent<FriendListTempData>();
            tempData.friendScript = this;
            tempData.tempI = i;
            tempData.dataUserId2 = friends[i].data_userId2;
            tempData.sendHeartLastedDate = friends[i].sendHeartLastedDate;
            tempData.SetData();
            friendUnitBox[i].GetComponentsInChildren<Button>()[1].onClick.AddListener(() => tempData.FirstButton());
            friendUnitBox[i].GetComponentsInChildren<Button>()[2].onClick.AddListener(() => tempData.InformationButton());
        }
    }

    public void AddFriend()
    {
        StartCoroutine(readData.AddFriend());
        //OpenAlertMessage(addFriendInputField.text, null);
    }

    public void SendHeartAllFriend()
    {
        if (friends.FindAll(x => x.sendHeartLastedDate != System.DateTime.Now.ToString("yyyy-MM-dd")).Count > 0)
        {
            StartCoroutine(readData.SendHeartAllFriend());
        }
        else
        {
            OpenAlertMessage(readData.jsonWordingData["alreadySendHeart"][readData.languageId].ToString(), null);
        }
    }

    public void ChangeTab(int position)
    {
        if (currentTab != position)
        {
            currentTab = position;
            if (currentTab != 1)
                sendHeartAllFriendButton.GetComponent<CanvasRenderer>().SetAlpha(0);
            else
                sendHeartAllFriendButton.GetComponent<CanvasRenderer>().SetAlpha(1);
            SetDataFriendListUnitBox();
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

    public void OpenInformation()
    {
        bgFadeCanvasGroup.alpha = 1;
        bgFadeCanvasGroup.blocksRaycasts = true;
        informationPopupCanvasGroup.alpha = 1;
        informationPopupCanvasGroup.blocksRaycasts = true;
    }

    public void CloseInformation()
    {
        bgFadeCanvasGroup.alpha = 0;
        bgFadeCanvasGroup.blocksRaycasts = false;
        informationPopupCanvasGroup.alpha = 0;
        informationPopupCanvasGroup.blocksRaycasts = false;
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
