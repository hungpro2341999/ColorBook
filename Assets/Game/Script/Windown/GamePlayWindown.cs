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
        CtrlPainting.Ins.Paint.SaveImg();
        GameManager.Ins.TrsLoading.gameObject.SetActive(true);
        Invoke("StartBackHome", 0.1f);
    }
    public void StartBackHome()
    {
        StartCoroutine(GameManager.Ins.StartLoading(() => { GameManager.Ins.OpenWindown(TypeWindown.Home); }, LoadBack));
    }
    public override void Event_Close()
    {

       
      
        //var home = ((WindownHome)GameManager.Ins.GetWindown(TypeWindown.Home));
        //home.GetTabMyArt().GetTabInProcess().AddToInforImageToDisk(CtrlPainting.Ins.CacheToPaint.PathSave.uniqueId, CtrlPainting.Ins.CacheToPaint.PathSave.categories);

    }

    public void LoadBack()
    {
        if (CtrlPainting.Ins.CacheToPaint.PathSave != null)
            CtrlPainting.Ins.ApplyToChage(CtrlPainting.Ins.CacheToPaint.PathSave.path);
        if (CtrlPainting.Ins.CacheToPaint.Paint != null)
        {
            CtrlPainting.Ins.CacheToPaint.Paint.LoadPaint();
        }
    }
}
