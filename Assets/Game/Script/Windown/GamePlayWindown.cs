using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayWindown : Windown
{
    public static int PlayCount = 0;
    public TabCtrl CtrlTab;
    public bool ShowContinue;
    public float TimeShow;
    public Transform TapToContinue;
    public float time;
 
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            time = 0;
            ShowContinue = false;
            TapToContinue.gameObject.SetActive(false);
            GameManager.Ins.isGamePause = false;
        }
        if(!ShowContinue)
        {
           time += Time.deltaTime;
            if(time>=TimeShow)
            {
                ShowContinue = true;
                GameManager.Ins.isGamePause = true;
                TapToContinue.gameObject.SetActive(true);
            }
        }
        
    }
    public override void Event_Open()
    {
        time = 0;
        ShowContinue = false;
        CtrlTab.SwitchTab(1);

        GameManager.Ins.CloseSingleWindown(TypeWindown.Completed);
        GameManager.Ins.UI_General.gameObject.SetActive(false);
    }


    public void OpenCompletedWindown()
    {
        GameManager.Ins.OpenSingleWindown(TypeWindown.Completed);
    }

    public void BackToHome()
    {
        PlayCount++;
        if(PlayCount%3==0)
        {
           // ManagerAds.Ins.ShowInterstitial();
        }
        CtrlPainting.Ins.Paint.SaveImg();
        GameManager.Ins.OpenLoading();
        Invoke("StartBackHome",1);
    }
    public void StartBackHome()
    {
        StartCoroutine(GameManager.Ins.StartLoading(() => { GameManager.Ins.OpenWindown(TypeWindown.Home); }, LoadBack));
    }
    public override void Event_Close()
    {
        if (CtrlPainting.Ins.Paint.SpriteImg.sprite)
        {
            CtrlPainting.Ins.Paint.SpriteImg.sprite = null;
        }

        
        if (CtrlPainting.Ins.Paint.TemPlayer.SpriteImg!=null)
        CtrlPainting.Ins.Paint.TemPlayer.SpriteImg.sprite = null;

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
    public void StartMoveCompleteWindown()
    {
        GameManager.Ins.TrsLoading01.gameObject.SetActive(true);
        Invoke("MoveToCompleteWindown",1);
    }
    public void MoveToCompleteWindown()
    {
      
      StartCoroutine(GameManager.Ins.StartLoading01(() => { CtrlTab.SwitchTab(2); }, () => { GameManager.Ins.OpenSingleWindown(TypeWindown.Completed); }));
    }
}
