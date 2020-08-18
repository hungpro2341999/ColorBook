using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TempLayer : MonoBehaviour
{
    public Transform TransImg;
    public Image Img;
    public Transform Privort;
    public Texture2D tex;
    public float posCurr;
    public  bool DoneFloodFill = true;
    public float Speed;
    public float max = 1000;
    public float SpeedStart;
    public static float PixelPanit;
    public float totalPixel;
    // Start is called before the first frame update
    private void Awake()
    {
        Img = TransImg.GetComponent<Image>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            PlayAimPaint();
        }
        if (!DoneFloodFill)
        {
            PlayAimPaint();
        }
    }

    public void Init()
    {
        
        tex = new Texture2D((int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height, TextureFormat.ARGB32, false);
         tex = duplicateTexture(tex);
        SpeedStart = 0;
        gameObject.SetActive(false);

    }
    public void SetTempText(Color[] color,Vector3 posCenter,bool Active)
    {

        
        gameObject.SetActive(Active);

       
        Privort.transform.position = Camera.main.ScreenToWorldPoint(posCenter);

        Vector3 pos = Privort.transform.localPosition;
        pos.z = 0;
        Privort.transform.localPosition = pos;



        TransImg.transform.position =  CtrlPainting.Ins.Paint.transform.position;
      //  Vector3 pos1 = TransImg.transform.localPosition;
      
     
        tex.SetPixels(color);

        totalPixel = (int)CtrlPainting.Ins.Width* (int)CtrlPainting.Ins.Height;

       tex.Apply();
        
        Img.overrideSprite = Sprite.Create(tex, new Rect(0, 0, (int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height), new Vector2(0.5f, 0.5f));
        Img.SetNativeSize();
       
        //  PixelPanit = totalPixel - PixelPanit;
        max = 1800;

    }
    public void PlayAimPaint()
    {
        SpeedStart += Time.deltaTime * Speed;
      
        RectTransform Rect = transform.GetChild(0).GetComponent<RectTransform>();
        posCurr += SpeedStart;
        Rect.sizeDelta = new Vector2(posCurr,posCurr);
        if(posCurr >= max)
        {
           //tex.SetPixels(CtrlPainting.Ins.Paint.colorReset);
           // tex.Apply()
            SpeedStart = 0;
           DoneFloodFill = true;
            Apply();
        }
    }

    public void Active()
    {

    }

    public void Apply()
    {
        gameObject.SetActive(false);
        CtrlPainting.Ins.Paint.Apply();
    }
    public void StartFloodFill()
    {
        
        RectTransform Rect = transform.GetChild(0).GetComponent<RectTransform>();
        posCurr = 0;
        DoneFloodFill = false;
        Rect.sizeDelta = new Vector2(posCurr, posCurr);
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
