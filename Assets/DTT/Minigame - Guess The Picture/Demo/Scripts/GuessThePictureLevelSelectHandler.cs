using System.Collections.Generic;
using DTT.MinigameBase.LevelSelect;
using UnityEngine;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Handle the level selection of the game.
    /// </summary>
    public class GuessThePictureLevelSelectHandler : LevelSelectHandler<GameSettings, GameResults, GameManager>
    {
        /// <summary>
        /// List of different level settings.
        /// </summary>
        [SerializeField]
        [Tooltip("List of different level settings.")]
        private List<GameSettings> _settings;
        
        /// <summary>
        /// Pick a <see cref="GameSettings"/> according to the level number.
        /// </summary>
        /// <param name="levelNumber">Level number to load.</param>
        /// <returns>The <see cref="GameSettings"/> of the specified level.</returns>
        protected override GameSettings GetConfig(int levelNumber) => _settings[ (levelNumber - 1) % _settings.Count];

        /// <summary>
        /// Calculate the score of the game.
        /// </summary>
        /// <param name="result">The <see cref="GameResults"/> of the game.</param>
        /// <returns>The score as a float between 0 and 1.</returns>
        protected override float CalculateScore(GameResults result) => Mathf.InverseLerp(5, 0, result.wrongGuesses);
        
    }
}