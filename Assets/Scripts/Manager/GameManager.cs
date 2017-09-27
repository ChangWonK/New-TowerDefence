using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

    void Awake()
    {

        DontDestroyOnLoad(gameObject);
        var pop = UIManager.i.CreatePopup<FadeInOut>(POPUP_TYPE.FRONT);
    
    }

    void Start()
    {
        TableManager.i.DataTableLoad();

        //ChangeScene(SCENE_NAME.LOGO_SCENE, IntroSceneExitWaiting, LogoScenePrepareWaiting);
    }

    //******************** Main_UI **********************//


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

            TowerData tabledata = TableManager.i.GetTable<TowerData>(2);

            Debug.Log(tabledata.Name);
        }
    }




    //******************** Scene **********************//

    public void ChangeScene(SCENE_NAME sceneName, UnityAction exit, UnityAction preFunc)
    {
        var pop = UIManager.i.FindUIObject<FadeInOut>();

        if (pop == null)
            pop = UIManager.i.CreatePopup<FadeInOut>(POPUP_TYPE.FRONT);

        pop.FadeOut(() => { SceneManager.i.ChangeScene(sceneName, exit, preFunc); });
    }

    public void IntroSceneExitWaiting()
    {
        SceneManager.i.Done();
    }

    public void LogoScenePrepareWaiting()
    {
        UIManager.i.CreatePopup<LogoPopup>(POPUP_TYPE.POPUP);
        var pop = UIManager.i.FindUIObject<FadeInOut>();

        SceneManager.i.Done();

        pop.FadeIn(() => UIManager.i.RemoveUIObject<FadeInOut>());
        StartCoroutine(DelayTimeFunc(1.8f, () => { ChangeScene(SCENE_NAME.GAME_SCENE, LogoSceneExitWaiting, MainScenePrepareWaiting); }));
    }

    public void LogoSceneExitWaiting()
    {
        UIManager.i.RemoveAllObject();

        SceneManager.i.Done();
    }

    public void MainScenePrepareWaiting()
    {
        UIManager.i.CreatePopup<MainPage>(POPUP_TYPE.PAGE);
        var pop = UIManager.i.FindUIObject<FadeInOut>();

        SceneManager.i.Done();

        pop.FadeIn(() => UIManager.i.RemoveUIObject<FadeInOut>());
    }

    public void MainSceneExitWaiting()
    {
        UIManager.i.RemoveAllObject();

        SceneManager.i.Done();
    }

    public void StageScenePrepareWaiting()
    {
        var pop = UIManager.i.FindUIObject<FadeInOut>();

        SceneManager.i.Done();

        pop.FadeIn(() => UIManager.i.RemoveUIObject<FadeInOut>());
    }

    private IEnumerator DelayTimeFunc(float time, UnityAction func)
    {
        yield return new WaitForSeconds(time);
        func.Invoke();
    }

    /////////////////////////////////// Scene ///////////////////////////////////

}
