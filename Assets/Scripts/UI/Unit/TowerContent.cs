using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerContent : UIObject
{

    public int Index;
    
    private Text _text;


    void Awake ()
    {
        GetComponent<Button>().onClick.AddListener(ButtonAction);
        _text = GetText("Text");		
	}


    public void SetUIData(int index)
    {
        Index = index;

        var table = TableManager.i.GetTable<TowerData>(index);

        _text.text = table.Name;
    }
    

    public void ButtonAction()
    {
        var popup = UIManager.i.FindUIObject<HaveTower>();
        popup.UpdateData(Index);
    }

	
	
	
}
