using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSelect : MonoBehaviour
{
   
    public bool DoneMove;
    public TabCtrl TabCtr;
    public Vector3 Target;
    public int indexCurr = 2;
    public bool TriggerStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.name);
        if(indexCurr != TabCtr.index)
        {
            if (!TriggerStart)
            {
                TriggerStart = true;
                SwichTab();
            }

         

                if (!DoneMove)
                {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, Target, 500);

                    if (transform.localPosition.x == Target.x)
                    {
                        TriggerStart = false;
                        DoneMove = true;
                    }
                    indexCurr = TabCtr.index;

                }
           



        }
       
      
    }

    public void SwichTab()
    {
        int i = TabCtr.index;
        switch(i)
        {
            case 1:
                Target = transform.localPosition;
                Target.x = -150;
               
                break;
            case 2:
                Target = transform.localPosition;
                Target.x = 0;
                break;
            case 3:
                Target = transform.localPosition;
                Target.x = 150;
                break;
        }
        SetMove();
    }

    public void SetMove()
    {
        DoneMove = false;
    }
}
