using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace OleksiiStepanov.Utils
{
    public class ListTools 
    {
        public static void MoveLastToFirst<T>(List<T> list)
        {
            if (list == null || list.Count < 2)
            {
                return;
            }
            
            var temp = list[^1];
            list.RemoveAt(list.Count - 1);
            list.Insert(0, temp);
        }
        
        public static List<int> GetRandomizedList(int min, int max)
        {
            List<int> numbers = Enumerable.Range(min, max - min + 1).ToList(); // Create list from min to max
            var random = new Random();

            // Shuffle the list
            for (int i = numbers.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (numbers[i], numbers[j]) = (numbers[j], numbers[i]); // Swap elements
            }

            return numbers;
        }
        
    }    
}

