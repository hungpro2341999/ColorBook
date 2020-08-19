using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTab : MonoBehaviour
{
    public TabCtrl tabCtrl;
    public int tabCurr = -1;
    public Transform[] trans;
    public Transform[] trans1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tabCurr!=(tabCtrl.index-1))
        {
            tabCurr = tabCtrl.index-1;
            ShowSelect(tabCurr);

        }
    }

    public void ShowSelect(int index)
    {
        for(int i=0;i<trans.Length;i++)
        {
            if(i==index)
            {
                trans[i].gameObject.SetActive(true);
                trans1[i].gameObject.SetActive(false);
            }
            else
            {
                trans[i].gameObject.SetActive(false);
                trans1[i].gameObject.SetActive(true);
            }
        }
    }
}
