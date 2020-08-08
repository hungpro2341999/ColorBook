using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSelect : MonoBehaviour
{
    public Animator Anim;
    // Start is called before the first frame update
    public bool select = false;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartAnim);
    }

    public void StartAnim()
    {
        select = !select;
        Anim.SetBool("Open", select);
    }
}
