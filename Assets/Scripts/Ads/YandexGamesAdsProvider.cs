using YG;

public class YandexGamesAdsProvider : IAdsProvider, IStickyBannerAds, IInterstitialAds, IRewardedAds
{
    public IStickyBannerAds StickyBanner => this;
    public IInterstitialAds Interstitial => this;
    public IRewardedAds Rewarded => this;
    
    public void Init()
    {
        ShowStickyBannerAd();
    }

    public void ShowStickyBannerAd()
    {
        YG2.StickyAdActivity(true);
    }

    public void ShowInterstitialAd()
    {
        YG2.InterstitialAdvShow();
    }

    public void ShowRewardedAd(RewardAdsType type)
    {
        YG2.RewardedAdvShow(type.ToString());
    }
}
