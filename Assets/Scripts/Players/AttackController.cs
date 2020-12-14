using CreatorKitCodeInternal;
using UnityEngine;
using Zenject;

namespace ARPG.Players
{
    public class AttackController : MonoBehaviour, AnimationControllerDispatcher.IAttackFrameReceiver
    {
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Transform _attackPosition;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _projectileForce;

        private int _attackParameterId;
        private Raycaster _raycaster;
        private RaycastHit _hitInfo;

        public bool IsAttacking { get; private set; }

        [Inject]
        public void Construct(Raycaster raycaster)
        {
            _raycaster = raycaster;
        }

        private void Awake()
        {
            _attackParameterId = Animator.StringToHash("Attack");
        }

        public void Attack(Vector3 position)
        {
            if (!_raycaster.RaycastGround(position, out _hitInfo)) return;

            IsAttacking = true;
            _rootTransform.LookAt(_hitInfo.point);
            _animator.SetTrigger(_attackParameterId);
        }

        public void AttackFrame()
        {
            var projectile = Instantiate(_projectile, _attackPosition.position, Quaternion.identity);
            var direction = (_hitInfo.point - transform.position).normalized;

            projectile.GetComponent<Rigidbody>().AddForce(direction * _projectileForce);

            Destroy(projectile, 2.0f);

            IsAttacking = false;
        }
    }
}