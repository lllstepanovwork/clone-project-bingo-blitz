using System;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldActiveState : BingoFieldState
    {
        public override void Enter(Action onComplete = null)
        {
            gameObject.SetActive(true);
        }

        public override void Exit(Action onComplete = null)
        {
            gameObject.SetActive(false);
        }

        public override void ResetState()
        {
            gameObject.SetActive(false);
        }
    }
}