using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindownHome : Windown
{
    public TabCtrl tabSaveImag;
    private void Start()
    {
        Event_Open();
    }
    public override void Event_Open()
    {
        GameManager.Ins.UI_General.gameObject.SetActive(true);
        tabSaveImag.Init();
    }
}
