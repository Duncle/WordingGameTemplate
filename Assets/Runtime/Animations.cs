using System;
using System.Collections;
using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Class that handles basic animations.
    /// </summary>
    public static class Animations
    {
        /// <summary>
        /// Handles basic linear transition from one float value to another.
        /// </summary>
        /// <param name="from">Start value.</param>
        /// <param name="to">End value.</param>
        /// <param name="time">Duration.</param>
        /// <param name="onValueChanged">Invoked when the value changes.</param>
        public static IEnumerator Value(float from, float to, float time, Action<float> onValueChanged)
        {
            float min = Mathf.Min(from, to);
            float max = Mathf.Max(from, to);
            float value = from;
            float startTime = Time.time;
            while (value != to)
            {
                float percValue = (Time.time - startTime) / time;
                value = from + ((to - from) * percValue);

                value = Mathf.Clamp(value, min, max);

                onValueChanged(value);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}