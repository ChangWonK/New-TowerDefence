using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InfinityScrollView : MonoBehaviour
{
    public int _ScrollObjScale;                          //초기화는 HaveTower 클래스에서
    public int _Gap;                                    //초기화는 HaveTower 클래스에서
    public int _InstanceScrollCount;          //초기화는 HaveTower 클래스에서

    public RectTransform _Content;
    public ScrollRect _ScrollRect;

    private int _speciesNum;                      //초기화는 HaveTower 클래스에서
    private int _currentNum;
    private int _scrollDataCount;                 //초기화는 HaveTower 클래스에서
    private List<TowerContent> _scrollList;

    void Awake()
    {
        _currentNum = 0;
        _scrollList = new List<TowerContent>();
        _ScrollRect = GetComponent<ScrollRect>();
        _Content = _ScrollRect.content;

    }

    public void SelectScrollView(int speciesIndex, int scrollDataCount)
    {
        _ScrollRect.inertia = false;
        _speciesNum = speciesIndex;
        _scrollDataCount = scrollDataCount;

        InfinityScrollViewStart();
    }

    public void InfinityScrollViewStart()
    {
        _currentNum = 0;
        _scrollList.Clear();
        _Content.transform.localPosition = new Vector2(100, 0);
        _ScrollRect.onValueChanged.RemoveAllListeners();

        for (int i = 0; i < _InstanceScrollCount; i++)
        {
            var scrollObj = _Content.GetComponentsInChildren<TowerContent>();

            var conScript = scrollObj[i].GetComponent<TowerContent>();

            conScript.SetUIData(i + _speciesNum);

            _scrollList.Add(scrollObj[i]);
            scrollObj[i].transform.localPosition = new Vector2(0, -(_currentNum * _Gap) - _ScrollObjScale);
            _currentNum++;
        }

        _ScrollRect.onValueChanged.AddListener(ListChange);
        
        _Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (_scrollDataCount * _Gap) + _ScrollObjScale);

        


    }

    float PrePos = 0;
    private void ListChange(Vector2 vec)
    {
        if (-_Content.anchoredPosition.y - PrePos < -(_Gap))
        {
            if (_currentNum >= _scrollDataCount)
                return;

            PrePos -= _Gap;

            var scrollObj = _scrollList[0];
            _scrollList.RemoveAt(0);
            scrollObj.transform.localPosition = new Vector2(0, -(_currentNum * _Gap) - _ScrollObjScale);
            _scrollList.Add(scrollObj);
            _scrollList[_InstanceScrollCount - 1].SetUIData(_speciesNum + _currentNum);
            _currentNum++;
        }

        else if(-_Content.anchoredPosition.y - PrePos>0)
        {
            if (_currentNum <= _InstanceScrollCount)
                return;

            PrePos += _Gap;
            var scrollObj = _scrollList[_scrollList.Count - 1];
            _scrollList.RemoveAt(_scrollList.Count - 1);
            scrollObj.transform.localPosition = new Vector2(0, -(((_currentNum - (_InstanceScrollCount + 1)) * _Gap) + _ScrollObjScale));
            _scrollList.Insert(0, scrollObj);
            _scrollList[0].SetUIData(_speciesNum + _currentNum - (_InstanceScrollCount+1));
            _currentNum--;
        }
    }
}
