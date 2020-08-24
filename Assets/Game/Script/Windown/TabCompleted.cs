using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TabCompleted : Tab
{
    [SerializeField]
    public bool isChangeCategories = false;
    public List<PathPainted> ListUnique = new List<PathPainted>();
    public List<PathPainted> ListPaintChange = new List<PathPainted>();
    public List<PathPainted> ListPaintChangeColor = new List<PathPainted>();
    public string path;
    public GameObject PerbIcon;
    public string pathId;
    public Transform Parent;
    public List<Paint> paint = new List<Paint>();


    public Paint GetPaint(string nameCategroies,string unique)
    {
        for(int i=0;i<paint.Count;i++)
        {
            if (paint[i] == null)
                continue;
            if(paint[i].unique==unique && paint[i].categories == nameCategroies)
            {
               
                return paint[i];
            }
        }
        return null;
    }
    public void Init()
    {

        Debug.Log("Init Tab COmpleted");
     
         

        if (!PlayerPrefs.HasKey(path))
        {

            List<Painted> List = new List<Painted>();
        
            ApplySave(List);
            Debug.Log(GetPaintes().ToString());
        }
        else
        {
         
            var source = GetPaintes();
            if (source.Count == 0)
            {
                return;
            }
            foreach (var painted in source)
            {
                ListUnique.Add(new PathPainted(painted.nameCategories,painted.unique,painted.unique));
            }
            Debug.Log(GetPaintes().ToString());
        }
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(GetPaintes().Count);
        }
    }
    public override void TriggerTab()
    {
        Init();



        int i = 0;

        

        foreach (PathPainted file in ListUnique)
        {
            Debug.Log(i++);
            float width = 0;
            float height = 0;
            Texture2D tex = new Texture2D(500, 500, TextureFormat.RGB24, false);
           
            var a = Instantiate(PerbIcon, Parent).GetComponent<Paint>();
            a.isChangeCategories = isChangeCategories;
            a.typeLocal = path;
            a.Load(file.nameCategories, file.unique, path);

            paint.Add(a);

           if(File.Exists(Path.Combine(SaveFilePath, (file.unique + ".jpg"))))
            {
                if (tex.LoadImage(File.ReadAllBytes(Path.Combine(SaveFilePath, (file.unique + ".jpg")))))
                {

                    tex.Apply(false, true);
                    width = tex.width;
                    height = tex.height;
                    tex = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
                    if (tex.LoadImage(File.ReadAllBytes(Path.Combine(SaveFilePath, (file.unique + ".jpg")))))
                    {
                        Debug.Log(width + "  " + height);
                        a.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
                    }


                }

            }



        }
    }

    public override void Reflect()
    {
       
            for(int i=0;i<ListPaintChange.Count;i++)
        {
            AddPainted(ListPaintChange[i]);
        }
            for(int i=0;i<ListPaintChangeColor.Count;i++)
        {
            ChangePaint(ListPaintChangeColor[i]);
        }
        ListPaintChange = new List<PathPainted>();
        ListPaintChangeColor = new List<PathPainted>();


    }

    public void ChangePaint(PathPainted file)
    {
        for(int i=0;i<paint.Count;i++)
        {
            if(paint[i].categories == file.nameCategories && paint[i].unique == file.unique)
            {
                paint[i].ChangeColor();
            }
        }
    }

    public void AddPainted(PathPainted file)
    {
        float width = 0;
        float height = 0;
        Texture2D tex = new Texture2D(500, 500, TextureFormat.RGB24, false);
        var a = Instantiate(PerbIcon, Parent).GetComponent<Paint>();
        a.isChangeCategories = isChangeCategories;
        a.typeLocal = path;
        a.Load(file.nameCategories, file.unique, path);
        


        if (tex.LoadImage(File.ReadAllBytes(Path.Combine(SaveFilePath,(file.unique+".jpg")))))
        {

            tex.Apply(false, true);
            width = tex.width;
            height = tex.height;
            tex = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
            if (tex.LoadImage(File.ReadAllBytes(Path.Combine(SaveFilePath, (file.unique + ".jpg")))))
            {
                Debug.Log(width + "  " + height);
                a.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
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
    [System.Serializable]

    public class PathPainted
    {
        public PathPainted(string nameCategories,string path,string unique)
        {
            this.unique = unique;
            this.nameCategories = nameCategories;
            this.path = path;
        }
    
        public string nameCategories;
        public string unique;
        public string pathImg
        {
            get { return path+".jpg"; }
            set { path = value; }
        } 

       public string path;
    }
    [System.Serializable]
    public class ListPainted
    {
        public List<Painted> Paints = new List<Painted>();
        public ListPainted(List<Painted> ListPainted)
        {
            this.Paints = ListPainted;
        }
    }

    [System.Serializable]
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
        for(int i=0;i<paint.Count;i++)
        {
            if(painted.unique == paint[i].unique)
            {
                paint.Remove(paint[i]);
                Debug.Log("Remove");
                break;

            }
        }
        Debug.Log(GetPaintes().ToString());
        ApplySave(paint);
    }

    public void ApplySave(List<Painted> lists)
    {
        ListPainted painted = new ListPainted(lists);
        string json = JsonUtility.ToJson(painted);

        Debug.Log("Json : "+json);
        PlayerPrefs.SetString(path, json);
        PlayerPrefs.Save();

        var s = JsonUtility.FromJson<ListPainted>(PlayerPrefs.GetString(path)).Paints;
        Debug.Log("s : " + s.Count);
    }

    public List<Painted> GetPaintes()
    {
        string json = PlayerPrefs.GetString(path);
        return JsonUtility.FromJson<ListPainted>(json).Paints;
    }

    public void AddToInforImageToDisk(string nameUnique,string nameCategories)
    {
        bool hasExits = false;
        var source = GetPaintes();
        Debug.Log(source.Count);
        Painted paint = new Painted(nameUnique, nameCategories);


        for(int i = 0; i < source.Count; i++)
        {
            if(source[i].nameCategories == paint.nameCategories && source[i].unique == nameUnique)
            {
                hasExits = true;
                break;
            }
        }


        if (!hasExits)
        {

            Debug.Log("Da Luu");
            AddPainted(paint);
            ListPaintChange.Add(new PathPainted(nameCategories, path, nameUnique));
        }
        else
        {
            Debug.Log(path);
            if(!ListPaintChangeColor.Contains(new PathPainted(nameCategories, path, nameUnique)))
            {
                ListPaintChangeColor.Add(new PathPainted(nameCategories, path, nameUnique));
            }

            
        }
      
        
    }
    
}
