using UnityEngine;
using System;
using System.Collections.Generic;

public class TableManager : SingleTone<TableManager>
{
    public Dictionary<Type, DataTable> TableList = new Dictionary<Type, DataTable>();
    private bool _init = false;

    // 테이블 추가는 여기다가 정의하도록 한다         
    // eX : typeof(Class Type) , Add<Class Type>()) -> 추가 형식 
    public void DataTableLoad()
    {
        if (_init)
            return;

        TableList.Add(typeof(TowerData), Add<TowerData>());
        TableList.Add(typeof(ItemData), Add<ItemData>());
        TableList.Add(typeof(SkillData), Add<SkillData>());

        _init = true;
    }


    private T Add<T>() where T : DataTable, new()
    {
        T addClass = new T();

        addClass.Initialize(typeof(T).Name);

        return addClass;
    }

    public T GetTable<T>(int ID) where T : Table<T>, ITableRow, new()
    {
        DataTable temp;


        if (TableList.Count < 1)
            DataTableLoad();


        TableList.TryGetValue(typeof(T), out temp);

        if (temp != null)
            return (temp as T).GetRow(ID);
        else
            return null;
    }

    public int GetTableCount<T>() where T : Table<T>, ITableRow, new()
    {
        DataTable temp;

        if (TableList.Count < 1)
            DataTableLoad();

        TableList.TryGetValue(typeof(T), out temp);

        if (temp != null)
            return (temp as T).rowCount;
        else
            return 0;
    }

}





