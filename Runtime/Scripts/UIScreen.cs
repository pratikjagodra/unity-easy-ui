using UnityEngine;

namespace PJ.Easy.UI
{
    public abstract class UIScreen : UIBase
    {
        [SerializeField] protected GameObject root;

        public override void OnShowScreen()
        {
            root.SetActive(true);
        }

        public override void OnHideScreen()
        {
            root.SetActive(false);
        }
    }
}