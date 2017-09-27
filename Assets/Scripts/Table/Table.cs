using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public interface ITable
{
    void Initialize(string name);
}

public interface ITableRow
{
    void Parse(XmlNode node);

    int ID { get; }
}

public interface ITableMultiRow : ITableRow
{
    void ParseAdditional(XmlNode node);
}

public class DataTable
{
    public string TableName;

    public virtual void Initialize(string name)
    {
        Debug.Log("DataTAble Initialize");
    }

}

public class Table<T> : DataTable, ITable where T : class, ITableRow, new()
{
    public Dictionary<int, T> rowDic
    {
        get { return _rowDic; }
    }

    protected Dictionary<int, T> _rowDic = new Dictionary<int, T>();

    public const string filePath = "table/";

    public bool isInitialize { get; private set; }

    public override void Initialize(string name)
    {
        if (isInitialize == true)
            return;

        string xmlPath = filePath + name;

        TextAsset textAsset = (TextAsset)Resources.Load(xmlPath);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList xmlNodeList = xmlDoc.GetElementsByTagName(TableHelper.xmlNodeName);

        foreach (XmlNode xmlNode in xmlNodeList)
            this.AddRow(xmlNode);

        isInitialize = true;

        this.OnCompleteInitialize();
    }

    protected virtual void AddRow(XmlNode xmlNode)
    {
        T row = new T();
        row.Parse(xmlNode);
        _rowDic.Add(row.ID, row);
    }

    protected virtual void OnCompleteInitialize()
    {

    }

    public void Clear()
    {
        isInitialize = false;
        _rowDic.Clear();
    }

    public T GetRow(int ID)
    {
        T result;
        if (_rowDic.TryGetValue(ID, out result) == false)
        {
            Debug.LogError(string.Format("Not Exist. ID: {0}", ID));
            return default(T);
        }

        return result;
    }

    public int rowCount
    { get { return _rowDic.Count; } }
}

public class MultiRowTable<T> : Table<T> where T : class, ITableMultiRow, new()
{
    private T _lastRow;

    protected override void AddRow(XmlNode xmlNode)
    {
        string id = xmlNode.GetString("ID");
        if (id.CompareTo("-") != 0)
        {
            T row = new T();
            row.Parse(xmlNode);
            _rowDic.Add(row.ID, row);
            _lastRow = row;
            return;
        }

        _lastRow.ParseAdditional(xmlNode);
    }

}
