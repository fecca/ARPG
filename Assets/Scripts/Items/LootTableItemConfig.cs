using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ARPG.Items
{
	[Serializable]
	public class LootTableItemConfig
	{
		public ItemConfig itemConfig;
		[Range(1, 100)] public int chance;
	}
}