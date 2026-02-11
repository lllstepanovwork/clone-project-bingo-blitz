using System;
using System.Collections.Generic;
using System.Threading;
using BingoBlitzClone.Gameplay;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BingoBlitz.UI
{
    public class CombinationHint : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] private Color startColor;
        [SerializeField] private Color redColor;
        [SerializeField] private List<Image> elements;

        private CombinationInfo _combinationInfo;

        private CancellationTokenSource _cancellationTokenSource;

        [Inject]
        public void Construct(CombinationInfo combinationInfo)
        {
            _combinationInfo = combinationInfo;
        }

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            HintLoopAsync(_cancellationTokenSource.Token).Forget();
        }

        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            
            ResetColor();
        }

        private async UniTaskVoid HintLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (var combination in _combinationInfo.Combinations)
                {
                    if (token.IsCancellationRequested) return;

                    ApplyCombination(combination);
                    await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: token);

                    ResetColor();
                    await UniTask.Yield(token);
                }
            }
        }

        private void ApplyCombination(Combination combination)
        {
            for (int y = 0; y < Combination.Size; y++)
            {
                for (int x = 0; x < Combination.Size; x++)
                {
                    int index = y * Combination.Size + x;
                    if (combination.Get(x, y))
                        elements[index].color = redColor;
                }
            }
        }

        private void ResetColor()
        {
            foreach (var element in elements)
                element.color = startColor;
        }
    }
}