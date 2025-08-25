using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Holds the settings for the level of the game.
    /// </summary>
    [CreateAssetMenu(fileName = "Picture Settings", menuName = "DTT/Mini Game/Guess The Picture/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        /// <summary>
        /// The picture for the player to guess.
        /// </summary>
        [SerializeField]
        [Tooltip("The picture for the player to guess.")]
        private Sprite _picture;

        /// <summary>
        /// The word represented in the picture.
        /// </summary>
        [SerializeField]
        [Tooltip("The word represented in the picture.")]
        private string _word;

        /// <summary>
        /// The additional letters that the player can pick from to form a word.
        /// </summary>
        [SerializeField]
        [Tooltip("The additional letters that the player can pick from to form a word.")]
        private string _additionalLetters;

        /// <summary>
        /// The amount of hints available to reveal a square of the grid.
        /// </summary>
        [SerializeField]
        [Tooltip("The amount of hints available to reveal a square of the grid.")]
        private int _hints;

        /// <summary>
        /// The amount of squares revealed at the start of the game.
        /// </summary>
        [SerializeField]
        [Tooltip("The amount of squares revealed at the start of the game.")]
        private int _revealsOnStart = 1;

        /// <summary>
        /// The size of the grid that covers the picture.
        /// </summary>
        [SerializeField]
        [Tooltip("The size of the grid that covers the picture.")]
        private Vector2Int _gridSize = new Vector2Int(2, 2);

        /// <summary>
        /// Whether or not the picture should be hidden by the grid.
        /// </summary>
        [SerializeField]
        [Tooltip("Whether or not the picture should be hidden by the grid.")]
        private bool _hidePicture = true;

        /// <summary>
        /// The color for the letter options.
        /// </summary>
        [SerializeField]
        [Tooltip("The color for the letter options.")]
        private Color[] _letterColors;

        /// <summary>
        /// The amount of hints available to reveal a square of the grid.
        /// </summary>
        public int Hints => _hints;

        /// <summary>
        /// The amount of squares revealed at the start of the game.
        /// </summary>
        public int RevealsOnStart => _revealsOnStart;

        /// <summary>
        /// The picture for the player to guess.
        /// </summary>
        public Sprite Picture => _picture;

        /// <summary>
        /// The word represented in the picture.
        /// </summary>
        public string Word => _word;

        /// <summary>
        /// The additional letters that the player can pick from to form a word.
        /// </summary>
        public string AdditionalLetters => _additionalLetters;

        /// <summary>
        /// The size of the grid that covers the picture.
        /// </summary>
        public Vector2Int GridSize => _gridSize;

        /// <summary>
        /// Whether or not the picture should be hidden by the grid.
        /// </summary>
        public bool HidePicture => _hidePicture;

        /// <summary>
        /// The color for the letter options.
        /// </summary>
        public Color[] LetterColors => _letterColors;
    }
}
