using PaintCraft.Canvas.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Paint : MonoBehaviour
{
    public bool isChangeCategories = false;
    public ColoringPageConfig PageColorPainting;
    public string categories;
    public string unique;
    public string PathPainting ="";
    public string typeLocal;
    public void ContinuePaint()
    {

    }

    public void DeletePaint()
    {
        Delete();
    }

    public void ResetPaint()
    {
        SaveTextureAsPNG(PageColorPainting.OutlineTexture, PathPainting);
        LoadPaint();
        ShowImageIcon ShowImageIcon = GameManager.Ins.GetHome().GetTabCategories().GetShowImageIcon(categories, unique);
        ShowImageIcon.LoadIcon();

    }

    public void Load(string nameCategories,string unique,string path)
    {
        Debug.Log(typeLocal);
        Debug.Log(SaveDirectory);
        this.categories = nameCategories;
        this.unique     = unique;
        PageColorPainting = GetColorPainting(categories, unique);
        this.PathPainting = Path.Combine(SaveDirectory, unique + ".jpg");
       // LoadPaint();

    }


    public ColoringPageConfig GetColorPainting(string nameCategories,string unique)
    {

        return ((WindownHome)GameManager.Ins.GetWindown(TypeWindown.Home)).tabCategories.GetPaintConfig(nameCategories, unique);

      
    }
    public void OpenGameWindown()
    {

        GameManager.Ins.OpenLoading();
        Invoke("StartLoading", 1f);

    }

    public void StartLoading()
    {

        StartCoroutine(GameManager.Ins.StartLoading(() => { GameManager.Ins.OpenWindown(TypeWindown.Painting); }, LoadPainting));
    }
    public void StartDelete()
    {
        StartCoroutine(GameManager.Ins.StartLoading(() => {}, Delete));
    }
    public void Delete()
    {
        if (File.Exists(PathPainting))
        {
            File.Delete(PathPainting);
        }
        var home = GameManager.Ins.GetHome();
        
        if (typeLocal == "InProcess")
        {
            var w = home.tabSaveImg.GetTabInProcess();
            w.Remove(new TabCompleted.Painted(unique, categories));
            w.paint.Remove(this);
            Destroy(gameObject);
        }
        else if(typeLocal == "Completed")
        {
            var w = home.tabSaveImg.GetTabCompleted();
            w.Remove(new TabCompleted.Painted(unique, categories));
            w.paint.Remove(this);
            Destroy(gameObject);
        }
        else
        {
            var w = home.tabSaveImg.GetTabShared();
            w.Remove(new TabCompleted.Painted(unique, categories));
            w.paint.Remove(this);
            Destroy(gameObject);
        }
     
       
    }

    public void LoadPainting()
    {
        if (isChangeCategories)
        {


         
            Debug.Log(PathPainting);

            ShowImageIcon ShowImageIcon = GameManager.Ins.GetHome().GetTabCategories().GetShowImageIcon(categories, unique);
            DataCategori.PathSavePainting Save = new DataCategori.PathSavePainting(categories, PathPainting, PageColorPainting.UniqueId);
            CtrlPainting.Ins.StartPaintingFromMyArt(PageColorPainting, true, this, Save, ShowImageIcon);

        }
        else
        {

           
            Debug.Log(PathPainting);


            DataCategori.PathSavePainting Save = new DataCategori.PathSavePainting(categories, PathPainting, PageColorPainting.UniqueId);
            CtrlPainting.Ins.StartPaintingFromMyArt(PageColorPainting, true, this, Save, null);

        }


    }

     

    public void LoadPaint()
    {
        float width = 0;
        float height = 0;
        Texture2D tex = new Texture2D(500, 500, TextureFormat.RGB24, false);
        Debug.Log(gameObject.name);
        
        if (tex.LoadImage(File.ReadAllBytes(PathPainting)))
        {

            tex.Apply(false, true);
            width = tex.width;
            height = tex.height;
            tex = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
            if (tex.LoadImage(File.ReadAllBytes(PathPainting)))
            {
                Debug.Log(width + "  " + height);
                transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
            }


        }

    }

    public  void ChangeColor()
    {
        float width = 0;
        float height = 0;
        Texture2D tex = new Texture2D(500, 500, TextureFormat.RGB24, false);
        if (tex.LoadImage(File.ReadAllBytes(PathPainting)))
        {

            tex.Apply(false, true);
            width = tex.width;
            height = tex.height;
            tex = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
            if (tex.LoadImage(File.ReadAllBytes(PathPainting)))
            {
                Debug.Log(width + "  " + height);
                transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
            }


        }
    }

    string SaveDirectory
    {
        get
        {
            string dir = Path.Combine(Application.persistentDataPath, typeLocal);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }

    public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {


        File.WriteAllBytes(_fullPath, _texture.EncodeToPNG());
      

        Debug.Log("Kb was saved as: " + _fullPath);
    }


}
