using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindownLoading :Windown
{
    public bool Loading = false;
    public Image LoadingImg;
    public Text tex;
    public float speed;
    
    public override void Event_Open()
    {
        LoadingImg.fillAmount = 0;
     
        StartCoroutine(StartShow());
    }

 

    public IEnumerator StartShow()
    {
        tex.text = "LOADING DATA....";
        
        yield return new WaitForSeconds(1);

        tex.text = "GET READY TO COLOR THEM UP!";
        while (LoadingImg.fillAmount<=1)
        {
            yield return new WaitForSeconds(0);
            LoadingImg.fillAmount += speed * Time.deltaTime;
            if (LoadingImg.fillAmount>=1)
            {
                GameManager.Ins.OpenWindown(TypeWindown.Home);
                GameManager.Ins.CloseSingleWindown(TypeWindown.Loading);
            }
            


        }

       
    }

   
    

}
