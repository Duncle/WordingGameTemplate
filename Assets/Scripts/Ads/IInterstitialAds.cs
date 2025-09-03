using System;

public interface IInterstitialAds
{
    event Action<bool> Closed;
    void ShowInterstitialAd();
}