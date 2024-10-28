using System;
using System.Collections.Generic;

namespace OleksiiStepanov.Gameplay
{
    public class BingoCombinations
    {
        private readonly int _gridSize = 5;
        private readonly List<int[]> _bingoCombinations;
        
        public BingoCombinations(int gridSize)
        {
            _gridSize = gridSize;

            _bingoCombinations = GenerateCombinations();
        }

        public List<int[]> GetBingoCombinations()
        {
            return _bingoCombinations;
        }

        private List<int[]> GenerateCombinations()
        {
            List<int[]> combinations = new List<int[]>();

            // Rows
            for (int i = 0; i < _gridSize; i++)
            {
                int[] row = new int[_gridSize];
                for (int j = 0; j < _gridSize; j++)
                {
                    row[j] = i * _gridSize + j;
                }
                combinations.Add(row);
            }

            // Columns
            for (int j = 0; j < _gridSize; j++)
            {
                int[] column = new int[_gridSize];
                for (int i = 0; i < _gridSize; i++)
                {
                    column[i] = i * _gridSize + j;
                }
                combinations.Add(column);
            }

            // Left-to-right diagonal
            int[] leftToRightDiagonal = new int[_gridSize];
            for (int i = 0; i < _gridSize; i++)
            {
                leftToRightDiagonal[i] = i * _gridSize + i;
            }
            combinations.Add(leftToRightDiagonal);

            // Right-to-left diagonal
            int[] rightToLeftDiagonal = new int[_gridSize];
            for (int i = 0; i < _gridSize; i++)
            {
                rightToLeftDiagonal[i] = i * _gridSize + (_gridSize - i - 1);
            }
            combinations.Add(rightToLeftDiagonal);

            return combinations;
        }
    }
}