using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
	[CreateAssetMenu(menuName = "LootTable")]
	public class LootTableConfig : ScriptableObject
	{
		public List<LootTableItemConfig> itemConfigs;
	}
}