using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum SCENE_NAME { INTRO_SCENE = 0, LOGO_SCENE, MAIN_SCENE, STAGE_SCENE, GAME_SCENE, NONE = 100 }

public class SceneManager : Singleton<SceneManager>
{

    private UnityAction exit;
    private UnityAction prepare;

    public SCENE_NAME CurrentScene;


    void Awake()
    {
        CurrentScene = SCENE_NAME.NONE;
    }


    public void ChangeScene(SCENE_NAME nextScene, UnityAction exitFunc, UnityAction preFunc)
    {
        if (nextScene == CurrentScene)
            return;

        exit = exitFunc;
        prepare = preFunc;

        StartCoroutine(WaitingScene(nextScene));

    }

    private bool _waiting = false;

    public void Done()
    {
        _waiting = false;
    }

    private IEnumerator WaitingScene(SCENE_NAME nextScene)
    {
        _waiting = true;

        exit.Invoke();

        while (_waiting)
            yield return null;


        yield return StartCoroutine(LoadScene(nextScene));

        Debug.Log("LoadScene");

        StartCoroutine(PrepareScene());
    }

    private IEnumerator LoadScene(SCENE_NAME sceneEnum)
    {

        var progress = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)sceneEnum);

        while (progress.isDone == false)
        {
            Debug.Log(progress.isDone);

            yield return null;
        }
    }

    private IEnumerator PrepareScene()
    {
        _waiting = true;

        prepare.Invoke();

        while (_waiting)
            yield return null;
    }

}
