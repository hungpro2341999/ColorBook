using PaintCraft.Canvas.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCategori : MonoBehaviour
{
    public int countCategories;
    [SerializeField]
    public List<Categories> categories;
    public void Init()
    {
        for (int i = 0; i < countCategories; i++)
        {

        }
    }



    [System.Serializable]
    public class Categories
    {
        public Categories(string NameCategories,List<Image> ListPainting)
        {
            this.NameCategories = NameCategories;
            this.ListPainting   = ListPainting;
        }

        public string NameCategories;
        public List<Image> ListPainting = new List<Image>();
    }

    [System.Serializable]
    public class ListPathSave
    {
      public ListPathSave(List<PathSavePainting> List)
        {
            this.List = List;
        }
      public  List<PathSavePainting> List = new List<PathSavePainting>();
    }



    [System.Serializable]
    public class PathSavePainting
    {

        public PathSavePainting(string categories, string path, string unique)
        {
            this.categories = categories;
            this.path = path;
            this.uniqueId = unique;
        }
       
       public string categories;
       public string path;
       public string uniqueId;
    }
}
