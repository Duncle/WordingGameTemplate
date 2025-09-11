using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DTT.GuessThePicture
{
    /// <summary>
    /// The grid for the picture.
    /// </summary>
    [RequireComponent(typeof(FlexibleGridLayout))]
    public class PictureGrid : MonoBehaviour
    {
        /// <summary>
        /// Prefab for an element of the grid.
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab for an element of the grid.")]
        private PictureGridElement _gridElementPrefab;

        /// <summary>
        /// The child elements of the grid.
        /// </summary>
        private List<PictureGridElement> _gridElements = new List<PictureGridElement>();

        /// <summary>
        /// Invoked when the element is clicked.
        /// </summary>
        public event Action<PictureGridElement> GridElementClicked;

        /// <summary>
        /// Instantiates the elements for the grid.
        /// </summary>
        /// <param name="gridSize">The size of the grid.</param>
        /// <param name="revealsOnStart">The amount of grid squares to reveal when the game starts.</param>
        
        public int ClosedCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < _gridElements.Count; i++)
                    if (!_gridElements[i].Faded) count++;
                return count;
            }
        }
        
        public bool HasClosed => ClosedCount > 0;
        
        public void InstantiateGridElements(Vector2Int gridSize, int revealsOnStart)
        {
            FlexibleGridLayout currentGridLayout = this.transform.GetComponent<FlexibleGridLayout>();
            int childAmount = gridSize.x * gridSize.y;

            for(int i = 0; i < childAmount; i++)
            {
                PictureGridElement gridElement = Instantiate(_gridElementPrefab, this.transform);
                _gridElements.Add(gridElement);
                gridElement.Clicked += OnGridElementClicked;
            }

            if (revealsOnStart < childAmount)
            {
                for (int j = 0; j < revealsOnStart; j++)
                {
                    int elementNumber = Random.Range(0, childAmount - 1);

                    if (_gridElements[elementNumber].Faded)
                    {
                        j--;
                        continue;
                    }

                    _gridElements[elementNumber].FadeOut();
                }
            }
            SetInteractable(true);
        }

        /// <summary>
        /// Animates the fade out animation of each element to reveal the picture.
        /// </summary>
        public void FadeOutAll()
        {
            for(int i = 0; i < _gridElements.Count; i++)
                _gridElements[i].FadeOut();
        }

        /// <summary>
        /// Called when an element of the grid is clicked.
        /// </summary>
        /// <param name="pictureGridElement">The grid element that has been clicked.</param>
        public void OnGridElementClicked(PictureGridElement pictureGridElement) => GridElementClicked?.Invoke(pictureGridElement);

        /// <summary>
        /// Enables or disables the button component of the grid elements.
        /// </summary>
        /// <param name="state">Whether it should be enabled or disabled.</param>
        public void SetInteractable(bool state)
        {
            for (int i = 0; i < _gridElements.Count; i++)
                _gridElements[i].GetComponent<Button>().enabled = state;
        }

        /// <summary>
        /// Clears the grid of it's child objects.
        /// </summary>
        public void ClearGrid()
        {
            foreach(PictureGridElement child in _gridElements)
                Destroy(child.gameObject);

            _gridElements.Clear();
        }
    }
}
