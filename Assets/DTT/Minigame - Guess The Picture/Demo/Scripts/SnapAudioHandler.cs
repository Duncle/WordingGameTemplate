using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Handles audio for the <see cref="SnapPoint"/> component.
    /// </summary>
    public class SnapAudioHandler : AudioSourceReferencer
    {
        /// <summary>
        /// Snap point of this handler.
        /// </summary>
        [SerializeField]
        [Tooltip("Snap point of this handler.")]
        private SnapPoint _snapPoint;

        /// <summary>
        /// Snap sound effect.
        /// </summary>
        [SerializeField]
        [Tooltip("Snap sound effect.")]
        private AudioClip _snapClip;

        /// <summary>
        /// Adds listeners.
        /// </summary>
        private void OnEnable() => _snapPoint.LetterSnapped += PlaySnap;

        /// <summary>
        /// Removes listeners.
        /// </summary>
        private void OnDisable() => _snapPoint.LetterSnapped -= PlaySnap;

        /// <summary>
        /// Plays snap sound effect.
        /// </summary>
        private void PlaySnap() => p_AudioSource.PlayOneShot(_snapClip);
    }
}