using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindownHome : Windown
{
    public TabMyArt tabSaveImg;
    public TabCatogories tabCategories;
    public TabShared tabShared;
   
    private void Start()
    {
       
        tabCategories.Init();
        tabSaveImg.Init();
    }
    public override void Event_Open()
    {
        Debug.Log("Run");
        GameManager.Ins.UI_General.gameObject.SetActive(true);
        tabSaveImg.ReflectTab();
       
    }

    public TabCatogories GetTabCategories()
    {
        return tabCategories;
    }

    public TabMyArt GetTabMyArt()
    {
        return tabSaveImg;
    }

  
}
