using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TabCompleted : Tab
{
   
    public string path;
    public GameObject PerbIcon;
    public string pathId;
    public Transform Parent;
    public override void TriggerTab()
    {
        int i = 0;
        foreach (string file in System.IO.Directory.GetFiles(SaveDirectory))
        {
            Debug.Log(i++);
            float width = 0;
            float height = 0;
            Texture2D tex = new Texture2D(500, 500, TextureFormat.RGB24, false);
            var a = Instantiate(PerbIcon, Parent);


            pathId = file;

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
}
