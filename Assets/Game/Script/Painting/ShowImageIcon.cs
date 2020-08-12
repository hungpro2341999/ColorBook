using PaintCraft.Canvas.Configs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ShowImageIcon : MonoBehaviour
{
    public string nameCategories;
    public bool Especial;
    public ColoringPageConfig Page;
    public bool Load;
    public string PathPainting="";
    private void Start()
    {

         PathPainting = SaveFilePath;
   
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OpenGameWindow);
    }

    // Start is called before the first frame update
    public void LoadIcon()
    {
        
      
        Button button = GetComponent<Button>();
     
        Texture2D tex = new Texture2D((int)Page.GetSize().x, (int)Page.GetSize().y, TextureFormat.RGBA32, false);
        if (File.Exists(SaveFilePath))
        {
            if (tex.LoadImage(File.ReadAllBytes(SaveFilePath)))
            {
                tex.Apply(false, true);
                button.targetGraphic.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, (int)Page.GetSize().x, (int)Page.GetSize().y), new Vector2(0.5f, 0.5f));
            }
        }





    }

   

   
    void Update()
    {
        
    }

    string SaveDirectory
    {
        get
        {
            string dir = Path.Combine(Application.persistentDataPath, "InProcess");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }

    string SaveFilePath
    {
        get
        {
            return Path.Combine(SaveDirectory, Page.UniqueId + ".jpg");
        }
    }

    public void OpenGameWindow()
    {

       StartCoroutine(GameManager.Ins.StartLoading(() => { GameManager.Ins.OpenWindown(TypeWindown.Painting); }

        , StartLoading));
     
      

    }

    public void StartLoading()
    {
        if (File.Exists(PathPainting))
        {
            CtrlPainting.Ins.Paint.PathSave = PathPainting;
            DataCategori.PathSavePainting Save = new DataCategori.PathSavePainting(nameCategories, PathPainting, Page.UniqueId);
            CtrlPainting.Ins.StartPainting(Page, true, this, Save);


        }
        else
        {

            CtrlPainting.Ins.Paint.PathSave = PathPainting;
            DataCategori.PathSavePainting Save = new DataCategori.PathSavePainting(nameCategories, "", Page.UniqueId);
            CtrlPainting.Ins.StartPainting(Page, false, this, Save);
            var home = ((WindownHome)GameManager.Ins.GetWindown(TypeWindown.Home));
            home.GetTabMyArt().GetTabInProcess().AddToInforImageToDisk(CtrlPainting.Ins.CacheToPaint.PathSave.uniqueId, CtrlPainting.Ins.CacheToPaint.PathSave.categories);
        }

       
    }

    

    
}
