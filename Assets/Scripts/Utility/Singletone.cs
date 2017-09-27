using UnityEngine;

// 모노 비헤이버 전용 싱글톤
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;    

    public static T i
    {
        get
        { 
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    DontDestroyOnLoad(_instance.gameObject);
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton) " + typeof(T).ToString();

                    DontDestroyOnLoad(singleton);
                }
            }

            return _instance;
            }
       }
 }    
