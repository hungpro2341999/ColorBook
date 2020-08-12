using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Windown> Windowns = new List<Windown>();
   
    public static GameManager Ins;
    public bool isGameOver = false;
    public bool isGamePause = false;

    public Transform UI_General;
    public Transform UI_GamePlay;
    public CtrlPainting Ctrl;

    public bool Loading = false;
    public Transform TrsLoading;
   
   
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
     
        //  PlayerPrefs.DeleteAll();
        Ctrl.Initialize();
        GameManager.Ins.OpenWindown(TypeWindown.Home);
    }
    private void Start()
    {
      
    }
    private void Update()
    {


            Application.targetFrameRate = 120;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerPrefs.DeleteAll();
            }
        
    }
    public void OpenWindown(Windown win)
    {
        OpenWindown(win.type);
    }
    public void OpenWindown(TypeWindown type)
    {
        foreach(Windown win in Windowns)
        {
       
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

     public void OpenSellAll(string nameCategories)
    {

    }
  public WindownHome GetHome()
    {
        return (WindownHome)GetWindown(TypeWindown.Home);
    }
    public IEnumerator StartLoading(System.Action ActionEnd,System.Action ActionLoading)
    {
        TrsLoading.gameObject.SetActive(true);
        ActionLoading();
       
        Loading = true;
       

         yield return new WaitForSeconds(2);
        TrsLoading.gameObject.SetActive(false);

        if (ActionEnd != null)
        {
            ActionEnd();
        }



    }
    public void StartLoadingData(System.Action Action)
    {
        
      
    }



    public void StartSyncAciton(System.Action ActionEnd, System.Action ActionLoading)
    {

    }

    
   
}
