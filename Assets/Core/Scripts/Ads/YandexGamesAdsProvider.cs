using YG;

public class YandexGamesAdsProvider : IAdsProvider, IStickyBannerAds, IInterstitialAds
{
    public IStickyBannerAds StickyBanner { get; }
    public IInterstitialAds Interstitial { get; }
    public IRewardedAds Rewarded { get; }
    public void InitializeAsync()
    {
        
    }

    public void ShowStickyBannerAd()
    {
        
    }

    public void ShowInterstitialAd()
    {
        
    }
}
