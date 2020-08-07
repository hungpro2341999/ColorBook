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

        if (GUILayout.Button("LoadAnimal", buttonStyle))
        {

            List<Sprite> Images = new List<Sprite>();
            var Paint = GameHelper.GetAllAssetAtPath<Sprite>(null, "Assets/Resources/Animal");


            for (int i = 0; i < Paint.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Images.Add(Paint[i]);
                }
            }

            
              

                var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/Animal");
              
                
                for(int i=0;i<Asset.Count;i++)
            {
                Asset[i].UniqueId = "Style_" + i;
               
                Asset[i].outlineTexture = Images[i].texture;
                EditorUtility.SetDirty(Asset[i]);

            }


            EditorUtility.SetDirty(target);

        }
        if (GUILayout.Button("AddAnimToGame", buttonStyle))
        {
            var ctrl = GameObject.Find("LoadData").GetComponent<DataCategori>();
            ctrl.categories[0].NameCategories = "Categories_1";
          
            var Asset = GameHelper.GetAllAssetAtPath<ColoringPageConfig>(null, "Assets/Resources/ColorPainting/Animal");
            var Obj = GameObject.Find("GameMananger").GetComponent<DataMananger>().CategoriesPerb;
            var parent = GameObject.Find("Content_01").transform;
            for(int i=0;i<Asset.Count;i++)
            {
               var a = Instantiate(Obj, parent);
            
                a.transform.GetChild(0).GetComponent<ShowImageIcon>().Page = Asset[i];
                ctrl.categories[0].ListPainting.Add(a.transform.GetChild(0).GetComponent<Image>());
            }


          
        }

        GUILayout.EndHorizontal();

    }
   
}
