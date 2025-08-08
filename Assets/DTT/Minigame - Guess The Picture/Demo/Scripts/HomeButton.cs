using UnityEngine;
using UnityEngine.UI;
using System;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Handles the home button for the UI of the game.
    /// </summary>
    public class HomeButton : MonoBehaviour
    {
        /// <summary>
        /// Button component.
        /// </summary>
        private Button _button;

        /// <summary>
        /// Gets necessary components.
        /// </summary>
        private void Awake() => _button = GetComponent<Button>();

        /// <summary>
        /// Adds listeners.
        /// </summary>
        private void OnEnable() => _button.onClick.AddListener(OnHomeButtonPressed);

        /// <summary>
        /// Removes listeners.
        /// </summary>
        private void OnDisable() => _button.onClick.RemoveListener(OnHomeButtonPressed);

        /// <summary>
        /// Action for when the button is pressed.
        /// </summary>
        public event Action HomeButtonPressed;

        /// <summary>
        /// Return to the home page.
        /// </summary>
        private void OnHomeButtonPressed() => HomeButtonPressed?.Invoke();

    }
}

