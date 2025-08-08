using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Handles audio for an <see cref="GuessLetter"/> component.
    /// </summary>
    public class LetterAudioHandler : AudioSourceReferencer
    {
        /// <summary>
        /// Picture guess letter of this handler.
        /// </summary>
        [SerializeField]
        [Tooltip("Picture guess letter of this handler.")]
        private GuessLetter _letter;

        /// <summary>
        /// Pickup sound effect.
        /// </summary>
        [SerializeField]
        [Tooltip("Pickup sound effect.")]
        private AudioClip _pickUpClip;

        /// <summary>
        /// Drop sound effect.
        /// </summary>
        [SerializeField]
        [Tooltip("Drop sound effect.")]
        private AudioClip _dropClip;

        /// <summary>
        /// Adds listeners.
        /// </summary>
        private void OnEnable()
        {
            _letter.PickUp += PlayPickup;
            _letter.Drop += PlayDrop;
        }

        /// <summary>
        /// Removes listeners.
        /// </summary>
        private void OnDisable()
        {
            _letter.PickUp -= PlayPickup;
            _letter.Drop -= PlayDrop;
        }

        /// <summary>
        /// Plays pickup sound effect.
        /// </summary>
        /// <param name="letter">Picked up letter.</param>
        private void PlayPickup(GuessLetter letter) => p_AudioSource.PlayOneShot(_pickUpClip);

        /// <summary>
        /// Plays drop sound effect.
        /// </summary>
        /// <param name="letter">Dropped letter.</param>
        private void PlayDrop(GuessLetter letter) => p_AudioSource.PlayOneShot(_dropClip);
    }
}