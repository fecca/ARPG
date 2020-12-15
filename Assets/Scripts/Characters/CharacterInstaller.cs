using ARPG.Attacking;
using ARPG.Items;
using ARPG.Moving;
using UnityEngine;
using Zenject;

namespace ARPG.Characters
{
	public class CharacterInstaller : MonoInstaller
	{
		[SerializeField] private Projectile ProjectilePrefab;

		public override void InstallBindings()
		{
			Container.Bind<Raycaster>().AsSingle();
			Container.Bind<NavmeshSampler>().AsSingle();
			Container.Bind<Camera>().FromMethod(c => Camera.main).AsSingle();
			Container.BindFactory<Vector3, Vector3, float, float, Projectile, Projectile.Factory>()
				.FromComponentInNewPrefab(ProjectilePrefab);

			Container.DeclareSignal<EnemyDeathSignal>();
			Container.BindInterfacesAndSelfTo<LootFountain>().AsSingle();
		}
	}
}