using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LitJson;


[Serializable]
public class Equipment
{
    public int equipmentId;
    public string equipmentUserId;

    public int lvPlus;
    public string[] dataGemUserId;
    public string warriorUserIdEquiped; // 0 = free , warriorUserId = equiped

    public int rare;
    public int equipmentType; //1 = weapon, 2 = armor, 3 = helm, 4 = boots, 5 = accessory
    public int warriorId;

    public void SetupData(JsonData jsonEquipmentData)
    {
        rare = Convert.ToInt32(jsonEquipmentData[equipmentId.ToString()]["rare"].ToString());
        equipmentType = Convert.ToInt32(jsonEquipmentData[equipmentId.ToString()]["equipmentType"].ToString());
        warriorId = Convert.ToInt32(jsonEquipmentData[equipmentId.ToString()]["warriorId"].ToString());
    }

    public void SetupNewData(int itemId, string itemUserId, int rare)
    {
        equipmentId = itemId;
        equipmentUserId = itemUserId;
        lvPlus = 0;
        if (rare == 6)
        {
            dataGemUserId = new string[3];
            dataGemUserId[0] = "-1";
            dataGemUserId[1] = "-1";
            dataGemUserId[2] = "-1";
        }
        else if (rare >= 4)
        {
            dataGemUserId = new string[2];
            dataGemUserId[0] = "-1";
            dataGemUserId[2] = "-1";
        }
        else if (rare >= 2)
        {
            dataGemUserId = new string[1];
            dataGemUserId[0] = "-1";
        }
        warriorUserIdEquiped = "0";
    }
}


