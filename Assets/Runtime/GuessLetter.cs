using DTT.MinigameBase.Handles;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// A letter used to fill a word guess.
    /// </summary>
    [RequireComponent(typeof(MoveHandle))]
    public class GuessLetter : SnapObject
    {
        /// <summary>
        /// Text the letter should be drawn to.
        /// </summary>
        [SerializeField]
        [Tooltip("Text the letter should be drawn to.")]
        private Text _text;

        /// <summary>
        /// The background image for the letter.
        /// </summary>
        [SerializeField]
        private Image _background;

        /// <summary>
        /// Invoked when the letters is picked up.
        /// </summary>
        public event Action<GuessLetter> PickUp;

        /// <summary>
        /// Invoked when the letter is dropped.
        /// </summary>
        public event Action<GuessLetter> Drop;

        /// <summary>
        /// Reference to the <see cref="MoveHandle"/> of this object.
        /// </summary>
        public MoveHandle MoveHandle { get; private set; }

        /// <summary>
        /// Last snap point the letter snapped to.
        /// </summary>
        public SnapPoint LastSnapPoint { get; internal set; }

        /// <summary>
        /// Character this object represents.
        /// </summary>
        public char Letter { get; private set; }

        /// <summary>
        /// Gets necessary components.
        /// </summary>
        protected virtual void Awake() => MoveHandle = GetComponent<MoveHandle>();

        /// <summary>
        /// Constructor for the letter.
        /// </summary>
        /// <param name="letter">the letter to display</param>
        /// <param name="color">the color for the background</param>
        public void Initialize(char letter, Color color)
        {
            Letter = letter;
            _text.text = letter.ToString();
            _background.color = color;
        }

        /// <summary>
        /// Adds necessary listeners.
        /// </summary>
        protected virtual void OnEnable()
        {
            MoveHandle.PointerUp += OnDrop;
            MoveHandle.PointerDown += OnPickUp;
        }

        /// <summary>
        /// Removed listeners.
        /// </summary>
        protected virtual void OnDisable()
        {
            MoveHandle.PointerUp -= OnDrop;
            MoveHandle.PointerDown -= OnPickUp;
        }

        /// <summary>
        /// Snaps this object to a certain position.
        /// Override to implement your own behavior for when the object snaps.
        /// </summary>
        /// <param name="position">Position to snap to.</param>
        public override void SnapToPosition(Vector2 position) => RectTransform.position = position;

        /// <summary>
        /// Called when the letter is dropped.
        /// </summary>
        private void OnDrop(PointerEventData eventData) => Drop?.Invoke(this);

        /// <summary>
        /// Called when the letter is picked up.
        /// </summary>
        private void OnPickUp(PointerEventData eventData) => PickUp?.Invoke(this);
    }
}
