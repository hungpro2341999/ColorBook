using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TypeWindown {SellAll,Home,
    Painting,Completed,Loading
}
public class Windown : MonoBehaviour

   
{
    public bool isStartClose = false;
    public float timeToClose;
    public Animator Animtion;
   
    public TypeWindown type;
    public void Open()
    {
        isStartClose = false;
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
                StartClose(0.8f);
            }
            else
            {
                Animtion.SetBool("Open", false);
            }
        }
       

    }

    private void LateUpdate()
    {
       if(isStartClose)
        if(timeToClose<0)
        {
            gameObject.SetActive(false);
                isStartClose = false;
        }
        else
        {
            timeToClose -= Time.deltaTime;
        }
    }
    public void UnActive()
    {
        gameObject.SetActive(false);
    }
    void StartClose(float time)
    {
        isStartClose = true;
        timeToClose = time;
        gameObject.SetActive(false);
    }
    public virtual void Event_Open()
    {

    }
    public virtual void Event_Close()
    {

    }

}
