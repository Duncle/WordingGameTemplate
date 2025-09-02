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
    
        private IAdsProvider _provider;
    
        [Inject]
        public void Construct(IAdsProvider provider) => _provider = provider;

        public void Start()
        {
            _provider.Init();
            _gameManager.Finish += GameManager_OnGameFinished;
        }

        private void GameManager_OnGameFinished(GameResults gameResults)
        {
            if (gameResults.LevelIndex % 3 == 0) 
                ShowInterstitialAd();
        }
    
        public void ShowInterstitialAd() => _provider.Interstitial.ShowInterstitialAd();
    
        public void ShowRewarded(RewardAdsType type)
        {
            _provider.Rewarded.ShowRewardedAd(type);
            YG2.onRewardAdv += OnRewardedShown;
        }

        private void OnRewardedShown(string rewardType)
        {
            RewardAdsType rewardAdsType = (RewardAdsType)Enum.Parse(typeof(RewardAdsType), rewardType);
        
            switch (rewardAdsType)
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
        
            YG2.onRewardAdv -= OnRewardedShown;
        }
    }
}
