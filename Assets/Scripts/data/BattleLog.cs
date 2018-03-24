using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BattleLog
{
    public int attackerId = 0;
    public string attackerUserId = "";
    public int defenderId = 0;
    public string defenderUserId = "";
    public int typeOfCharAtk = 0;
    public int skillId = 0;
    public int ordinal = 0;

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
    public int positionAtk;
    public int positionDef;
}
