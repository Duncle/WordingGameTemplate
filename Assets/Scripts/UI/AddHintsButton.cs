using System;
using UnityEngine;
using UnityEngine.UI;

public class AddHintsButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AdsService _adsService;

    private void Awake() =>
        _button.onClick.AddListener(() => _adsService.ShowRewarded(RewardAdsType.AddAdditionalHints));

    private void OnDestroy() =>
        _button.onClick.RemoveAllListeners();
}
