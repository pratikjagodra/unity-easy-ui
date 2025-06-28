using UnityEngine;

namespace PJ.Easy.UI
{
    public abstract class UICanvas : UIBase
    {
        [SerializeField] protected Canvas canvas;

        public override void OnShowScreen()
        {
            canvas.enabled = true;
        }

        public override void OnHideScreen()
        {
            canvas.enabled = false;
        }
    }
}
