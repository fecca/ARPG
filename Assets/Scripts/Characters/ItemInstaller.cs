using ARPG.Items;
using UnityEngine;
using Zenject;

namespace ARPG.Characters
{
	public class ItemInstaller : MonoInstaller
	{
		[SerializeField] private LootItemOnGround Prefab;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<LootFountain>().AsSingle();
			Container.BindFactory<ItemConfig, Vector3, LootItemOnGround, LootItemOnGround.Factory>()
				.FromComponentInNewPrefab(Prefab);
		}
	}
}