using CreatorKitCodeInternal;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ARPG.Moving
{
	public class MovementController : MonoBehaviour, AnimationControllerDispatcher.IFootstepFrameReceiver
	{
		[SerializeField] private NavMeshAgent _navMeshAgent;
		[SerializeField] private Animator _animator;

		private Raycaster _raycaster;
		private NavmeshSampler _navmeshSampler;
		private int _speedParameterId;
		private float _movementSpeed;
		private Transform _target;

		[Inject]
		public void Construct(Raycaster raycaster, NavmeshSampler navmeshSampler)
		{
			_raycaster = raycaster;
			_navmeshSampler = navmeshSampler;
		}

		private void Awake()
		{
			_speedParameterId = Animator.StringToHash("Speed");
		}

		private void Update()
		{
			if (_target != null)
			{
				MoveToWorldPosition(_target.position, 4.0f);
			}

			_navMeshAgent.speed = _movementSpeed;
			_animator.SetFloat(_speedParameterId,
				_navMeshAgent.hasPath ? _navMeshAgent.velocity.magnitude / _navMeshAgent.speed : 0);
		}

		private void MoveToWorldPosition(Vector3 worldPosition, float movementSpeed)
		{
			_navMeshAgent.SetDestination(worldPosition);
			transform.LookAt(worldPosition);
			_movementSpeed = movementSpeed;
		}

		public void MoveToMousePosition(Vector3 mousePosition, float movementSpeed)
		{
			if (!_raycaster.RaycastGround(mousePosition, out var hitInfo)) return;
			if (!_navmeshSampler.SampleGroundPosition(hitInfo.point, out var navMeshHit)) return;

			MoveToWorldPosition(navMeshHit.position, movementSpeed);
		}

		public void StopMoving()
		{
			_navMeshAgent.ResetPath();
		}

		public void FootstepFrame()
		{
		}

		public void DisableAgent()
		{
			_navMeshAgent.enabled = false;
		}

		public void FollowTarget(Transform target)
		{
			_target = target;
		}
	}
}