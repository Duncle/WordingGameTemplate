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
        public readonly float timeTaken;

        /// <summary>
        /// The amount of wrong guesses that the player gave.
        /// </summary>
        public readonly int wrongGuesses;

        /// <summary>
        /// The amount of hints used to solve the game.
        /// </summary>
        public readonly int hintsUsed;

        /// <summary>
        /// Sets the result information.
        /// </summary>
        /// <param name="timeTaken">Time the player took to finish the game in seconds.</param>
        /// <param name="hintsUsed">The amount of hints used to solve the game.</param>
        /// <param name="wrongGuesses">The amount of wrong guesses that the player gave.</param>
        public GameResults(float timeTaken, int hintsUsed, int wrongGuesses)
        {
            this.timeTaken = timeTaken;
            this.wrongGuesses = wrongGuesses;
            this.hintsUsed = hintsUsed;
        }

        /// <summary>
        /// Returns result info in string format for debugging.
        /// </summary>
        /// <returns>Result in string format.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Amount of wrong guesses: ");
            sb.Append(wrongGuesses);
            sb.Append('\t');
            sb.Append("Amount of hints used: ");
            sb.Append(hintsUsed);
            sb.Append('\t');
            sb.Append("Time taken (s): ");
            sb.Append(timeTaken);
            return sb.ToString();
        }
    }
}
