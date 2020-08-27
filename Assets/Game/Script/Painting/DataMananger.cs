using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMananger : MonoBehaviour
{
    public static DataMananger Ins;
    public GameObject CategoriesPerb;
    public GameObject AnimSelectPreb;
    private void Awake()
    {
        if(Ins!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
        }
    }
    public void AddEffSelect(Vector3 posTouch,Transform trans)
    {
        AnimSelectPreb.gameObject.SetActive(true);
        AnimSelectPreb.transform.parent = trans;
        AnimSelectPreb.transform.GetChild(1).transform.position = posTouch;
        AnimSelectPreb.transform.GetChild(1).GetChild(0).transform.localPosition = AnimSelectPreb.transform.GetChild(2).transform.position;
    }

}
