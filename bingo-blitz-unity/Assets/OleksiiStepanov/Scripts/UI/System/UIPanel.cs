using UnityEngine;
using UnityEngine.Serialization;

namespace BingoBlitzClone.UI
{
    public enum UIViewAnimationType
    {
        Fade,
        FromBottom,
        None
    }

    public abstract class UIPanel : MonoBehaviour
    {
        [Header("UI Panel Content")]
        public CanvasGroup canvasGroup;
        public Transform contentTransform;
        public UIViewAnimationType animationType;

        public virtual void OnUIPanelOpened()
        {
        }
    }
}