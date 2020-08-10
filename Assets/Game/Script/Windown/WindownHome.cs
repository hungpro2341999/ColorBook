using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindownHome : Windown
{
    public TabCompleted tabSaveImg;
    public TabCatogories tabCategories;
   
    private void Start()
    {
        tabCategories.Init();
    }
    public override void Event_Open()
    {
        Debug.Log("Run");
        GameManager.Ins.UI_General.gameObject.SetActive(true);
        tabSaveImg.Init();
       
    }

    public TabCatogories GetTabCategories()
    {
        return tabCategories;
    }

    public TabCompleted GetTabSaveImg()
    {
        return tabSaveImg;
    }
}
