using System;
using ARPG.Inputs;
using ARPG.Zenject;
using CreatorKitCodeInternal;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ARPG.Players
{
    public class AttackController : MonoBehaviour, AnimationControllerDispatcher.IAttackFrameReceiver
    {
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Transform _attackPosition;
        [SerializeField] private Animator _animator;
        [SerializeField] private AttackSkill[] _attackSkills;

        private int _attackParameterId;
        private Raycaster _raycaster;
        private RaycastHit _hitInfo;
        private AttackSkill _currentAttackSkill;
        private ISignalBusAdapter _signalBusAdapter;

        public bool IsAttacking { get; private set; }

        [Inject]
        public void Construct(ISignalBusAdapter signalBusAdapter, Raycaster raycaster)
        {
            _signalBusAdapter = signalBusAdapter;
            _raycaster = raycaster;
        }

        private void Awake()
        {
            _attackParameterId = Animator.StringToHash("Attack");
            _currentAttackSkill = _attackSkills[0];
        }

        private void Start()
        {
            _signalBusAdapter.Subscribe<KeyUpSignal>(OnKeyUp);
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
            switch (_currentAttackSkill.attackType)
            {
                case AttackType.Single:
                    AttackSingle(_currentAttackSkill);
                    break;
                case AttackType.Spread:
                    AttackSpread(_currentAttackSkill);
                    break;
                case AttackType.Area:
                    AttackArea(_currentAttackSkill);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            IsAttacking = false;
        }

        private void AttackSingle(AttackSkill attackSkill)
        {
            var projectile = Instantiate(attackSkill.projectile, _attackPosition.position, Quaternion.identity);
            var direction = (_hitInfo.point - transform.position).normalized;

            projectile.GetComponent<Rigidbody>().AddForce(direction * attackSkill.projectileForce);

            Destroy(projectile, 2.0f);
        }

        private void AttackSpread(AttackSkill attackSkill)
        {
            var numberOfObjects = 8;
            for (var i = -1; i < 2; i++)
            {
                var angle = i * Mathf.PI * 2f / numberOfObjects;
                var attackPosition = _attackPosition.position;
                var positionInCircle = new Vector3(Mathf.Cos(angle), attackPosition.y, Mathf.Sin(angle));
                var newPosition = attackPosition + positionInCircle;

                var projectile = Instantiate(attackSkill.projectile, newPosition, Quaternion.identity);

                var direction = (newPosition - attackPosition).normalized;
                projectile.GetComponent<Rigidbody>().AddForce(direction * attackSkill.projectileForce);

                Destroy(projectile, 200.0f);
            }
        }

        private void AttackArea(AttackSkill attackSkill)
        {
            var numberOfObjects = 8;
            for (var i = 0; i < numberOfObjects; i++)
            {
                var angle = i * Mathf.PI * 2f / numberOfObjects;
                var attackPosition = _attackPosition.position;
                var positionInCircle = new Vector3(Mathf.Cos(angle), attackPosition.y, Mathf.Sin(angle));
                var newPosition = attackPosition + positionInCircle;

                var projectile = Instantiate(attackSkill.projectile, newPosition, Quaternion.identity);

                var direction = (newPosition - attackPosition).normalized;
                projectile.GetComponent<Rigidbody>().AddForce(direction * attackSkill.projectileForce);

                Destroy(projectile, 200.0f);
            }
        }

        private void OnKeyUp(KeyUpSignal signal)
        {
            switch (signal.KeyCode)
            {
                case KeyCode.Alpha0:
                    _currentAttackSkill = _attackSkills[0];
                    break;
                case KeyCode.Alpha1:
                    _currentAttackSkill = _attackSkills[1];
                    break;
                case KeyCode.Alpha2:
                    _currentAttackSkill = _attackSkills[2];
                    break;
            }
        }
    }
}