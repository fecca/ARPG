using System.Collections.Generic;
using System.Linq;
using ARPG.Characters;
using ARPG.Items;
using ARPG.Moving;
using ARPG.Zenject;
using UnityEngine;
using Zenject;

namespace ARPG.Debugging
{
	public class DebugInputHandler : MonoBehaviour
	{
		[SerializeField] private GameObject lootItemPrefab;
		private ISignalBusAdapter _signalBusAdapter;
		private Raycaster _raycaster;

		[Inject]
		public void Construct(ISignalBusAdapter signalBusAdapter, Raycaster raycaster)
		{
			_signalBusAdapter = signalBusAdapter;
			_raycaster = raycaster;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.K))
			{
				FindObjectsOfType<Enemy>().ToList().ForEach(ec => ec.Kill());
			}

			if (Input.GetMouseButtonDown(0))
			{
				if (!_raycaster.RaycastGround(Input.mousePosition, out var hitInfo)) return;

				var lootTable = ScriptableObject.CreateInstance<LootTableConfig>();
				var itemConfig = ScriptableObject.CreateInstance<ItemConfig>();
				itemConfig.name = "Item";
				itemConfig.prefab = lootItemPrefab;
				lootTable.itemConfigs = new List<LootTableItemConfig>
				{
					new LootTableItemConfig
					{
						chance = 100,
						itemConfig = itemConfig
					}
				};
				_signalBusAdapter.Fire(new EnemyDeathSignal(hitInfo.point, lootTable));
			}
		}
	}
}