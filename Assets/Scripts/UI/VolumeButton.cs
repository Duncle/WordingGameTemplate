using UnityEngine;
using UnityEngine.UI;
using YG;

public class VolumeButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private AudioSettingsManager audioSettingsManager;
    [SerializeField] private Sprite volumeOnSprite;
    [SerializeField] private Sprite volumeOffSprite;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
        YG2.onGetSDKData += ApplyIconFromCloud;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
        YG2.onGetSDKData -= ApplyIconFromCloud;
    }

    private void Start()
    {
        ApplyIconFromCloud();
    }

    private void ApplyIconFromCloud()
    {
        bool soundOn = (YG2.saves == null) ? true : YG2.saves.isSoundEnabled;
        audioSettingsManager.SetMuted(!soundOn);
        UpdateIcon(soundOn);
    }

    private void OnClick()
    {
        audioSettingsManager.ToggleMute();
        UpdateIcon(!audioSettingsManager.IsMuted);
    }

    private void UpdateIcon(bool soundOn)
    {
        if (icon != null)
            icon.sprite = soundOn ? volumeOnSprite : volumeOffSprite;
    }
}