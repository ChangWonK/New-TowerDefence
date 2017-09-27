using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainPage : UIPopupBase
{
    private Text _title;
    private Text _subTitle;

    void Awake()
    {
        //_title = GetText("Text_Title"); ;
        //_subTitle = GetText("Text_SubTitle"); ;

        //_title.text = "Real";
        //_subTitle.text = "SubReal";

        //var temp = transform.Find("Dummy2");

        ////var temppp = GetText(temp, "Text_four");
        //var temppp = GetText("Dummy2", "Text_four");

        //Debug.Log(temppp);

    }

    void Start()
    {
        Transform buttons = transform.Find("Buttons");

        GetButton(buttons, "Btn_LoadScene").onClick.AddListener(() => GameManager.i.ChangeScene
            (SCENE_NAME.STAGE_SCENE, GameManager.i.MainSceneExitWaiting, GameManager.i.StageScenePrepareWaiting));

        GetButton(buttons, "Btn_HaveTower").onClick.AddListener(() => UIManager.i.CreatePopup<HaveTower>(POPUP_TYPE.STACK));
        GetButton(buttons, "Btn_TowerShop").onClick.AddListener(() => UIManager.i.CreatePopup<TowerShopSecond>(POPUP_TYPE.STACK));
    }


}
