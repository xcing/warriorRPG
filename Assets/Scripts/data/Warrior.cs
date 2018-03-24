using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LitJson;
using System.Collections.Generic;
using System.Collections;

[Serializable]
public class Warrior
{
    public int warriorId;
    public string warriorUserId;
    public int level = 1;
    public float exp = 0.0f;
    public int lvPlus = 0;
    public List<string> dataEquipmentUserId;
    public List<int> skillLv;
    public List<int> skillLvPlus;
    public int position;
    //public bool isLeader;
    public bool isLock;

    public int hp = 0;
    public int atk = 0;
    public int def = 0;
    public int spd = 0;
    public int luck = 0;

    public int hpBegin = 0;
    public int atkBegin = 0;
    public int defBegin = 0;
    public int spdBegin = 0;
    public int luckBegin = 0;

    public int hpPlus = 0;
    public int atkPlus = 0;
    public int defPlus = 0;
    public int spdPlus = 0;
    public int luckPlus = 0;

    public int hpPercentPlus = 0;
    public int atkPercentPlus = 0;
    public int defPercentPlus = 0;
    public int spdPercentPlus = 0;
    public int luckPercentPlus = 0;

    public int rare;
    public string[] name = new string[3];

    public int amountSkill;
    public int elementId;
    public int weaponTypeId;
    //public int leaderSkillId;
    public string defaultStatWhenLvUp;
    public string[] defaultStatWhenLvUpArray;
    public string defaultStatWhenPlusLvUp;
    public string[] defaultStatWhenPlusLvUpArray;

    public Skill[] skill;
    public List<int[]> conditionType1 = new List<int[]>(); // ตอนโจมตี
    public List<int[]> conditionType2 = new List<int[]>(); // ติดตัว
    public List<int[]> conditionType3 = new List<int[]>(); // ตอนโดนตี
    public List<int[]> conditionType4 = new List<int[]>(); // ตอนเข้าเทิร์น
    public float damageAmplifier = 0.1f;

    public bool haveData = false;

    public Warrior()
    {

    }

    public void SetupData(ReadData readData)
    {
        SetDataFromDefault(readData);
        SetSkillDataFromUserData(readData);
        SetDataFromLevelAndEquipment(readData);
    }

    public void SetupNewData(int itemId, string itemUserId)
    {
        warriorId = itemId;
        warriorUserId = itemUserId;
        level = 1;
        exp = 0;
        lvPlus = 0;
        List<string> dataEquipmentUserIdListTemp = new List<string>();
        dataEquipmentUserIdListTemp.Add("0");
        dataEquipmentUserIdListTemp.Add("0");
        dataEquipmentUserIdListTemp.Add("0");
        dataEquipmentUserIdListTemp.Add("0");
        dataEquipmentUserIdListTemp.Add("0");
        dataEquipmentUserIdListTemp.Add("0");
        dataEquipmentUserId = dataEquipmentUserIdListTemp;
        List<int> skillLvListTemp = new List<int>();
        skillLvListTemp.Add(1);
        skillLvListTemp.Add(1);
        skillLvListTemp.Add(1);
        skillLv = skillLvListTemp;
        skillLvPlus = skillLvListTemp;
        position = 0;
        isLock = false;
    }

    /*public void setLeader(bool isLeader)
    {
        this.isLeader = isLeader;
    }*/

