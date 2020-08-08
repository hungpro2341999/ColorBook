using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayCtrl : MonoBehaviour
{

    public static GamePlayCtrl Ins;
    
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
    }
}
