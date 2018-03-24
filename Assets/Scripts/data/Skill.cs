using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LitJson;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Skill
{
    public int skillId;
    public string[] name = new string[3];
    public string[] detail = new string[3];
    public int warriorId;
    public int enemyId;
    public int dmgBegin;
    public int dmgUpPerLv;
    public int selfCondId;
    public int buffValueBegin;
    public int buffUpPerLv;
    public int decreseDmgReceiveBegin;
    public int decreseDmgReceiveUpPerLv;
    public int percentUseBegin;
    public int percentUseUpPerLv;
    public int ordinal;
    public bool haveData = false;

    public int useWhen;
    public int hitAmount;
    public string[] hitDmg;
    public int dmg;
    public int target;
    public int conditionId;
    public int condAcc;
    public int buffType;
    public int buffValue;
    public int buffTarget;
    public int decreseDmgReceive;
    public int percentUse;

    public Skill()
    {

    }

    public void SetDataFromDefault(int warriorIdParam, int ordinal, JsonData jsonSkill)
    {
        warriorId = warriorIdParam;
        string warriorIdStr = warriorId.ToString();
        ordinal++;
        string ordinalStr = ordinal.ToString();
        skillId = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["skillId"]);
        name[0] = (string)jsonSkill[warriorIdStr][ordinalStr]["name"][0];
        name[1] = (string)jsonSkill[warriorIdStr][ordinalStr]["name"][1];
        name[2] = (string)jsonSkill[warriorIdStr][ordinalStr]["name"][2];

        detail[0] = (string)jsonSkill[warriorIdStr][ordinalStr]["detail"][0];
        detail[1] = (string)jsonSkill[warriorIdStr][ordinalStr]["detail"][1];
        detail[2] = (string)jsonSkill[warriorIdStr][ordinalStr]["detail"][2];

        
        useWhen = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["useWhen"]);
        hitAmount = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["hitAmount"]);
        string hitDmgTemp = (string)jsonSkill[warriorIdStr][ordinalStr]["hitDmg"];
        hitDmg = hitDmgTemp.Split(","[0]);
        dmgBegin = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["dmg"]);
        dmgUpPerLv = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["dmgUpPerLv"]);
        target = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["target"]);
        conditionId = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["conditionId"]);
        condAcc = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["condAcc"]);
        selfCondId = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["selfCondId"]);
        buffType = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["buffType"]);
        buffValueBegin = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["buffValue"]);
        buffUpPerLv = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["buffUpPerLv"]);
        buffTarget = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["buffTarget"]);
        decreseDmgReceiveBegin = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["decreseDmgReceive"]);
        decreseDmgReceiveUpPerLv = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["decreseDmgReceiveUpPerLv"]);
        percentUseBegin = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["percentUse"]);
        percentUseUpPerLv = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["percentUseUpPerLv"]);

        this.ordinal = ordinal;
        haveData = false;
    }

    public void setDataForBattle(int warriorIdParam, int ordinal, JsonData jsonSkill)
    {
        warriorId = warriorIdParam;
        string warriorIdStr = warriorId.ToString();
        ordinal++;
        string ordinalStr = ordinal.ToString();
        this.skillId = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["skillId"]);
        name[0] = (string)jsonSkill[warriorIdStr][ordinalStr]["name"][0];
        name[1] = (string)jsonSkill[warriorIdStr][ordinalStr]["name"][1];
        name[2] = (string)jsonSkill[warriorIdStr][ordinalStr]["name"][2];

        
        dmg = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["dmg"]);
        target = Convert.ToInt32((string)jsonSkill[warriorIdStr][ordinalStr]["target"]);
     
        this.ordinal = ordinal;
        haveData = false;
    }

    public void calculateStatSKill(int skillLv, int skillLvPlus)
    {
        if (dmgBegin > 0)
            dmg = dmgBegin + (dmgUpPerLv * (skillLv - 1));
        else
            dmg = 0;
        if (buffValueBegin > 0)
            buffValue = buffValueBegin + (buffUpPerLv * (skillLv - 1));
        else
            buffValue = 0;
        if (decreseDmgReceiveBegin > 0)
            decreseDmgReceive = decreseDmgReceiveBegin + (decreseDmgReceiveUpPerLv * (skillLv - 1));
        else
            decreseDmgReceive = 0;
        percentUse = percentUseBegin + (percentUseUpPerLv * (skillLvPlus - 1));
        haveData = true;
    }
}
