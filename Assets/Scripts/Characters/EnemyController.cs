using ARPG.Items;
using ARPG.Zenject;
using UnityEngine;
using Zenject;

namespace ARPG.Characters
{
	public class EnemyController : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private SphereCollider _collider;
		[SerializeField] private LootTable _lootTable;

		private int _faintParameterId;
		private ISignalBusAdapter _signalBusAdapter;

		[Inject]
		private void Construct(ISignalBusAdapter signalBusAdapter)
		{
			_signalBusAdapter = signalBusAdapter;
		}

		private void Awake()
		{
			_faintParameterId = Animator.StringToHash("Death");
		}

		public void TakeDamage()
		{
			_collider.enabled = false;
			_animator.SetTrigger(_faintParameterId);
			_signalBusAdapter.Fire(new EnemyDeathSignal(transform.position, _lootTable));
		}
	}
}