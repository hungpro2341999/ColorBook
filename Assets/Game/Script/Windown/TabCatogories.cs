using PaintCraft.Canvas.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabCatogories : Tab
{
    public List<Categories> ListCategories = new List<Categories>();
    public static float LeftPos;
    public static float RightPos;
    public static float TopPos;
    public static float BottomPos;
    public Transform Left;
    public Transform Right;
    public Transform Top;
    public Transform Bottom;
    public ScrollRect ScrollRect;
    // Start is called before the first frame update
    public void Init()
    {
        LeftPos = Left.transform.position.x;
        RightPos = Right.transform.position.x;
        TopPos = Top.transform.position.y;
        BottomPos = Bottom.transform.position.y;
        foreach(var categories in ListCategories)
        {
            categories.Init();
        }

        foreach (var categories in ListCategories)
        {
            var visible = categories.isVisible();
            categories.Visible.gameObject.SetActive(visible);
        }
    }

    private void Update()
    {
        if (Vector3.SqrMagnitude(ScrollRect.velocity) <= 0.01f)
            return;
        foreach(var categories in ListCategories)
        {
            var visible = categories.isVisible();
            categories.Visible.gameObject.SetActive(visible);
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
