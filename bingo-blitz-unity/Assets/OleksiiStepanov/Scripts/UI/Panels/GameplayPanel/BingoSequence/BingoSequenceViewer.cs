using System.Collections.Generic;
using BingoBlitzClone.Game;
using BingoBlitzClone.Utils;
using UnityEngine;
using Zenject;

namespace BingoBlitzClone.Gameplay
{
    public class BingoSequenceViewer : MonoBehaviour
    {
        [SerializeField] private List<BingoBall> bingoBalls = new List<BingoBall>();
        [SerializeField] private List<Sprite> bingoBallsSprites = new List<Sprite>();
        [SerializeField] private Transform layout;

        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Init()
        {
            foreach (var bingoBall in bingoBalls)
            {
                bingoBall.Init();
            }
        }
        
        public void OnEnable()
        {
            _signalBus.Subscribe<BingoSequence.NewNumberSignal>(OnNewBingoNumberCreated);
        }
        
        public void OnDisable()
        {
            _signalBus.Unsubscribe<BingoSequence.NewNumberSignal>(OnNewBingoNumberCreated);
        }

        private void OnNewBingoNumberCreated(BingoSequence.NewNumberSignal signal)
        {
            ListTools.MoveLastToFirst(bingoBalls);
            
            Sprite bingoBallSprite = GetBingoBallSprite(signal.Number);
            
            bingoBalls[0].Show(signal.Number, bingoBallSprite);
            bingoBalls[0].transform.SetAsFirstSibling();
            
            UpdateBingoBallsVisuals();
        }

        private Sprite GetBingoBallSprite(int number)
        {
            var index = (number - 1) / 15;
            return bingoBallsSprites[index];
        }

        private void UpdateBingoBallsVisuals()
        {
            bingoBalls[1].ScaleDown();
            bingoBalls[^3].Hide(BingoSequenceTransparencyType.Quarter);
            bingoBalls[^2].Hide(BingoSequenceTransparencyType.Half);
            bingoBalls[^1].Hide(BingoSequenceTransparencyType.Clear);
        }
    }   
}
