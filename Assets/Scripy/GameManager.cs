using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Windown> Windowns = new List<Windown>();
   
    public static GameManager Ins;
    public bool isGameOver = false;
    public bool isGamePause = false;
   

  
   
    private void Awake()
    {
       
        if (Ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
     //  PlayerPrefs.DeleteAll();
       
       
    }
    private void Start()
    {
        OpenWindown(TypeWindown.Select);
    }
    public void OpenWindown(Windown win)
    {
        OpenWindown(win.type);
    }
    public void OpenWindown(TypeWindown type)
    {
        foreach(Windown win in Windowns)
        {
            if (win == null)
                continue;
            if(win.type == type)
            {
                win.Open();
              
            }
            else
            {
                win.Close();
            }
        }
    }

   
    public void OpenSingleWindown(TypeWindown type)
    {
        foreach (Windown win in Windowns)
        {
            if (win.type == type)
            {
                win.Open();
                break;
            }

        }

    }
    public void OpenSingleWindown(Windown type)
    {
        OpenSingleWindown(type.type);

    }
    public void closeSingleWindown(Windown type)
    {
        CloseSingleWindown(type.type);
    }
    public void CloseSingleWindown(TypeWindown type)
    {
        foreach (Windown win in Windowns)
        {
            if (win.type == type)
            {
                win.Close();
                break;
            }

        }
    }
    
    public Windown GetWindown(TypeWindown type)
    {
        foreach (Windown win in Windowns)
        {
            if (win.type == type)
            {
                return win;
            }

        }
        return null;
    }

   

    public void PauseGame()
    {
        GameManager.Ins.isGamePause = true;
       
    }
  
   
}
