using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
	[CreateAssetMenu(menuName = "Item")]
	public class ItemConfig : ScriptableObject
	{
		public new string name;
		public GameObject prefab;
		public List<ItemStat> stats;
	}
}