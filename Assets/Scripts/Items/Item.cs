using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
	[CreateAssetMenu(menuName = "Item")]
	public class Item : ScriptableObject
	{
		public new string name;
		public List<ItemStat> stats;
	}
}