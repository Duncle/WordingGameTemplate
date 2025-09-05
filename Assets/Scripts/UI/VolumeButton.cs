using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class VolumeButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] AudioSettingsManager audioSettingsManager;
    [SerializeField] Sprite volumeOnSprite;
    [SerializeField] Sprite volumeOffSprite;
    
    private void Awake() => button.onClick.AddListener(OnClick);

    private void Start()
    {
        //Here
        YG2.saves.isSoundEnabled = true;
    }

    private void OnDestroy() => button.onClick.RemoveListener(OnClick);

    private void OnClick() {
        
        audioSettingsManager.ToggleMute();
    }
}
