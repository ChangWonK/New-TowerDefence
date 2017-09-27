using System;
using System.Xml;
using UnityEngine;


public static class TableHelper
{
    public const string xmlNodeName = "xml_node";

    //에러나면 내용이 일치하지 않다는것
    public static string GetString(this XmlNode node, string name)
    {
        return node.SelectSingleNode(name).InnerText;
    }

    public static uint GetUInt(this XmlNode node, string name)
    {
        string val = node.GetString(name);
        if (string.IsNullOrEmpty(val)) return 0;
        return Convert.ToUInt32(val);
    }

    public static int GetInt(this XmlNode node, string name)
    {
        string val = node.GetString(name);
        if (string.IsNullOrEmpty(val)) return 0;
        return Convert.ToInt32(val);
    }

    public static ulong GetULong(this XmlNode node, string name)
    {
        string val = node.GetString(name);
        if (string.IsNullOrEmpty(val)) return 0;
        return Convert.ToUInt64(val);
    }

    public static bool GetBool(this XmlNode node, string name)
    {
        string val = node.GetString(name);
        if (string.IsNullOrEmpty(val))
            return false;
        else if ("0" == val)
            return false;
        else
            return true;
    }

    public static float GetFloat(this XmlNode node, string name)
    {
        string val = node.GetString(name);
        if (string.IsNullOrEmpty(val)) return 0.0f;
        return (float)Convert.ToDouble(val);
    }

    public static T GetEnum<T>(this XmlNode node, string name)
    {
        return (T)System.Enum.Parse(typeof(T), node.GetString(name).UppercaseFirst(), true);
    }

    private static string UppercaseFirst(this string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;

        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

}
