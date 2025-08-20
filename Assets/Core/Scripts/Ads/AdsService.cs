using System;
using UnityEngine;
using YG;
using Zenject;

public class AdsService : MonoBehaviour
{
    public event Action<RewardAdsType> OnRewardAdsShown;
    
    private IAdsProvider _provider;
    
    [Inject]
    public void Construct(IAdsProvider provider) => _provider = provider;

    public void Start() => _provider.Init();
    
    public void ShowInterstitialAd()
    {
        _provider.Interstitial.ShowInterstitialAd();
    }
    
    public void ShowRewarded(RewardAdsType type)
    {
        _provider.Rewarded.ShowRewardedAd(type);
    }
}
