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
    private void Awake()
    {
        if(Ins==null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       // Intialize Painting
          Init();
        T_Complete.text += "1 Complete";
        Paint.Init();
        T_Complete.text += "2 Complete";
        CameraZoom.Init(this);
        T_Complete.text += "3 Complete";



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

}
