using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager I;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string exposedParam = "MasterVolume";
    private const string MutedKey = "audio_muted";

    private bool _muted;

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        _muted = PlayerPrefs.GetInt(MutedKey, 0) == 1;
        ApplyVolume();
    }

    public void ToggleMute()
    {
        _muted = !_muted;
        PlayerPrefs.SetInt(MutedKey, _muted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyVolume();
    }

    public void SetMuted(bool value)
    {
        _muted = value;
        PlayerPrefs.SetInt(MutedKey, _muted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyVolume();
    }

    public bool IsMuted => _muted;

    public void MuteForAd(bool value)
    {
        // Временно глушим на время рекламы, не меняя пользовательский флаг
        mixer.SetFloat(exposedParam, value ? -80f : (_muted ? -80f : 0f));
    }

    private void ApplyVolume()
    {
        mixer.SetFloat(exposedParam, _muted ? -80f : 0f);
    }
}