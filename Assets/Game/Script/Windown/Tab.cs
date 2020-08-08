using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
   
    public int index;
    public System.Action OnCompleteChangeTab;
    public bool DoneMove = true;
    public Vector3 TargetPos;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public virtual void TriggerTab()
    {

    }
    


}
