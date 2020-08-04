/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;


namespace PaintCraft.UI.Sliders{
	[RequireComponent(typeof(Slider))]
	public abstract class SliderValueBinder : MonoBehaviour {
		public abstract float Value{ get; set;}

		Slider slider;
		void Start(){
			slider = GetComponent<Slider>();
			slider.onValueChanged.AddListener((value)=>{
				OnValueChanged(value);
			});
		}
		
		
		void OnValueChanged(float newValue){
			Value = newValue;
		}
		
		
		void LateUpdate(){
			if (Value != slider.value){
				slider.value = Value;
			}
		}
	}
}
