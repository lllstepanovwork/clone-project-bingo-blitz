using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    public abstract class BingoFieldElementState : MonoBehaviour
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void ResetState();

        public virtual void SetNumber(int number) {}
    }
}