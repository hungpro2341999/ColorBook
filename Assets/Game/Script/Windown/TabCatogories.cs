using PaintCraft.Canvas.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabCatogories : Tab
{
    public List<Categories> ListCategories = new List<Categories>();


    // Start is called before the first frame update
    public void Init()
    {
        foreach(var categories in ListCategories)
        {
            categories.Init();
        }
    }

   // public void 
      public void ChangeCategories(string nameCategories,string path,string unique)
    {
        foreach(var categories in ListCategories)
        {
            if(categories.nameCategories == nameCategories)
            {
                DataCategori.PathSavePainting pathSave = new DataCategori.PathSavePainting(nameCategories, path, unique);
                categories.ChangeCategories(pathSave);
            }
        }
    }

    public string GetPath(string name, string unique)
    {
        foreach (var categories in ListCategories)
        {
            if (categories.nameCategories == name)
            {
                 return categories.GetPath(unique);
            }
        }
        return  "";
    }

    public ColoringPageConfig GetPaintConfig(string name,string unique)
    {
        foreach (var categories in ListCategories)
        {
            if (categories.nameCategories == name)
            {
                return categories.GetPathConfig(unique);
            }
        }
        return null;
    }

    public ShowImageIcon GetShowImageIcon(string name,string unique)
    {
        foreach (var categories in ListCategories)
        {
            if (categories.nameCategories == name)
            {
                return categories.GetIconShow(unique);
            }
        }
        return null;
    }
}
