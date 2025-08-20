using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Base class for getting an audio source from an object.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public abstract class AudioSourceReferencer : MonoBehaviour
    {
        /// <summary>
        /// Audio source of the component.
        /// </summary>
        protected AudioSource p_AudioSource { get; private set; }

        /// <summary>
        /// Gets the audio component.
        /// </summary>
        protected virtual void Awake() => p_AudioSource = GetComponent<AudioSource>();
    }
}