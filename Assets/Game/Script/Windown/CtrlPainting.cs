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
    public  CacheToPainting CacheToPaint = new CacheToPainting();
    public List<string> PaintChage;
     
   
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



    public void StartPainting(ColoringPageConfig Color,bool Load,ShowImageIcon ButtonChange, DataCategori.PathSavePainting PathSave)
    {
        CacheToPaint.Change = ButtonChange;
        CacheToPaint.PathSave = PathSave;
        CacheToPaint.Paint = GameManager.Ins.GetHome().GetTabMyArt().GetTabInProcess().GetPaint(PathSave.categories, PathSave.uniqueId);
        Paint.load = Load;
        PageConfig = Color;
        Init();
        CameraZoom.Init(this);
        Paint.Init();
       

    }

    public void StartPaintingFromMyArt(ColoringPageConfig Color, bool Load, Paint paint, DataCategori.PathSavePainting PathSave, ShowImageIcon ButtonChange)
    {
        CacheToPaint.Paint = paint;
        CacheToPaint.PathSave = PathSave;
        CacheToPaint.Change = ButtonChange;

        Paint.load = Load;
        PageConfig = Color;
        Init();
        CameraZoom.Init(this);
        Paint.Init();
    }
    [System.Serializable]
    public class CacheToPainting
    {

        public ShowImageIcon Change;
        public DataCategori.PathSavePainting PathSave;
        public Paint Paint;
    }


    public void ApplyToChage(string path)
    {


    
        if(CacheToPaint.Change!=null)
        {
            CacheToPaint.Change.LoadIcon();
        }
        if (CacheToPaint.Paint != null)
        {
            CacheToPaint.Paint.LoadPaint();
        }
       
        
        var home =   ((WindownHome)GameManager.Ins.GetWindown(TypeWindown.Home));
        home.tabCategories.ChangeCategories(CacheToPaint.PathSave.categories, path, CacheToPaint.PathSave.uniqueId);

        CacheToPaint.Change = null;
        CacheToPaint.Paint = null;
        CacheToPaint.PathSave = null;
        
       
    }

   public void ApplyToChangeToCompled()
    {
        if (CacheToPaint.Paint!=null)
        {
            CacheToPaint.Paint.LoadPaint();
        }

      
        var home = ((WindownHome)GameManager.Ins.GetWindown(TypeWindown.Home));
        home.GetTabMyArt().GetTabCompleted().AddToInforImageToDisk(CacheToPaint.PathSave.uniqueId, CacheToPaint.PathSave.categories);
    }

   
    public void ApplyToChangeToShared()
    {
        if (CacheToPaint.Paint != null)
        {
            CacheToPaint.Paint.LoadPaint();
        }
        var home = ((WindownHome)GameManager.Ins.GetWindown(TypeWindown.Home));
        home.GetTabMyArt().GetTabShared().AddToInforImageToDisk(CacheToPaint.PathSave.uniqueId, CacheToPaint.PathSave.categories);

    }
    
   
    
}
