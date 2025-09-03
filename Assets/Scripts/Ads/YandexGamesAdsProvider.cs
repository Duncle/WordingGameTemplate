using System;
using YG;

public class YandexGamesAdsProvider : IAdsProvider, IStickyBannerAds, IInterstitialAds, IRewardedAds
{
    public IStickyBannerAds StickyBanner => this;
    public IInterstitialAds Interstitial => this;
    public IRewardedAds Rewarded => this;

    public event Action<bool> Closed;
    public event Action<RewardAdsType> RewardEarned;
    
    public void Init()
    {
        ShowStickyBannerAd(true);
        
        YG2.onCloseInterAdvWasShow += wasShown => Closed?.Invoke(wasShown);

        YG2.onRewardAdv += typeStr =>
        {
            if (Enum.TryParse<RewardAdsType>(typeStr, out RewardAdsType t))
                RewardEarned?.Invoke(t);
        };
    }

    public void ShowStickyBannerAd(bool active = true) => YG2.StickyAdActivity(true);
    
    public void ShowInterstitialAd() => YG2.InterstitialAdvShow();
    
    public void ShowRewardedAd(RewardAdsType type) => YG2.RewardedAdvShow(type.ToString());
}
