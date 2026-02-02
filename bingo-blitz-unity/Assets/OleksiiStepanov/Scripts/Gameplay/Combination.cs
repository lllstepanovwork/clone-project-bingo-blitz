using System;
using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    [Serializable]
    public class Combination
    {
        public const int Size = 5;

        [SerializeField]
        private bool[] cells = new bool[Size * Size];

        public bool Get(int x, int y)
        {
            return cells[y * Size + x];
        }

        public void Set(int x, int y, bool value)
        {
            cells[y * Size + x] = value;
        }
        
        public bool IsMatch(Combination other)
        {
            if (other == null) return false;

            for (int i = 0; i < Size * Size; i++)
            {
                if (cells[i] != other.cells[i])
                    return false;
            }
            
            return true;
        }
        
        public void Reset()
        {
            Array.Clear(cells, 0, cells.Length);
        }
    }
}