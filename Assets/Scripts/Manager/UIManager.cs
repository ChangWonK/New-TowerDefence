using UnityEngine;
using System.Text;
using System.Collections.Generic;

public enum POPUP_TYPE { PAGE = 0, STACK, POPUP, FRONT }

public class UIManager : MonoBehaviour
{

    private static UIManager _instance = null;
    public static UIManager i
    {
        get
        {
            if (_instance == null)
            {

                var intance =FindObjectOfType<UIManager>();

                if (intance != null)
                {
                    _instance = intance;

                    return _instance;
                }

                _instance = Instantiate<UIManager>(Resources.Load<UIManager>("Prefabs/UI/UIManager"));
            }

            return _instance;
        }
    }

    private Transform _worldTrans;
    private Transform _pageTrans;
    private Transform _stackTrans;
    private Transform _popupTrans;
    private Transform _frontTrans;


    // 내가 string을 쓰는데 왜? const를 붙일까? 
    private const string WorldUI = "WorldUI";
    private const string PageUI = "PAGE";
    private const string StackUI = "STACK";
    private const string PopupUI = "POPUP";
    private const string FrontUI = "FRONT";
    private const string UIPath = "Prefabs/UI/";
    private const string ButtonPath = "Prefabs/UI/Button/";
    private const string SLASH = "/";

    private StringBuilder _textbulider = new StringBuilder(256);
    private List<UIPopupBase> uiObject_List = new List<UIPopupBase>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        FindSpecificTransform();
        
    }

    private void FindSpecificTransform()
    {
        _worldTrans = transform.Find(WorldUI);
        _pageTrans = transform.Find(PageUI);
        _stackTrans = transform.Find(StackUI);
        _popupTrans = transform.Find(PopupUI);
        _frontTrans = transform.Find(FrontUI);
    }

    // 어떤 팝업이던지 이제 이렇게 만들겠다라는 함수  
    public T CreatePopup<T>(POPUP_TYPE poptype) where T : UIPopupBase
    {
        _textbulider.Length = 0;
        _textbulider.Append(UIPath);
        _textbulider.Append(poptype.ToString());
        _textbulider.Append(SLASH);
        _textbulider.Append(typeof(T).ToString());

        GameObject pop = Resources.Load(_textbulider.ToString()) as GameObject;

        GameObject popup = Instantiate(pop);

        switch (poptype)
        {
            case POPUP_TYPE.PAGE:
                popup.transform.SetParent(_pageTrans, false);                
                break;
            case POPUP_TYPE.STACK:
                popup.transform.SetParent(_stackTrans, false);
                break;
            case POPUP_TYPE.POPUP:
                popup.transform.SetParent(_popupTrans, false);
                break;
            case POPUP_TYPE.FRONT:
                popup.transform.SetParent(_frontTrans, false);
                break;
        }

        var returnValue = popup.GetComponent<T>();

        returnValue.TYPE = poptype;

        uiObject_List.Add(returnValue);

        return returnValue;
    }

    public T FindUIObject<T>() where T : UIPopupBase
    {
        T obj = uiObject_List.Find((c) => c is T) as T;

        if (obj == null)
        {
            return null;
        }

        return obj.GetComponent<T>();

    }

    public void RemoveUIObject<T>() where T : UIPopupBase
    {
        T obj = uiObject_List.Find((c) => c is T) as T;

        if (obj != null)
        {
            uiObject_List.Remove(obj);
            Destroy(obj.gameObject);
        }
    
    }

    public void RemoveAllObject()
    {
        uiObject_List.FindAll((c) =>
        {
            if (c is FadeInOut)
            {
                return false;
            }
            else
            {
                if (c == null)
                    return true;

                Destroy(c.gameObject);
                uiObject_List.Remove(c);
                return true;
            }
        });
    }

}
