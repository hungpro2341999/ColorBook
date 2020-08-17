using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceColor : MonoBehaviour
{
    public GameObject Color;
    public string[] ColorString;
    public void Init()
    {
        for(int i=0;i<ColorString.Length;i++)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(ColorString[i], out color))
            {
                var a = Instantiate(Color, transform);
                a.transform.GetChild(0).GetComponent<Image>().color = color;
            }
        }

     
       
    }
}
