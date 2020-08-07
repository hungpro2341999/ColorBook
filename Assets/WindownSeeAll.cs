using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindownSeeAll : MonoBehaviour
{
    public GameObject IconPerb;
    public List<GameObject> PoolIcon;
    public Transform parent;
    public DataCategori Data;
    // Start is called before the first frame update
    void Start()
    {
        int count = Data.categories[0].ListPainting.Count;
      for (int i=0;i<50;i++)
        {
            var icon =  Instantiate(IconPerb, parent);
            PoolIcon.Add(icon);
        }

      for(int i=0;i<PoolIcon.Count;i++)
        {
            if (i < count)
            {
                PoolIcon[i].transform.GetChild(0).GetComponent<Image>().sprite = Data.categories[0].ListPainting[i].sprite;
                var a = Data.categories[0].ListPainting[i];
                PoolIcon[i].gameObject.SetActive(true);
            }
            else
            {
                PoolIcon[i].gameObject.SetActive(false);
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
