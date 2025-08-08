using UnityEngine;
using UnityEngine.UI;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// Class that handles a grid layout and allows child elements to stretch.
    /// </summary>
    public class FlexibleGridLayout : LayoutGroup
    {
        /// <summary>
        /// The amount of rows for the grid.
        /// </summary>
        [Tooltip("The amount of rows for the grid")]
        [SerializeField]
        private int _rows;

        /// <summary>
        /// The amount of columns for the grid.
        /// </summary>
        [Tooltip("The amount of columns for the grid")]
        [SerializeField]
        private int _columns;

        /// <summary>
        /// The spacing between each cell inside the grid.
        /// </summary>
        [Tooltip("The spacing between each cell inside the grid")]
        [SerializeField]
        private Vector2 _spacing;

        /// <summary>
        /// The amount of rows for the grid.
        /// </summary>
        public int Rows => _rows;

        /// <summary>
        /// The amount of columns for the grid.
        /// </summary>
        public int Column => _columns;

        /// <summary>
        /// The size of each cell inside the grid.
        /// </summary>
        private Vector2 _cellSize;

        /// <summary>
        /// Updates the size of the children when the grid is stretched.
        /// </summary>
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            ResizeChildren();
        }

        public override void CalculateLayoutInputVertical() { }

        public override void SetLayoutHorizontal() { }

        public override void SetLayoutVertical() { }

        /// <summary>
        /// Resizes the child elements of the layout object.
        /// </summary>
        private void ResizeChildren()
        {
            // Calculate the amount of rows and columns.
            float sqrRt = Mathf.Sqrt(transform.childCount);
            _rows = Mathf.CeilToInt(sqrRt);
            _columns = Mathf.CeilToInt(sqrRt);

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            // Calculate the size of the cells according to grid size, spacing and padding.
            float cellWidth = parentWidth / (float)_columns - ((_spacing.x / (float)_columns) * 2) - (padding.left / (float) _columns) - (padding.right / (float)_columns);
            float cellHeight = parentHeight / (float)_rows - ((_spacing.y / (float)_rows) * 2) - (padding.top / (float)_rows) - (padding.bottom / (float)_rows); 

            _cellSize.x = cellWidth;
            _cellSize.y = cellHeight;

            int columnCount = 0;
            int rowCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / _columns;
                columnCount = i % _columns;

                RectTransform rectChild = rectChildren[i];

                float xPos = (_cellSize.x * columnCount) + (_spacing.x * columnCount) + padding.left;
                float yPos = (_cellSize.y * rowCount) + (_spacing.y * rowCount) + padding.top; 

                SetChildAlongAxis(rectChild, 0, xPos, _cellSize.x);
                SetChildAlongAxis(rectChild, 1, yPos, _cellSize.y); 
            }
        }
    }
}
