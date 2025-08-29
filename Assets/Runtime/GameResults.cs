using System.Text;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Class that contains the results for a guess the picture game.
    /// </summary>
    public class GameResults
    {
        /// <summary>
        /// Time it took to finish the game in seconds.
        /// </summary>
        public readonly float TimeTaken;

        /// <summary>
        /// The amount of wrong guesses that the player gave.
        /// </summary>
        public readonly int WrongGuesses;

        /// <summary>
        /// The amount of hints used to solve the game.
        /// </summary>
        public readonly int HintsUsed;

        public readonly int LevelIndex;

        /// <summary>
        /// Sets the result information.
        /// </summary>
        /// <param name="timeTaken">Time the player took to finish the game in seconds.</param>
        /// <param name="hintsUsed">The amount of hints used to solve the game.</param>
        /// <param name="wrongGuesses">The amount of wrong guesses that the player gave.</param>
        /// <param name="levelIndex"></param>
        public GameResults(float timeTaken, int hintsUsed, int wrongGuesses, int levelIndex)
        {
            this.TimeTaken = timeTaken;
            this.WrongGuesses = wrongGuesses;
            this.HintsUsed = hintsUsed;
            this.LevelIndex = levelIndex;
        }

        /// <summary>
        /// Returns result info in string format for debugging.
        /// </summary>
        /// <returns>Result in string format.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Amount of wrong guesses: ");
            sb.Append(WrongGuesses);
            sb.Append('\t');
            sb.Append("Amount of hints used: ");
            sb.Append(HintsUsed);
            sb.Append('\t');
            sb.Append("Time taken (s): ");
            sb.Append(TimeTaken);
            sb.Append('\t');
            sb.Append("Level index: ");
            sb.Append(LevelIndex);
            return sb.ToString();
        }
    }
}
