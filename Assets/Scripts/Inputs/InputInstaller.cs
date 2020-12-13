using Zenject;

namespace ARPG.Inputs
{
	public class InputInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.DeclareSignal<MouseButtonDownSignal>();
			Container.DeclareSignal<MouseButtonHoldSignal>();
			Container.DeclareSignal<MouseButtonUpSignal>();
		}
	}
}