using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PvEOnline.AIs
{
    public abstract class AI
    {
        protected int PickAction(List<int> probs, Random rnd)
        {
            int totalWeight = probs.Sum();
            int weightedPick = rnd.Next(totalWeight);
            foreach (var item in probs)
            {
                if (weightedPick < item)
                {
                    return item;
                }
                weightedPick -= item;
            }
            throw new InvalidOperationException("List must have changed...");
        }
        protected void ShuffleList<T>(List<T> list, Random rnd) //Fisher-Yates
        {
            int len = list.Count;
            for (int i = 0; i < len; i++)
            {
                int p = i +(int)(rnd.NextDouble()*(len-1));
                T t = list[p];
                list[p] = list[i];
                list[i] = t;
            }
        }
        protected void Shuffle<T>(T[] arr, Random rnd)
        {
            int len = arr.Length;
            for (int i = 0; i < len; i++)
            {
                int p = i + (int)(rnd.NextDouble() * (len - 1));
                T t = arr[p];
                arr[p] = arr[i];
                arr[i] = t;
            }
        }
    }
    public enum Elements
    {
        Fire,
        Water,
        Light,
        Dark
    }
}
