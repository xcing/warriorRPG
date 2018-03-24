using UnityEngine;
using System.Collections;

public class WarriorListTempData : MonoBehaviour {
    public string warriorUserId;
    public int tempI;
    public int warriorSize;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenWarriorDetail()
    {
        if (tempI == warriorSize)
        {
            GameObject.Find("WarriorListScript").GetComponent<WarriorList>().OpenPopupUpgradeWarriorBag();
        }
        else
        {
            GameObject.Find("WarriorListScript").GetComponent<WarriorList>().warriorI = tempI;
            GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>().warriorUserId = warriorUserId;
            GameObject.Find("WarriorDetailScript").GetComponent<WarriorDetail>().SetValueWarrior();
            GameObject.Find("MainScript").GetComponent<Home>().OpenWarriorDetail();
        }
    }
}
