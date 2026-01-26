using BingoBlitzClone.Game;
using BingoBlitzClone.UI;
using UnityEngine;
using Zenject;

namespace BingoBlitzClone.Loading
{
    public class LoadingStepUIInit : LoadingStepBase
    {
        private UIManager _uiManager;

        [Inject]
        public void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public override void Enter()
        {
            _uiManager.Init(Exit);
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.UIInit;
        }
    }
}
