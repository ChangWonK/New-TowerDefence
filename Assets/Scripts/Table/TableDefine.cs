using UnityEngine;
using System.Collections;
using System;
using System.Xml;


// 엑셀 시트이름과 같이 할것 
public class TowerData : Table<TowerData> , ITableRow
{
    public const string PrefabPath = "Prefabs/UI/ScrollItem/TowerContent";

    public int LocalIndex;
    public string Name;
    public string Species;
    public string Rank;
    public int Level;
    public float AtkPower;
    public float AtkSpeed;
    public float AtkRange;
    public float MoveSpeed;    
    public int Cost;

    public int FirstSkillIndex;
    public int SecondSkillIndex;
    public int ThirdSkillIndex;


    public int ID
    {
        get
        {
            return LocalIndex;
        }
    }

    public void Parse(XmlNode node)
    {   
        LocalIndex = node.GetInt("LocalIndex");
        Name = node.GetString("Name");
        Species = node.GetString("Species");
        Rank = node.GetString("Rank");
        AtkPower = node.GetFloat("AtkPower");
        AtkSpeed = node.GetFloat("AtkSpeed");
        AtkRange = node.GetFloat("AtkRange");
        MoveSpeed = node.GetFloat("MoveSpeed");
        Cost = node.GetInt("Cost");
        FirstSkillIndex = node.GetInt("FirstSkillIndex");
        SecondSkillIndex = node.GetInt("SecondSkillIndex");
        ThirdSkillIndex = node.GetInt("ThirdSkillIndex");
    }
}

public class ItemData : Table<ItemData>, ITableRow
{
    public const string PrefabPath = "Prefabs/UI/ScrollItem/ItemContent";

    public int LocalIndex;
    public string Name;
    public string Rank;
    public string Kind;
    public float AtkPower;
    public float AtkSpeed;
    public float AtkRange;
    public float MoveSpeed;
    public int Cost;

    public int ID
    {
        get
        {
            return LocalIndex;
        }
    }

    public void Parse(XmlNode node)
    {
        LocalIndex = node.GetInt("LocalIndex");
        Name = node.GetString("Name");
        Rank = node.GetString("Rank");
        Kind = node.GetString("Kind");
        AtkPower = node.GetInt("AtkPower");
        AtkSpeed = node.GetFloat("AtkSpeed");
        AtkRange = node.GetFloat("AtkRange");
        MoveSpeed = node.GetFloat("MoveSpeed");
        Cost = node.GetInt("Cost");
    }
}

public class SkillData : Table<SkillData>, ITableRow
{
    public const string PrefabPath = "Prefabs/UI/ScrollItem/TowerContent";

    public int LocalIndex;
    public string Name;

    public int ID
    {
        get
        {
            return LocalIndex;
        }
    }

    public void Parse(XmlNode node)
    {
        LocalIndex = node.GetInt("LocalIndex");
        Name = node.GetString("Name");
    }
}

