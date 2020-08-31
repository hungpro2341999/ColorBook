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
    public Transform HorizontalDash;
    // Start is called before the first frame update
    public void Init()
    {
        LeftPos = Left.transform.position.x;
        RightPos = Right.transform.position.x;
        TopPos = Top.transform.position.y;
        BottomPos = Bottom.transform.position.y;
        foreach(var categories in ListCategories)
        {
            if (categories == null)
                continue;
            categories.Init();
        }

        //foreach (var categories in ListCategories)s
        //{
        //    var visible = categories.isVisible();
        //    categories.Visible.gameObject.SetActive(visible);
        //}
        for (int i = 0; i < ListCategories.Count; i++)
        {
            if (ListCategories[i] == null)
                continue;
                ListCategories[i].Visible.gameObject.SetActive(true);
         
              //  ListCategories[i].Visible.gameObject.SetActive(false);
            
        }
    }

    private void Update()
    {
        if (Vector3.SqrMagnitude(ScrollRect.velocity) >= 0.01f)
        {
            foreach (var categories in ListCategories)
            {
                if (categories == null)
                    continue;
                var visible = categories.isVisible();
                categories.Visible.gameObject.SetActive(visible);
                if (visible)
                {
                    categories.SelectHorizontal();
                }


            }
        }
        if(GameManager.Ins.check)
        {
           if(GameManager.Ins.isHorizontal)
            {
                HorizontalDash.GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                HorizontalDash.GetComponent<Image>().raycastTarget = true;
            }

        }
       
    }

    // public void 
    public void ChangeCategories(string nameCategories,string path,string unique)
    {
        foreach(var categories in ListCategories)
        {
            if (categories == null)
                continue;
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
            if (categories == null)
                continue;
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
            if (categories == null)
                continue;
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
            if (categories == null)
                continue;
            if (categories.nameCategories == name)
            {
                return categories.GetIconShow(unique);
            }
        }
        return null;
    }

    public Categories GetCategories(string name)
    {
        foreach(var categories in ListCategories)
        {
            if (categories == null)
                continue;
            if(categories.nameCategories == name)
            {
                return categories;
            }

            
        }

        return null;
    }
}
