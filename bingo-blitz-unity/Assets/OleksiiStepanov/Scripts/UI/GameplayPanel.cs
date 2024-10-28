using System.Collections.Generic;
using UnityEngine;

namespace OleksiiStepanov.UI
{
    public class GameplayPanel : UIPanel
    {
        [Header("Content")] 
        [SerializeField] private List<GameplayPanelLayout> layouts;

        public void Init(int numberOfFields)
        {
            HideAllLayouts();
            
            layouts[numberOfFields-1].gameObject.SetActive(true);
            layouts[numberOfFields-1].Init();
        }

        private void HideAllLayouts()
        {
            foreach (var layout in layouts)
            {
                layout.gameObject.SetActive(false);
            }
        }
    }    
}

