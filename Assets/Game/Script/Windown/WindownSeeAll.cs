using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindownSeeAll : Windown
{
    public GameObject IconPerb;
    public List<GameObject> PoolIcon;
    public Transform parent;
    public DataCategori Data;
    public Text nameCategories;
    // Start is called before the first frame update
    

    public void Init()
    {
        int count = Data.categories[0].ListPainting.Count;
        for (int i = 0; i < 80; i++)
        {
            var icon = Instantiate(IconPerb, parent);
            PoolIcon.Add(icon);
        }
    }

    // Update is called once per frame
   
    public void ShowAll(string nameCategories)
    {
        this.nameCategories.text = nameCategories;
        int count = 0;
        List<Image> ImgShow = new List<Image>();
        for(int i=0;i < Data.categories.Count;i++)
        {
            if(Data.categories[i].NameCategories == nameCategories)
            {
                ImgShow = Data.categories[i].ListPainting;
                count = ImgShow.Count;
                break;
            }
        }
          
        for (int i = 0; i < PoolIcon.Count; i++)
        {
            if (i < count)
            {
                PoolIcon[i].transform.GetChild(0).GetComponent<Image>().sprite = ImgShow[i].sprite;
                PoolIcon[i].gameObject.SetActive(true);
            }
            else
            {
                PoolIcon[i].gameObject.SetActive(false);
            }

        }
    }
}
