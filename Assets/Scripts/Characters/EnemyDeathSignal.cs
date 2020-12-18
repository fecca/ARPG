using ARPG.Items;
using UnityEngine;

namespace ARPG.Characters
{
	public class EnemyDeathSignal
	{
		public readonly Vector3 Position;
		public readonly LootTableConfig LootTableConfig;

		public EnemyDeathSignal(Vector3 position, LootTableConfig lootTableConfig)
		{
			LootTableConfig = lootTableConfig;
			Position = position;
		}
	}
}