using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Abstract class for objects that can snap.
    /// </summary>
    public abstract class SnapObject : MonoBehaviour
    {
        /// <summary>
        /// RectTransform of this object.
        /// </summary>
        public RectTransform RectTransform => (RectTransform)transform;

        /// <summary>
        /// Called when an object snaps to a new position.
        /// </summary>
        /// <param name="position">Position to snap to.</param>
        public abstract void SnapToPosition(Vector2 position);
    }
}