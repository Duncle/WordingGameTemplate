using System;

public interface IRewardedAds
{
    event Action<RewardAdsType> RewardEarned;
    void ShowRewardedAd(RewardAdsType type);
}
