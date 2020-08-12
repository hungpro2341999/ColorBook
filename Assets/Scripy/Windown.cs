using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TypeWindown {SellAll,Home,
    Painting,Completed,Loading
}
public class Windown : MonoBehaviour

   
{

    public float timeToClose;
    public Animator Animtion;
   
    public TypeWindown type;
    public void Open()
    {
        
        Event_Open();
        gameObject.SetActive(true);
        if (Animtion == null)
        {
           
        }
        else
        {
           
            Animtion.SetBool("Open", true);
        }
       
    }

    public void Close()
    {
        if (gameObject.activeSelf)
        {
            Event_Close();
            if (Animtion == null)
            {
                StartCoroutine(IEClose());
            }
            else
            {
                Animtion.SetBool("Open", false);
            }
        }
       

    }

    public void UnActive()
    {
        gameObject.SetActive(false);
    }
    IEnumerator IEClose()
    {
        yield return new WaitForSeconds(timeToClose);
        gameObject.SetActive(false);
    }
    public virtual void Event_Open()
    {

    }
    public virtual void Event_Close()
    {

    }

}
