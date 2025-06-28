using UnityEngine;

namespace PJ.Easy.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        [SerializeField] protected bool isStackable = true;

        public bool IsStackable => isStackable;

        public abstract void OnShowScreen();
        public abstract void OnHideScreen();
    }
}
