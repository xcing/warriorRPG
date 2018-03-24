using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MoneyStore
{
    public List<int> itemType;
    public List<int> itemId;
    public string lastRefreshDate;

    public MoneyStore()
    {
        
    }

    public void RandomItem(){
        itemType = new List<int>();
        itemId = new List<int>();
        int itemTypeRand;
        for (int i = 0; i < 12; i++)
        {
            itemTypeRand = UnityEngine.Random.Range(1, 100);
            if (itemTypeRand <= 5)
            {
                itemType.Add(2);
                RandomItemId(i, itemType[i]);
            }
            else if (itemTypeRand <= 25)
            {
                itemType.Add(1);
                RandomItemId(i, itemType[i]);
            }
            else if (itemTypeRand <= 60)
            {
                itemType.Add(3);
                RandomItemId(i, itemType[i]);
            }
            else if (itemTypeRand <= 100)
            {
                itemType.Add(4);
                RandomItemId(i, itemType[i]);
            }
        }
        lastRefreshDate = DateTime.Now.ToString("yyyy/MM/dd");
    }

    public void RandomItemId(int i, int type)
    {
        switch (type)
        {
            case 1:
                itemId.Add(UnityEngine.Random.Range(1, 3));
                break;
            case 2:
                itemId.Add(UnityEngine.Random.Range(1, 3));
                break;
            case 3:
                itemId.Add(UnityEngine.Random.Range(1, 3));
                break;
            case 4:
                itemId.Add(UnityEngine.Random.Range(1, 3));
                break;
        }
    }
}


