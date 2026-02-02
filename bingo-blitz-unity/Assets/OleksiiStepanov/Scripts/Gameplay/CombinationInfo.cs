using System;
using System.Collections.Generic;
using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    [Serializable]
    public class CombinationInfo
    {
        [Header("Content")] 
        public List<Combination> Combinations;

        public bool IsMatch(Combination checkCombination)
        {
            foreach (var combination in Combinations)
            {
                if (combination.IsMatch(checkCombination))
                    return true;
            }

            return false;
        }
    }
}