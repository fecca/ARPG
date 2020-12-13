using Zenject;

namespace ARPG.Zenject
{
	public class ZenjectInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			SignalBusInstaller.Install(Container);

			Container.Bind<ISignalBusAdapter>().To<SignalBusAdapter>().AsSingle();
		}
	}
}