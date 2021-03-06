﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

public class TabCtrl : MonoBehaviour
{
    public ScrollRect _Scroll;
    public List<Tab> Tabs = new List<Tab>();
    public float offsetChange;
    public HorizontalScrollSnap ScrollSnap;
    public int index;
    public System.Action OnCompleteChangeTab;
    public bool DoneMove = true;
    public Vector3 TargetPos;
    public Transform Content;
    bool check;
    public bool InitStart;
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
        if (InitStart)
        {
            float offset = -offsetChange;
            for (int i = 0; i < Tabs.Count; i++)
            {
                Vector3 pos = Tabs[i].transform.localPosition;
                pos.x = -720 + (offsetChange * i);
                // Debug.Log(offsetChange * i);
                Tabs[i].transform.localPosition = pos;
            }
            SwitchTab(index);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_Scroll != null)
        {
            if (Mathf.Abs(_Scroll.velocity.x) < 0.01f && !GameManager.Ins.press)
            {
                if (check)
                {
                    Active(ScrollSnap._currentPage);
                    check = false;
                }
            }
            else
            {
                check = true;
            }
            MoveTo();
        }
        else
        {
            MoveTo();
        }
       
    }

    public void SwitchTab(int tab)
    {
        var scroll = GetComponent<HorizontalScrollSnap>();
        if (scroll != null)
        {
            scroll._currentPage = tab - 1;
        }
       
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

            Content.localPosition = Vector3.MoveTowards(Content.localPosition, TargetPos, Time.deltaTime * 5500);
            if (Content.localPosition.x == TargetPos.x)
            {
                DoneMove = true;
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
