//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.SceneManagement;

//public class SceneControl : MonoBehaviour
//{
//    private static SceneControl _instance = null;
//    public static SceneControl I
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                Debug.LogError("SceneControl is Null");
//            }

//            return _instance;
//        }
//    }

//    void Awake()
//    {
//        DontDestroyOnLoad(gameObject);
//        _instance = this;
//    }


//    void OnEnable()
//    {
//        UnityEngine.SceneManagement.SceneManager.sceneLoaded += LoadedScene;
//        UnityEngine.SceneManagement.SceneManager.sceneUnloaded += UnLoadedScene;
//    }
//    void OnDisable()
//    {
//        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= LoadedScene;
//        UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= UnLoadedScene;
//    }

//    public UnityAction<string> LoadScene = (sceneName) =>
//    {

//        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
//    };

//    public UnityAction<Scene, LoadSceneMode> LoadedScene = (c, b) =>
//    {
//        Debug.Log("씬이 로드가 완료되었다  " + c.name);
//        GameManager.i.LoadedScene(c.name);
//    };

//    public UnityAction<Scene> UnLoadedScene = (c) =>
//    {
//        Debug.Log("씬이 언로드가 완료되었다  " + c.name);
//        GameManager.i.UnLoadedScene();
//    };

//    // 씬이 바뀐후에 , 내가 뭔가 추가로 준비해야할 부분이 있다. 

//    // 씬이 현재 돌아가고 있는 상태를 사람이 보기 쉽게끔 확인해야할 부분이 있다 

//    //  씬이 종료시키기전에 내가 뭔가 정리를 해야할 부분이 있다. 





//}

