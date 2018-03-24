using UnityEngine;
using System.Collections;

public class WarriorListTeamTempData : MonoBehaviour {
    public Team teamScript;
    public string warriorUserId;
    public int tempI;
    public int warriorSize;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveWarriorInTeam()
    {
        if (tempI == warriorSize)
        {
            teamScript.OpenPopupUpgradeWarriorBag();
        }
        else
        {
            if(teamScript.currentPositionEnter != -1){
                teamScript.readData.dataUserModel.warrior.Find(x => x.warriorUserId == warriorUserId).position = (teamScript.currentPositionEnter + 1);
                teamScript.SetDataTeam();
                teamScript.SetDataWarriorStock();
            }
        }
    }
}