    public void SetDataFromDefault(ReadData readData)
    {
        string warriorIdStr = warriorId.ToString();
        name[0] = (string)readData.jsonWarriorData[warriorIdStr]["name"][0];
        name[1] = (string)readData.jsonWarriorData[warriorIdStr]["name"][1];
        name[2] = (string)readData.jsonWarriorData[warriorIdStr]["name"][2];
        rare = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["rare"]);
        hpBegin = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["hp"]);
        atkBegin = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["atk"]);
        defBegin = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["def"]);
        spdBegin = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["spd"]);
        luckBegin = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["luck"]);
        elementId = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["elementId"]);
        //leaderSkillId = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["leaderSkillId"]);
        defaultStatWhenLvUp = (string)readData.jsonWarriorData[warriorIdStr]["defaultStatWhenLvUp"];
        defaultStatWhenLvUpArray = defaultStatWhenLvUp.Split(',');
        defaultStatWhenPlusLvUp = (string)readData.jsonWarriorData[warriorIdStr]["defaultStatWhenPlusLvUp"];
        defaultStatWhenPlusLvUpArray = defaultStatWhenPlusLvUp.Split(',');
        amountSkill = Convert.ToInt32((string)readData.jsonWarriorData[warriorIdStr]["amountSkill"]);

        skill = new Skill[amountSkill];
        for (int i = 0; i < amountSkill; i++)
        {
            skill[i] = new Skill();
            skill[i].SetDataFromDefault(warriorId, i, readData.jsonSkillData);
        }
    }

    public void SetSkillDataFromUserData(ReadData readData)
    {
        for (int i = 0; i < amountSkill; i++)
        {
            skill[i].calculateStatSKill(skillLv[i], skillLvPlus[i]);
        }
    }

    /*public void SetDataFromDataUser(ReadData readData)//plus stat from lv, skill, talent, equipment
    {
        hp += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[0])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[0]));
        atk += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[1])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[1]));
        def += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[2])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[2]));
        spd += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[3])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[3]));
        luck += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[4])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[4]));
    }*/

    public void SetDataFromLevelAndEquipment(ReadData readData)
    {
        hpPlus = 0;
        atkPlus = 0;
        defPlus = 0;
        spdPlus = 0;
        luckPlus = 0;

        hpPlus += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[0])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[0]));
        atkPlus += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[1])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[1]));
        defPlus += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[2])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[2]));
        spdPlus += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[3])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[3]));
        luckPlus += ((level - 1) * Convert.ToInt32((string)defaultStatWhenLvUpArray[4])) + (lvPlus * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[4]));

        hpPlus += ((lvPlus + 1)/10) * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[0]);
        atkPlus += ((lvPlus + 1) / 10) * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[1]);
        defPlus += ((lvPlus + 1) / 10) * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[2]);
        spdPlus += ((lvPlus + 1) / 10) * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[3]);
        luckPlus += ((lvPlus + 1) / 10) * Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[4]);

        if (lvPlus == 100)
        {
            hpPlus += Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[0]);
            atkPlus += Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[1]);
            defPlus += Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[2]);
            spdPlus += Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[3]);
            luckPlus += Convert.ToInt32((string)defaultStatWhenPlusLvUpArray[4]);
        }

        foreach (string equipmentUserId in dataEquipmentUserId)
        {
            if (equipmentUserId == "0")
            {
                continue;
            }
            Equipment equipmentModel = readData.dataUserModel.equipment.Find(x => x.equipmentUserId == equipmentUserId);
            if (equipmentModel.equipmentId == 0)
            {
                continue;
            }
            hpPlus += Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["hp"]) + (equipmentModel.lvPlus * Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["hpPlusPerLv"]));
            atkPlus += Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["atk"]) + (equipmentModel.lvPlus * Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["atkPlusPerLv"]));
            defPlus += Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["def"]) + (equipmentModel.lvPlus * Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["defPlusPerLv"]));
            spdPlus += Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["spd"]) + (equipmentModel.lvPlus * Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["spdPlusPerLv"]));
            luckPlus += Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["luck"]) + (equipmentModel.lvPlus * Convert.ToInt32((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["luckPlusPerLv"]));
            if ((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["conditionId"] != "0")
            {
                SetCondition((string)readData.jsonEquipmentData[equipmentModel.equipmentId.ToString()]["conditionId"], readData.jsonConditionData);
            }
            foreach (string gemUserId in equipmentModel.dataGemUserId)
            {
                if (gemUserId == "0" || gemUserId == "-1")
                {
                    continue;
                }
                Gem gemModel = readData.dataUserModel.gem.Find(x => x.gemUserId == gemUserId);
                if (gemModel.gemId == 0)
                {
                    continue;
                }
                switch (readData.jsonGemData[gemModel.gemId.ToString()]["buffType"].ToString())
                {
                    case "1":
                        hpPercentPlus += Convert.ToInt32((string)readData.jsonGemData[gemModel.gemId.ToString()]["buffValue"]);
                        break;
                    case "2":
                        atkPercentPlus += Convert.ToInt32((string)readData.jsonGemData[gemModel.gemId.ToString()]["buffValue"]);
                        break;
                    case "3":
                        defPercentPlus += Convert.ToInt32((string)readData.jsonGemData[gemModel.gemId.ToString()]["buffValue"]);
                        break;
                    case "4":
                        spdPercentPlus += Convert.ToInt32((string)readData.jsonGemData[gemModel.gemId.ToString()]["buffValue"]);
                        break;
                    case "5":
                        luckPercentPlus += Convert.ToInt32((string)readData.jsonGemData[gemModel.gemId.ToString()]["buffValue"]);
                        break;
                    default:
                        hpPercentPlus += Convert.ToInt32((string)readData.jsonGemData[gemModel.gemId.ToString()]["buffValue"]);
                        break;
                }
                /*if ((string)readData.jsonGemData[gemModel.gemId.ToString()]["conditionId"] != "0")
                {
                    SetCondition((string)readData.jsonGemData[gemModel.gemId.ToString()]["conditionId"], readData.jsonConditionData);
                }*/
            }

            /*string conditionTemp = (string)readData.jsonEquipmentData[equipmentId]["conditionId"]; //use when 1 equipment have more than 1 condition
            string[] conditionArray = conditionTemp.Split(","[0]);
            foreach (string conditionId in conditionArray)
            {
                SetCondition(conditionId, readData.jsonConditionData);
            }*/
        }

        //SetDataFromPassiveSkill(readData);

        hp = (hpBegin + hpPlus) * (hpPercentPlus + 100) / 100;
        atk = (atkBegin + atkPlus) * (atkPercentPlus + 100) / 100;
        def = (defBegin + defPlus) * (defPercentPlus + 100) / 100;
        spd = (spdBegin + spdPlus) * (spdPercentPlus + 100) / 100;
        luck = (luckBegin + luckPlus) * (luckPercentPlus + 100) / 100;

        haveData = true;
    }

    /*public void SetDataFromPassiveSkill(ReadData readData)
    {
        for (int i = 0; i < amountSkill; i++)
        {
            skill[i].calculateStatSKill(skillLv[i], skillLvPlus[i]);
            if (skillLv[i] > 0 && skill[i].useWhen == 2 && skill[i].selfCondId != 0)
            {
                SetCondition(skill[i].selfCondId.ToString(), readData.jsonConditionData);
            }
            if (skillLv[i] > 0 && skill[i].useWhen == 2 && skill[i].reactCondId != 0)
            {
                SetCondition(skill[i].reactCondId.ToString(), readData.jsonConditionData);
            }
        }
    }*/

    public void SetCondition(string conditionId, JsonData jsonConditionData)
    {
        if (conditionId == "0")
        {
            return;
        }
        int condId = Convert.ToInt32(conditionId);
        int type = Convert.ToInt32((string)jsonConditionData[conditionId]["type"]);
        int durationTurn = Convert.ToInt32((string)jsonConditionData[conditionId]["durationTurn"]);
        //string name = jsonConditionData[conditionId]["name"].ToString();
        int[] conditionModel = new int[2];
        conditionModel[0] = condId; // conditionId
        conditionModel[1] = durationTurn; // turn
        switch (type)
        {

            case 1:
                int[] cond = conditionType1.Find(x => x[0] == condId);
                if (cond != null)
                {
                    if (cond[1] < durationTurn)
                    {
                        //Debug.Log(name + "increase duration from " + cond[1] + " to " + durationTurn);
                        cond[1] = durationTurn;

                    }

                }
                else
                {
                    //Debug.Log("add cond '" + name + "' duration " + conditionModel[1]);
                    conditionType1.Add(conditionModel);
                }

                //conditionType1.RemoveAt(0);
                break;
            case 2:
                int[] cond2 = conditionType2.Find(x => x[0] == condId);
                if (cond2 != null)
                {
                    if (cond2[1] < durationTurn)
                    {
                        // Debug.Log(name + "increase duration from " + cond2[1] + " to " + durationTurn);
                        cond2[1] = durationTurn;
                    }

                }
                else
                {
                    //  Debug.Log("add cond '" + name + "' duration " + conditionModel[1]);
                    conditionType2.Add(conditionModel);
                }
                break;
            case 3:
                int[] cond3 = conditionType3.Find(x => x[0] == condId);
                if (cond3 != null)
                {
                    if (cond3[1] < durationTurn)
                    {
                        //  Debug.Log(name + "increase duration from " + cond3[1] + " to " + durationTurn);
                        cond3[1] = durationTurn;
                    }

                }
                else
                {
                    //Debug.Log("add cond '" + name + "' duration " + conditionModel[1]);
                    conditionType3.Add(conditionModel);
                }
                break;
            case 4:
                int[] cond4 = conditionType4.Find(x => x[0] == condId);
                if (cond4 != null)
                {
                    if (cond4[1] < durationTurn)
                    {
                        // Debug.Log(name + "increase duration from " + cond4[1] + " to " + durationTurn);
                        cond4[1] = durationTurn;
                    }

                }
                else
                {
                    // Debug.Log("add cond '" + name + "' duration " + conditionModel[1]);
                    conditionType4.Add(conditionModel);
                }
                break;
        }
        //Debug.Log (conditionType2[0][0]+ " " + conditionType2[0][1]);
    }

    public void RemoveCondition(int conditionId, JsonData jsonConditionData)
    {
        if (conditionId == 0)
        {
            return;
        }
        int type = Convert.ToInt32((string)jsonConditionData[conditionId]["type"]);
        switch (type)
        {
            case 1:
                conditionType1.Remove(conditionType1.Find(x => x[0] == conditionId));
                break;
            case 2:
                conditionType2.Remove(conditionType2.Find(x => x[0] == conditionId));
                break;
            case 3:
                conditionType3.Remove(conditionType3.Find(x => x[0] == conditionId));
                break;
            case 4:
                conditionType4.Remove(conditionType4.Find(x => x[0] == conditionId));
                break;
        }
    }
}



