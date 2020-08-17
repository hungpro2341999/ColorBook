﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CompleteWindown :Windown
{
    // Start is called before the first frame update
    public Image img;
  
   
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Event_Open()
    {
        GameManager.Ins.isGamePause = true;
        Texture2D tex = new Texture2D((int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height, TextureFormat.RGB24, false);
        var mainTexture = (Texture2D)CtrlPainting.Ins.Paint.material.mainTexture;


        tex = mainTexture;


        img.sprite = Sprite.Create(tex, new Rect(0, 0, (int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height), new Vector2(0.5f, 0.5f));
        img.SetNativeSize();
    }
    public override void Event_Close()
    {
        GameManager.Ins.isGamePause = false;
    }
    public void Load()
    {

       
    }

    public void SaveToCompleted()
    {
        GameManager.Ins.TrsLoading.gameObject.SetActive(true);
        Invoke("StartSaveToCompleted", 0.5f);


    }

    public void StartSaveToCompleted()
    {
        StartCoroutine(GameManager.Ins.StartLoading(() => { }, () => { CtrlPainting.Ins.Paint.SaveToCompleted(); }));
    }
    public void SaveToShared()
    {
        GameManager.Ins.TrsLoading.gameObject.SetActive(true);
        Invoke("StartSaveToShared", 0.5f);

    }

    public void StartSaveToShared()
    {
        StartCoroutine(GameManager.Ins.StartLoading(() => { }, () => { CtrlPainting.Ins.Paint.SaveToShared(); }));
    }
    public void Continue()
    {
        GameManager.Ins.CloseSingleWindown(TypeWindown.Completed);
    }
    public void Back()
    {
        GameManager.Ins.CloseSingleWindown(TypeWindown.Completed);
    }

    public void ContinuePaint()
    {
        GameManager.Ins.OpenWindown(TypeWindown.Home);
    }
}
