using TMPro;
using UnityEngine;
using Zenject;

namespace ARPG.Items
{
	public class LootItemOnGround : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

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
			_text.text = _itemConfig.name;
			Instantiate(_itemConfig.prefab, transform);
		}

		public class Factory : PlaceholderFactory<ItemConfig, Vector3, LootItemOnGround>
		{
		}
	}
}