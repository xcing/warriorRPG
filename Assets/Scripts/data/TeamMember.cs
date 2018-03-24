using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class TeamMember
{
    public int amountWarriorInTeam;
    public int warriorLeader;

    public string[] warriorId;
    public string[] warriorBackupId;
}


