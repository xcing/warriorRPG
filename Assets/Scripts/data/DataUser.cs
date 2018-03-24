using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class DataUser
{
    public string userId;
    public int characterId;
    public int level;
    public int exp;
    public int vip;
    public int expVip;
    public int energy;
    public int money;
    public int ore;
    public int fame;
    public int heart;
    public int diamond;
    public int bag_warrior;
    public int bag_equipment;
    public int bag_gem;
    public List<Warrior> warrior;
    public List<WarriorSoul> warriorSoul;
    public List<Equipment> equipment;
    public List<Gem> gem;
    public List<List<List<int>>> stage;
    public List<QuestData> quest;
    public List<WarriorQuest> warriorquest;
    public List<Mail> mail;
    public List<Friend> friend;

    public DataUser()
    {

    }
}


