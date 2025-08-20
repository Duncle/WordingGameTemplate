using UnityEngine;

public class AdsService : MonoBehaviour
{
    private readonly IAdsProvider _provider;
    public AdsService(IAdsProvider provider) => _provider = provider;

    public void Init() => _provider.InitializeAsync();
    
    public void ShowInterstitialAd()
    {
        _provider.Interstitial.ShowInterstitialAd();
    }
    
    public void ShowRewarded()
    {
        _provider.Rewarded.ShowRewardedAd();
    }
}
