using ARPG.Items;
using ARPG.Zenject;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ARPG.Characters
{
	public class EnemyController : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private NavMeshAgent _navMeshAgent;
		[SerializeField] private LootTableConfig lootTableConfig;

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
			_navMeshAgent.enabled = false;
			_animator.SetTrigger(_faintParameterId);
			_signalBusAdapter.Fire(new EnemyDeathSignal(transform.position, lootTableConfig));
		}
	}
}