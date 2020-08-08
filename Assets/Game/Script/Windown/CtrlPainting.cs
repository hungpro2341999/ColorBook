using PaintCraft.Canvas.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlPainting : MonoBehaviour
{
    public static CtrlPainting Ins = null;

    public ColoringPageConfig PageConfig;
    public PaintingLayer Paint;
    public CtrlCameraZoom CameraZoom;
    public float Width;
    public float Height;
    public Text T_Complete;
   
    // Start is called before the first frame update
    void Start()
    {
        // Intialize Painting




    }
    public void Initialize()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        if (PageConfig != null)
        {
            Width = PageConfig.GetSize().x;
            Height = PageConfig.GetSize().y;
        }
     

    }

    public void ChangeColor(Color color)
    {
        Paint.ColorPainting = color;
    }



    public void StartPainting(ColoringPageConfig Color,bool Load)
    {
        Paint.load = Load;
        PageConfig = Color;
        Init();
        CameraZoom.Init(this);
        Paint.Init();
    }
}
