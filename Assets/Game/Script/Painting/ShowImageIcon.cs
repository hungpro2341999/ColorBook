using PaintCraft.Canvas.Configs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ShowImageIcon : MonoBehaviour
{
    public bool Especial;
    public ColoringPageConfig Page;
    public bool Load;
    // Start is called before the first frame update
    void Start()
    {
        //Texture2D CloneTexture2D = new Texture2D((int)Page.GetSize().x, (int)Page.GetSize().y, TextureFormat.ARGB32, false);

        //Graphics.CopyTexture(Page.OutlineTexture, CloneTexture2D);



        //CloneTexture2D.Apply(false, true);
        //button.targetGraphic.GetComponent<Image>().sprite = Sprite.Create(CloneTexture2D, new Rect(0, 0, (int)Page.GetSize().x, (int)Page.GetSize().y), new Vector2(0.5f, 0.5f));


        Button button = GetComponent<Button>();
        button.onClick.AddListener(OpenGameWindow);
        Texture2D tex = new Texture2D((int)Page.GetSize().x, (int)Page.GetSize().y, TextureFormat.RGB24, false);
        if (File.Exists(SaveFilePath))
        {
            if (tex.LoadImage(File.ReadAllBytes(SaveFilePath)))
            {
                tex.Apply(false, true);
                button.targetGraphic.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, (int)Page.GetSize().x, (int)Page.GetSize().y), new Vector2(0.5f, 0.5f));
            }
        }





    }

    private void OnEnable()
    {
       
    }
    public void LoadIcon()
    {

        Button button = GetComponent<Button>();

   

        Texture2D tex = new Texture2D((int)Page.GetSize().x, (int)Page.GetSize().y, TextureFormat.RGB24, false);


        if (File.Exists(SaveFilePath))
        {
            if (tex.LoadImage(File.ReadAllBytes(SaveFilePath)))
            {
                tex.Apply(false, true);
                button.targetGraphic.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, (int)Page.GetSize().x, (int)Page.GetSize().y), new Vector2(0.5f, 0.5f));
            }
        }
        else
        {
           


        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string SaveDirectory
    {
        get
        {
            string dir = Path.Combine(Application.persistentDataPath, "InProcess");
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
            return Path.Combine(SaveDirectory, Page.UniqueId + ".jpg");
        }
    }

    public void OpenGameWindow()
    {
        GameManager.Ins.OpenWindown(TypeWindown.Painting);
        if (File.Exists(SaveFilePath))
        {
            CtrlPainting.Ins.StartPainting(Page, true);
        }
        else
        {
            CtrlPainting.Ins.StartPainting(Page, false);
        }
        
       
    }
}
