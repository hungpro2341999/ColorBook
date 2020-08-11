using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayWindown : Windown
{
   
    public override void Event_Open()
    {
        GameManager.Ins.UI_General.gameObject.SetActive(false);
    }

    public void OpenCompletedWindown()
    {
        GameManager.Ins.OpenSingleWindown(TypeWindown.Completed);
    }

    public void BackToHome()
    {
        GameManager.Ins.OpenWindown(TypeWindown.Home);
    }
    public override void Event_Close()
    {
        if(CtrlPainting.Ins.CacheToPaint.PathSave!=null)
        CtrlPainting.Ins.ApplyToChage(CtrlPainting.Ins.CacheToPaint.PathSave.path);
    }
}
