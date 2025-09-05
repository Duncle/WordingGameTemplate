using UnityEngine;
using UnityEngine.Audio;
using YG;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager I;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string exposedParam = "MasterVolume";

    private bool _muted;

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        // применяем значение из сейва (на старте там дефолт true)
        ApplyFromSaves();

        // если сейвы подтянутся асинхронно — применим повторно
        YG2.onGetSDKData += ApplyFromSaves;   // отписка ниже в OnDestroy
    }

    private void OnDestroy()
    {
        if (I == this) YG2.onGetSDKData -= ApplyFromSaves;
    }

    private void ApplyFromSaves()
    {
        bool soundOn = (YG2.saves == null) ? true : YG2.saves.isSoundEnabled;
        _muted = !soundOn;
        ApplyVolume();
    }

    public void ToggleMute()
    {
        _muted = !_muted;
        if (YG2.saves != null)
        {
            YG2.saves.isSoundEnabled = !_muted;
            YG2.SaveProgress();
        }
        ApplyVolume();
    }

    public void SetMuted(bool value)
    {
        _muted = value;
        if (YG2.saves != null)
        {
            YG2.saves.isSoundEnabled = !_muted;
            YG2.SaveProgress();
        }
        ApplyVolume();
    }

    public bool IsMuted => _muted;

    public void MuteForAd(bool value)
    {
        mixer.SetFloat(exposedParam, value ? -80f : (_muted ? -80f : 0f));
    }

    private void ApplyVolume()
    {
        mixer.SetFloat(exposedParam, _muted ? -80f : 0f);
    }
}