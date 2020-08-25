using PaintCraft.Canvas.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Categories : MonoBehaviour
{
    public string nameCategories = "";
    public ShowImageIcon[] ColorPageConfig;
    public string Key;
    public int LimitShowStart;
    public bool isScroll;
    public ScrollRect Scroll;
    public RectTransform Rect;
    public Transform Visible;
    public List<ColoringPageConfig> listItem;
    public List<Sprite> ListImage = new List<Sprite>();
    public ShowImageIcon[] ListIcon = new ShowImageIcon[0];
    public GameObject PaintColoring;
    public bool isInstace = false;
    public Transform parent;
    public int index;

    // Start is called before the first frame update
    private void Start()
    {
       
      
        Rect = GetComponent<RectTransform>();
    }
    public void AddListIcon()
    {
        ListIcon = transform.GetComponentsInChildren<ShowImageIcon>();
    }
    public void OnDrag()
    {
        isScroll = true;
    }
    public void OnEndDrag()
    {
        isScroll = false;
    }
    public void Init()
    {

        if(isInstace)
        {
            ColorPageConfig = new ShowImageIcon[listItem.Count];
            for(int i=0;i<listItem.Count;i++)
            {
               var  a =   Instantiate(PaintColoring, parent);
                var aa = a.transform.GetChild(0).transform.GetChild(0);
                aa.GetComponent<ShowImageIcon>().nameCategories = nameCategories;
                aa.GetComponent<ShowImageIcon>().Page = listItem[i];
                aa.GetComponent<Image>().sprite = ListImage[i];
                GameObject.Find("LoadData").GetComponent<DataCategori>().categories[index].ListPainting.Add(aa.GetComponent<Image>());
                ColorPageConfig[i] = aa.GetComponent<ShowImageIcon>();
             
            }

        }
        else
        {

            ColorPageConfig = transform.gameObject.GetComponentsInChildren<ShowImageIcon>();
        }

     
        if(!PlayerPrefs.HasKey(Key))
        {
           
            List<DataCategori.PathSavePainting> ListPath = new List<DataCategori.PathSavePainting>();
          
            foreach (var paint in ColorPageConfig)
            {


                DataCategori.PathSavePainting Paint = new DataCategori.PathSavePainting(nameCategories, "", paint.Page.UniqueId);
                ListPath.Add(Paint);
            }
            DataCategori.ListPathSave Saves = new DataCategori.ListPathSave(ListPath);
            string json = JsonUtility.ToJson(Saves);

            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();

            string count = PlayerPrefs.GetString(Key);
           int coun1 =  JsonUtility.FromJson<DataCategori.ListPathSave>(count).List.Count;
            Debug.Log("Change : " + coun1);

        }
        else
        {
            var categories = GetCategories();
            int i = 0;
            foreach(DataCategori.PathSavePainting pathSave in categories)
            {
                
                if(pathSave.path != "")
                {
                    ColorPageConfig[i].PathPainting = pathSave.path;
                    ColorPageConfig[i].LoadIcon();
                }
                i++;
            }
        }

        for(int i=0;i<ColorPageConfig.Length;i++)
        {
            if(i<LimitShowStart)
            {
                ColorPageConfig[i].IsShowStart = true;
                ColorPageConfig[i].Visible();
                try
                {
                    ColorPageConfig[i].Anim.SetBool("Load1", true);
                }
                catch(System.Exception e)
                {

                }
               
            }
            else
            {
                ColorPageConfig[i].IsShowStart = false;
                ColorPageConfig[i].UnVisible();
            }
           
        }
       


    }

    public ShowImageIcon GetIconShow(string unique)
    {
        for (int i = 0; i < ColorPageConfig.Length; i++)
        {
            if (ColorPageConfig[i].Page.UniqueId == unique)
            {
                return ColorPageConfig[i];
            }
        }
        return null;
    }

    public  ColoringPageConfig GetPathConfig(string unique)
    {
        for(int i = 0; i < ColorPageConfig.Length;i++)
        {
            if(ColorPageConfig[i].Page.UniqueId == unique)
            {
                return ColorPageConfig[i].Page;
            }
        }
        return null;
    }

    public List<DataCategori.PathSavePainting> GetCategories()
    {
        string json = PlayerPrefs.GetString(Key);
       return JsonUtility.FromJson<DataCategori.ListPathSave>(json).List;
     
    }

    public void ChangeCategories(DataCategori.PathSavePainting Path)
    {
        var PathSavePainting = GetCategories();
      for(int i=0;i<PathSavePainting.Count;i++)
        {
           
            if(PathSavePainting[i].uniqueId == Path.uniqueId)
            {
                PathSavePainting[i] = Path;
                break;
            }
        }
        ApplySave(PathSavePainting);
    }

    public void ApplySave(List<DataCategori.PathSavePainting> ListPath)
    {
        DataCategori.ListPathSave Saves = new DataCategori.ListPathSave(ListPath);
        string json = JsonUtility.ToJson(Saves);

        PlayerPrefs.SetString(Key, json);
        PlayerPrefs.Save();
        
    }
    public string GetPath(string unique)
    {
        var PathSavePainting = GetCategories();
        for (int i = 0; i < PathSavePainting.Count; i++)
        {

            if (PathSavePainting[i].uniqueId == unique)
            {
                return PathSavePainting[i].path;
            }
        }
        return null;
    }
    public void ShowAll()
    {
       GameManager.Ins.OpenWindown(TypeWindown.SellAll);

        var win = GameManager.Ins.GetWindown(TypeWindown.SellAll).GetComponent<WindownSeeAll>();
        win.ShowAll(nameCategories);
    }

    private void Update()
    {
        isScroll = (Vector3.Magnitude(Scroll.velocity) > 0.01f);
        if (!isScroll)
            return;

         

        for (int i =0; i< ColorPageConfig.Length;i++)
        {

                ColorPageConfig[i].Check();
            }
               
           
          
        
    }

    public bool isVisible()
    {
        if(TabCatogories.TopPos>=Rect.position.y && TabCatogories.BottomPos<=Rect.position.y)
        {
            return true;
        }

        return false;
    }
   

}
