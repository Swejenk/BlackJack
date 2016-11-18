using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Jens Malm 
 * 2016-09
 * BlackJackGame
 **/
namespace IListExtensions
{
    public static class Extensions
    {
        private static Random randomizer = new Random();

        /// <summary>
        /// Based on Fisher–Yates shuffle algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = randomizer.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
        /// <summary>
        /// Pop extension for List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Pop<T>(this IList<T> list)
        {
            var local = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return local;
        }
        /// <summary>
        /// Push extension for List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void Push<T>(this List<T> list, T item)
        {
            list.Add(item);
        }
    }
}
