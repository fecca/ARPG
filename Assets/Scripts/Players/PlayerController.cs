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
		private Camera _mainCamera;
		private int _speedParameterId;

		[Inject]
		public void Construct(ISignalBusAdapter signalBusAdapter)
		{
			_signalBusAdapter = signalBusAdapter;
		}

		private void Awake()
		{
			_mainCamera = Camera.main;
			_speedParameterId = Animator.StringToHash("Speed");
		}

		private void Start()
		{
			_signalBusAdapter.Subscribe<MouseButtonDownSignal>(OnMouseButtonDown);
			_signalBusAdapter.Subscribe<MouseButtonHoldSignal>(OnMouseButtonHold);
			_signalBusAdapter.Subscribe<MouseButtonUpSignal>(OnMouseButtonUp);
		}

		private void OnMouseButtonDown(MouseButtonDownSignal signal)
		{
			Move(signal.MousePosition);
		}

		private void OnMouseButtonHold(MouseButtonHoldSignal signal)
		{
			Move(signal.MousePosition);
		}

		private void OnMouseButtonUp(MouseButtonUpSignal signal)
		{
			_navMeshAgent.SetDestination(transform.position);
			SetSpeed(0);
		}

		private void Move(Vector3 position)
		{
			var ray = _mainCamera.ScreenPointToRay(position);
			if (Physics.Raycast(ray, out var hitInfo, 1000.0f, _groundLayer))
			{
				if (NavMesh.SamplePosition(hitInfo.point, out var navMeshHit, 1.0f, NavMesh.AllAreas))
				{
					_navMeshAgent.SetDestination(navMeshHit.position);
					transform.LookAt(navMeshHit.position);
					SetSpeed(_navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
				}
			}
		}

		private void SetSpeed(float speed)
		{
			_animator.SetFloat(_speedParameterId, speed);
		}

		public void AttackFrame()
		{
		}

		public void FootstepFrame()
		{
		}
	}
}