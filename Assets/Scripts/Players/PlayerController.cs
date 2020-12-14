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
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _projectileForce;

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

        private void Update()
        {
            SetSpeed(_navMeshAgent.hasPath ? _navMeshAgent.velocity.magnitude / _navMeshAgent.speed : 0);
        }

        private void OnMouseButtonDown(MouseButtonDownSignal signal)
        {
            switch (signal.Button)
            {
                case 0:
                    Move(signal.MousePosition);
                    break;
                case 1:
                    _navMeshAgent.ResetPath();
                    Attack(signal.MousePosition);
                    break;
            }
        }

        private void OnMouseButtonHold(MouseButtonHoldSignal signal)
        {
            switch (signal.Button)
            {
                case 0:
                    Move(signal.MousePosition);
                    break;
                case 1:
                    Attack(signal.MousePosition);
                    break;
            }
        }

        private void OnMouseButtonUp(MouseButtonUpSignal signal)
        {
            switch (signal.Button)
            {
                case 0:
                    Move(signal.MousePosition);
                    break;
                case 1:
                    break;
            }
        }

        private void Move(Vector3 position)
        {
            if (!Physics.Raycast(_mainCamera.ScreenPointToRay(position), out var hitInfo, 1000.0f, _groundLayer)) return;
            if (!NavMesh.SamplePosition(hitInfo.point, out var navMeshHit, 1.0f, NavMesh.AllAreas)) return;

            _navMeshAgent.SetDestination(navMeshHit.position);
            transform.LookAt(navMeshHit.position);
        }

        private void SetSpeed(float speed)
        {
            _animator.SetFloat(_speedParameterId, speed);
        }

        private void Attack(Vector3 position)
        {
            if (!Physics.Raycast(_mainCamera.ScreenPointToRay(position), out var hitInfo, 1000.0f, _groundLayer)) return;

            transform.LookAt(hitInfo.point);

            var projectile = Instantiate(_projectile);
            var playerPosition = transform.position;
            var direction = (hitInfo.point - playerPosition).normalized;

            projectile.transform.position = playerPosition + Vector3.up;
            projectile.GetComponent<Rigidbody>().AddForce(direction * _projectileForce);

            Destroy(projectile, 2.0f);
        }

        public void AttackFrame()
        {
        }

        public void FootstepFrame()
        {
        }
    }
}