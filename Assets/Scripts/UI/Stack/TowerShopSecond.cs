using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShopSecond : UIPopupBase
{

    private List<int> _contentList = new List<int>();
    private ScrollRect _scroll;
    private TowerInformation _information;

	void Awake ()
    {

        _scroll = GetComponentInChildren<ScrollRect>();

        _information = GetComponentInChildren<TowerInformation>();

        Transform buttons = transform.Find("SelectButtons");
        GetButton(buttons, "Btn_Human").onClick.AddListener(() => CreateScrollContent(0) );
        GetButton(buttons, "Btn_Machine").onClick.AddListener(() => CreateScrollContent(1));

        Transform UpgradeButtons = transform.Find("UpgradeButtons");
        GetButton(UpgradeButtons, "Btn_AtkPower", () => Test(gettedIndex));
        GetButton(UpgradeButtons, "Btn_AtkSpeed", () => Debug.Log("ㅇ"));
        GetButton(UpgradeButtons, "Btn_AtkRange", () => Debug.Log("ㅇ"));


    }

    void Test(int index)
    {
        Debug.Log("됨?");
        var test = TableManager.i.GetTable<TowerData>(index);

        test.AtkPower += 10;

        _information.SetUIData(index);
    }


    // 분류별 컨텐트 스크롤 리스트 생성
    private void CreateScrollContent( int partIndex)
    {
       
            _contentList.Clear();

        int towerCount = TableManager.i.GetTableCount<TowerData>();
        GameObject content = Resources.Load<GameObject>(TowerData.PrefabPath);

        switch(partIndex)
        {
            case 0:

                for (int i =0; i < towerCount; i++)
                {
                    var table = TableManager.i.GetTable<TowerData>(i);

                    var scollerIn = Instantiate(content, _scroll.viewport.GetChild(0));

                    var conScript = scollerIn.GetComponent<TowerContent>();

                    conScript.SetUIData(i);
                    _contentList.Add(i);
                }
                break;

            case 1:
                for (int i = 5; i < 10; i++)
                {
                    var table = TableManager.i.GetTable<TowerData>(i);

                    _contentList.Add(i);
                }
                break;

        }
    }

    private int gettedIndex;
    public void UpdateInformationData(int index)
    {
        gettedIndex = index;
        _information.SetUIData(index);
    }




	
	
	
}
