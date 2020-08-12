using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMyArt : TabCtrl
{
    


    public TabInProcess GetTabInProcess()
    {
        return (TabInProcess)Tabs[0];
    }

    public TabCompleted GetTabCompleted()
    {
        return (TabCompleted)Tabs[1];
    }


    public TabShared GetTabShared()
    {
        return (TabShared)Tabs[2];
    }

}
