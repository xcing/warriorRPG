using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StageListTempData : MonoBehaviour
{
    public StageList stageList;
    public Text stageName;
    public Text dropInfo;
    public Image dropItemImage1;
    public Text dropItemText1;
    public Image dropItemImage2;
    public Text dropItemText2;
    public Image dropItemImage3;
    public Text dropItemText3;
    public Button battleButton;
    public int ordinal;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetData()
    {
        stageName.text = stageList.mapScript.readData.jsonStageData[stageList.areaId.ToString()][stageList.level.ToString()][ordinal.ToString()]["name"][stageList.mapScript.readData.languageId].ToString();
        dropInfo.text = stageList.mapScript.readData.jsonWordingData["dropInfo"][stageList.mapScript.readData.languageId].ToString();
        dropItemImage1.sprite = Resources.Load<Sprite>("UI/warriorRandomIcon" + stageList.mapScript.readData.jsonStageData[stageList.areaId.ToString()][stageList.level.ToString()][ordinal.ToString()]["drop_warriorRare"].ToString());
        dropItemImage2.sprite = Resources.Load<Sprite>("UI/equipmentRandomIcon" + stageList.mapScript.readData.jsonStageData[stageList.areaId.ToString()][stageList.level.ToString()][ordinal.ToString()]["drop_equipmentRare"].ToString());
        dropItemImage3.sprite = Resources.Load<Sprite>("UI/gemRandomIcon" + stageList.mapScript.readData.jsonStageData[stageList.areaId.ToString()][stageList.level.ToString()][ordinal.ToString()]["drop_gemRare"].ToString());
        dropItemText1.text = "สุ่มนักรบเขียว";
        dropItemText2.text = "สุ่มอุปกรณ์เขียว";
        dropItemText3.text = "สุ่มพลอยเขียว";
    }

    public void BattleButton()
    {
        stageList.mapScript.OpenAlertMessage("ต่อสู้ " + stageName.text);
    }
}
