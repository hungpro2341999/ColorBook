using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabCtrl : MonoBehaviour
{

    public List<Tab> Tabs = new List<Tab>();
    public float offsetChange;

    public int index;
    public System.Action OnCompleteChangeTab;
    public bool DoneMove = true;
    public Vector3 TargetPos;
    public void Init()
    {
        for (int i = 0; i < Tabs.Count; i++)
        {
            Tabs[i].TriggerTab();

        }

    }

    public void AciveAll()
    {
        for(int i=0;i<Tabs.Count;i++)
        {
            Tabs[i].gameObject.SetActive(true);
        }
    }

    public void Active(int index)
    {
        for (int i = 0; i < Tabs.Count; i++)
        {
            if (Tabs[i].index == index)
            {
                Tabs[i].gameObject.SetActive(true);
            }
            else
            {
                Tabs[i].gameObject.SetActive(false);
            }
          
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float offset = -offsetChange;
        for(int i=0;i<Tabs.Count;i++)
        {
            Vector3 pos = Tabs[i].transform.localPosition;
            pos.x = -720 + (offsetChange * i);
       //     Debug.Log(offsetChange * i);
            Tabs[i].transform.localPosition = pos;
        }
        SwitchTab(index);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
    }

    public void SwitchTab(int tab)
    {
        index = tab;
        AciveAll();
        switch (tab)
        {
            case 1:
                Vector3 pos = transform.localPosition;
                pos.x = 720;
              
                SetUpMove(pos);
                break;
            case 2:
                Vector3 pos1 = transform.localPosition;
                pos1.x = 0;
               
                SetUpMove(pos1);
                break;
            case 3:
                Vector3 pos2 = transform.localPosition;
                pos2.x = -720;
               
                SetUpMove(pos2);
                break;
        }
    }

    public void SetUpMove(Vector3 pos)
    {
        TargetPos = pos;
        DoneMove = false;
    }
    public void MoveTo()
    {
        if (!DoneMove)
        {

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPos, Time.deltaTime * 5500);
            if (transform.localPosition.x == TargetPos.x)
            {
                DoneMove = false;
                Active(index-1);
            }
        }


    }

  

    public void ReflectTab()
    {
        for (int i = 0; i < Tabs.Count; i++)
        {
            Tabs[i].Reflect();
        }
    }

    
}
