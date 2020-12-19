using System.Collections.Generic;
using System.Linq;
using ARPG.Characters;
using ARPG.Moving;
using ARPG.Zenject;
using UnityEngine;
using Zenject;

namespace ARPG.Items
{
	public class LootFountain : IInitializable
	{
		private ISignalBusAdapter _signalBusAdapter;
		private LootItemOnGround.Factory _lootItemOnGroundFactory;
		private NavmeshSampler _navmeshSampler;

		public LootFountain(ISignalBusAdapter signalBusAdapter, LootItemOnGround.Factory lootItemOnGroundFactory,
			NavmeshSampler navmeshSampler)
		{
			_navmeshSampler = navmeshSampler;
			_signalBusAdapter = signalBusAdapter;
			_lootItemOnGroundFactory = lootItemOnGroundFactory;
		}

		public void Initialize()
		{
			_signalBusAdapter.Subscribe<EnemyDeathSignal>(OnEnemyDeath);
		}

		private void OnEnemyDeath(EnemyDeathSignal signal)
		{
			if (signal.LootTableConfig == null || signal.LootTableConfig.itemConfigs.Count <= 0) return;

			var loot = RollLoot(signal.LootTableConfig);
			SpawnLoot(loot, signal.Position);
		}

		private IEnumerable<ItemConfig> RollLoot(LootTableConfig lootTableConfig)
		{
			return lootTableConfig.itemConfigs.Where(lootTableItem => lootTableItem.chance > Random.Range(0, 101))
				.Select(lootTableItem => lootTableItem.itemConfig);
		}

		private void SpawnLoot(IEnumerable<ItemConfig> loot, Vector3 position)
		{
			foreach (var itemConfig in loot)
			{
				var insideUnitSphere = Random.insideUnitSphere;
				var randomPosition = position + new Vector3(insideUnitSphere.x, insideUnitSphere.y, insideUnitSphere.z);
				if (_navmeshSampler.SampleGroundPosition(randomPosition, out var hit))
				{
					_lootItemOnGroundFactory.Create(itemConfig, hit.position);
					// var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
					// go.transform.localScale = Vector3.one * 0.1f;
					// go.transform.position = hit.position;
					// go.name = "go";
				}
			}
		}
	}
}