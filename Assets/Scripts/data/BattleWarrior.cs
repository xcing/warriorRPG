using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LitJson;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BattleWarrior : ICloneable {

    public int characterId;
    public string warriorUserId;
    public int level = 1;
    public float exp = 0.0f;
    public int lvPlus = 0;
    public List<string> dataEquipmentUserId;
    public List<int> skillLv;

    public List<Skill> skillList;
    public int typeOfChar = 0;
    public int position = 0;
    public int hp = 0;
    public int atk = 0;
    public int def = 0;
    public int spd = 0;
    public int luck = 0;


    object ICloneable.Clone()
    {
        return this.Clone();
    }
    public BattleWarrior Clone()
    {
        return (BattleWarrior)this.MemberwiseClone();
    }

    public BattleWarrior()
    {

    }

    public BattleWarrior(BattleWarrior item)
    {
    /*    this.atk = item.atk;
        this.atk = item.characterId;
        this.atk = item.dataEquipmentUserId;
        this.atk = item.def;
        this.atk = item.exp;
        this.atk = item.hp;
        this.atk = item.level;
        this.atk = item.luck;
        this.atk = item.lvPlus;
        this.atk = item.;*/
        
    }

    public void setDataWarrior(Warrior warriorData, int langId)
    {

        this.characterId = warriorData.warriorId;
        this.warriorUserId = warriorData.warriorUserId;
        this.hp = warriorData.hp;
        this.atk = warriorData.atk;
        this.def = warriorData.def;
        this.spd = warriorData.spd;
        this.luck = warriorData.luck;
        this.typeOfChar = 1;
        this.position = warriorData.position;

        skillList = new List<Skill>();

        for (int i = 0; i < warriorData.skill.Length; i++)
        {
            skillList.Add(warriorData.skill[i]);
        }

        
    }

    public void setDataEnemy(string id, JsonData jsonEnemyData, JsonData jsonSkill , int langId)
    {

        characterId = Convert.ToInt32((string)jsonEnemyData[id]["enemyId"]);
        warriorUserId = "0";
        hp = Convert.ToInt32((string)jsonEnemyData[id]["hp"]);
        atk = Convert.ToInt32((string)jsonEnemyData[id]["atk"]);
        def = Convert.ToInt32((string)jsonEnemyData[id]["def"]);
        spd = Convert.ToInt32((string)jsonEnemyData[id]["spd"]);
        typeOfChar = 2;


        skillList = new List<Skill>();

        for (int i = 0; i < Convert.ToInt32((string)jsonEnemyData[id]["amountSkill"]); i++)
        {
            Skill skill = new Skill();
            skill.skillId = Convert.ToInt32((string)jsonSkill[id][i]["skillId"]);
            skill.enemyId = Convert.ToInt32((string)jsonSkill[id][i]["enemyId"]);
            skill.useWhen = Convert.ToInt32((string)jsonSkill[id][i]["useWhen"]);
            skill.hitAmount = Convert.ToInt32((string)jsonSkill[id][i]["hitAmount"]);
            skill.hitDmg = ((string)jsonSkill[id][i]["hitDmg"]).Split(","[0]);
            skill.dmg = Convert.ToInt32((string)jsonSkill[id][i]["dmg"]);
            skill.target = Convert.ToInt32((string)jsonSkill[id][i]["target"]);
            skill.conditionId = Convert.ToInt32((string)jsonSkill[id][i]["conditionId"]);
            skill.condAcc = Convert.ToInt32((string)jsonSkill[id][i]["condAcc"]);
            skill.selfCondId = Convert.ToInt32((string)jsonSkill[id][i]["selfCondId"]);
            skill.buffType = Convert.ToInt32((string)jsonSkill[id][i]["buffType"]);
            skill.buffValue = Convert.ToInt32((string)jsonSkill[id][i]["buffValue"]);
            skill.buffTarget = Convert.ToInt32((string)jsonSkill[id][i]["buffTarget"]);
            skill.decreseDmgReceive = Convert.ToInt32((string)jsonSkill[id][i]["decreseDmgReceive"]);
            skill.percentUse = Convert.ToInt32((string)jsonSkill[id][i]["percentUse"]);
            skill.ordinal = Convert.ToInt32((string)jsonSkill[id][i]["ordinal"]);
            skill.name[0] = (string)jsonSkill[id][i]["name"][0];
            skill.name[1] = (string)jsonSkill[id][i]["name"][1];
            skill.name[2] = (string)jsonSkill[id][i]["name"][2];

            skillList.Add(skill);
        }
             
  
    }

    public int ActionSkill(BattleWarrior target, List<BattleLog> battleLog)
    {
        //normal atk
        int dmg = atk;
        dmg = atk / 1;
        target.hp -= dmg;
        BattleLog log = new BattleLog();

        log.attackerId = this.characterId;
        log.defenderId = target.characterId;
        log.attackerUserId = this.warriorUserId;
        log.defenderUserId = target.warriorUserId;
        log.typeOfCharAtk = this.typeOfChar;
        log.dmg = dmg;
        log.skillId = 0;
        log.positionAtk = this.position;
        log.positionDef = target.position;
        battleLog.Add(log);
        
        //skill
        foreach (Skill skill in skillList)
        {
            if (skill.useWhen != 4)
            {
                if (UnityEngine.Random.Range(1, 1) <= skill.percentUse)
                {
                    //dmg = skill.dmg * atk;
                    dmg = atk/1;
                    target.hp -= dmg;
                    log = new BattleLog();
                    log.attackerId = this.characterId;
                    log.defenderId = target.characterId;
                    log.attackerUserId = this.warriorUserId;
                    log.defenderUserId = target.warriorUserId;
                    log.typeOfCharAtk = this.typeOfChar;
                    log.dmg = dmg;
                    log.hitAmount = skill.hitAmount;
                    log.skillId = skill.skillId;
                    log.ordinal = skill.ordinal;
                    log.positionAtk = this.position;
                    log.positionDef = target.position;
                    battleLog.Add(log);
                }
                else
                {
                    break;
                }
            }
        }

       if (target.hp <= 0 && this.hp <= 0)
           return 3;
       else if (target.hp > 0 && this.hp > 0)
           return 4;
       else if (target.hp <= 0)
           return 1;
       else
           return 2;
            

    }

    
}
