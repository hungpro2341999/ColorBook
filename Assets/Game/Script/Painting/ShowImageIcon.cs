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
        nameCategories = "Basic";
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OpenGameWindow);
    }

    // Start is called before the first frame update
    public void LoadIcon()
    {
        //Texture2D CloneTexture2D = new Texture2D((int)Page.GetSize().x, (int)Page.GetSize().y, TextureFormat.ARGB32, false);

        //Graphics.CopyTexture(Page.OutlineTexture, CloneTexture2D);



        //CloneTexture2D.Apply(false, true);
        //button.targetGraphic.GetComponent<Image>().sprite = Sprite.Create(CloneTexture2D, new Rect(0, 0, (int)Page.GetSize().x, (int)Page.GetSize().y), new Vector2(0.5f, 0.5f));
        Debug.Log("ChageImage : " + gameObject.name);
      

        Button button = GetComponent<Button>();
        button.onClick.AddListener(OpenGameWindow);
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
            string dir = Path.Combine(Application.persistentDataPath, PathPainting);
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
        GameManager.Ins.OpenWindown(TypeWindown.Painting);

        PathPainting = ((WindownHome)GameManager.Ins.GetWindown(TypeWindown.Home)).tabCategories.GetPath("Basic", Page.UniqueId);
       
        if (File.Exists(SaveFilePath))
        {
            DataCategori.PathSavePainting Save = new DataCategori.PathSavePainting(nameCategories,PathPainting,Page.UniqueId);
            CtrlPainting.Ins.StartPainting(Page, true,this,Save);
        }
        else
        {
            DataCategori.PathSavePainting Save = new DataCategori.PathSavePainting(nameCategories,"", Page.UniqueId);
            CtrlPainting.Ins.StartPainting(Page, false,this,Save);
        }
        
       
    }
}
