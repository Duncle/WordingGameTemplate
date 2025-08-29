using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DTT.Utils.Extensions;
using Random = UnityEngine.Random;
using System.Linq;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Handles generating the UI elements and snapping letters to the snap points.
    /// </summary>
    public class GuessThePictureInterface : MonoBehaviour
    {
        /// <summary>
        /// Prefab for the letters.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab for the letters.")]
        private GuessLetter _letterPrefab;

        /// <summary>
        /// Prefab for the snap points.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab for the snap points.")]
        private SnapPoint _snapPointPrefab;

        /// <summary>
        /// <see cref="GuessLetterBoard"/> where the letters snap to when not used.
        /// </summary>
        [SerializeField]
        [Tooltip("Snap board where the letters snap to when not used.")]
        private GuessLetterBoard _letterSnapBoard;

        /// <summary>
        /// Layout group for the snap points.
        /// </summary>
        [SerializeField]
        [Tooltip("Layout group for the snap points.")]
        private GridLayoutGroup _snapLayoutGroup;

        /// <summary>
        /// The picture to guess.
        /// </summary>
        [SerializeField]
        [Tooltip("The picture to guess.")]
        private Image _picture;

        /// <summary>
        /// The grid that covers the picture.
        /// </summary>
        [SerializeField]
        [Tooltip("The grid that covers the picture.")]
        private PictureGrid _pictureGrid;

        /// <summary>
        /// Invoked when all snap points are filled with a letter.
        /// </summary>
        public event Action<string> WordCompleted;

        /// <summary>
        /// Invoked when a hint is used.
        /// </summary>
        public event Action<int> HintUsed;
        public event Action<int> HinAmountChanged;

        /// <summary>
        /// All current letter UI elements.
        /// </summary>
        private List<GuessLetter> _letters = new List<GuessLetter>();

        /// <summary>
        /// All current snap point UI elements.
        /// </summary>
        private List<SnapPoint> _snapPoints = new List<SnapPoint>();

        /// <summary>
        /// List holding the white spaces in the snap layout group.
        /// The object represent a space character in the word.
        /// </summary>
        private List<GameObject> _whiteSpaces = new List<GameObject>();

        /// <summary>
        /// Current string used to generate the UI.
        /// </summary>
        private string _currentGuessLetters;

        /// <summary>
        /// The correct word to check.
        /// </summary>
        private string _answer;

        /// <summary>
        /// The amount of hints available to reveal the elements of the picture grid.
        /// </summary>
        private int _hints;

        /// <summary>
        /// Adds listeners to the elements.
        /// </summary>
        private void OnEnable() => AddListeners();

        /// <summary>
        /// Removes listeners from the UI elements.
        /// </summary>
        private void OnDisable() => ClearListeners();

        /// <summary>
        /// The amount of hints available to reveal the elements of the picture grid.
        /// </summary>
        public int Hints
        {
            get => _hints;
            set => _hints = value;
        }

        /// <summary>
        /// Generates the UI elements of the mini game.
        /// </summary>
        /// <param name="settings">The settings for the game.</param>
        public void GenerateUI(GameSettings settings)
        {
            _hints = settings.Hints;
            _picture.sprite = settings.Picture;
            _answer = settings.Word;

            // Clears the grid and sets it if the picture should be hidden.
            _pictureGrid.ClearGrid();

            if (settings.HidePicture) 
                _pictureGrid.InstantiateGridElements(settings.GridSize, settings.RevealsOnStart);

            ClearLetterBoard();
            _currentGuessLetters = (settings.Word + settings.AdditionalLetters).Shuffle();

            // Gets the estimated size of the snap points.
            RectTransform layoutRect = (RectTransform)_snapLayoutGroup.transform;
            float gridSizeWidth =
                layoutRect.rect.width - _snapLayoutGroup.padding.left - _snapLayoutGroup.padding.right;
            float gridSizeHeight =
                layoutRect.rect.height - _snapLayoutGroup.padding.top - _snapLayoutGroup.padding.bottom;
            float letterSizeX = gridSizeWidth / (float)settings.Word.Length;

            // Creates a snap point for each letter in the secret word.
            for (int i = 0; i < settings.Word.Length; i++)
            {
                // If the letter is a space, it fill the layout group with an empty object.
                if (Char.IsWhiteSpace(settings.Word[i]))
                {
                    GameObject whiteSpace = new GameObject();
                    whiteSpace.AddComponent<RectTransform>();
                    whiteSpace.transform.SetParent(_snapLayoutGroup.transform);
                    _whiteSpaces.Add(whiteSpace);
                }
                else
                {
                    SnapPoint snapPoint = Instantiate(_snapPointPrefab, _snapLayoutGroup.transform);
                    _snapPoints.Add(snapPoint);

                    snapPoint.LetterSnapped += CheckWordComplete;
                    snapPoint.RemovedLetter += OnLetterReplaced;
                }
            }

            // Creates a guess letter object for each letter in the word and in the additional letters.
            for (int i = 0; i < _currentGuessLetters.Length; i++)
            {
                if (Char.IsWhiteSpace(_currentGuessLetters[i]))
                    continue;

                // Creates a letter.
                GuessLetter letterUI = Instantiate(_letterPrefab, _letterSnapBoard.transform);
                _letters.Add(letterUI);
                letterUI.Drop += OnLetterDropped;
                letterUI.PickUp += OnLetterPickedUp;
                letterUI.Initialize(_currentGuessLetters[i], settings.LetterColors[Random.Range(0, settings.LetterColors.Length)]);
                _letterSnapBoard.AddSnapObject(letterUI, true);

                // Sets the size of the letters equal to the snap points.
                letterUI.RectTransform.sizeDelta = new Vector2(letterSizeX - _snapLayoutGroup.spacing.x, gridSizeHeight);
            }
        }

        /// <summary>
        /// Removes the listeners and destroys all UI elements related to the mini game.
        /// </summary>
        public void ClearLetterBoard()
        {
            ClearListeners();

            foreach (GuessLetter letter in _letters)
                Destroy(letter.gameObject);

            foreach (SnapPoint snapPoint in _snapPoints)
                Destroy(snapPoint.gameObject);

            foreach (GameObject whiteSpace in _whiteSpaces)
                Destroy(whiteSpace);

            _letters.Clear();
            _snapPoints.Clear();
            _whiteSpaces.Clear();
            _letterSnapBoard.ClearBoard();
        }

        /// <summary>
        /// Adds the listeners to the game UI elements.
        /// </summary>
        private void AddListeners()
        {
            _pictureGrid.GridElementClicked += OnPictureGridElementClicked;

            foreach (GuessLetter letter in _letters)
            {
                letter.Drop += OnLetterDropped;
                letter.PickUp += OnLetterPickedUp;
            }

            foreach (SnapPoint snapPoint in _snapPoints)
            {
                snapPoint.LetterSnapped += CheckWordComplete;
                snapPoint.RemovedLetter += OnLetterReplaced;
            }
        }

        /// <summary>
        /// Removes the listeners from the game UI elements.
        /// </summary>
        private void ClearListeners()
        {
            foreach (GuessLetter letter in _letters)
            {
                letter.Drop -= OnLetterDropped;
                letter.PickUp -= OnLetterPickedUp;
            }

            foreach (SnapPoint snapPoint in _snapPoints)
            {
                snapPoint.LetterSnapped -= CheckWordComplete;
                snapPoint.RemovedLetter -= OnLetterReplaced;
            }
        }

        /// <summary>
        /// Sets the letters interactable.
        /// </summary>
        /// <param name="interactable">Whether the letters should be interactable.</param>
        public void SetInteractable(bool interactable)
        {
            foreach (GuessLetter letter in _letters)
                letter.MoveHandle.enabled = interactable;

            _pictureGrid.SetInteractable(interactable);
        }

        public void ChangeHintsAmount() => HinAmountChanged?.Invoke(_hints);

        /// <summary>
        /// Handles picking up a letter.
        /// </summary>
        /// <param name="letter">Picked up letter.</param>
        private void OnLetterPickedUp(GuessLetter letter) => _letterSnapBoard.RemoveSnapObject(letter);

        /// <summary>
        /// Handles when a letter is replaced by another letter on one of the snap points.
        /// </summary>
        /// <param name="letter">Letter to be replaced.</param>
        public void OnLetterReplaced(GuessLetter letter)
        {
            GuessLetter newOccupant = letter.LastSnapPoint.CurrentLetter;

            if (letter != newOccupant && newOccupant.LastSnapPoint != null)
            {
                // Sets the letter on the last snap point of the other letter.
                newOccupant.LastSnapPoint.SetLetter(letter);
            }
            else
            {
                // Sets the letter back on the letter board.
                letter.transform.SetParent(_letterSnapBoard.transform);
                _letterSnapBoard.AddSnapObject(letter);
                letter.LastSnapPoint= null;
            }
        }

        /// <summary>
        /// Handles when a letter is dropped after being held.
        /// </summary>
        /// <param name="letter">Dropped letter.</param>
        public void OnLetterDropped(GuessLetter letter)
        {
            List<SnapPoint> overlapping = new List<SnapPoint>();

            // Gets all overlapping snap points.
            foreach (SnapPoint snapPoint in _snapPoints)
            {
                if (letter.RectTransform.GetWorldRect().Overlaps(snapPoint.RectTransform.GetWorldRect()))
                    overlapping.Add(snapPoint);
            }

            // If none are found, snap back to the letter board.
            if (overlapping.Count == 0)
            {
                letter.LastSnapPoint = null;
                _letterSnapBoard.AddSnapObject(letter);
                letter.transform.SetParent(_letterSnapBoard.transform);
                return;
            }

            // Check which overlapping snap point is the closest.
            Vector2 letterWorldPos = letter.RectTransform.GetWorldRect().center;
            SnapPoint bestOverlap = overlapping.OrderBy(snappedObjectEntry => Vector2.Distance(letterWorldPos, snappedObjectEntry.RectTransform.GetWorldRect().center)).FirstOrDefault();
            bestOverlap.SnapLetter(letter);
        }

        /// <summary>
        /// Checks if all snap points have been filled and invokes
        /// <see cref="WordCompleted"/> when all snap points are filled in.
        /// </summary>
        private void CheckWordComplete()
        {
            char[] wordResult = new char[_answer.Length];
            int spaces = 0;

            for (int i = 0; i < wordResult.Length; i++)
            {
                if (Char.IsWhiteSpace(_answer[i]))
                {
                    spaces++;
                    wordResult[i] = ' ';
                    continue;
                }
                else if (_snapPoints[i - spaces].CurrentLetter == null)
                {
                    return;
                }

                wordResult[i] = _snapPoints[i - spaces].CurrentLetter.Letter;
            }

            string resultString = new string(wordResult);

            if (_answer == resultString)
                _pictureGrid.FadeOutAll();

            WordCompleted?.Invoke(resultString);
        }

        /// <summary>
        /// Checks if there are enough hints to reveal an element of the grid.
        /// </summary>
        /// <param name="pictureGridElement">The grid element that was clicked.</param>
        private void OnPictureGridElementClicked(PictureGridElement pictureGridElement)
        {
            if (_hints > 0)
            {
                pictureGridElement.FadeOut();
                _hints--;
                HintUsed?.Invoke(_hints);
            }
        }
    }
}