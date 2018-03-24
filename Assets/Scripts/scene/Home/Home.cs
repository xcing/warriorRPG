using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Home : MonoBehaviour
{

    //public int languageId; // 0 = en, 1 = th, 2 = cn
    public CanvasGroup mainCanvasGroup;
    public CanvasGroup bgFadeCanvasGroup;
    public CanvasGroup bgFade2CanvasGroup;
    public CanvasGroup warriorListCanvasGroup;
    public CanvasGroup warriorDetailCanvasGroup;
    public CanvasGroup equipmentListCanvasGroup;
    public string userId;
    public ReadData readData;
    public GameObject alertMessageBg;
    GameObject[] bgMusic;

    public float countdown;

    void Awake()
    {
        bgMusic = GameObject.FindGameObjectsWithTag("bgMusic");
        if (bgMusic.Length > 1)
        {
            for (int i = bgMusic.Length; i > 1; i--)
            {
                Destroy(bgMusic[i - 1]);
            }
        }
        DontDestroyOnLoad(bgMusic[0]);
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

    public void WarriorListButton()
    {
        mainCanvasGroup.blocksRaycasts = false;
        bgFadeCanvasGroup.alpha = 1;
        bgFadeCanvasGroup.blocksRaycasts = true;
        warriorListCanvasGroup.alpha = 1;
        warriorListCanvasGroup.blocksRaycasts = true;
    }

    public void BattleButton()
    {
        Application.LoadLevel("Map");
    }

    public void Battle2Button()
    {
        Application.LoadLevel("Battle");
    }

    public void TeamButton()
    {
        Application.LoadLevel("Team");
    }

    public void ShopButton()
    {
        Application.LoadLevel("Shop");
    }

    public void InboxButton()
    {
        Application.LoadLevel("Inbox");
    }

    public void OpenChestButton()
    {
        Application.LoadLevel("OpenChest");
    }

    public void QuestButton()
    {
        Application.LoadLevel("Quest");
    }

    public void FriendButton()
    {
        Application.LoadLevel("Friend");
    }

    public void OpenWarriorDetail()
    {
        warriorDetailCanvasGroup.alpha = 1;
        warriorDetailCanvasGroup.blocksRaycasts = true;
    }

    public void CloseWarriorList()
    {
        mainCanvasGroup.blocksRaycasts = true;
        bgFadeCanvasGroup.alpha = 0;
        bgFadeCanvasGroup.blocksRaycasts = false;
        warriorListCanvasGroup.alpha = 0;
        warriorListCanvasGroup.blocksRaycasts = false;
    }

    public void CloseWarriorDetail()
    {
        warriorDetailCanvasGroup.alpha = 0;
        warriorDetailCanvasGroup.blocksRaycasts = false;
        if (GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>().monsterEntity != null)
        {
            GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>().monsterEntity.SetActive(false);
        }
    }

    public void OpenEquipmentList()
    {
        bgFade2CanvasGroup.alpha = 1;
        bgFade2CanvasGroup.blocksRaycasts = true;
        equipmentListCanvasGroup.alpha = 1;
        equipmentListCanvasGroup.blocksRaycasts = true;
        if (GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>().monsterEntity != null)
        {
            GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>().monsterEntity.SetActive(false);
        }
    }

    public void CloseEquipmentList()
    {
        bgFade2CanvasGroup.alpha = 0;
        bgFade2CanvasGroup.blocksRaycasts = false;
        equipmentListCanvasGroup.alpha = 0;
        equipmentListCanvasGroup.blocksRaycasts = false;
        if (GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>().monsterEntity != null)
        {
            GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>().monsterEntity.SetActive(true);
        }
    }

    public void CloseEquipmentListFromEquipmentManage()
    {
        bgFade2CanvasGroup.alpha = 0;
        bgFade2CanvasGroup.blocksRaycasts = false;
        equipmentListCanvasGroup.alpha = 0;
        equipmentListCanvasGroup.blocksRaycasts = false;
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
}
