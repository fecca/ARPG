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
            _navMeshAgent.speed = _movementSpeed;
            _animator.SetFloat(_speedParameterId, _navMeshAgent.hasPath ? _navMeshAgent.velocity.magnitude / _navMeshAgent.speed : 0);
        }

        public void Move(Vector3 mousePosition, float movementSpeed)
        {
            if (!_raycaster.RaycastGround(mousePosition, out var hitInfo)) return;
            if (!_navmeshSampler.SampleGroundPosition(hitInfo.point, out var navMeshHit)) return;

            _navMeshAgent.SetDestination(navMeshHit.position);
            transform.LookAt(navMeshHit.position);
            _movementSpeed = movementSpeed;
        }

        public void StopMoving()
        {
            _navMeshAgent.ResetPath();
        }

        public void FootstepFrame()
        {
        }
    }
}