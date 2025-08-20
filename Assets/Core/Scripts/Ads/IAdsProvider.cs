using System.Threading.Tasks;

public interface IAdsProvider
{
    IStickyBannerAds StickyBanner { get; }
    IInterstitialAds Interstitial { get; }
    IRewardedAds Rewarded { get; }
    void Init();
}
