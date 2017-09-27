using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HaveTower : UIPopupBase
{
    private InfinityScrollView _scrollView;
    private TowerInformation _information;
    private int TowerCount;

    public Button _shopBtn;

    void Awake()
    {

           _scrollView = GetComponentInChildren<InfinityScrollView>();
        _information = GetComponentInChildren<TowerInformation>();

        _scrollView._InstanceScrollCount = 6;       //몇개를 불러놓고 돌릴건지
        _scrollView._Gap = 200;                              // 아이템간 거리 차이
        _scrollView._ScrollObjScale = 100;            // 아이템 크기
        TowerCount = 10;
    }

    void Start()
    {
        Transform buttons = transform.Find("SelectButtons");
        GetButton(buttons, "Btn_Human").onClick.AddListener(() => _scrollView.SelectScrollView(0, TowerCount));
        GetButton(buttons, "Btn_Machine").onClick.AddListener(() => _scrollView.SelectScrollView(10, TowerCount));
        _shopBtn = GetButton(buttons, "Btn_TowerShop");
        _shopBtn.gameObject.SetActive(false);
        _shopBtn.onClick.AddListener(CreateShop);

        var asd = TableManager.i.GetTable<ItemData>(4);

        Debug.Log(asd.Name);
        Debug.Log(asd.Kind);


        CreateContent();
    }

    private void CreateContent()
    {
        GameObject content = Resources.Load<GameObject>(TowerData.PrefabPath);

        for (int i = 0; i < _scrollView._InstanceScrollCount; i++)
        {
            var scollerIn = Instantiate(content, _scrollView._Content);
        }
    }

    private int _getIndex;
    private void CreateShop()
    {
       var shop = UIManager.i.CreatePopup<TowerShop>(POPUP_TYPE.STACK);
        shop._ScrollIndex = _getIndex;
    }

    /// <summary>
    /// ///////////////ㅈㅈㅈㅈㅈㅈ
    /// </summary>
    /// <param name="index"></param>
    /// 
    public void UpdateData(int index)
    {
        if(_shopBtn.gameObject.activeSelf)
        _shopBtn.gameObject.SetActive(true);

        _getIndex = index;
        _information.SetUIData(index);
    }


}
