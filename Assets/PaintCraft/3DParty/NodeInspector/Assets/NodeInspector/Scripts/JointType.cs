/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

namespace NodeInspector.Editor{
    public enum JointType {
        Nan,
        OneToOne_IN,
        OneToMany_IN,
        ManyToOne_IN,
        OneToOne_OUT,
        OneToMany_OUT,
        ManyToOne_Out,
        ManyToMany_IN,
        ManyToMany_OUT,
        OneWay_OUT,
        OneWay_IN //This works only for class attribute
    }
}
