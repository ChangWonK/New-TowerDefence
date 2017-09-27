using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemContent : UIObject
{

    public int Index;

    private Text _text;


    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ButtonAction);
        _text = GetText("Text");
    }


    public void SetUIData(int index)
    {
        Index = index;

        var table = TableManager.i.GetTable<ItemData>(index);

        _text.text = table.Name;
    }


    public void ButtonAction()
    {
        var popup = UIManager.i.FindUIObject<ItemShop>();
    }
}
