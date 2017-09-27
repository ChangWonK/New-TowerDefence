using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShop : UIPopupBase
{
    public int _ScrollIndex;

    private Button _upgardeBtn;
    private Button _buyBtn;
    private Button _sellBtn;


    void Awake()
    {
        Transform shopbtn = transform.Find("ShopButtons");

        _upgardeBtn = GetButton(shopbtn, "Btn_Upgrade");
        _buyBtn = GetButton(shopbtn, "Btn_Buy");
        _sellBtn = GetButton(shopbtn, "Btn_Sell");
    }

    void Start()
    {
        UpdateShopButton();

        _buyBtn.onClick.AddListener(BuyButton);
        _upgardeBtn.onClick.AddListener(UpgradeButton);
        _sellBtn.onClick.AddListener(SellButton);


        Transform skillbtn = transform.Find("SkillButtons");

        GetButton(skillbtn, "Btn_FirstSkill").onClick.AddListener(() => SkillButton(1));
        GetButton(skillbtn, "Btn_SecondSkill").onClick.AddListener(() => SkillButton(2));
        GetButton(skillbtn, "Btn_ThirdSkill").onClick.AddListener(() => SkillButton(3));

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.i.RemoveUIObject<TowerShop>();
        }
    }

    private void UpdateShopButton()
    {
        for (int i = 0; i < UserInformation._TowerList.Count; i++)
        {
            if (UserInformation._TowerList[i] == _ScrollIndex)
            {
                _buyBtn.interactable = false;
                return;
            }
        }

        _sellBtn.interactable = false;
        _upgardeBtn.interactable = false;
    }

    private void UpgradeButton()
    {
        var table = TableManager.i.GetTable<TowerData>(_ScrollIndex);
        table.Level += 1;

        UIManager.i.FindUIObject<HaveTower>().UpdateData(_ScrollIndex);
    }

    private void BuyButton()
    {
        UserInformation._TowerList.Add(_ScrollIndex);

        _buyBtn.interactable = false;
        _upgardeBtn.interactable = true;
        _sellBtn.interactable = true;
    }

    private void SellButton()
    {
        UserInformation._TowerList.Remove(_ScrollIndex);

        _buyBtn.interactable = true;
        _upgardeBtn.interactable = false;
        _sellBtn.interactable = false;
    }

    private void SkillButton(int numSkill)
    {
        int skillIndex = 0;
        switch (numSkill)
        {
            case 1:
                {
                    skillIndex = TableManager.i.GetTable<TowerData>(_ScrollIndex).FirstSkillIndex;
                }
                break;
            case 2:
                {
                    skillIndex = TableManager.i.GetTable<TowerData>(_ScrollIndex).SecondSkillIndex;

                }
                break;
            case 3:
                {
                    skillIndex = TableManager.i.GetTable<TowerData>(_ScrollIndex).ThirdSkillIndex;
                }
                break;

            default:
                break;
        }

        var skill = TableManager.i.GetTable<SkillData>(skillIndex);
        GetText("Txt_Skill").text = skill.Name;

    }

}
