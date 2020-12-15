using System.Linq;
using ARPG.Characters;
using ARPG.Zenject;
using UnityEngine;
using Zenject;

namespace ARPG.Items
{
	public class LootFountain : IInitializable
	{
		private ISignalBusAdapter _signalBusAdapter;

		public LootFountain(ISignalBusAdapter signalBusAdapter)
		{
			_signalBusAdapter = signalBusAdapter;
		}

		public void Initialize()
		{
			_signalBusAdapter.Subscribe<EnemyDeathSignal>(OnEnemyDeath);
		}

		private void OnEnemyDeath(EnemyDeathSignal signal)
		{
			DetermineLoot(signal.LootTable);
		}

		private void DetermineLoot(LootTable lootTable)
		{
			var loot = lootTable.items.Where(lootTableItem => lootTableItem.chance > Random.Range(0, 101))
				.Select(lootTableItem => lootTableItem.item);
		}
	}
}