using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Windown> Windowns = new List<Windown>();
   
    public static GameManager Ins;
    public bool isGameOver = false;
    public bool isGamePause = false;
    public bool isLoading = false;
    public bool isConnectInternet = false;
    public Transform UI_General;
    public Transform UI_GamePlay;
    public CtrlPainting Ctrl;

    public bool Loading = false;
    public Transform TrsLoading;
    public Transform TrsLoading01;
    public Transform TrsConnectInternet;
    public static Vector3 directMouse;
    private Vector3 posInit;
    public bool press;
    public bool check;
    public  bool isHorizontal; 

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
        Windowns[4].gameObject.SetActive(true);

        GameManager.Ins.OpenWindown(TypeWindown.Loading);
    }
    private void Start()
    {
      
    }
    private void Update()
    {


            Application.targetFrameRate = 80;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerPrefs.DeleteAll();
            }
        if (Input.GetMouseButtonDown(0))
        {
            press = true;
            posInit = Input.mousePosition;
            check = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            press = false;
            check = false;

        }
        if (press)
        {
            directMouse = Vector3.Normalize(Input.mousePosition - posInit);
            float angle = Vector3.Angle(directMouse, new Vector3(0, 1, 0));

            if (angle != 0)
            {
                if (!check)
                {
                    check = true;
                    if ((0 <= angle && angle <= 45) || (135 <= angle && angle <= 180))
                    {
                        isHorizontal = false;
                    }
                    else
                    {
                        isHorizontal = true;
                    }
                }
              
            }
           
        }
      

        bool connect = (Application.internetReachability == NetworkReachability.NotReachable);
        if(connect)
        {
            TrsConnectInternet.gameObject.SetActive(true);
        }
        else
        {
            TrsConnectInternet.gameObject.SetActive(false);
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
       
        isLoading = true;
        ActionLoading();
       
        Loading = true;
       

         yield return new WaitForSeconds(0.5f);

        isLoading = false;
        if (ActionEnd != null)
        {
            ActionEnd();
        }

        Invoke("CloseLoading", 1);   


    }
    public IEnumerator StartLoading01(System.Action ActionEnd, System.Action ActionLoading)
    {

        isLoading = true;
        Loading = true;
        Debug.Log("Loading");
        ActionLoading();

      


   
      
        yield return new WaitForSeconds(1);
        if (ActionEnd != null)
        {
            ActionEnd();
        }
        isLoading = false;
      

        TrsLoading01.gameObject.SetActive(false);
       

     


    }


    public void StartLoadingData(System.Action Action)
    {
        
      
    }



    public void StartSyncAciton(System.Action ActionEnd, System.Action ActionLoading)
    {

    }

    public void OpenLoading()
    {
        TrsLoading.GetComponent<Animator>().SetBool("Open", true);
    }
    public void CloseLoading()
    {

        TrsLoading.GetComponent<Animator>().SetBool("Open", false);

    }
    
   
}
