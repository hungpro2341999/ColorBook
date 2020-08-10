using PaintCraft.Canvas.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Categories : MonoBehaviour
{
    public string nameCategories = "";
    public ShowImageIcon[] ColorPageConfig;
    public string Key;
    
    // Start is called before the first frame update
   

    public void Init()
    {
        ColorPageConfig = transform.gameObject.GetComponentsInChildren<ShowImageIcon>();
       
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

    


}
