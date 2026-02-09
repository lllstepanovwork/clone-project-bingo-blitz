using System;
using DG.Tweening;
using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    public abstract class BingoFieldState : MonoBehaviour
    {
        public abstract void Enter(Action onComplete = null);
        public abstract void Exit(Action onComplete = null);
        public abstract void ResetState();
    }
}