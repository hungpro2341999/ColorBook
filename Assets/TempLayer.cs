using PaintCraft.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TempLayer : MonoBehaviour
{
    public Transform TransImg;
  
    public Transform Privort;
    public Texture2D tex;
    public float posCurr;
    public  bool DoneFloodFill = true;
    public float Speed;
    public float max = 1000;
    public float SpeedStart;
    public static float PixelPanit;
    public float totalPixel;
    public Shader shader;
    public SpriteRenderer SpriteImg;
    // Start is called before the first frame update
    private void Awake()
    {
        SpriteImg = GetComponent<SpriteRenderer>();
  //      Img = TransImg.GetComponent<Image>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (!DoneFloodFill)
        {
            PlayAimPaint();
        }
    }

    public void Init()
    {
        //float width = CtrlPainting.Ins.PageConfig.GetSize().x;
        //float height = CtrlPainting.Ins.PageConfig.GetSize().y;
        //tex = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);
        //tex = duplicateTexture(tex);
        //MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);

        //Mesh mesh = MeshUtil.CreatePlaneMesh(width, height);

        //mf.mesh = mesh;

        //MeshRenderer mr = GOUtil.CreateComponentIfNoExists<MeshRenderer>(gameObject);

        //Material material = new Material(shader);

        //mr.material = material;
        //tex.SetPixels(((Texture2D)CtrlPainting.Ins.Paint.material.mainTexture).GetPixels());

        //tex.Apply();
        //mr.material.mainTexture = tex;

        //



        tex = new Texture2D((int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height, TextureFormat.RGB24, false);
        ////tex = duplicateTexture(tex);
        ////SpeedStart = 0;
        ////gameObject.SetActive(false);
        ////tex.SetPixels((CtrlPainting.Ins.Paint.CloneTexure2D).GetPixels());
        Graphics.CopyTexture(CtrlPainting.Ins.Paint.CloneTexure2D, tex);
        tex.Apply();
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, (int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height), new Vector2(0.5f, 0.5f));





    }
    public void SetTempText(Color[] color,Vector3 posCenter,bool Active,bool change)
    {
       

        gameObject.SetActive(Active);


        Privort.transform.position = Camera.main.ScreenToWorldPoint(posCenter);

        Vector3 pos = Privort.transform.position;
        pos.z = -1;
       
        Privort.transform.position = pos;



        TransImg.transform.position = (Vector3.zero+new Vector3(0,0,-1));

        
        if (change)
        {
            SpriteImg.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            CtrlPainting.Ins.Paint.SpriteImg.maskInteraction = SpriteMaskInteraction.None;
           // tex = CtrlPainting.Ins.Paint.CloneTexure2D;
            Graphics.CopyTexture(CtrlPainting.Ins.Paint.CloneTexure2D, tex);
            tex.Apply();


            SpriteImg.sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100F, 0, SpriteMeshType.FullRect);

            SpriteImg.sortingLayerName = "Layer3";

           
        }
        else
        {
            SpriteImg.sortingLayerName = "Layer1";
            SpriteImg.maskInteraction = SpriteMaskInteraction.None;
            CtrlPainting.Ins.Paint.SpriteImg.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            Apply();
        }

        max = 80;

    }
    bool load = false;
    public void PlayAimPaint()
    {
        SpeedStart += Time.deltaTime * Speed;
      
      
        posCurr += SpeedStart;
        Privort.localScale = new Vector2(posCurr,posCurr);
        if(posCurr>=max/2)
        {
            if(!load)
            {

               
                load = true;
            }
        }
        if(posCurr >= max)
        {
          //  gameObject.SetActive(false);
         //    Apply();
            //tex.SetPixels(CtrlPainting.Ins.Paint.colorReset);
          //  tex.Apply();
            SpeedStart = 0;
           DoneFloodFill = true;
           


        }
    }

    public void Active()
    {

    }

    public void Apply()
    {
      //  gameObject.SetActive(false);
      CtrlPainting.Ins.Paint.ApplyColor();
    }
    public void StartFloodFill()
    {
        
      //  RectTransform Rect = transform.GetChild(0).GetComponent<RectTransform>();
        posCurr = 0;
        DoneFloodFill = false;
    //    Rect.sizeDelta = new Vector2(posCurr, posCurr);
        load = false;
    }
  
    Texture2D duplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}
