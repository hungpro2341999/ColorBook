/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using PaintCraft.Tools;
using UnityEditor;
using System.Reflection;


namespace PaintCraft.Editor{
    [CustomPropertyDrawer(typeof(PointColor))]
    public class PointColorPropertyDrawer : PropertyDrawer {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            FieldInfo fieldInfo = property.serializedObject.targetObject.GetType().GetField(property.name, 
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PointColor c = (PointColor)fieldInfo.GetValue(property.serializedObject.targetObject);
            Color newColor = EditorGUI.ColorField(position, label, c.Color);
            if (newColor != c.Color){
                c.Color = newColor;
                property.serializedObject.UpdateIfRequiredOrScript();
                property.serializedObject.ApplyModifiedProperties();
                PrefabUtility.RecordPrefabInstancePropertyModifications(property.serializedObject.targetObject);
            }

        }
    }
}
