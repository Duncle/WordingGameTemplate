using System;
using DTT.GuessThePicture;
using UnityEngine;
using YG;
using Zenject;

namespace Ads
{
    public class AdsService : MonoBehaviour
    {
        public event Action<RewardAdsType> OnRewardAdsShown;

        [SerializeField] private GuessThePictureInterface _guessThePictureInterface;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private int hintsToAddOnHintsButton = 3;
    
        [Header("Interstitial Show Threshold")]
        [SerializeField] private int levelsBetweenInter = 3;
        [SerializeField] private float minSecondsBetween = 60f;
        
        private int _levelsSinceInter = 0;
        private float _nextAllowedTime = 0f;
        private bool _interShowing = false;
        
        private IAdsProvider _provider;
    
        [Inject]
        public void Construct(IAdsProvider provider) => _provider = provider;

        public void OnEnable()
        {
            _provider.Init();
            _gameManager.Finish += GameManager_OnGameFinished;

            _provider.Interstitial.Closed += OnInterClose;
            _provider.Rewarded.RewardEarned += OnRewardedEarn;
            
        }

        private void OnDisable() {
            _gameManager.Finish -= GameManager_OnGameFinished;
            
            _provider.Interstitial.Closed -= OnInterClose;
            _provider.Rewarded.RewardEarned -= OnRewardedEarn;
        }

        private void GameManager_OnGameFinished(GameResults gameResults)
        {
            _levelsSinceInter++;

            if (_interShowing) return;
            if (_levelsSinceInter < levelsBetweenInter) return;
            if (Time.realtimeSinceStartup < _nextAllowedTime) return;

            _interShowing = true;
            _provider.Interstitial.ShowInterstitialAd();
        }
    
        private void ShowInterstitialAd() => _provider.Interstitial.ShowInterstitialAd();

        private void OnInterClose(bool wasShown)
        {
            _interShowing = false;

            if (wasShown)
            {
                _levelsSinceInter = 0;
                _nextAllowedTime = Time.realtimeSinceStartup + minSecondsBetween;
            }
        }
        
        public void ShowRewarded(RewardAdsType type) => _provider.Rewarded.ShowRewardedAd(type);
        
        private void OnRewardedEarn(RewardAdsType rewardType)
        {
            switch (rewardType)
            {
                case RewardAdsType.AddAdditionalHints:
                    int calculatedHintsToAdd = hintsToAddOnHintsButton;
                    _guessThePictureInterface.Hints += calculatedHintsToAdd;
                    _guessThePictureInterface.ChangeHintsAmount();
                    break;
                default:
                    Debug.LogError("There is not such Ads type!");
                    break;
            }
        }
    }
}
