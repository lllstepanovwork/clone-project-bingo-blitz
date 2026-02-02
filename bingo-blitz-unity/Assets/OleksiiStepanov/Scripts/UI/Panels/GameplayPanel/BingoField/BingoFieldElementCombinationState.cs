using DG.Tweening;
using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldElementCombinationState : BingoFieldElementState
    {
        public override void Enter()
        {
            gameObject.SetActive(true);
            
            transform.DOShakeScale(0.5f, 0.2f);
        }

        public override void Exit()
        {
            gameObject.SetActive(false);
        }
    }
}