﻿using PaintCraft.Canvas.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCategori : MonoBehaviour
{
    public int countCategories;
    [SerializeField]
    public List<Categories> categories;
    public void Init()
    {
        for (int i = 0; i < countCategories; i++)
        {

        }
    }



    [System.Serializable]
    public class Categories
    {
        public string NameCategories;
        public List<Image> ListPainting = new List<Image>();
    }
}
