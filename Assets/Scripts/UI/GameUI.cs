//using BansheeGz.BGDatabase;
using Core.Scripts.UI;
using DTT.MinigameBase;
using DTT.MinigameBase.Timer;
using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Handles the UI of the game for the demo.
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        /// <summary>
        /// Reference to the game manager of this scene.
        /// </summary>
        [SerializeField] [Tooltip("Reference to the game manager of this scene")]
        private GameManager _guessThePictureManager;

        /// <summary>
        /// Reference to the guess the picture UI of this scene.
        /// </summary>
        [SerializeField] [Tooltip("Reference to the guess the picture UI of this scene")]
        private GuessThePictureInterface _guessThePictureUI;

        /// <summary>
        /// Reference to the restart button.
        /// </summary>
        [SerializeField] [Tooltip("Reference to the restart button")]
        private RestartButton _restartButton;

        /// <summary>
        /// Reference to the pause button.
        /// </summary>
        [SerializeField] [Tooltip("Reference to the pause button")]
        private PauseButton _pauseButton;

        /// <summary>
        /// Reference to the home button.
        /// </summary>
        [SerializeField] [Tooltip("Reference to the play button")]
        private HomeButton _homeButton;

        [SerializeField] private NextLevelButton _nextLevelButton;

        /// <summary>
        /// The text for the number of hints.
        /// </summary>
        [SerializeField] [Tooltip("The text for the number of hints")]
        private TMP_Text _hintsText;

        [Header("Localization")]
        [SerializeField] private string _fmtId = "LPYFDsjf/U6ypu3QgwxNYw";
        [SerializeField] private string _table = "LT_UI";
        [SerializeField] private string _field = "localizedString";
        [Space(5)]
        
        /// <summary>
        /// The timer for the UI of the game.
        /// </summary>
        [SerializeField] [Tooltip("The timer for the UI of the game")]
        private Timer _timer;

        /// <summary>
        /// Sound effect for when the correct answer is given.
        /// </summary>
        [SerializeField] [Tooltip("Sound effect for when the correct answer is given")]
        private AudioClip _correctClip;

        /// <summary>
        ///  The _levelSelectHandler field is used to navigate back to the level selection on finish.
        /// </summary>
        [SerializeField] [Tooltip(" The level select handler of that game")]
        private GuessThePictureLevelSelectHandler _levelSelectHandler;
        
        [SerializeField] private AddHintsButton _addHintsButton;
        [SerializeField] private GameObject _hintsUiRoot;
        
        /// <summary>
        /// On enable subscribes to required events.
        /// </summary>
        private void OnEnable()
        {
            _guessThePictureManager.Finish += OnFinished;
            _guessThePictureManager.Started += OnStarted;
            _guessThePictureUI.HintUsed += UpdateHintText;
            _guessThePictureUI.HinAmountChanged += UpdateHintText;
            _homeButton.HomeButtonPressed += HomePage;
            _pauseButton.PauseButtonPressed += TogglePaused;
            _restartButton.RestartButtonPressed += RestartGameLevel;
        }

        /// <summary>
        /// On disable unsubscribes from events.
        /// </summary>
        private void OnDisable()
        {
            _guessThePictureManager.Finish -= OnFinished;
            _guessThePictureManager.Started -= OnStarted;
            _guessThePictureUI.HintUsed -= UpdateHintText;
            _guessThePictureUI.HinAmountChanged -= UpdateHintText;
            _homeButton.HomeButtonPressed -= HomePage;
            _pauseButton.PauseButtonPressed -= TogglePaused;
            _restartButton.RestartButtonPressed -= RestartGameLevel;
        }

        /// <summary>
        /// Resets the timer when the game is started and sets the hint text.
        /// </summary>
        private void OnStarted()
        {
            UpdateHintText(_guessThePictureUI.Hints);
            _timer.Begin();
            //_homeButton.gameObject.SetActive(false);
            _pauseButton.OnStart();
            _guessThePictureManager.LevelIndex = _levelSelectHandler.CurrentLevel;
            
            ToggleHintsUI(_guessThePictureUI.ClosedSquaresLeft > 0);
        }

        /// <summary>
        /// Logs the results on console and stops the timer.
        /// </summary>
        /// <param name="results">The results of the game.</param>
        private void OnFinished(GameResults results)
        {
            _timer.Stop();
            GetComponent<AudioSource>().PlayOneShot(_correctClip);
            Debug.Log(results.ToString());
            _homeButton.gameObject.SetActive(true);
            _nextLevelButton.gameObject.SetActive(true);
            
            ToggleHintsUI(false);
        }

        /// <summary>
        /// Updates the text for the number of hints.
        /// </summary>
        /// <param name="currentHints">The amount of current hints.</param>
        private void UpdateHintText(int currentHints)
        {
            //var fmt = LanguageManager.GetLocalizedName(_fmtId, _table, _field);
            //if (string.IsNullOrEmpty(fmt)) Debug.LogError("There is no such field in DB!");

            //var locale = BGRepo.I.Addons.Get<BGAddonLocalization>().CurrentLocale;
            string num = currentHints.ToString();

            _hintsText.text = "Осталось открытий: " + num;

            //Если необходимо ещё и выравнивание/RTL с таблицы Locale:
            //var row = BGRepo.I["Localization"].FindEntity(e => e.Get<string>("name") == locale);
            //bool rtl = row?.Get<bool>("isRightToLeft") ?? LanguageManager.IsArabicLike(locale);
            //_hintsText.isRightToLeftText = rtl;
            //_hintsText.alignment = rtl ? TextAlignmentOptions.Right : TextAlignmentOptions.Left;

            ToggleHintsUI(_guessThePictureUI.ClosedSquaresLeft > 0);
        }

        private void ToggleHintsUI(bool show)
        {
            // если задан общий контейнер — управляем им
            if (_hintsUiRoot != null)
                _hintsUiRoot.SetActive(show);
            else if (_hintsText != null)
                _hintsText.gameObject.SetActive(show);

            if (_addHintsButton != null)
                _addHintsButton.gameObject.SetActive(show);
        }
        
        /// <summary>
        /// Toggles the paused state of the game.
        /// </summary>
        /// <param name="paused">Whether or not the game has been paused.</param>
        private void TogglePaused(bool paused)
        {
            if (paused)
            {
                _guessThePictureManager.Pause();
                _timer.Stop();
                _homeButton.gameObject.SetActive(true);
                _nextLevelButton.gameObject.SetActive(true);
            }
            else
            {
                _guessThePictureManager.Continue();
                _timer.Resume();
                //_homeButton.gameObject.SetActive(false);
                _nextLevelButton.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Restarts the game level.
        /// </summary>
        private void RestartGameLevel()
        {
            _guessThePictureManager.Restart();
            UpdateHintText(_guessThePictureUI.Hints);
        }

        /// <summary>
        /// Return to the level select home page.
        /// </summary>
        private void HomePage() => _levelSelectHandler.ShowLevelSelect();
    }
}
