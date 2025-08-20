using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IAdsProvider>().To<YandexGamesAdsProvider>().AsSingle();
        Container.Bind<AdsService>().AsSingle();
    }
}
