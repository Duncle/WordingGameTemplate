#if UNITY_EDITOR

using DTT.PublishingTools;
using UnityEditor;

namespace DTT.GuessThePicture.Editor
{
    /// <summary>
    /// Class that handles opening the editor window for the guess the picture package.
    /// </summary>
    internal static class ReadMeOpener
    {
        /// <summary>
        /// Opens the readme for this package.
        /// </summary>
        [MenuItem("Tools/DTT/Guess The Picture/ReadMe")]
        private static void OpenReadMe() => DTTEditorConfig.OpenReadMe("dtt.guess-the-picture");
    }
}
#endif