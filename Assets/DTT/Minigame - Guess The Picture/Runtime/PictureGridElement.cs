using System;
using UnityEngine;
using UnityEngine.UI;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Class that handles an element of the picture grid.
    /// </summary>
    [RequireComponent(typeof(Button), typeof(CanvasGroup))]
    public class PictureGridElement : MonoBehaviour
    {
        /// <summary>
        /// Invoked when the element is clicked.
        /// </summary>
        public event Action<PictureGridElement> Clicked;

        /// <summary>
        /// Canvas group of the button.
        /// </summary>
        private CanvasGroup _canvasGroup;

        /// <summary>
        /// Whether the canvas group is faded.
        /// </summary>
        private bool _faded = false;

        /// <summary>
        /// Whether the canvas group is faded.
        /// </summary>
        public bool Faded => _faded;

        /// <summary>
        /// The button component of the grid element.
        /// </summary>
        private Button _button;

        /// <summary>
        /// Sets the button component.
        /// </summary>
        private void Awake() =>_button = GetComponent<Button>();

        /// <summary>
        /// Sets the canvas group alpha and adds listeners.
        /// </summary>
        private void OnEnable()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 1;
            _button.onClick.AddListener(()=>Clicked?.Invoke(this));
        }

        /// <summary>
        /// Removes listeners.
        /// </summary>
        private void OnDisable() => _button.onClick.RemoveAllListeners();

        /// <summary>
        /// Fades in the button.
        /// </summary>
        public void FadeOut()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _faded = true;
            _button.onClick.RemoveAllListeners();
            StartCoroutine(Animations.Value(_canvasGroup.alpha, 0, 1f, (value) => _canvasGroup.alpha = value));
        }
    }
}
