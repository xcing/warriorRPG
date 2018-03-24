using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using LitJson;

[Serializable]
public class Mail
{
    public string mailId;
    public string[] subject;
    public string[] detail;
    public string itemType;
    public string itemId;
    public int itemAmount;
}
