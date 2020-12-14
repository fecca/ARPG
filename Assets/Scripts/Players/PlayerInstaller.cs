using UnityEngine;
using Zenject;

namespace ARPG.Players
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Raycaster>().AsSingle();
            Container.Bind<NavmeshSampler>().AsSingle();
            Container.Bind<Camera>().FromMethod(c => Camera.main).AsSingle();
        }
    }
}