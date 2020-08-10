using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMyArt : TabCtrl
{
    


    public TabInProcess GetTabInProcess()
    {
        return (TabInProcess)Tabs[0];
    }

    public TabInProcess GetTabCompleted()
    {
        return (TabInProcess)Tabs[1];
    }




   
}
