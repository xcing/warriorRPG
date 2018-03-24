using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StageList : MonoBehaviour
{
    public Map mapScript;

    public int areaId;
    public int level;
    public Text headerText;
    public Button StageLevelButton1;
    public Button StageLevelButton2;
    public Button StageLevelButton3;

    //stage list
    public int stageHave;
    int heightStagePanelBox;
    public Image stageBoxScrollRect;
    public Image stageBox;
    public GameObject stageListPrefab;
    public GameObject[] stageUnitBox;

    void Start()
    {
        areaId = 1;
        level = 1;
        
        StageLevelButton1.GetComponentInChildren<Text>().text = mapScript.readData.jsonWordingData["normal"][mapScript.readData.languageId].ToString();
        StageLevelButton2.GetComponentInChildren<Text>().text = mapScript.readData.jsonWordingData["hard"][mapScript.readData.languageId].ToString();
        StageLevelButton3.GetComponentInChildren<Text>().text = mapScript.readData.jsonWordingData["hell"][mapScript.readData.languageId].ToString();

        //SetDataStageList();
    }

    public void SetDataStageList()
    {
        headerText.text = mapScript.readData.jsonAreaData[areaId.ToString()][mapScript.readData.languageId].ToString();

        for (int i = stageBox.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(stageBox.transform.GetChild(i).gameObject);
        }
        if (mapScript.readData.dataUserModel.stage[areaId - 1][level - 1].IndexOf(0) == -1)
            stageHave = 6;
        else
            stageHave = (mapScript.readData.dataUserModel.stage[areaId - 1][level - 1].IndexOf(0) + 1);

        stageUnitBox = new GameObject[stageHave];
        stageListPrefab = Resources.Load<GameObject>("Prefab/StageList");
        if (stageHave > 3)
            heightStagePanelBox = (stageHave * 190) + 20;
        else
            heightStagePanelBox = Convert.ToInt32(stageBoxScrollRect.rectTransform.rect.height);

        if (heightStagePanelBox < stageBoxScrollRect.rectTransform.rect.height)
        {
            stageBox.rectTransform.anchoredPosition = new Vector3(0, 0);
        }
        else
        {
            stageBox.rectTransform.anchoredPosition = new Vector3(0, (heightStagePanelBox - stageBoxScrollRect.rectTransform.rect.height) / -2);
        }
        stageBox.rectTransform.sizeDelta = new Vector2(stageBox.rectTransform.rect.width, heightStagePanelBox);

        for (int i = 0; i < stageHave; i++)
        {
            stageUnitBox[i] = Instantiate(stageListPrefab, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
            stageUnitBox[i].transform.SetParent(stageBox.transform, true);
            stageUnitBox[i].transform.localScale = new Vector3(1, 1, 1);
            stageUnitBox[i].transform.position = stageBox.transform.position;
            stageUnitBox[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-23, (heightStagePanelBox / 2) - 105 - (i * 190));
            if (mapScript.readData.dataUserModel.stage[areaId - 1][level - 1][i] == 1)
                stageUnitBox[i].GetComponent<Image>().color = new Color32(73, 178, 57, 255);
            else
                stageUnitBox[i].GetComponent<Image>().color = new Color32(0, 0, 0, 80);

            StageListTempData tempData = stageUnitBox[i].GetComponent<StageListTempData>();
            tempData.stageList = this;
            tempData.ordinal = (i + 1);
            tempData.SetData();
            stageUnitBox[i].GetComponentInChildren<Button>().onClick.AddListener(() => tempData.BattleButton());
        }
    }

    public void PickLevel(int position)
    {
        if(position == 1){
            level = position;
            SetDataStageList();
        }
        else if (mapScript.readData.dataUserModel.stage[areaId - 1][position - 2][5] == 1)
        {
            level = position;
            SetDataStageList();
        }
        else
        {
            if(position == 2){
                mapScript.OpenAlertMessage(mapScript.readData.jsonWordingData["mustPassLevel"][mapScript.readData.languageId].ToString() + mapScript.readData.jsonWordingData["normal"][mapScript.readData.languageId].ToString() + mapScript.readData.jsonWordingData["first"][mapScript.readData.languageId].ToString());
            }
            else if (position == 3)
            {
                mapScript.OpenAlertMessage(mapScript.readData.jsonWordingData["mustPassLevel"][mapScript.readData.languageId].ToString() + mapScript.readData.jsonWordingData["hard"][mapScript.readData.languageId].ToString() + mapScript.readData.jsonWordingData["first"][mapScript.readData.languageId].ToString());
            }
        }
    }

    void Update()
    {

    }
}


