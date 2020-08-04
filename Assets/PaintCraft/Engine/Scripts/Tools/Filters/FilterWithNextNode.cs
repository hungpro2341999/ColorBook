/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools{
    public abstract class FilterWithNextNode : IFilter {        
        [OneWay]
        public IFilter NextFilter;        
        
        #region implemented abstract members of FilterWithNextNode
        public override void Apply(BrushContext brushLineContext)
        {
            if (brushLineContext.Points.Count == 0 && brushLineContext.BasePoints.Count == 0){
                UnityEngine.Debug.LogWarning("No points in brush context");
            }

            bool runNextFilter = FilterBody(brushLineContext);

            if (runNextFilter){                
                RunNextFilter(brushLineContext);
            }
               
            FilterFinalizer(brushLineContext);
        }
        #endregion


        /// <summary>
        /// Filters the body.
        /// </summary>
        /// <returns><c>true</c>, if you want to run nextFilter, <c>false</c> otherwise.</returns>
        /// <param name="brushLineContext">Brush line context.</param>
        public abstract bool FilterBody(BrushContext brushLineContext);


        /// <summary>
        /// Filters the finalizer. 
        /// We run this finalizer after nextFilter execution done
        /// </summary>
        /// <param name="brushLineContext">Brush line context.</param>
        protected virtual void FilterFinalizer(BrushContext brushLineContext){
            
        }


        public void RunNextFilter(BrushContext brushContext){
            if (NextFilter != null){
                NextFilter.Apply(brushContext);
            }
        }
    }       
}
