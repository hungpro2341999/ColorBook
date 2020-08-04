/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;


namespace PaintCraft.Utils {

	public class MathUtil {

		public static int IncrementIntLoop(int valueToIncrement, int minValueInclusive, int maxValueExclusive){
			int result = valueToIncrement+ 1;
			if (result >= maxValueExclusive){
				result = minValueInclusive;
			}
			return result;
		}

		public static int DecrementIntLoop(int valueToDecrement, int minValueInclusive, int maxValueExclusive){
			int result = valueToDecrement- 1;
			if (result < minValueInclusive){
				result = maxValueExclusive - 1;
			}
			return result;
		}


		/// <summary>
		/// Loops the value.
		/// if value > max reduce it until it less or equal max
		/// the same for min
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="value">Value.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		public static float LoopValue(float value, float min, float max){
			float diff = max - min;
			if (diff < 0){
				Debug.LogError("max must be greater than min");
			}

			if (value > max){
				int d = (int)( (value - max) / diff);
				value -= (d+ 1) * diff;
			} else if ( value < min){
				int d = (int)((value - max) / diff);
				value -= (d  - 1) * diff;
			}
			return value;
		}

	}


}
