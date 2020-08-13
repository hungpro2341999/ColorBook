using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using PaintCraft.Canvas.Configs;


[CustomEditor(typeof(LoadData))]

public class LoadStyleImage : Editor
{



    public override void OnInspectorGUI()
    {
        int index = 0;
        var Data = (LoadData)target;
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        GUIStyle buttonStyle2 = new GUIStyle(GUI.skin.button);

       
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.fixedWidth = 150.0f;
        buttonStyle.fixedHeight = 35.0f;
        buttonStyle.fontSize = 15;
        buttonStyle2.fixedWidth = 200.0f;
        buttonStyle2.fixedHeight = 25.0f;


        base.OnInspectorGUI();

        
        GUILayout.Label("Load_Style_1");
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();

        GUILayout.Space(5);
      
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("LoalPool", buttonStyle))
        {
            GameObject.Find("SellAllTab").GetComponent<WindownSeeAll>().Init();
        }
         if (GUILayout.Button("LoadBasic", buttonStyle))
        {

            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Basic");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

                var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/BasicAsset");
              
                
                for(int i=0;i<Asset.Count;i++)
            {
                Asset[i].UniqueId = Images[i].name;
               
                Asset[i].outlineTexture = Images[i].texture;
                EditorUtility.SetDirty(Asset[i]);

            }
            EditorUtility.SetDirty(target);

        }
        if (GUILayout.Button("AddAnimToGame", buttonStyle))
        {
            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Basic");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

            var ctrl = GameObject.Find("LoadData").GetComponent<DataCategori>();
            ctrl.categories[0].NameCategories = "Basic";
          
            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/BasicAsset");
            var Obj = GameObject.Find("GameMananger").GetComponent<DataMananger>().CategoriesPerb;
            var parent = GameObject.Find("Basic").transform;
            for(int i=0;i<Asset.Count;i++)
            {
               var a = Instantiate(Obj, parent);

                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().nameCategories = "Basic";
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().Page = Asset[i];
                ctrl.categories[0].ListPainting.Add(a.transform.GetChild(0).GetComponent<Image>());
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Images[i];
            }

        }
        index++;

        GUILayout.EndHorizontal();

