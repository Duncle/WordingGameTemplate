using DTT.MinigameBase;
using DTT.MinigameBase.Timer;
using System;
using DTT.MinigameBase.UI;
using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Manager class for the Guess the Picture game. Handles starting, pausing, resuming and finishing the game.
    /// </summary>
    public class GameManager : MonoBehaviour, IMinigame<GameSettings, GameResults>, IRestartable
    {
        /// <summary>
        /// UI component of the game.
        /// </summary>
        [SerializeField]
        [Tooltip("UI component of the game.")]
        private GuessThePictureInterface _guessThePictureInterface;

        /// <summary>
        /// The settings for the game.
        /// </summary>
        [SerializeField]
        [Tooltip("The settings for the game.")]
        private GameSettings _settings;

        /// <summary>
        /// Whether or not the game should start on awake.
        /// </summary>
        [SerializeField]
        [Tooltip("Whether or not the game should start on awake.")]
        private bool _startOnAwake;

        /// <summary>
        /// Is called when the game has started.
        /// </summary>
        public event Action Started;

        /// <summary>
        /// Is called when the game has been Paused;
        /// </summary>
        public event Action Paused;

        /// <summary>
        /// Is called when the game has finished.
        /// </summary>
        public event Action<GameResults> Finish;

        /// <summary>
        /// Is true when the game is paused.
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// Is true when the game has started and isn't finished.
        /// </summary>
        public bool IsGameActive => !IsPaused;

        /// <summary>
        /// The settings for the game.
        /// </summary>
        public GameSettings Settings
        {
            get => _settings;
            set => _settings = value;
        }

        /// <summary>
        /// The timer for the game.
        /// </summary>
        public Timer Timer => _timer;

        /// <summary>
        /// The amount of wrong guesses the player has made.
        /// </summary>
        private int _wrongGuesses;

        /// <summary>
        /// The amount of hints used by the player.
        /// </summary>
        private int _hintsUsed;

        /// <summary>
        /// The timer for the game.
        /// </summary>
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// Subscribe to events.
        /// </summary>
        private void OnEnable()
        {
            _guessThePictureInterface.HintUsed += OnHintUsed;
            _guessThePictureInterface.WordCompleted += Completed;
        }

        /// <summary>
        /// Unsubscribe to events.
        /// </summary>
        private void OnDisable()
        {
            _guessThePictureInterface.HintUsed += OnHintUsed;
            _guessThePictureInterface.WordCompleted -= Completed;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void Start()
        {
            if (_startOnAwake)
                StartGame(_settings);
        }

        /// <summary>
        /// Start Guess the Picture game.
        /// </summary>
        /// <param name="settings">The settings for the game.</param>
        public void StartGame(GameSettings settings)
        {
            _settings = settings;
            _timer.Begin();
            _wrongGuesses = 0;
            _guessThePictureInterface.SetInteractable(true);
            _guessThePictureInterface.GenerateUI(settings);
            Started?.Invoke();
        }

        /// <summary>
        /// Stops the game activities and timer.
        /// </summary>
        public void Pause()
        {
            _guessThePictureInterface.SetInteractable(false);
            _timer.Stop();
            Paused?.Invoke();
        }

        /// <summary>
        /// Continues the game.
        /// </summary>
        public void Continue()
        {
            _guessThePictureInterface.SetInteractable(true);
            _timer.Resume();
        }

        /// <summary>
        /// Restarts the current game.
        /// </summary>
        public void Restart() => StartGame(_settings);

        /// <summary>
        /// Finishes the current game.
        /// </summary>
        public void ForceFinish()
        {
            _guessThePictureInterface.SetInteractable(false);
            _timer.Stop();
            Finish?.Invoke(new GameResults((float)_timer.TimePassed.TotalSeconds, _hintsUsed, _wrongGuesses));
        }

        /// <summary>
        /// Check if the answer is correct and finish the game if it is correct.
        /// </summary>
        /// <param name="answer">The answer given by the player.</param>
        private void Completed(string answer)
        {
            if (answer == _settings.Word)
                ForceFinish();
            else
                _wrongGuesses++;
        }

        /// <summary>
        /// Sets the amount of hints used.
        /// </summary>
        /// <param name="currentHints">The current amount of hints left.</param>
        private void OnHintUsed(int currentHints) => _hintsUsed = Settings.Hints - currentHints;
    }
}
