using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindownLoading :Windown
{
    public bool Loading = false;
    public override void Event_Open()
    {
        //Loading = true;
        //StartCoroutine(StartLoading());
        //StartCoroutine(StartShow());
    }

    public IEnumerator StartLoading()
    {
      
    
       
        yield return new WaitForSeconds(1);
     
      
        Loading = false;
    }

    public IEnumerator StartShow()
    {
        while (Loading)
        {
                yield return new WaitForSeconds(0);
           

        }
        GameManager.Ins.OpenWindown(TypeWindown.Home);
    }

}
