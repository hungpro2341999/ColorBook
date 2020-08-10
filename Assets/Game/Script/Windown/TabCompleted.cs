using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TabCompleted : Tab
{
    [SerializeField]
    public List<PathPainted> ListUnique = new List<PathPainted>();
    public string path;
    public GameObject PerbIcon;
    public string pathId;
    public Transform Parent;
    public void Init()
    {
        if (!PlayerPrefs.HasKey(path))
        {

            List<Painted> List = new List<Painted>();
        
            ApplySave(List);
        }
        else
        {
            var source = GetPaintes();
            foreach(var painted in source)
            {
                ListUnique.Add(new PathPainted(painted.nameCategories,painted.unique));
            }
        }
    }

    public override void TriggerTab()
    {
        
        int i = 0;

        

        foreach (PathPainted file in ListUnique)
        {
            Debug.Log(i++);
            float width = 0;
            float height = 0;
            Texture2D tex = new Texture2D(500, 500, TextureFormat.RGB24, false);
            var a = Instantiate(PerbIcon, Parent);


            pathId = file.pathImg;

            if (tex.LoadImage(File.ReadAllBytes(SaveFilePath)))
            {

                tex.Apply(false, true);
                width = tex.width;
                height = tex.height;
                tex = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
                if (tex.LoadImage(File.ReadAllBytes(SaveFilePath)))
                {
                    Debug.Log(width + "  " + height);
                    a.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
                }

               





            }


        }
    }

    string SaveDirectory
    {
        get
        {
            string dir = Path.Combine(Application.persistentDataPath,path);
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
            return Path.Combine(SaveDirectory, pathId);
        }
    }
    [SerializeField]

    public class PathPainted
    {
        public PathPainted(string nameCategories,string path)
        {
            this.nameCategories = nameCategories;
            this.path = path;
        }
        public string nameCategories;
        public string pathImg
        {
            get { return path+".jpg"; }
            set { path = value; }
        } 

       public string path;
    }
    [SerializeField]
    public class ListPainted
    {
        public List<Painted> Paints = new List<Painted>();
        public ListPainted(List<Painted> ListPainted)
        {
            this.Paints = ListPainted;
        }
    }

    [SerializeField]
    public class Painted
    {
        public Painted(string unique,string nameCategories)
        {
            this.unique = unique;
            this.nameCategories = nameCategories;
        }
        public string unique;
        public string nameCategories;

    }

    public void AddPainted(Painted painted)
    {
        var paint = GetPaintes();
        paint.Add(painted);
        ApplySave(paint);
    }

    public void Remove(Painted painted)
    {
        var paint = GetPaintes();
        paint.Remove(painted);
        ApplySave(paint);
    }

    public void ApplySave(List<Painted> lists)
    {
        ListPainted painted = new ListPainted(lists);
        string json = JsonUtility.ToJson(painted);
        PlayerPrefs.SetString(path, json);
        PlayerPrefs.Save();
    }

    public List<Painted> GetPaintes()
    {
        string json = PlayerPrefs.GetString(path);
        return JsonUtility.FromJson<ListPainted>(json).Paints;
    }

    public void ApplyShow()
    {

    }
}
