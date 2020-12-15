using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
	[CreateAssetMenu(menuName = "LootTable")]
	public class LootTable : ScriptableObject
	{
		public List<LootTableItem> items;
	}
}