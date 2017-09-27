using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInformation : UIObject
{
    private Image _image;
    private Text _name;
    private Text _species;
    private Text _rank;
    private Text _level;
    private Text _atkPower;
    private Text _atkSpeed;
    private Text _atkRange;
    private Text _moveSpeed;


    void Awake()
    {
        _image = GetImage("Img_Tower");
        _name = GetText("Txt_Name");
        _species = GetText("Txt_Species");
        _rank = GetText("Txt_Rank");
        _level = GetText("Txt_Level");

        _atkPower = GetText("Txt_AtkPower");
        _atkSpeed = GetText("Txt_AtkSpeed");
        _atkRange = GetText("Txt_AtkRange");
        _moveSpeed = GetText("Txt_MoveSpeed");
    }


    public void SetUIData(int index)
    {
        var table = TableManager.i.GetTable<TowerData>(index);

        _name.text = "이름 : " + table.Name;
        _species.text = "종족 : " + table.Species;
        _rank.text = "계급 : " + table.Rank;
        _level.text = "레벨 : " + table.Level.ToString();
        _atkPower.text = "공격력 : " + table.AtkPower.ToString();
        _atkSpeed.text = "공격속도 : " + table.AtkSpeed.ToString();
        _atkRange.text = "공격사거리 : " + table.AtkRange.ToString();
        _moveSpeed.text = "이동속도 : " + table.MoveSpeed.ToString();
    }
}
