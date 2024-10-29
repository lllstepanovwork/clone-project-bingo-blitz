using UnityEngine;
using System.Collections.Generic;
using OleksiiStepanov.Gameplay;

namespace OleksiiStepanov.UI
{
    public class GameplayPanelLayout : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] private BingoSequence bingoSequence;
        [SerializeField] private List<BingoField> bingoFields;

        private int bingoFieldDoneCounter = 0;
        
        public void Init()
        {
            bingoFieldDoneCounter = 0;
            
            foreach (var bingoField in bingoFields)
            {
                bingoField.Init();
            }
            
            bingoSequence.Init();
        }
        
        public void StartGame() 
        {
            bingoSequence.StartBingoSequence();
        }

        public void PlayShakeAnimation()
        {
            foreach (var bingoField in bingoFields)
            {
                bingoField.PlayShakeAnimation();
            }
        }

        private void OnEnable()
        {
            BingoField.OnBingoFieldCompleted += BingoFieldOnOnBingoFieldCompleted;
        }
        
        private void OnDisable()
        {
            BingoField.OnBingoFieldCompleted -= BingoFieldOnOnBingoFieldCompleted;
        }
        
        private void BingoFieldOnOnBingoFieldCompleted()
        {
            bingoFieldDoneCounter++;

            if (bingoFieldDoneCounter == bingoFields.Count)
            {
                bingoSequence.Stop();
                Debug.Log("GAME OVER!");
            }
        }
    }
}
