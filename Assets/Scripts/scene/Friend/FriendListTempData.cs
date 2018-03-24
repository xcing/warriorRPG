using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

class FriendListTempData : MonoBehaviour
{
    public FriendList friendScript;
    public int tempI;
    public string dataUserId2;
    public Image displayImage;
    public Text lvAndName;
    public Text warPowerText;
    public Text warPower;
    public Button sendHeartButton;
    public string sendHeartLastedDate;
    WWW www;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData()
    {
        StartCoroutine(downloadImg(friendScript.friends[tempI].displayImage));
        lvAndName.text = "Lv." + friendScript.friends[tempI].level + " " + friendScript.friends[tempI].name;
        warPowerText.text = friendScript.readData.jsonWordingData["warPower"][friendScript.readData.languageId].ToString();
        warPower.text = friendScript.friends[tempI].warPower.ToString("#,#");
        if (friendScript.currentTab == 1)
        {
            if (System.DateTime.Now.ToString("yyyy-MM-dd") != sendHeartLastedDate)
                sendHeartButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/heartIcon");
            else
                sendHeartButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/sendedHeartIcon");
        }
        else if (friendScript.currentTab == 2)
            sendHeartButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/addIcon");
        else if (friendScript.currentTab == 3)
            sendHeartButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/cancelIcon");
    }

    IEnumerator downloadImg(string url)
    {
        Texture2D texture = new Texture2D(1, 1);
        WWW www = new WWW(url);
        yield return www;
        www.LoadImageIntoTexture(texture);

        Sprite image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        displayImage.sprite = image;
    }

    public void FirstButton()
    {
        if (friendScript.currentTab == 1)
        {
            if (System.DateTime.Now.ToString("yyyy-MM-dd") != sendHeartLastedDate)
            {
                friendScript.currentDataUserId2 = dataUserId2;
                StartCoroutine(friendScript.readData.SendHeartToFriend());
            }
        }
        else if (friendScript.currentTab == 2)
        {
            friendScript.currentDataUserId2 = dataUserId2;
            StartCoroutine(friendScript.readData.AcceptFriend());
        }
        else if (friendScript.currentTab == 3)
        {
            friendScript.currentDataUserId2 = dataUserId2;
            StartCoroutine(friendScript.readData.CancelFriend());
        }
    }

    public void InformationButton()
    {
        friendScript.OpenInformation();
        //friendScript.OpenAlertMessage(tempI.ToString() + " Information", null);
    }
}
