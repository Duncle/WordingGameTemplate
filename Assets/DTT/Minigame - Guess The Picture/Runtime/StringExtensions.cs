using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Contains extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Randomizes the order of characters in the string.
        /// </summary>
        /// <param name="str">Pre-shuffled string.</param>
        /// <returns>Shuffled string.</returns>
        public static string Shuffle(this string str) =>
            new string(str.ToCharArray().OrderBy(s => (Random.Range(0,2) % 2) == 0).ToArray());
    }
}