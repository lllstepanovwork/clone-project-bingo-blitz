using System;
using System.Collections.Generic;
using BingoBlitzClone.Utils;
using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldColumn : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] private List<BingoFieldElement> elements;

        private int _bingoFieldId;
        
        public void Init(int bingoFieldId, int minValue, int maxValue)
        {
            _bingoFieldId = bingoFieldId;
            
            CreateNumberList(minValue, maxValue);
        }
        
        private void CreateNumberList(int minValue, int maxValue)
        {
            var randomValueList = ListTools.GetRandomizedList(minValue, maxValue);

            var count = Math.Min(elements.Count, randomValueList.Count);
            
            for (int i = 0; i < count; i++)
            {
                elements[i].Init(_bingoFieldId, randomValueList[i]);
            }
        }

        public List<BingoFieldElement> GetElements()
        {
            return elements;
        }
    }
}