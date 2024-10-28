using UnityEngine;
using UnityEngine.Serialization;

namespace OleksiiStepanov.UI
{
    public enum UIViewAnimationType
    {
        FromBottom,
        None
    }

    public abstract class UIPanel : MonoBehaviour
    {
        [Header("UI View Content")]
        public CanvasGroup canvasGroup;
        public Transform contentTransform;
        public UIViewAnimationType animationType;
    }
}