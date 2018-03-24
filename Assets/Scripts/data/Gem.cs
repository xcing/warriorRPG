using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LitJson;

[Serializable]
public class Gem
{
    public int gemId;
    public string gemUserId;
    public string equipmentUserIdEquiped; // 0 = free , equipmentUserId = equiped

    public int buffType;
    public int rare;

    public void SetupData(JsonData jsonGemData)
    {
        buffType = Convert.ToInt32(jsonGemData[gemId.ToString()]["buffType"].ToString());
        rare = Convert.ToInt32(jsonGemData[gemId.ToString()]["rare"].ToString());
    }

    public void SetupNewData(int itemId, string itemUserId)
    {
        gemId = itemId;
        gemUserId = itemUserId;
        equipmentUserIdEquiped = "0";
    }
}

