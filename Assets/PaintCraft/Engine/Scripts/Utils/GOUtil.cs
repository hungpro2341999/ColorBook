/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace PaintCraft.Utils{
	public class GOUtil {
        
		public static T AddComponentIfNoExists<T>(GameObject parentObject) where T:Component{
			T result = parentObject.GetComponent<T>();
			if (result == null){
				result = parentObject.AddComponent<T>();
			}
			return result;
		}

		public static T CreateGameObject<T>(string name, GameObject parent, float zOffset) where T:Component{
			return CreateGameObject<T>(name, parent, new Vector3(0.0f,0.0f,zOffset));
        }

		public static T CreateGameObject<T>(string name, GameObject parent) where T:Component{
			return CreateGameObject<T>(name, parent, Vector3.zero);
		}

		public static T CreateGameObject<T>(string name, GameObject parent, Vector3 localPosition) where T:Component{
			GameObject go = new GameObject(name);
			go.layer = parent.layer;
			go.transform.parent = parent.transform;
			ResetLocalPosition(go);
			go.transform.localPosition = localPosition;
			return go.AddComponent<T>();
		}

		public static void ResetLocalPosition(GameObject go){
			ResetLocalPosition(go.transform);
		}

		public static void ResetLocalPosition(Transform t){
			t.localPosition = Vector3.zero;
			t.localScale = Vector3.one;
			t.localRotation = Quaternion.identity;
		}

		public static T CreateComponentIfNoExists<T>(GameObject go) where T:Component{
			T result = go.GetComponent<T>();
			if (result == null){
				result = go.AddComponent<T>();
			}
			return result;
		}

	}
}
