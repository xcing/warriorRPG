using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class Map : MonoBehaviour
{

    public ReadData readData;
    public GameObject alertMessageBg;

    public CanvasGroup stageListCanvasGroup;
    public CanvasGroup bgFadeCanvaseGroup;

    public float countdown;
    public Image energyGuage;
    public Text energyValue;
    public Text energyTiming;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        energyValue.text = readData.dataUserModel.energy + "/" + (readData.dataUserModel.level + 99);
        energyGuage.fillAmount = (float)readData.dataUserModel.energy / (float)(readData.dataUserModel.level + 99);
        energyTiming.text = "02:35";

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

    public void OpenAlertMessage(string message)
    {
        alertMessageBg.transform.position = new Vector3(0, 46, 0);
        alertMessageBg.GetComponent<CanvasGroup>().alpha = 1;
        alertMessageBg.GetComponent<CanvasGroup>().blocksRaycasts = true;
        alertMessageBg.GetComponentInChildren<Text>().text = message;
    }

    public void OpenArea(int areaId)
    {
        if (areaId != 1)
        {
            if (readData.dataUserModel.stage[areaId - 2][0][5] == 1)
            {
                StartCoroutine(OpenAreaScript(areaId));
            }
            else
            {
                OpenAlertMessage(readData.jsonWordingData["areaIsLock"][readData.languageId].ToString());
            }
        }
        else
        {
            StartCoroutine(OpenAreaScript(areaId));
        }
    }

    public IEnumerator OpenAreaScript(int areaId)
    {
        GameObject.Find("StageListScript").GetComponent<StageList>().areaId = areaId;
        GameObject.Find("StageListScript").GetComponent<StageList>().level = 1;
        bgFadeCanvaseGroup.alpha = 1;
        bgFadeCanvaseGroup.blocksRaycasts = true;
        stageListCanvasGroup.alpha = 1;
        stageListCanvasGroup.blocksRaycasts = true;
        GameObject.Find("StageListScript").GetComponent<StageList>().SetDataStageList();
        yield return null;
    }

    public void CloseArea()
    {
        bgFadeCanvaseGroup.alpha = 0;
        bgFadeCanvaseGroup.blocksRaycasts = false;
        stageListCanvasGroup.alpha = 0;
        stageListCanvasGroup.blocksRaycasts = false;
    }
}
