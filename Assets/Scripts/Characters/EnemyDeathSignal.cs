using ARPG.Items;
using UnityEngine;

namespace ARPG.Characters
{
	public class EnemyDeathSignal
	{
		public readonly Vector3 Position;
		public readonly LootTable LootTable;

		public EnemyDeathSignal(Vector3 position, LootTable lootTable)
		{
			LootTable = lootTable;
			Position = position;
		}
	}
}