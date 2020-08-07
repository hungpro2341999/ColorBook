using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonChangePainting : MonoBehaviour
{
    private Color color;
    private void Start()
    {
        color = GetComponent<Image>().color;
        GetComponent<Button>().onClick.AddListener(ChangeColor);
    }

    public void ChangeColor()
    {
        CtrlPainting.Ins.ChangeColor(color);
    }
}
