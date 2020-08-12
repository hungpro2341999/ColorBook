using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider LoadingBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyn());
    }

    // Update is called once per frame
   
   

    IEnumerator LoadAsyn()
    {
        AsyncOperation operetion = SceneManager.LoadSceneAsync("ColorPainting");
        while(!operetion.isDone)
        {
            float process = Mathf.Clamp01(operetion.progress / .9f);
            Debug.Log(process);
            LoadingBar.value = process;
            yield return null;
        }
    }
}
