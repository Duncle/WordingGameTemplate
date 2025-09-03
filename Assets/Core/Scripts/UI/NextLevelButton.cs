using System;
using DTT.GuessThePicture;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GuessThePictureLevelSelectHandler _levelSelect;

        private void Reset()
        {
            _button = GetComponent<Button>();
            _gameManager = FindObjectOfType<GameManager>(true);
            _levelSelect = FindObjectOfType<GuessThePictureLevelSelectHandler>(true);
        }

        private void Awake()
        {
            if (_button == null) _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _gameManager.Started += OnStarted;
            _gameManager.Finish  += OnFinished;
        }

        private void OnDisable()
        {
            _gameManager.Started -= OnStarted;
            _gameManager.Finish  -= OnFinished;
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnStarted() => gameObject.SetActive(false);

        private void OnFinished(GameResults _)
        {
            // Покажем кнопку только если есть следующий уровень
            gameObject.SetActive(_levelSelect.HasNextLevel);
        }

        public void OnClick()
        {
            Debug.Log("Clicked");
            // Если последнего нет — можно уйти в Level Select
            if (!_levelSelect.StartNextLevel())
            {
                _levelSelect.ShowLevelSelect();
            }
        }
    }
}