        GUILayout.Label("Load_Cartoon");
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("LoadCartoon", buttonStyle))
        {

            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Cartoon");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/CartoonAsset");


            for (int i = 0; i < Asset.Count; i++)
            {
                Asset[i].UniqueId = Images[i].name;

                Asset[i].outlineTexture = Images[i].texture;
                EditorUtility.SetDirty(Asset[i]);

            }
            EditorUtility.SetDirty(target);

        }

      
        if (GUILayout.Button("AddCartoonToGame", buttonStyle))
        {
            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Cartoon");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }


            var ctrl = GameObject.Find("LoadData").GetComponent<DataCategori>();
            ctrl.categories[1].NameCategories = "Cartoon";

            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/CartoonAsset");
            var Obj = GameObject.Find("GameMananger").GetComponent<DataMananger>().CategoriesPerb;
            var parent = GameObject.Find("Cartoon").transform;
          
            for (int i = 0; i < Asset.Count; i++)
            {
                var a = Instantiate(Obj, parent);


                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().Page = Asset[i];
                ctrl.categories[1].ListPainting.Add(a.transform.GetChild(0).GetComponent<Image>());
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().LoadIcon();
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().GetComponent<Image>().sprite = Images[i];
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().nameCategories = "Cartoon";
            }



        }
        index++;

        GUILayout.EndHorizontal();

        GUILayout.Label("Load_Cartoon");
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();


        if (GUILayout.Button("LoadCat", buttonStyle))
        {

            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Cat");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/CatAsset");

            
            for (int i = 0; i < Asset.Count; i++)
            {
                Asset[i].UniqueId = Images[i].name;

                Asset[i].outlineTexture = Images[i].texture;
                EditorUtility.SetDirty(Asset[i]);

            }
            EditorUtility.SetDirty(target);

        }
        if (GUILayout.Button("AddCatToGame", buttonStyle))
        {
            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Cat");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }
            var ctrl = GameObject.Find("LoadData").GetComponent<DataCategori>();
            ctrl.categories[2].NameCategories = "Cat";

            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/CatAsset");
            var Obj = GameObject.Find("GameMananger").GetComponent<DataMananger>().CategoriesPerb;
            var parent = GameObject.Find("Cat").transform;
            ctrl.categories[2].ListPainting = new List<Image>();
            for (int i = 0; i < Asset.Count; i++)
            {
                var a = Instantiate(Obj, parent);

                a.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().Page = Asset[i];
                ctrl.categories[2].ListPainting.Add(a.transform.GetChild(0).GetComponent<Image>());

                a.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().GetComponent<Image>().sprite = Images[i];
                a.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().nameCategories = "Cat";
            }



        }
        index++;

        GUILayout.EndHorizontal();


        GUILayout.Label("Editor's choice");
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();


        if (GUILayout.Button("LoadEditor's choice", buttonStyle))
        {

            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Editor's choice");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

           

            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/Editor's choiceAsset");


            for (int i = 0; i < Asset.Count; i++)
            {
                Asset[i].UniqueId = Images[i].name;

                Asset[i].outlineTexture = Images[i].texture;
                EditorUtility.SetDirty(Asset[i]);

            }
            EditorUtility.SetDirty(target);

        }
        if (GUILayout.Button("AddEditor's choiceToGame", buttonStyle))
        {
            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Editor's choice");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

            var ctrl = GameObject.Find("LoadData").GetComponent<DataCategori>();
            ctrl.categories[3].NameCategories = "Editor's choice";

            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/Editor's choiceAsset");
            var Obj = GameObject.Find("GameMananger").GetComponent<DataMananger>().CategoriesPerb;
            var parent = GameObject.Find("Editor's choice").transform;
            ctrl.categories[3].ListPainting = new List<Image>();
            for (int i = 0; i < Asset.Count; i++)
            {
                var a = Instantiate(Obj, parent);

                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().Page = Asset[i];
                ctrl.categories[3].ListPainting.Add(a.transform.GetChild(0).GetComponent<Image>());
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Images[i];
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().nameCategories = "Editor's choice";

            }



        }
        index++;

        GUILayout.EndHorizontal();


        GUILayout.Label("Human");
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();


        if (GUILayout.Button("AddHuman", buttonStyle))
        {
            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Human");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

          

            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/HumanAsset");


            for (int i = 0; i < Asset.Count; i++)
            {
                Asset[i].UniqueId = Images[i].name;

                Asset[i].outlineTexture = Images[i].texture;
                EditorUtility.SetDirty(Asset[i]);

            }
            EditorUtility.SetDirty(target);

        }
        if (GUILayout.Button("AddHumanToGame", buttonStyle))
        {
            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Human");

            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

            var ctrl = GameObject.Find("LoadData").GetComponent<DataCategori>();
            ctrl.categories[4].NameCategories = "Human";

            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/HumanAsset");
            var Obj = GameObject.Find("GameMananger").GetComponent<DataMananger>().CategoriesPerb;
            var parent = GameObject.Find("Human").transform;
            ctrl.categories[4].ListPainting = new List<Image>();
            for (int i = 0; i < Asset.Count; i++)
            {
                var a = Instantiate(Obj, parent);

                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().Page = Asset[i];
                ctrl.categories[4].ListPainting.Add(a.transform.GetChild(0).GetComponent<Image>());
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Images[i];
                a.transform.GetChild(0).transform.GetChild(0).GetComponent<ShowImageIcon>().nameCategories = "Human";
            }



        }
        index++;

        GUILayout.EndHorizontal();

    }
    public bool isExitsImg(Sprite sprite,Sprite sprite0)

    {
        return sprite.name == sprite0.name;
    }

    
   
   
}
