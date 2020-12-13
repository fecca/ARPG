using System;
using ARPG.Inputs;
using ARPG.Zenject;
using CreatorKitCodeInternal;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ARPG.Players
{
	public class PlayerController : MonoBehaviour,
		AnimationControllerDispatcher.IAttackFrameReceiver,
		AnimationControllerDispatcher.IFootstepFrameReceiver
	{
		[SerializeField] private NavMeshAgent _navMeshAgent;
		[SerializeField] private Animator _animator;
		[SerializeField] private LayerMask _groundLayer;

		private ISignalBusAdapter _signalBusAdapter;

		[Inject]
		public void Construct(ISignalBusAdapter signalBusAdapter)
		{
			_signalBusAdapter = signalBusAdapter;
		}

		private void Start()
		{
			_signalBusAdapter.Subscribe<MouseButtonClickedSignal>(OnMouseButtonClicked);
		}

		private void Update()
		{
			_animator.SetFloat(Animator.StringToHash("Speed"), _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
		}

		private void OnMouseButtonClicked(MouseButtonClickedSignal signal)
		{
			var ray = Camera.main.ScreenPointToRay(signal.MousePosition);
			if (Physics.Raycast(ray, out var hitInfo, 1000.0f, _groundLayer))
			{
				if (NavMesh.SamplePosition(hitInfo.point, out var hit, 1.0f, NavMesh.AllAreas))
				{
					_navMeshAgent.SetDestination(hit.position);
				}
			}
		}

		public void AttackFrame()
		{
		}

		public void FootstepFrame()
		{
		}
	}
}