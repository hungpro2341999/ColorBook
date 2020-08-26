using UnityEngine;
using UnityEngine.UI;


public class CompleteWindown :Windown
{
    // Start is called before the first frame update
    public Image img;
  
   
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Event_Open()
    {
        float ratio = CtrlPainting.Ins.Width / CtrlPainting.Ins.Height;

        float width = 678;
        float height = (CtrlPainting.Ins.Height * width) / CtrlPainting.Ins.Width;
        CtrlPainting.Ins.Paint.CloneTexure2D.Apply();
        GameManager.Ins.isGamePause = true;
        Texture2D tex = new Texture2D((int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height, TextureFormat.RGB24, false);
        var mainTexture = (Texture2D)CtrlPainting.Ins.Paint.CloneTexure2D;


        tex = mainTexture;

        if (height > 678)
        {
            width = (CtrlPainting.Ins.Width * 687 )/ (CtrlPainting.Ins.Height);
            height = 678;
        }
        img.GetComponent<RectTransform>().sizeDelta =new Vector2(width, height);
        img.sprite = Sprite.Create(tex, new Rect(0, 0, (int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height), new Vector2(0.5f, 0.5f));
       // img.SetNativeSize();
    }
    public override void Event_Close()
    {
        GameManager.Ins.isGamePause = false;
    }
    public void Load()
    {

       
    }

    public void SaveToCompleted()
    {
        GameManager.Ins.TrsLoading01.gameObject.SetActive(true);
        Invoke("StartSaveToCompleted", 0.8f);


    }

    public void StartSaveToCompleted()
    {
        StartCoroutine(GameManager.Ins.StartLoading01(() => { }, () => { CtrlPainting.Ins.Paint.SaveToCompleted(); }));
    }
    public void SaveToShared()
    {
        GameManager.Ins.TrsLoading01.gameObject.SetActive(true);

        Invoke("StartSaveToShared", 0.8f);

    }

    public void StartSaveToShared()
    {
        StartCoroutine(GameManager.Ins.StartLoading01(() => { }, () => { CtrlPainting.Ins.Paint.SaveToShared(); }));
    }
    public void Continue()
    {
        
       GameManager.Ins.CloseSingleWindown(TypeWindown.Completed);
    }
    public void BackToHome()
    {

        ((GamePlayWindown)GameManager.Ins.GetWindown(TypeWindown.Painting)).StartBackHome();
    }

    public void ContinuePaint()
    {

        ((GamePlayWindown)GameManager.Ins.GetWindown(TypeWindown.Painting)).BackToHome();


    }
}
