using DTT.PublishingTools;
using UnityEditor;

namespace DTT.GuessThePicture.Editor
{
    /// <summary>
    /// Editor class for the game settings of guess the picture.
    /// </summary>
    [CustomEditor(typeof(GameSettings))]
    [DTTHeader("dtt.guess-the-picture")]
    public class GameSettingsEditor : DTTInspector
    {
        /// <summary>
        /// Draws custom editor with DTT header and checks if the amount of reveals is lower than the quantity of objects inside the grid.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SerializedProperty gridSize = serializedObject.FindProperty("_gridSize");
            SerializedProperty revealsOnStart = serializedObject.FindProperty("_revealsOnStart");
            float gridSquares = gridSize.vector2IntValue.x * gridSize.vector2IntValue.y;

            if (gridSquares <= revealsOnStart.intValue)
            {
                EditorUtility.DisplayDialog("Amount of Reveals Warning", "The amount of initial reveals should be lower than the quantity of objects inside the grid", "Accept");
                revealsOnStart.intValue = (int)gridSquares - 1;
            }

            serializedObject.ApplyModifiedProperties();
            DrawDefaultInspector();
        }
    }
}