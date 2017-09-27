using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : UIPopupBase
{

    void Start()
    {
        Transform kindBtn = transform.Find("KindButtons");
        GetButton(kindBtn, "Btn_Helmet");
        GetButton(kindBtn, "Btn_Weapon");

        Transform shopBtn = transform.Find("ShopButtons");
        GetButton(shopBtn, "Btn_Buy");
        GetButton(shopBtn, "Btn_Upgrade");
        GetButton(shopBtn, "Btn_Sell");
    }

    private void Buy()
    {

    }


}
