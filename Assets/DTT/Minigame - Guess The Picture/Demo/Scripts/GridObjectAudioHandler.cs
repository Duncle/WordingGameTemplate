using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Handles audio for an <see cref="PictureGridElement"/> component.
    /// </summary>
    public class GridObjectAudioHandler : AudioSourceReferencer
    {
        /// <summary>
        /// Grid object of this handler.
        /// </summary>
        [SerializeField]
        [Tooltip("Picture guess letter of this handler.")]
        private PictureGridElement _gridElement;

        /// <summary>
        /// Click sound effect.
        /// </summary>
        [SerializeField]
        [Tooltip("Pickup sound effect.")]
        private AudioClip _click;

        /// <summary>
        /// Adds listeners.
        /// </summary>
        private void OnEnable() => _gridElement.Clicked += PlayClick;

        /// <summary>
        /// Removes listeners.
        /// </summary>
        private void OnDisable() => _gridElement.Clicked -= PlayClick;

        /// <summary>
        /// Plays click sound effect.
        /// </summary>
        /// <param name="obj">Clicked object</param>
        private void PlayClick(PictureGridElement gridElement) => p_AudioSource.PlayOneShot(_click);
    }
}
