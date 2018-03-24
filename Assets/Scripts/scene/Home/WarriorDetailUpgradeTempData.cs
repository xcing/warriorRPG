using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WarriorDetailUpgradeTempData : MonoBehaviour
{
    public string warriorUserId;
    public int tempI;
    public WarriorDetail warriorDetail;

    void Start()
    {

    }

    void Update()
    {

    }

    public void PickWarriorStockDetail()
    {
        warriorDetail.borderWarriorStockPick.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(warriorDetail.warriorStockUnitBox[tempI].GetComponent<Image>().rectTransform.anchoredPosition.x, warriorDetail.warriorStockUnitBox[tempI].GetComponent<Image>().rectTransform.anchoredPosition.y);
        warriorDetail.currentPickUpgradeWarrior = warriorUserId;
    }
}
