using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FileData : MonoBehaviour
{

    /*public void SaveTeamMember(TeamMember teamMemberData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/teamMember.dat");
        bf.Serialize(file, teamMemberData);
        file.Close();
    }

    public TeamMember LoadTeamMember()
    {
        if (File.Exists(Application.persistentDataPath + "/teamMember.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/teamMember.dat", FileMode.Open);
            TeamMember data = (TeamMember)bf.Deserialize(file);
            file.Close();
            return data;
        }
        return null;
    }*/

    public void SaveMoneyStore(MoneyStore moneyStoreData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/moneyStore.dat");
        bf.Serialize(file, moneyStoreData);
        file.Close();
    }

    public MoneyStore LoadMoneyStore()
    {
        if (File.Exists(Application.persistentDataPath + "/moneyStore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/moneyStore.dat", FileMode.Open);
            MoneyStore data = (MoneyStore)bf.Deserialize(file);
            file.Close();
            return data;
        }
        return null;
    }

    public void SaveDataUser(DataUser userDataModel)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/userData.dat");
        bf.Serialize(file, userDataModel);
        file.Close();
    }

    public DataUser LoadDataUser()
    {
        if (File.Exists(Application.persistentDataPath + "/userData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/userData.dat", FileMode.Open);
            DataUser data = (DataUser)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            Application.LoadLevel("home");
        }
        return null;
    }
}

