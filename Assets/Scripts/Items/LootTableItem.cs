using System;
using UnityEngine;

namespace ARPG.Items
{
	[Serializable]
	public class LootTableItem
	{
		public Item item;
		[Range(1, 100)] public int chance;
	}
}