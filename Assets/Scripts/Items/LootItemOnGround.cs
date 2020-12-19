using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace ARPG.Items
{
	public class LootItemOnGround : MonoBehaviour
	{
		[SerializeField] private Transform parent;

		private ItemConfig _itemConfig;
		private Vector3 _position;

		[Inject]
		public void Construct(ItemConfig itemConfig, Vector3 position)
		{
			_position = position;
			_itemConfig = itemConfig;
		}

		private void Start()
		{
			transform.position = _position;
			Instantiate(_itemConfig.prefab, parent);
		}

		public class Factory : PlaceholderFactory<ItemConfig, Vector3, LootItemOnGround>
		{
		}
	}
}