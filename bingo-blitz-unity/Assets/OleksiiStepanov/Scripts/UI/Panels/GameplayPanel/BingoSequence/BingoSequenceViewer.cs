using System.Collections.Generic;
using BingoBlitzClone.Game;
using BingoBlitzClone.Utils;
using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    public class BingoSequenceViewer : MonoBehaviour
    {
        [SerializeField] private List<BingoBall> bingoBalls = new List<BingoBall>();
        [SerializeField] private List<Sprite> bingoBallsSprites = new List<Sprite>();
        [SerializeField] private Transform layout;

        public void Init()
        {
            foreach (var bingoBall in bingoBalls)
            {
                bingoBall.Init();
            }
        }
        
        public void OnEnable()
        {
            BingoSequence.OnNewBingoNumberCreated += OnNewBingoNumberCreated;
        }
        
        public void OnDisable()
        {
            BingoSequence.OnNewBingoNumberCreated -= OnNewBingoNumberCreated;
        }

        private void OnNewBingoNumberCreated(int number)
        {
            ListTools.MoveLastToFirst(bingoBalls);
            
            Sprite bingoBallSprite = GetBingoBallSprite(number);
            
            bingoBalls[0].Show(number, bingoBallSprite);
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
